Imports System.Text

Public Class WizardExceptionDialog

    Private _exception As Exception
    Private _txt As IList(Of String)

    Sub New(ex As Exception, txt As IList(Of String))

        InitializeComponent()

        _exception = ex
        _txt = txt
    End Sub

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        MyBase.OnLoad(e)

        Dim sb As New StringBuilder()

        For Each s In _txt
            sb.AppendLine(s)
        Next

        sb.AppendLine()
        sb.AppendLine("---[Exception]---")

        If _exception IsNot Nothing Then

            Dim ex As Exception = _exception

            Do Until ex Is Nothing
                sb.Append(ex.Message)
                sb.Append(Environment.NewLine + Environment.NewLine)
                ex = ex.InnerException
            Loop

            ExceptionTextBox.Text = sb.ToString()
            stackTraceTextBox.Text = _exception.StackTrace
        End If

    End Sub
End Class