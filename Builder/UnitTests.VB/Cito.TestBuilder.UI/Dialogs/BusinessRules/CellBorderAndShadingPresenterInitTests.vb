Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class CellBorderAndShadingPresenterInitTests
    Inherits baseBorderAndShadingPresenterInitTests

    Friend Overrides Function CreateStyle() As TableStyleDto
        Return TableStyleDto.SingleCell()
    End Function

End Class
