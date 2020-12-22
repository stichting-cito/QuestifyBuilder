Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass()>
Public Class ColumBorderAndShadingPresenterInitTests
    Inherits baseBorderAndShadingPresenterInitTests

    Friend Overrides Function CreateStyle() As TableStyleDto
        Return TableStyleDto.Column()
    End Function

End Class
