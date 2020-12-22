Imports System.Linq
Imports Cito.Tester.ContentModel
Imports System.Web

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(OrderScoringParameter))>
    Friend Class OrderScoringManipulator : Implements IOrderScoringManipulator

        Private ReadOnly _scoreParameter As OrderScoringParameter
        Private ReadOnly _adaptee As IGapScoringManipulator(Of Integer?)

        Public Sub New(manipulator As IFindingManipulator, param As OrderScoringParameter)
            _adaptee = New IntegerScoreManipulator(manipulator, param)
            _scoreParameter = param
        End Sub


        Private Sub Clear() Implements IOrderScoringManipulator.Clear
            _adaptee.Clear()
        End Sub

        Private Sub RemoveKey(key As String) Implements IOrderScoringManipulator.RemoveKey
            _adaptee.RemoveKey(key)
        End Sub

        Private Function CanBeRemovedFromFactSet(parameterCollectionId As String) As Boolean Implements IScoreManipulator.CanBeRemovedFromFactSet
            Return _adaptee.CanBeRemovedFromFactSet(parameterCollectionId)
        End Function

        Private Function CreateFactSetTarget() As Integer Implements IScoreManipulator.CreateFactSetTarget
            Return _adaptee.CreateFactSetTarget()
        End Function

        Private Sub CreateFindingTarget(parameterCollectionId As String) Implements IScoreManipulator.CreateFindingTarget
            _adaptee.CreateFindingTarget(parameterCollectionId)
        End Sub

        Private ReadOnly Property FactSetTarget As Integer? Implements IScoreManipulator.FactSetTarget
            Get
                Return _adaptee.FactSetTarget
            End Get
        End Property

        Private Function GetFactIdForKey(key As String) As String Implements IScoreManipulator.GetFactIdForKey
            Return _adaptee.GetFactIdForKey(key)
        End Function

        Private Function GetHowToScoreMethod() As EnumScoringMethod Implements IScoreManipulator.GetHowToScoreMethod
            Return _adaptee.GetHowToScoreMethod()
        End Function

        Private Function GetKeysAlreadyManipulated() As IEnumerable(Of String) Implements IScoreManipulator.GetKeysAlreadyManipulated
            Return _adaptee.GetKeysAlreadyManipulated()
        End Function

        Private Function GetManipulatableKeys() As IEnumerable(Of String) Implements IScoreManipulator.GetManipulatableKeys
            Return _adaptee.GetManipulatableKeys()
        End Function

        Private Sub RemoveFactSetTarget(factSetNumber As Integer) Implements IScoreManipulator.RemoveFactSetTarget
            _adaptee.RemoveFactSetTarget(factSetNumber)
        End Sub

        Private Sub SetFactSetTarget(factSetNumber As Integer?) Implements IScoreManipulator.SetFactSetTarget
            _adaptee.SetFactSetTarget(factSetNumber)
        End Sub

        Private Sub SetHowToScoreMethod(e As EnumScoringMethod) Implements IScoreManipulator.SetHowToScoreMethod
            _adaptee.SetHowToScoreMethod(e)
        End Sub

        Private Function GetDomainForKey(ByVal key As String) As String Implements IScoreManipulator.GetDomainForKey
            Return _adaptee.GetDomainForKey(key)
        End Function


        Private Function GetFactSetNumbers(parameterCollectionId As String) As IEnumerable(Of Integer) Implements IOrderScoringManipulator.GetFactSetNumbers
            Dim factSetNumbers = New List(Of Integer)()
            For Each orderValue In _scoreParameter.Value
                factSetNumbers = factSetNumbers.Union(_adaptee.GetFactSetNumbers(orderValue.Id)).ToList()
            Next

            Return factSetNumbers
        End Function

        Private Function GetkeyStatus() As IDictionary(Of String, Integer) Implements IOrderScoringManipulator.GetKeyStatus
            Dim gapkeys = _adaptee.GetKeyStatus()

            Dim keys As New Dictionary(Of String, Integer)

            gapkeys.Keys.ToList().ForEach(Sub(key) keys.Add(key, gapkeys(key).First.Value.Value))

            Return keys
        End Function

        Private Sub SetKey(key As String, order As Integer) Implements IOrderScoringManipulator.SetKey

            _adaptee.RemoveKey(key)
            _adaptee.SetKey(key, New GapValue(Of Integer?)(order))

        End Sub

        Public Function GetDisplayValueForKey(key As String) As String Implements IScoreManipulator.GetDisplayValueForKey
            Dim stringDisplayValue As String = _adaptee.GetDisplayValueForKey(key)
            Dim integerDisplayValue As Integer

            If Integer.TryParse(stringDisplayValue, integerDisplayValue) Then

                Dim subKey = Chr(Asc("A") + integerDisplayValue - 1).ToString()

                Dim match As ParameterCollection = _scoreParameter.Value.FirstOrDefault(Function(subPrm) subPrm.Id = subKey)
                If (match IsNot Nothing) Then
                    Dim firstPlainTextParameter = DirectCast(match.InnerParameters.FirstOrDefault(Function(prm) TypeOf prm Is PlainTextParameter), PlainTextParameter)

                    If (firstPlainTextParameter IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(firstPlainTextParameter.Value)) Then
                        Return HttpUtility.HtmlDecode(firstPlainTextParameter.Value)
                    End If
                End If

                Return subKey
            End If

            Return stringDisplayValue
        End Function

        Public Function GetBaseValueForKey(key As String) As BaseValue Implements IScoreManipulator.GetBaseValueForKey
            Return _adaptee.GetBaseValueForKey(key)
        End Function

        Public Function GetValueForKey(key As String) As String Implements IScoreManipulator.GetValueForKey
            Return _adaptee.GetValueForKey(key)
        End Function

        Private Function IsValid(ByVal key As Integer) As Boolean Implements IOrderScoringManipulator.IsValid

            If key = 0 Then
                Return True
            End If

            Dim countOfThisKey As Integer = GetkeyStatus().Where(Function(k) k.Value = key).Count()
            If countOfThisKey > 1 Then
                Return False
            End If

            Return True
        End Function



        Private Class IntegerScoreManipulator
            Inherits BaseGapScoringManipulator(Of Integer?)

            Sub New(findingManipulator As IFindingManipulator, param As ScoringParameter)
                MyBase.New(findingManipulator, param)
            End Sub

            Protected Overrides Function GetDefaultValue() As GapValue(Of Integer?)
                Return New GapValue(Of Integer?)(0)
            End Function

            Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of Integer?))
                Return lst.Select(Function(e) New GapValue(Of Integer?)(DirectCast(e, IntegerValue).Value))
            End Function
        End Class

    End Class

End Namespace
