Imports Questify.Builder.Logic.Service.Factories

Public Module ExceptionHelper

    Public Sub ShowDialog(ByVal exception As Exception)
        Dim sb As New System.Text.StringBuilder()
        sb.AppendLine(My.Resources.ErrorThrown)
        sb.AppendFormat("{0}{1}", Environment.NewLine, exception.Message)

        Dim innerEx As Exception = exception.InnerException
        Do Until innerEx Is Nothing
            sb.AppendFormat("{0}Inner exception:{0}{1}", Environment.NewLine, innerEx.Message)
            innerEx = innerEx.InnerException
        Loop

        MessageBox.Show(sb.ToString(), My.Resources.ErrorThrown, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub



    Public Sub ShowWizardExceptionHandler(ByVal owner As IWin32Window,
                                      wizardType As Type,
                                      ByVal ex As Exception,
                                      bankId As Integer,
                                      ParamArray text() As String)

        Dim lst As New List(Of String)
        Dim assembly As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim fileVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location)
        lst.Add(String.Format("{0} {1}", My.Application.Info.Title, fileVersion.ProductVersion))
        Dim bank = DtoFactory.Bank.Get(bankId)
        If (bank IsNot Nothing) Then
            Dim lstBank As New List(Of String)
            lstBank.Add(bank.Name)
            Dim b = bank.ParentBankId
            While (b.HasValue)
                Dim parentBank = DtoFactory.Bank.Get(b.value)
                lstBank.Add(parentBank.Name)
                b = parentBank.ParentBankId
            End While
            lstBank.Reverse()
            lst.Add(String.Format("Bank : {0}", String.Join(" / ", lstBank.ToArray())))
        End If

        lst.Add(String.Format("Wizard : {0}", wizardType))
        For Each s As String In text
            lst.Add(s)
        Next

        Using dialog As New WizardExceptionDialog(ex, lst)
            dialog.ShowDialog(owner)
        End Using
    End Sub

End Module
