Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Friend Class ScoringManipulatorDecorator(Of TScoringParameter As ScoringParameter) : Implements IScoreManipulator

        Private ReadOnly _decoree As IScoreManipulator

        Public Sub New(param As TScoringParameter, decoree As IScoreManipulator)
            Me.Param = param
            _decoree = decoree
        End Sub


        Protected Overridable ReadOnly Property Param As TScoringParameter



        Protected Overridable Function CanBeRemovedFromFactSet(parameterCollectionId As String) As Boolean Implements IScoreManipulator.CanBeRemovedFromFactSet
            Return _decoree.CanBeRemovedFromFactSet(parameterCollectionId)
        End Function

        Protected Overridable Function CreateFactSetTarget() As Integer Implements IScoreManipulator.CreateFactSetTarget
            Return _decoree.CreateFactSetTarget()
        End Function

        Protected Overridable Sub CreateFindingTarget(parameterCollectionId As String) Implements IScoreManipulator.CreateFindingTarget
            _decoree.CreateFindingTarget(parameterCollectionId)
        End Sub

        Protected Overridable ReadOnly Property FactSetTarget As Integer? Implements IScoreManipulator.FactSetTarget
            Get
                Return _decoree.FactSetTarget
            End Get
        End Property

        Protected Overridable Function GetDisplayValueForKey(key As String) As String Implements IScoreManipulator.GetDisplayValueForKey
            Return _decoree.GetDisplayValueForKey(key)
        End Function

        Public Function GetBaseValueForKey(key As String) As BaseValue Implements IScoreManipulator.GetBaseValueForKey
            Return _decoree.GetBaseValueForKey(key)
        End Function

        Protected Overridable Function GetValueForKey(key As String) As String Implements IScoreManipulator.GetValueForKey
            Return _decoree.GetValueForKey(key)
        End Function

        Protected Overridable Function GetFactIdForKey(key As String) As String Implements IScoreManipulator.GetFactIdForKey
            Return _decoree.GetFactIdForKey(key)
        End Function

        Protected Overridable Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer) Implements IScoreManipulator.GetFactSetNumbers
            Return _decoree.GetFactSetNumbers(scoreKey)
        End Function

        Protected Overridable Function GetHowToScoreMethod() As EnumScoringMethod Implements IScoreManipulator.GetHowToScoreMethod
            Return _decoree.GetHowToScoreMethod()
        End Function

        Protected Overridable Function GetKeysAlreadyManipulated() As IEnumerable(Of String) Implements IScoreManipulator.GetKeysAlreadyManipulated
            Return _decoree.GetKeysAlreadyManipulated()
        End Function

        Protected Overridable Function GetManipulatableKeys() As IEnumerable(Of String) Implements IScoreManipulator.GetManipulatableKeys
            Return _decoree.GetManipulatableKeys()
        End Function

        Protected Overridable Sub RemoveFactSetTarget(factSetNumber As Integer) Implements IScoreManipulator.RemoveFactSetTarget
            _decoree.RemoveFactSetTarget(factSetNumber)
        End Sub

        Protected Overridable Sub SetFactSetTarget(factSetNumber As Integer?) Implements IScoreManipulator.SetFactSetTarget
            _decoree.SetFactSetTarget(factSetNumber)
        End Sub

        Protected Overridable Sub SetHowToScoreMethod(e As EnumScoringMethod) Implements IScoreManipulator.SetHowToScoreMethod
            _decoree.SetHowToScoreMethod(e)
        End Sub

        Protected Overridable Function GetDomainForKey(ByVal key As String) As String Implements IScoreManipulator.GetDomainForKey
            Return _decoree.GetDomainForKey(key)
        End Function


        Protected Function CanOverrideDisplayValue(ByVal baseValue As String) As Boolean
            Return Not (String.IsNullOrEmpty(baseValue) OrElse DefaultStringOperations.IsKeyValueCatchAll(baseValue))
        End Function
    End Class
End Namespace