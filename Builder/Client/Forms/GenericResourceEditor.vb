
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

#Region " Fields "

    Private _datasource As GenericResourceEntity
    Private _resourceManager As DataBaseResourceManager
    Private _singleResourceFetchLock As Object = New Object()
    Private ReadOnly _contextIdentifierForXhtmlResourceEditor As Nullable(Of Integer)
    Private _forceClose As Boolean = False
    Private ReadOnly _windowfacade As IWindowFacade = New WindowFacade()

    Private _userSavedTheResource As Boolean = False

#End Region

#Region " Properties "

    ''' <summary>
    ''' Gets a value indicating whether this instance is dirty.
    ''' </summary>
    ''' <value><c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
    Public ReadOnly Property IsDirty() As Boolean
        Get
            ' if edited control did not loose focus yet, the datasource is not dirty yet
            For Each c As Control In Me.Controls
                c.Focus()
            Next

            Return _datasource.HasChangesInTopology OrElse
                MetaData.IsDirty OrElse
                ResourceCustomProperties1.RemovedEntities.Count > 0 OrElse
                (ResourceEditorControl.Viewer IsNot Nothing AndAlso ResourceEditorControl.Viewer.HasChangesToPropagate)
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether the user has saved the resource (at least once) during the edit session.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [user has saved the resource]
    ''' </value>
    Public ReadOnly Property UserSavedTheResource As Boolean
        Get
            Return _userSavedTheResource
        End Get
    End Property

#End Region

#Region " Methods "

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GenericResourceEditor" /> class.
    ''' </summary>
    Public Sub New()
        MyBase.New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler TestSessionContext.ResourceNeeded, AddressOf ResourceNeeded
    End Sub

    Public Sub New(ByVal app As ASyncResourceProtocolManager)
        MyBase.New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GenericResourceEditor" /> class.
    ''' </summary>
    ''' <param name="resourceId">The datasource.</param>
    Public Sub New(ByVal resourceId As Guid)
        Me.New()
        ' validate parameters
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

#End Region

#Region " Public "

    ''' <summary>
    ''' Datas the bind.
    ''' </summary>
    Public Sub DataBind()
        ResourceEditorControl.ResourceManager = _resourceManager
        ResourceEditorControl.BankContextIdentifier = _contextIdentifierForXhtmlResourceEditor
        ResourceEditorControl.DataSource = _datasource
        MetaData.ResourceEntity = _datasource
        ResourceCustomProperties1.ResourceEntity = _datasource

        ' set titlebar caption
        SetTitleBarCaption()
    End Sub

#End Region

#Region " Private procedures "

    ''' <summary>
    ''' Sets the title bar caption.
    ''' </summary>
    Private Sub SetTitleBarCaption()
        Me.Text = String.Format(My.Resources.MediaEditorForResource, _datasource.Name)
    End Sub

    ''' <summary>
    ''' Saves if necessary.
    ''' </summary>
    Private Function SaveIfNecessary() As Boolean
        Dim returnValue As Boolean = True

        ' force validation of the editor control so that data is written to the resource entity.
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

    ''' <summary>
    ''' Saves as generic resource.
    ''' </summary>
    Private Function SaveAsGenericResource() As Boolean
        Dim returnValue As Boolean

        ' force validation of the editor control so that data is written to the resource entity.
        If ResourceEditorControl.Validate() Then

            Dim newCode As InputBoxResult = InputBox.Show(My.Resources.ItemEditor_PleaseEnterNewItemCode,
                                                          False,
                                                          My.Resources.ItemEditor_SaveAsText,
                                                          String.Format(My.Resources.CopyOf0, _datasource.Name), AddressOf ValidationHelper.IsValidResourceCode)
            If newCode.Text.Length > 0 Then
                Try
                    ' Request new clone
                    Dim myNewResource = _datasource.CopyToNew(newCode.Text.Trim)

                    'ruben 17-1-2011 added the versioning attributes
                    Dim orgResource As GenericResourceEntity = _datasource
                    myNewResource.OriginalName = orgResource.Name
                    myNewResource.OriginalVersion = orgResource.Version

                    'reset version
                    myNewResource.Version = String.Empty

                    ' Reset datasource
                    _datasource = myNewResource

                    ' Save resource
                    returnValue = SaveGenericResource()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, Application.ProductName)

                Finally
                    ' Update UI
                    _datasource = ResourceFactory.Instance.GetGenericResource(_datasource)
                    DataBind()
                End Try
            End If
        End If

        Return returnValue
    End Function

    ''' <summary>
    ''' Saves the generic resource.
    ''' </summary><returns></returns>
    Private Function SaveGenericResource() As Boolean
        If Me.MetaData.CanUpdateResource(False) Then 'Check whether workflow allows us to save current resource
            'Remove Custom Property Values
            RemoveCustomBankPropertyValues()

            ' notify resource editor that we're saving so last minute changes can be made.
            ResourceEditorControl.PreSave()

            ' set file extension column, when plain text for full-text indexing
            If _datasource.MediaType = "text/plain" Then
                _datasource.ResourceData.FileExtension = ".xml"
            End If

            ' try to update the size
            _datasource.Size = CInt(_datasource.ResourceData.BinData.Length / 1024)             ' save in Kb

            ' Save the the size as 1 KB to let the user know the object is not empty
            If _datasource.Size = 0 Then
                Dim realSize As Double = _datasource.ResourceData.BinData.Length / 1024
                If realSize > 0 Then _datasource.Size = 1
            End If

            'Increment MajorVersion if required by new status
            If _datasource.RequiresMajorVersionIncrement() Then
                If Not IncrementMajorVersion() Then
                    Return False
                End If
            End If

            'Save resource
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

    ''' <summary>
    ''' Handles the ResourceNeeded event.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Cito.Tester.ContentModel.ResourceNeededEventArgs" /> instance containing the event data.</param>
    Private Sub ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs) Handles ResourceEditorControl.ResourceNeeded
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    ''' <summary>
    ''' Handles the FormClosed event of the GenericResourceEditor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs" /> instance containing the event data.</param>
    Private Sub GenericResourceEditor_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        If _resourceManager IsNot Nothing Then
            TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
            _resourceManager.Dispose()
            _resourceManager = Nothing
        End If

        RemoveHandler TestSessionContext.ResourceNeeded, AddressOf ResourceNeeded
    End Sub

    ''' <summary>
    ''' Handles the Load event of the GenericResourceEditor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
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
            ' force validation of the editor control so that data is written to the resource entity.
            If ResourceEditorControl.Validate() Then
                If Not Me.SaveIfNecessary() Then e.Cancel = True Else e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        ' force validation of the editor control so that data is written to the resource entity.
        If ResourceEditorControl.Validate() Then
            If Me.IsDirty Then
                SaveGenericResource()
            End If
        End If
    End Sub

    Private Sub SaveCloseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveCloseToolStripMenuItem.Click, SaveCloseToolStripButton.Click
        ' force validation of the editor control so that data is written to the resource entity.
        If ResourceEditorControl.Validate() Then
            _forceClose = True
            SaveToolStripMenuItem_Click(sender, e)
            Me.Close()
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click, SaveAsToolStripButton.Click
        ' force validation of the editor control so that data is written to the resource entity.
        If ResourceEditorControl.Validate() Then
            If SaveAsGenericResource() Then
                SetTitleBarCaption()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ExitToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        If ResourceEditorControl.Validate() Then
            If Me.SaveIfNecessary() Then
                Me.Close()
            End If
        End If
    End Sub

#End Region

#End Region

End Class
