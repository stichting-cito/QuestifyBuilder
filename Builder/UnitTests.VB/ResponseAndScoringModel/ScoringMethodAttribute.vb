
''' <summary>
''' An attribute to override the default behavior of scoring.
''' This class can be used in combination with baseScoringTest
''' </summary>
<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Method)>
Public Class ScoringMethodAttribute
    Inherits Attribute

    Public ReadOnly Property Method As String

    Public Sub New(method As String)
        Me.Method = method
    End Sub
End Class