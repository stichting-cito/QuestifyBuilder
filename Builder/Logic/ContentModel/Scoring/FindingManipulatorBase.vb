
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace ContentModel.Scoring
    ''' <summary>
    ''' This is a base class for manipulating Findings. This class is Not specific to a certain model (response, scoring, concept...).
    ''' It tries to find a target to work on. This can be either a finding or a factset. Both are similar but factsets are used for 
    ''' more complex method of scoring.
    ''' </summary>
    Friend MustInherit Class FindingManipulatorBase
        Implements IFindingManipulator

        Private ReadOnly _justInTimeFinding As CreateObjectJIT(Of BaseFinding) 
        'this allows for querying uninitialized objects without creating anything.
        Private _target As IFindingManipulatorTarget 'Where to get Facts from
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

        ''' <summary>
        ''' Gets Keys as set.
        ''' The return type is somewhat peculiar.
        ''' There are OR and AND constructs possible:
        ''' - AND construct
        ''' The Finding will contain 2 or more facts with the same ID. Both facts have a fully supplied Value.
        ''' - OR construct
        ''' The Finding will contain 1 fact. This fact as a Value with multiple values.
        ''' </summary>
        ''' <param name="id">The identifier.</param>
        ''' <returns>IEnumerable{IEnumerable{Tester.ContentModel.BaseValue}}.</returns>
        Public Iterator Function GetKeys(id As String) As IEnumerable(Of IEnumerable(Of Cito.Tester.ContentModel.BaseValue)) _
            Implements IFindingManipulator.GetKeys
            Dim returned = 0
            If _justInTimeFinding.CurrentValue IsNot Nothing Then
                Dim target = GetTarget()


                'Modi Strict by string equals.
                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.Id = id)
                    For Each keyValue In fact.Values
                        Dim lst = GetFactValues(keyValue)
                        returned += 1
                        Yield lst
                    Next
                Next

                If (returned > 0) Then Return 'escape when stuff has been returned

                'When no items have been returned strict, return with special equals.
                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.EqualsById(id))
                    For Each keyValue In fact.Values
                        Dim lst = GetFactValues(keyValue)
                        returned += 1
                        Yield lst
                    Next
                Next

            End If
        End Function


        ''' <summary>
        ''' Gets the base fact values, taking care that concepts exist.
        ''' </summary>
        ''' <param name="id">The identifier.</param>
        ''' <returns>IEnumerable(Of Tester.ContentModel.BaseFactValue).</returns>
        Private Iterator Function GetBaseFactValue(id As String) As IEnumerable(Of BaseFactValue)
            Dim returned = 0
            If _justInTimeFinding.CurrentValue IsNot Nothing Then
                Dim target = GetTarget()


                'Modi Strict by string equals.
                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.Id = id)
                    For Each value In fact.Values
                        Yield value
                    Next
                Next

                If (returned > 0) Then Return 'escape when stuff has been returned

                'When no items have been returned strict, return with special equals.
                For Each fact In target.GetFacts().Where(Function(evalFact) evalFact.EqualsById(id))
                    For Each value In fact.Values
                        Yield value
                    Next
                Next

            End If
        End Function

        ''' <summary>
        ''' Gets the Facts.
        ''' </summary>
        ''' <param name="factId">The identifier.</param>
        ''' <returns>IEnumerable{Tester.ContentModel.BaseFact}.</returns>
        Public Function GetFacts(factId As String) As IEnumerable(Of BaseFact) Implements IFindingManipulator.GetFacts

            Dim target = GetTarget() 'TryFindTargetForID(factId)

            Return target.GetFacts().Where(Function(f) f.Id.StartsWith(factId))
        End Function

        ''' <summary>
        ''' Creates a key (If needed) and add a single BaseFactValue with a single BaseValue.
        ''' If you repeat this call with the same ID and another value, you are setting up an "AND" construct.
        ''' This is useful when a interaction can give a response with multiple values.
        ''' An example of this is the Multiple Choice where you can give multiple answers
        ''' eg:
        ''' [X] A
        ''' [ ] B
        ''' [X] C
        ''' Here you want to call the method twice with the same id. The first time with "A" as value, the second time with "B".
        ''' </summary>
        ''' <typeparam name="T">The type of parameter you wish to use (String,integer,decimal, byte array)</typeparam>
        ''' <param name="id">The identifier.</param>
        ''' <param name="value">The value.</param>
        Public Sub SetKey(Of T)(id As String, value As T) Implements IFindingManipulator.SetKey
            Dim fact As BaseFact = FindOrCreateFact(id)
            SetScore(fact, 1)
            Dim baseValue = CreateBaseValue(Of T)(value)
            fact.Values.Add(CreateBaseFactValue(id, baseValue))
        End Sub

        ''' <summary>
        ''' Creates a key (If needed) and add if needed a BaseFactValue. That contain all the given values.
        ''' This method is typically used when a interactions response can be one of multiple values.
        ''' eg:
        ''' If you ask to write down a positive even number between 1 and 5 the following answers are possible:
        ''' 2,4
        ''' you want to call this method with the appropriate id, and 2,4 as parameters.
        ''' Calling this method multiple times will be the same as calling this method once with all the parameters.
        ''' </summary>
        ''' <typeparam name="T">The type of parameter you wish to use (String,integer,decimal, byte array)</typeparam>
        ''' <param name="id">The identifier.</param>
        ''' <param name="value">The value.</param>
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

        ''' <summary>
        ''' Finds the key and replaces a BaseFactValue. That contain all the given values.
        ''' This method is typically used when a interactions response can be one of multiple values.
        ''' eg:
        ''' If you ask to write down a positive even number between 1 and 5 the following answers are possible:
        ''' 2,4
        ''' you want to call this method to change 2 into 1.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="id">The identifier.</param>
        ''' <param name="value">The value.</param>
        ''' <param name="index">The index.</param>
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

        ''' <summary>
        ''' Un-sets the key.
        ''' </summary>
        ''' <param name="factId">The fact identifier.</param>
        ''' <returns>The number of facts removed.</returns>
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
            'First Find
            Dim target = GetTarget() 'TryFindTargetForID(FactIdPostFix)

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
            'Just get first fact.

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


#Region "Properties"

        Protected ReadOnly Property JustInTimeFinding As CreateObjectJIT(Of BaseFinding)
            Get
                Return _justInTimeFinding
            End Get
        End Property

        Public Property FactIdPostFix As String Implements IFindingManipulator.FactIdPostFix

#End Region

#Region "Target"

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

#End Region

#Region "Must overrides"

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

#End Region

#Region "Can do on my own"

        Private Function CreateBaseValue(Of T)(value As T) As BaseValue
            Dim ret As BaseValue = Nothing
            If value Is Nothing Then Return ret
            'Add multitype support
            WhenObject(value, 
                       IsType(Of NoValueType(Of String))(
                            Sub(e As  NoValueType(Of String))
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
            'This call is done from a derived class to determine the domain name to use on either KeyValue or ConceptValue
            'This behavior needs to be flexible and therefore an "callback" mechanism has been introduced.
            'Please refer to "SetDomainOverride"
            Return _DomainNameGenerator(id)
        End Function

        Private Function DefaultDomainName(id As String) As String
            'this implementation is not very useful, but it just defines default behavior, and is overridable.
            'this method is set to the lambda "_DomainNameGenerator". 
            Return id
        End Function

#End Region

#Region "Overrides per score editor"

        ''' <summary>
        ''' Sets a method to override the name of the domain.
        ''' This will Be called just after creating a baseValue.
        ''' </summary>
        ''' <param name="override">The override.</param>
        ''' <exception cref="System.ArgumentNullException"></exception>
        Public Sub SetDomainOverride(override As Func(Of String, String)) _
            Implements IFindingManipulator.SetDomainOverride
            If (override Is Nothing) Then Throw New ArgumentNullException()
            _DomainNameGenerator = override
        End Sub

#End Region
    End Class
End Namespace
