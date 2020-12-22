Imports System.Linq
Imports System.Runtime.CompilerServices

Namespace HelperClasses

    Public Module UiExtensions

        <Extension()>
        Public Sub EnableFileNameDrop(ByVal txtBox As System.Windows.Forms.TextBox)

            txtBox.AllowDrop = True
            AddHandler txtBox.DragEnter, New DragEventHandler(Sub(sender, dragEvent)
                                                                  Dim hasData = dragEvent.Data.GetDataPresent("FileNameW")
                                                                  If (hasData) Then
                                                                      dragEvent.Effect = DragDropEffects.Link
                                                                  Else
                                                                      dragEvent.Effect = DragDropEffects.None
                                                                  End If
                                                              End Sub)

            AddHandler txtBox.DragDrop, New DragEventHandler(Sub(sender, dragEvent)
                                                                 Dim files As String() = CType(dragEvent.Data.GetData("FileNameW"), String())
                                                                 If (files IsNot Nothing AndAlso Not txtBox.ReadOnly) Then
                                                                     txtBox.Text = files.FirstOrDefault()
                                                                 End If
                                                             End Sub)
        End Sub

    End Module
End Namespace