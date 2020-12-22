Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class ColumAndRowBorderAndShadingPresenterInitTests
    Inherits baseBorderAndShadingPresenterInitTests

    Friend Overrides Function CreateStyle() As TableStyleDto
        Return TableStyleDto.ColAndRow()
    End Function

End Class
