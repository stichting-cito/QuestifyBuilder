
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace ContentModel.Scoring
    Friend MustInherit Class FindingManipulatorBase
        Implements IFindingManipulator

        Private ReadOnly _justInTimeFinding As CreateObjectJIT(Of BaseFinding)
        Private _target As IFindingManipulatorTarget
        Private _DomainNameGenerator As Func(Of String, String)
        Private _factSetNumber As Integer?

        Public Overridable ReadOnly Property IsConceptManipulator As Boolean Implements IFindingManipulator.IsConceptManipulator
            Get
                Return False
            End Get
        End Property

        Sub New(justInTimeFinding As CreateObjectJIT(Of BaseFinding))
            _justInTimeFinding = justInTimeFinding
            _DomainNameGenerator = AddressOf DefaultDomainName

            FactIdPostFix = String.Empty
        End Sub

        Public Sub Clear() Implements IFindingManipulator.Clear
            _justInTimeFinding.GetEnsuredValue.Facts.Clear()
        End Sub

        Public Sub RemoveFact(id As String) Implements IFindingManipulator.RemoveFact
            Dim target = GetTarget()
            target.RemoveFact(id)
        End Sub

        Public Iterator Function GetIds() As IEnumerable(Of String) Implements IFindingManipulator.GetIds
            If _justInTimeFinding.CurrentValue IsNot Nothing Then
                Dim target = GetTarget()
                For Each e In target.GetFacts()
                    Yield e.Id
                Next
            End If
        End Function

        Public Iterator Function GetKeys(id As String) As IEnumerable(Of IEnumerable(Of Cito.Tester.ContentModel.BaseValue)) _
    Implements IFindingManipulator.GetKeys
            Dim returned = 0
            If _justInTimeFinding.CurrentValue IsNot Nothing Then
                Dim target = GetTarget()


                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.Id = id)
                    For Each keyValue In fact.Values
                        Dim lst = GetFactValues(keyValue)
                        returned += 1
                        Yield lst
                    Next
                Next

                If (returned > 0) Then Return

                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.EqualsById(id))
                    For Each keyValue In fact.Values
                        Dim lst = GetFactValues(keyValue)
                        returned += 1
                        Yield lst
                    Next
                Next

            End If
        End Function


        Private Iterator Function GetBaseFactValue(id As String) As IEnumerable(Of BaseFactValue)
            Dim returned = 0
            If _justInTimeFinding.CurrentValue IsNot Nothing Then
                Dim target = GetTarget()


                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.Id = id)
                    For Each value In fact.Values
                        Yield value
                    Next
                Next

                If (returned > 0) Then Return

                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.EqualsById(id))
                    For Each value In fact.Values
                        Yield value
                    Next
                Next

            End If
        End Function

        Public Function GetFacts(factId As String) As IEnumerable(Of BaseFact) Implements IFindingManipulator.GetFacts

            Dim target = GetTarget()

            Return target.GetFacts().Where(Function(f) f.Id.StartsWith(factId))
        End Function

        Public Sub SetKey(Of T)(id As String, value As T) Implements IFindingManipulator.SetKey
            Dim fact As BaseFact = FindOrCreateFact(id)
            SetScore(fact, 1)
            Dim baseValue = CreateBaseValue(Of T)(value)
            fact.Values.Add(CreateBaseFactValue(id, baseValue))
        End Sub

        Public Sub SetKeyWithOptionals(Of T)(id As String, ParamArray value As T()) _
    Implements IFindingManipulator.SetKeyWithOptionals
            SetKeyWithOptionals(1, id, value)
        End Sub

        Public Sub SetKeyWithOptionals(Of T)(score As Integer, id As String, ParamArray value As T()) _
            Implements IFindingManipulator.SetKeyWithOptionals
            Dim fact As BaseFact = FindOrCreateFact(id)
            Dim val As BaseFactValue = fact.Values.FirstOrDefault(Function(e) e.Id = id)

            SetScore(fact:=fact, scoreToSet:=score)

            For Each e In value
                Dim baseValue = CreateBaseValue(Of T)(e)

                If (val IsNot Nothing) Then
                    AddExtraValues(val, baseValue)
                Else
                    val = CreateBaseFactValue(id, baseValue)
                    fact.Values.Add(val)
                End If
            Next
        End Sub

        Public Sub ReplaceKeyWithSpecificOptionals(Of T)(id As String, value As T, index As Integer, Optional score As Integer = 1) _
    Implements IFindingManipulator.ReplaceKeyWithSpecificOptionals
            Dim fact As BaseFact = FindOrCreateFact(id)
            SetScore(fact, score)
            Dim val As BaseFactValue = fact.Values.FirstOrDefault(Function(e) e.Id = id OrElse e.Domain = GetDomain(id))
            Dim baseVal = CreateBaseValue(Of T)(value)

            If (val Is Nothing) Then
                val = CreateBaseFactValue(id, baseVal)
                fact.Values.Add(val)
            Else
                ReplaceValue(val, baseVal, index)
            End If
        End Sub

        Public Function UnSetKey(factId As String) As Integer Implements IFindingManipulator.UnSetKey
            Dim ret = 0

            Dim fact = GetTarget().GetFact(factId)
            If fact IsNot Nothing Then
                SetScore(fact, 0)
                fact.Values.Clear()
                ret = 1
            End If

            Return ret
        End Function

        Public Function FindOrCreateFact(id As String) As BaseFact Implements IFindingManipulator.FindOrCreateFact
            Dim ret As BaseFact = Nothing
            Dim target = GetTarget()

            ret = target.GetFact(id)

            If (ret Is Nothing) Then
                ret = target.CreateFact(id)
            End If

            Return ret
        End Function


        Public Function GetPreProcessingMethods(key As String) As IEnumerable(Of String) Implements IFindingManipulator.GetPreProcessingMethods
            Dim value = GetBaseFactValue(key).FirstOrDefault()

            If (value Is Nothing) Then Return New List(Of String)()

            Return GetPreProcessingMethods(value)
        End Function

        Public Sub SetPreProcessingMethods(key As String, preProcessing As IEnumerable(Of String)) Implements IFindingManipulator.SetPreProcessingMethods

            Dim fact = GetFacts(key).FirstOrDefault()
            If (fact IsNot Nothing) Then
                Debug.Assert(fact.Values.Count <= 1, "Multiple values not handled")
                SetPreProcessingMethods(fact.Values.FirstOrDefault(), preProcessing)
            End If
        End Sub

        Public Function CanFactBeRemovedFromTarget() As Boolean _
            Implements IFindingManipulator.CanFactBeRemovedFromTarget
            Return GetTarget().CanFactBeRemoved()
        End Function



        Protected ReadOnly Property JustInTimeFinding As CreateObjectJIT(Of BaseFinding)
            Get
                Return _justInTimeFinding
            End Get
        End Property

        Public Property FactIdPostFix As String Implements IFindingManipulator.FactIdPostFix



        Public ReadOnly Property FactsetTarget As Integer? Implements IFindingManipulator.FactsetTarget
            Get
                Return _factSetNumber
            End Get
        End Property

        Friend Function GetTarget() As IFindingManipulatorTarget

            If (_target IsNot Nothing) Then Return _target
            _target = TryFindTargetForId(FactIdPostFix)

            If (_target.IsInFactSet) Then
                _factSetNumber = _target.FactSetNumber
            End If

            Return _target
        End Function

        Friend ReadOnly Property Target As IFindingManipulatorTarget
            Get
                Return _target
            End Get
        End Property

        Public Sub SetFactSetTarget(factSetNumber As Integer?) Implements IFindingManipulator.SetFactSetTarget
            _factSetNumber = factSetNumber
            _target = GetTargetForSetNumber(factSetNumber, FactIdPostFix)

            If (_target.IsInFactSet) Then
                _target.FactSetNumber = factSetNumber
            End If
        End Sub

        Public Sub CreateFindingTarget(key As String) Implements IFindingManipulator.CreateFindingTarget
            Dim thisTarget = New FindingManipulatorTarget(JustInTimeFinding, Me, FactIdPostFix)
            thisTarget.CreateFact(key)
        End Sub

        Public Sub RemoveFactSetTarget(factSetNumber As Integer) Implements IFindingManipulator.RemoveFactSetTarget
            SetFactSetTarget(factSetNumber)
            _target.RemoveFactSet()
        End Sub

        Public ReadOnly Property SetNumbers As IEnumerable(Of Integer) Implements IFindingManipulator.SetNumbers
            Get
                Return GetSetNumbers()
            End Get
        End Property



        Public MustOverride Sub SetScoringMethod(method As EnumScoringMethod) _
            Implements IFindingManipulator.SetScoringMethod

        Friend MustOverride Function TryFindTargetForId(postFix As String) As IFindingManipulatorTarget

        Friend MustOverride Function GetTargetForSetNumber(setNumber As Integer?, FactIdPostFix As String) As IFindingManipulatorTarget

        Public MustOverride Function GetScoringMethod() As EnumScoringMethod Implements IFindingManipulator.GetScoringMethod

        MustOverride Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer) Implements IFindingManipulator.GetFactSetNumbers

        Friend MustOverride Function CreateFact(id As String) As BaseFact

        Protected MustOverride Function CreateBaseFactValue(id As String, baseValue As BaseValue) As BaseFactValue

        Protected MustOverride Sub AddExtraValues(factValue As BaseFactValue, baseValue As BaseValue)

        Protected MustOverride Sub ReplaceValue(factValue As BaseFactValue, baseValue As BaseValue, index As Integer)

        Protected MustOverride Function GetFactValues(fact As BaseFactValue) As IEnumerable(Of BaseValue)

        Protected MustOverride Sub RemoveValueFromFactValues(factValue As BaseFactValue, compareValue As BaseValue)

        Protected MustOverride Sub SetScore(fact As BaseFact, scoreToSet As Integer)

        Protected MustOverride Function GetPreProcessingMethods(baseFactValue As BaseFactValue) As IEnumerable(Of String)

        Protected MustOverride Sub SetPreProcessingMethods(baseFactValue As BaseFactValue, preProcessing As IEnumerable(Of String))

        Public MustOverride ReadOnly Property CanManipulateSets As Boolean Implements IFindingManipulator.CanManipulateSets

        Public MustOverride ReadOnly Property HasSets As Boolean Implements IFindingManipulator.HasSets

        Public MustOverride Function CreateNewFactSet() As Integer Implements IFindingManipulator.CreateNewFactSet

        Protected MustOverride Function GetSetNumbers() As IEnumerable(Of Integer)



        Private Function CreateBaseValue(Of T)(value As T) As BaseValue
            Dim ret As BaseValue = Nothing
            If value Is Nothing Then Return ret
            WhenObject(value,
           IsType(Of NoValueType(Of String))(
                Sub(e As NoValueType(Of String))
                    If e.NoValueIsCorrect Then
                        ret = New NoValue()
                    else
                        ret = New StringValue(e)
                    End if
                End Sub),
              IsType(Of String)(Sub(e As String) ret = New StringValue(e)),
              IsType(Of Integer)(Sub(e As Integer) ret = New IntegerValue(e)),
              IsType(Of Boolean)(Sub(e As Boolean) ret = New BooleanValue(e)),
              IsType(Of Decimal)(Sub(e As Decimal) ret = New DecimalValue(e)),
              IsType(Of Byte())(Sub(e As Byte()) ret = New BinaryValue(e)),
              IsType(Of GapValue(Of Boolean))(Sub(e As GapValue(Of Boolean)) ret = New BooleanValue(e.Value)),
              IsType(Of GapValue(Of String))(Sub(e As GapValue(Of String))
                                                 ret = GetStringValue(e.Comparison, e.Value)
                                             End Sub),
              IsType(Of GapValue(Of Integer?))(Sub(e As GapValue(Of Integer?))
                                                   If e.Value.HasValue Then
                                                       ret = GetIntegerValue(e.Comparison, e.Value, e.Value2)
                                                   End If
                                               End Sub),
              IsType(Of GapValue(Of Decimal?))(Sub(e As GapValue(Of Decimal?))
                                                   If e.Value.HasValue Then
                                                       ret = GetDecimalValue(e.Comparison, e.Value, e.Value2)
                                                   End If
                                               End Sub),
              IsType(Of GapValue(Of MultiType))(Sub(e As GapValue(Of MultiType))
                                                    If e.Value IsNot Nothing Then
                                                        If e.Value.DecimalValue.HasValue Then
                                                            ret = GetDecimalValue(e.Comparison, e.Value.DecimalValue, If(e.Value2 IsNot Nothing, e.Value2.DecimalValue, Nothing))
                                                        ElseIf e.Value.IntegerValue.HasValue Then
                                                            ret = GetIntegerValue(e.Comparison, e.Value.IntegerValue, If(e.Value2 IsNot Nothing, e.Value2.IntegerValue, Nothing))
                                                        Else
                                                            ret = GetStringValue(e.Comparison, e.Value)
                                                        End If
                                                    End If
                                                End Sub),
              IsType(Of BaseValue)(Sub(e) ret = e),
              Otherwise(Sub() Throw New ArgumentException(GetType(T).ToString())))

            Return ret
        End Function

        Private Function GetStringValue(type As GapComparisonType, value As String) As BaseValue
            Dim ret As BaseValue
            Select Case type
                Case GapComparisonType.Equals
                    ret = New StringValue(value)
                Case GapComparisonType.Range
                    Return Nothing
                Case Else
                    ret = New StringComparisonValue(value, type.ToString())
            End Select
            Return ret
        End Function

        Private Function GetIntegerValue(type As GapComparisonType, value As Integer?, value2 As Integer?) As BaseValue
            Dim ret As BaseValue
            Select Case type
                Case GapComparisonType.Equals
                    ret = New IntegerValue(value.Value)
                Case GapComparisonType.Range
                    If Not value2.HasValue Then Return Nothing
                    ret = New IntegerRangeValue(value.Value, value2.Value)
                Case Else
                    ret = New IntegerComparisonValue(value.Value, type.ToString())
            End Select
            Return ret
        End Function

        Private Function GetDecimalValue(type As GapComparisonType, value As Decimal?, value2 As Decimal?) As BaseValue
            Dim ret As BaseValue
            Select Case type
                Case GapComparisonType.Equals
                    ret = New DecimalValue(value.Value)
                Case GapComparisonType.Range
                    If Not value2.HasValue Then Return Nothing
                    ret = New DecimalRangeValue(value.Value, value2.Value)
                Case Else
                    ret = New DecimalComparisonValue(value.Value, type.ToString())
            End Select
            Return ret
        End Function

        Protected Function GetDomain(id As String) As String
            Return _DomainNameGenerator(id)
        End Function

        Private Function DefaultDomainName(id As String) As String
            Return id
        End Function



        Public Sub SetDomainOverride(override As Func(Of String, String)) _
    Implements IFindingManipulator.SetDomainOverride
            If (override Is Nothing) Then Throw New ArgumentNullException()
            _DomainNameGenerator = override
        End Sub

    End Class
End Namespace
