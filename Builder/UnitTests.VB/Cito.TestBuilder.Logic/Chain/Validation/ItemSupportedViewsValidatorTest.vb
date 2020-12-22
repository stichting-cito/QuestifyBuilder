
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports FakeItEasy
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating

<TestClass()>
Public Class ItemSupportedViewsValidatorTest

    <TestMethod>
    Public Sub ContainsItemLayoutTemplateSupportedViews_ShouldReturnFalseIfViewsAreNotSupported()
        Dim itemLayoutTemplate As New ItemLayoutTemplate()
        itemLayoutTemplate.Targets.Add(New ItemLayoutTemplateTarget("word", ""))
        Dim handler As EventHandler(Of ResourceNeededEventArgs) = A.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).That.Matches(Function(e As ResourceNeededEventArgs) e.ResourceName = "ItemLayout"))).
            Invokes(Sub(i)
                        Dim e = i.GetArgument(Of ResourceNeededEventArgs)(1)
                        e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, SerializeHelper.XmlSerializeToString(itemLayoutTemplate), Nothing)
                    End Sub)

        Dim validator As New ItemSupportedViewsValidator(handler)
        Dim views As New List(Of String)
        views.Add("CES")

        Assert.IsFalse(validator.ContainsItemLayoutTemplateSupportedViews("ItemLayout", views))
    End Sub
End Class
