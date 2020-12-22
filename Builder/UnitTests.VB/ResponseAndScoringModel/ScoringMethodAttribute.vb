
<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Method)>
Public Class ScoringMethodAttribute
    Inherits Attribute

    Public ReadOnly Property Method As String

    Public Sub New(method As String)
        Me.Method = method
    End Sub
End Class