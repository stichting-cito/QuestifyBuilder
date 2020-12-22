
Imports Cito.ItemViewer.AsyncPluggableProtocol
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Security
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports ResourceNeededEventArgs = Cito.Tester.ContentModel.ResourceNeededEventArgs
Imports System.Linq
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces

Public Class GenericResourceEditor


    Private _datasource As GenericResourceEntity
    Private _resourceManager As DataBaseResourceManager
    Private _singleResourceFetchLock As Object = New Object()
    Private ReadOnly _contextIdentifierForXhtmlResourceEditor As Nullable(Of Integer)
    Private _forceClose As Boolean = False
    Private ReadOnly _windowfacade As IWindowFacade = New WindowFacade()

    Private _userSavedTheResource As Boolean = False



    Public ReadOnly Property IsDirty() As Boolean
        Get
            For Each c As Control In Me.Controls
                c.Focus()
            Next

            Return _datasource.HasChangesInTopology OrElse
                MetaData.IsDirty OrElse
                ResourceCustomProperties1.RemovedEntities.Count > 0 OrElse
                (ResourceEditorControl.Viewer IsNot Nothing AndAlso ResourceEditorControl.Viewer.HasChangesToPropagate)
        End Get
    End Property

    Public ReadOnly Property UserSavedTheResource As Boolean
        Get
            Return _userSavedTheResource
        End Get
    End Property




    Public Sub New()
        MyBase.New()

        InitializeComponent()

        AddHandler TestSessionContext.ResourceNeeded, AddressOf ResourceNeeded
    End Sub

    Public Sub New(ByVal app As ASyncResourceProtocolManager)
        MyBase.New()

        InitializeComponent()
    End Sub

    Public Sub New(ByVal resourceId As Guid)
        Me.New()
        Dim request = New ResourceRequestDTO With
    {
        .WithDependencies = False,
        .WithReferences = False,
        .WithCustomProperties = True,
        .WithUserInfo = True,
        .WithState = True,
        .WithHiddenResources = False
    }
        _datasource = CType(ResourceFactory.Instance.GetResourceByIdWithOption(resourceId, New GenericResourceEntityFactory(), request), GenericResourceEntity)
        _datasource.ResourceData = ResourceFactory.Instance.GetResourceData(_datasource)

        Debug.Assert(_datasource.Bank IsNot Nothing, "_datasource.bank is nothing")

        _resourceManager = New DataBaseResourceManager(_datasource.BankId)
        _contextIdentifierForXhtmlResourceEditor = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(_resourceManager)

    End Sub



    Public Sub DataBind()
        ResourceEditorControl.ResourceManager = _resourceManager
        ResourceEditorControl.BankContextIdentifier = _contextIdentifierForXhtmlResourceEditor
        ResourceEditorControl.DataSource = _datasource
        MetaData.ResourceEntity = _datasource
        ResourceCustomProperties1.ResourceEntity = _datasource


        SetTitleBarCaption()
    End Sub



    Private Sub SetTitleBarCaption()
        Me.Text = String.Format(My.Resources.MediaEditorForResource, _datasource.Name)
    End Sub

    Private Function SaveIfNecessary() As Boolean
        Dim returnValue As Boolean = True

        If ResourceEditorControl.Validate() Then
            If Me.IsDirty AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.MediaEntity, _datasource.BankId) Then
                Dim result As DialogResult = MessageBox.Show(My.Resources.Editor_SaveChangesQuestionMessage, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                Select Case result
                    Case DialogResult.Cancel
                        returnValue = False
                    Case DialogResult.No
                        returnValue = True
                    Case DialogResult.Yes
                        returnValue = SaveGenericResource()
                End Select
            End If
        End If

        Return returnValue
    End Function

    Private Function SaveAsGenericResource() As Boolean
        Dim returnValue As Boolean

        If ResourceEditorControl.Validate() Then

            Dim newCode As InputBoxResult = InputBox.Show(My.Resources.ItemEditor_PleaseEnterNewItemCode,
                                                          False,
                                                          My.Resources.ItemEditor_SaveAsText,
                                                          String.Format(My.Resources.CopyOf0, _datasource.Name), AddressOf ValidationHelper.IsValidResourceCode)
            If newCode.Text.Length > 0 Then
                Try
                    Dim myNewResource = _datasource.CopyToNew(newCode.Text.Trim)

                    Dim orgResource As GenericResourceEntity = _datasource
                    myNewResource.OriginalName = orgResource.Name
                    myNewResource.OriginalVersion = orgResource.Version

                    myNewResource.Version = String.Empty

                    _datasource = myNewResource

                    returnValue = SaveGenericResource()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Application.ProductName)

                Finally
                    _datasource = ResourceFactory.Instance.GetGenericResource(_datasource)
                    DataBind()
                End Try
            End If
        End If

        Return returnValue
    End Function

    Private Function SaveGenericResource() As Boolean
        If Me.MetaData.CanUpdateResource(False) Then
            RemoveCustomBankPropertyValues()

            ResourceEditorControl.PreSave()

            If _datasource.MediaType = "text/plain" Then
                _datasource.ResourceData.FileExtension = ".xml"
            End If

            _datasource.Size = CInt(_datasource.ResourceData.BinData.Length / 1024)

            If _datasource.Size = 0 Then
                Dim realSize As Double = _datasource.ResourceData.BinData.Length / 1024
                If realSize > 0 Then _datasource.Size = 1
            End If

            If _datasource.RequiresMajorVersionIncrement() Then
                If Not IncrementMajorVersion() Then
                    Return False
                End If
            End If

            _datasource.UpdateDependencies()
            Dim result As String = ResourceFactory.Instance.UpdateGenericResource(_datasource)
            _datasource = ResourceFactory.Instance.GetGenericResource(_datasource)
            _datasource.ResourceData = ResourceFactory.Instance.GetResourceData(_datasource)

            DataBind()

            If String.IsNullOrEmpty(result) Then
                _userSavedTheResource = True
                Return True
            Else
                MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Else
            MessageBox.Show(My.Resources.AllEditors_CannotUpdateBecauseOfState, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Function

    Private Sub RemoveCustomBankPropertyValues()
        If ResourceCustomProperties1.RemovedEntities.Count > 0 Then
            For Each removedEntity As CustomBankPropertyValueEntity In ResourceCustomProperties1.RemovedEntities.OfType(Of CustomBankPropertyValueEntity)()
                _datasource.CustomBankPropertyValueCollection.Remove(removedEntity)
            Next

            BankFactory.Instance.DeleteCustomPropertyValues(ResourceCustomProperties1.RemovedEntities)
            ResourceCustomProperties1.RemovedEntities.Clear()
        End If
    End Sub

    Private Function IncrementMajorVersion() As Boolean
        Return _windowfacade.OpenMajorVersionDialog(_datasource)
    End Function

    Private Sub ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs) Handles ResourceEditorControl.ResourceNeeded
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    Private Sub GenericResourceEditor_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        If _resourceManager IsNot Nothing Then
            TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
            _resourceManager.Dispose()
            _resourceManager = Nothing
        End If

        RemoveHandler TestSessionContext.ResourceNeeded, AddressOf ResourceNeeded
    End Sub

    Private Sub GenericResourceEditor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.MediaEntity, _datasource.BankId) Then
            SaveToolStripButton.Enabled = False
            SaveCloseToolStripMenuItem.Enabled = False
            SaveCloseToolStripMenuItem.Enabled = False
            SaveCloseToolStripButton.Enabled = False
            SaveAsToolStripMenuItem.Enabled = False
            SaveAsToolStripButton.Enabled = False
        End If

        DataBind()
    End Sub

    Private Sub GenericResourceEditor_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If Not _forceClose Then
            If ResourceEditorControl.Validate() Then
                If Not Me.SaveIfNecessary() Then e.Cancel = True Else e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        If ResourceEditorControl.Validate() Then
            If Me.IsDirty Then
                SaveGenericResource()
            End If
        End If
    End Sub

    Private Sub SaveCloseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveCloseToolStripMenuItem.Click, SaveCloseToolStripButton.Click
        If ResourceEditorControl.Validate() Then
            _forceClose = True
            SaveToolStripMenuItem_Click(sender, e)
            Me.Close()
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click, SaveAsToolStripButton.Click
        If ResourceEditorControl.Validate() Then
            If SaveAsGenericResource() Then
                SetTitleBarCaption()
            End If
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        If ResourceEditorControl.Validate() Then
            If Me.SaveIfNecessary() Then
                Me.Close()
            End If
        End If
    End Sub



End Class
