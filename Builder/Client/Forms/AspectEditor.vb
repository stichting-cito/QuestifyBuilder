
Imports System.ComponentModel
Imports System.Text
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class AspectEditor

    Public Event AspectChanged As EventHandler(Of Questify.Builder.UI.RefreshEventArgs)


    Private _aspect As Aspect
    Private _aspectResource As AspectResourceEntity
    Private _originalHash As Byte()
    Private _forceClose As Boolean = False
    Private _resourceManager As DataBaseResourceManager
    Private _contextIdentifierForItemViewer As Nullable(Of Integer)
    Private ReadOnly _windowfacade As IWindowFacade = New WindowFacade()



    Public Sub New(ByVal aspectResource As AspectResourceEntity)
        InitializeComponent()

        _aspectResource = aspectResource
        AddHandler TestSessionContext.ResourceNeeded, AddressOf GenericHandler_ResourceNeeded

        SetupAspect()
        BindControls()
    End Sub

    Private Sub GenericHandler_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    Private Sub SetupAspect()

        If (_resourceManager Is Nothing) Then
            _resourceManager = New DataBaseResourceManager(_aspectResource.BankId)
        End If

        _contextIdentifierForItemViewer = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(_resourceManager)
        AspectPropertyEditor.ContextIdentifier = _contextIdentifierForItemViewer

        If _aspectResource.IsNew Then
            _aspect = New Aspect()
            With _aspect
                .Identifier = String.Empty
                .Title = String.Empty
                .MaxScore = 0
            End With
            _originalHash = _aspect.GetMD5Hash()
        Else
            _aspectResource = ResourceFactory.Instance.GetAspect(_aspectResource)

            Try
                _aspect = _aspectResource.GetAspect

                _originalHash = _aspect.GetMD5Hash()

            Catch ex As ORMEntityOutOfSyncException
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                _aspect = Nothing
            End Try
        End If

    End Sub

    Private Sub BindControls()
        AspectPropertyEditor.Initialize(_aspect, _aspectResource.BankId, _resourceManager, _aspectResource)
        AspectResourceMetaData.ResourceEntity = _aspectResource
        AspectGeneral.Aspect = _aspect

        ValidateAspect(False)
    End Sub

    Private Sub EnsuresFormUpdateAfterEditing()
        AspectPropertyEditor.Validate()
    End Sub



    Protected ReadOnly Property IsDirty() As Boolean
        Get
            Return _aspectResource.IsNew OrElse ResourceDataIsDirty() OrElse _aspectResource.IsDirty OrElse AspectResourceMetaData.IsDirty()
        End Get
    End Property

    Protected ReadOnly Property ResourceDataIsDirty() As Boolean
        Get
            Dim currentEntityHash As Byte() = If(_aspect IsNot Nothing, _aspect.GetMD5Hash(), Nothing)
            Return Not ArrayHelper.CompareByteArray(_originalHash, currentEntityHash)
        End Get
    End Property

    Protected ReadOnly Property IsSavingAllowed() As Boolean
        Get
            Return Not AspectResourceMetaData.CanNotUpdate
        End Get
    End Property




    Public Property Resource() As AspectResourceEntity
        Get
            Return _aspectResource
        End Get
        Set(ByVal value As AspectResourceEntity)
            _aspectResource = value
        End Set
    End Property



    Private Function ValidateAspect(ByVal showMessageBox As Boolean) As Boolean
        Dim validatingFailed As Boolean = False
        Dim errorMessageBuilder As New StringBuilder()
        errorMessageBuilder.AppendFormat(My.Resources.AspectEditor_ValidateAspect_ValidationErrors, Environment.NewLine)

        If Not IsSavingAllowed Then
            validatingFailed = True
        End If
        _aspect.ValidateAllProperties()
        Dim dataErrorInfo As IDataErrorInfo = DirectCast(_aspect, IDataErrorInfo)
        If Not String.IsNullOrEmpty(dataErrorInfo.Error) Then
            errorMessageBuilder.Append(dataErrorInfo.Error)
            validatingFailed = True
        End If

        If validatingFailed Then
            If showMessageBox Then
                MessageBox.Show(errorMessageBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub AspectEditor_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        EnsuresFormUpdateAfterEditing()
        If Not _forceClose Then
            e.Cancel = Not SaveIfNecessary()
        End If
    End Sub

    Private Sub AspectEditor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
        _resourceManager.Dispose()
        _resourceManager = Nothing
        RemoveHandler TestSessionContext.ResourceNeeded, AddressOf GenericHandler_ResourceNeeded
    End Sub

    Private Sub AspectEditor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If _aspect Is Nothing Then
            _forceClose = True
            Close()
            Exit Sub
        End If

        Dim titleBarBinding As New Binding("text", _aspect, "identifier")
        AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
        DataBindings.Add(titleBarBinding)

        SaveAsToolStripButton.Enabled = Not _aspectResource.IsNew
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()
        If SaveIfNecessary() Then
            Close()
        End If
    End Sub

    Private Function SaveAsAspect() As Boolean
        Dim returnValue As Boolean = True
        Dim orgAspectResource As AspectResourceEntity = Nothing
        Dim isValidResourceName As Boolean = False
        Dim result As InputBoxResult = Nothing

        While (Not isValidResourceName OrElse Not returnValue)
            result = InputBox.Show(My.Resources.AspectEditor_PleaseEnterNewItemCode, False, My.Resources.ItemEditor_SaveAsText, String.Format(My.Resources.CopyOf0, _aspectResource.Name), AddressOf ValidationHelper.IsValidResourceCode)

            If result.ReturnCode = DialogResult.OK Then
                Try
                    isValidResourceName = IsNewResourceValid(_aspectResource.BankId, result.Text.Trim)
                    If (isValidResourceName) Then
                        orgAspectResource = _aspectResource
                        _aspectResource = _aspectResource.CopyToNew(result.Text.Trim)
                        _aspectResource.Version = String.Empty
                        _aspect.Identifier = _aspectResource.Name
                        returnValue = SaveAspect(False)
                        If (returnValue) Then
                            _aspectResource = ResourceFactory.Instance.GetAspect(_aspectResource)
                            OnAspectChanged(New Questify.Builder.UI.RefreshEventArgs(_aspectResource, True))
                        Else
                            _aspectResource = orgAspectResource
                            SetupAspect()

                            BindControls()
                        End If
                    Else
                        MessageBox.Show(My.Resources.TestEditor_TestCodeNameAlreadyExistsInBankHierarchy, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Application.ProductName)
                    Exit While
                End Try
            Else
                Exit While
            End If
        End While

        Return returnValue
    End Function

    Private Sub SaveAsToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripButton.Click, SaveAsToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()

        If SaveAsAspect() Then
            Dim titleBarBinding As Binding = DataBindings("text")
            If titleBarBinding IsNot Nothing Then
                DataBindings.Remove(titleBarBinding)
            End If
            titleBarBinding = New Binding("text", _aspect, "identifier")
            AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
            DataBindings.Add(titleBarBinding)
        End If
    End Sub

    Private Function IsNewResourceValid(ByVal bankId As Integer, ByVal resourceName As String) As Boolean
        Return Not String.IsNullOrEmpty(resourceName) AndAlso ResourceFactory.Instance.GetResourceByNameWithOption(bankId, resourceName, New ResourceRequestDTO()) Is Nothing
    End Function

    Private Function SaveAspect(Optional ByVal force As Boolean = True) As Boolean
        Dim success As Boolean = True
        AspectResourceMetaData.EndEdit()
        AspectGeneral.ValidateChildren()
        If ValidateAspect(True) Then
            AspectPropertyEditor.UpdateDescription()

            If Not force AndAlso Not IsDirty Then
                Return False
            End If

            _aspectResource.Name = _aspect.Identifier
            _aspectResource.Title = _aspect.Title
            _aspectResource.RawScore = _aspect.MaxScore
            _aspectResource.SetAspect(_aspect)
            AspectPropertyEditor.RemoveUnusedDependentResources()

            If _aspectResource.RequiresMajorVersionIncrement() AndAlso Not IncrementMajorVersion() Then
                Return False
            End If

            _aspectResource.UpdateDepencencies()
            Dim updateResult As String = ResourceFactory.Instance.UpdateAspectResource(_aspectResource)

            If String.IsNullOrEmpty(updateResult) Then
                success = True
                StatusTextLabel.Text = My.Resources.AspectEditor_Save_SavedStatusbarMessage
                OnAspectChanged(New Questify.Builder.UI.RefreshEventArgs(_aspectResource, True))
                _originalHash = _aspect.GetMD5Hash()
            Else
                MessageBox.Show(updateResult, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                success = False
                StatusTextLabel.Text = My.Resources.AspectEditor_Save_SaveFailedStatusbarMessage
            End If
        End If

        Return success
    End Function

    Private Function IncrementMajorVersion() As Boolean
        Return _windowfacade.OpenMajorVersionDialog(_aspectResource)
    End Function

    Private Sub SaveCloseToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveCloseToolStripButton.Click, SaveCloseToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()

        If SaveAspect(False) Then
            _forceClose = True
            Close()
        End If
    End Sub

    Private Function SaveIfNecessary() As Boolean
        Dim result As Boolean = True

        If IsDirty AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.AspectEntity, _aspectResource.BankId) Then
            Dim dialogResult As DialogResult = MessageBox.Show(My.Resources.Editor_SaveChangesQuestionMessage, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            Select Case dialogResult
                Case DialogResult.Cancel
                    result = False
                Case DialogResult.No
                    result = True
                Case DialogResult.Yes
                    result = SaveAspect()
            End Select
        End If

        Return result
    End Function

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripButton.Click, SaveToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()
        SaveAspect(False)
    End Sub

    Private Sub TitleToFormTitleFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = String.Format(My.Resources.AspectEditor_TitleBar, e.Value)
    End Sub


    Protected Overridable Sub OnAspectChanged(ByVal e As UI.RefreshEventArgs)
        RaiseEvent AspectChanged(Me, e)
    End Sub

End Class
