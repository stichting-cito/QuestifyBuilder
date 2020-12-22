Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class RowBorderAndShadingPresenterInitTests
    Inherits baseBorderAndShadingPresenterInitTests

    Friend Overrides Function CreateStyle() As TableStyleDto
        Return TableStyleDto.Row()
    End Function

End Class
