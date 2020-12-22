Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Web

Namespace ContentModel.Scoring

    Friend Class InlineChoiceScoringManipulator : Inherits ChoiceScoringManipulatorDecorator

        Public Sub New(param As ChoiceScoringParameter, decoree As IChoiceScoringManipulator)
            MyBase.New(param, decoree)
        End Sub

        Protected Overrides Function GetDisplayValueForKey(ByVal key As String) As String
            Dim baseValue = MyBase.GetDisplayValueForKey(key)

            If Not CanOverrideDisplayValue(baseValue) Then
                Return baseValue
            End If

            Dim subKey = DefaultStringOperations.GetSubParameterId(key)

            Dim match As ParameterCollection = Param.Value.FirstOrDefault(Function(subPrm) subPrm.Id = subKey)
            If match Is Nothing Then
                Return baseValue
            End If

            Dim firstPlainTextParameter = DirectCast(
                match.InnerParameters.FirstOrDefault(Function(prm) TypeOf prm Is PlainTextParameter),
                PlainTextParameter)

            If (firstPlainTextParameter IsNot Nothing) Then
                Return HttpUtility.HtmlDecode(firstPlainTextParameter.Value)
            End If

            Return baseValue
        End Function

    End Class

End Namespace