Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces

Public Class FormHelper
    Private Shared ReadOnly Windowfacade As IWindowFacade = New WindowFacade()

    Public Const MAX_OPEN_ITEMS As Integer = 2

    Public Shared Sub ShowPreviewTestProgressDialog(testPreviewLauncher As TestPreviewLauncher, ByRef cursor As Cursor)
        Dim dlg As New ProgressDialog(My.Resources.PreviewTestProgressCaption, True, New DoWorkEventHandler(AddressOf testPreviewLauncher.PreviewTest),
                                          Sub(p As ProgressDialog.ProgressHandler)
                                              If p.Progress Is Nothing OrElse p.Progress.ProgressPercentage = 0 Then
                                                  p.ProgressMarquee()
                                              Else
                                                  p.ProgressBlocks()
                                                  p.ProgressBar(p.Progress.ProgressPercentage)
                                              End If
                                              If p.Progress IsNot Nothing AndAlso p.Progress.UserState IsNot Nothing Then
                                                  p.SetText(p.Progress.UserState.ToString())
                                              End If
                                          End Sub)

        dlg.ShowDialog()
        If testPreviewLauncher.Errors.Count > 0 Then
            Dim sb As New StringBuilder()
            For Each msg In testPreviewLauncher.Errors
                sb.Append(msg.ToString())
            Next
            MessageBox.Show(sb.ToString(), My.Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        cursor = Cursors.Default
    End Sub

    Public Shared Function OpenResourcePropertyDialog(ByVal propertyEntity As IPropertyEntity, Optional ByVal defaultTab As Integer = 0) As Boolean
        If propertyEntity.IsNew Then
            MessageBox.Show(My.Resources.OnlyAllowedWhenResourceIsNotNew, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            Return Windowfacade.OpenResourcePropertyDialog(propertyEntity.Id, propertyEntity.GetType(), defaultTab)
        End If
    End Function

    Public Shared Function TestsContainItems(tests As IEnumerable(Of AssessmentTestResourceDto)) As Boolean
        For Each testEntity In tests
            Dim containsItems = (From dependency In DtoFactory.Test.GetDependencies(testEntity.resourceId)
                                 Where TypeOf dependency Is ItemResourceDto).Count > 0
            If Not containsItems Then
                MessageBox.Show(My.Resources.TestDoesNotContainAnyItemsPleaseAddItemsToTheTestBeforePublishing, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Next
        Return True
    End Function
    Public Shared Sub OpenItemByID(resourceId As Guid, bankId as Integer)
        Dim item As ItemResourceDto = new ItemResourceDto With {.BankId = bankId, .ResourceId = resourceId}
        OpenItem(item)
    End Sub

    Public Shared Sub OpenItem(ByVal entityToOpen As ItemResourceDto, Optional canMoveBack As Boolean = False, Optional canMoveNext As Boolean = False, Optional canChangeCode As Boolean = True)
        If entityToOpen IsNot Nothing Then
            If ResourceEntityExists(entityToOpen) Then
                If Windowfacade.ItemsToBeOpen.Count = MAX_OPEN_ITEMS AndAlso Not Windowfacade.ItemsToBeOpen.Contains(entityToOpen.resourceId) Then
                    MessageBox.Show(String.Format(My.Resources.MainForm_MaxOpenItemsReached, MAX_OPEN_ITEMS), My.Resources.MainForm_MaxOpenItemsReachedTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf AddToOpenItems(entityToOpen.resourceId) Then
                    Windowfacade.OpenItemEditorById(entityToOpen.resourceId, canMoveBack, canMoveNext, canChangeCode)
                End If
            End If
        End If
    End Sub

    private Shared Function ResourceEntityExists(ByVal resourceEntity As ItemResourceDto) As Boolean
        If resourceEntity Is Nothing Then Throw New ArgumentNullException("resourceEntity")
        Return ResourceFactory.Instance.ResourceExists(resourceEntity.bankId, resourceEntity.resourceId, False, New ItemResourceEntityFactory())
    End Function

    Public Shared Function AddToOpenItems(id As Guid) As Boolean
        If Windowfacade.ItemsToBeOpen.Contains(id) Then
            If (Windowfacade.FocusItem(id)) Then
                Return False
            Else
                Return True
            End If
        Else
            Windowfacade.ItemsToBeOpen.Add(id)
            Return True
        End If
    End Function
End Class
