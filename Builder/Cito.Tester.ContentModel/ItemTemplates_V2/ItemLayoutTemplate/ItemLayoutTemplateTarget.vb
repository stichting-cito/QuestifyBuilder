

<Serializable>
Public Class ItemLayoutTemplateTarget
    Inherits TargetBase



    Public Sub New(name As String, template As String)
        MyBase.New(name)
        Me.Template = New CDATA(template)
    End Sub


    Public Sub New(name As String, description As String, template As String)
        MyBase.New(name, description)
        Me.Template = New CDATA(template)
    End Sub


    Public Sub New()
        MyBase.New()
    End Sub



End Class