Imports System.Linq

Public Class ParameterEditorHelper

    Public Shared Function AddToolTip(ByVal labelName As String, ByVal description As String, ByVal paramLabel As Label) As ToolTip
        If Not String.IsNullOrEmpty(description) Then
            description = AddNewLines(description)
            Dim toolTipControl As New ToolTip()
            toolTipControl.ToolTipIcon = ToolTipIcon.Info
            toolTipControl.ToolTipTitle = labelName
            toolTipControl.SetToolTip(paramLabel, description)
            Return toolTipControl
        End If
        Return Nothing
    End Function

    Private Shared Function AddNewLines(description As String) As String
        Dim result As String = String.Empty
        Dim lines As String() = description.Split(New String() {"[\n]"}, StringSplitOptions.RemoveEmptyEntries)
        For x As Integer = 0 To lines.Count - 1
            If x = lines.Count - 1 Then
                result = String.Concat(result, lines(x))
            Else
                result = String.Concat(result, lines(x), Environment.NewLine)
            End If
        Next
        Return result
    End Function

End Class
