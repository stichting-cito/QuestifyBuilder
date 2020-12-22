Public Interface IMathMlEditorPlugin

    Function GetMathMlEditorControl(inContentMode As Boolean) As IMathMlEditorControl

    Function RenderPng(mathMl As String) As Byte()

    Function RenderPng(mathMl As String, imageOptions As Dictionary(Of String, String)) As Byte()

    Function RenderPng(mathMl As String, imageOptions As Dictionary(Of String, String), ByRef verticalAlignValue As Double) As Byte()

End Interface
