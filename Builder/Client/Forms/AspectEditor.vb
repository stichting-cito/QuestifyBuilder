
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
Imports Questify.Builder.Logic.ContentModel.Scoring

Public Class AspectEditor

    Public Event AspectChanged As EventHandler(Of Questify.Builder.UI.RefreshEventArgs)

#Region "Fields"

    Private _aspect As Aspect
    Private _aspectResource As AspectResourceEntity
    Private _originalHash As Byte()
    Private _forceClose As Boolean = False
    Private _resourceManager As DataBaseResourceManager
    Private _contextIdentifierForItemViewer As Nullable(Of Integer)
    Private ReadOnly _windowfacade As IWindowFacade = New WindowFacade()

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal aspectResource As AspectResourceEntity)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _aspectResource = aspectResource
        AddHandler TestSessionContext.ResourceNeeded, AddressOf GenericHandler_ResourceNeeded
        AddHandler AspectPropertyEditor.MaxScoreChanged, AddressOf RefreshScoreTranslationEditor

        SetupAspect()
    End Sub

    Private Sub GenericHandler_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    Private Sub SetupAspect()

        If (_resourceManager Is Nothing) Then
            _resourceManager = New DataBaseResourceManager(_aspectResource.BankId)
        End If

        _contextIdentifierForItemViewer = TestBuilderAsyncProtocolContextManager.RegisterNewResourceManager(_resourceManager)

        If _aspectResource.IsNew Then
            'Create new aspect
            _aspect = New Aspect()
            With _aspect
                .Identifier = String.Empty
                .Title = String.Empty
                .MaxScore = 0
            End With
            _originalHash = _aspect.GetMD5Hash()
        Else
            'Make sure _aspectResource is the full entity
            _aspectResource = ResourceFactory.Instance.GetAspect(_aspectResource)

            'Get resourcedata
            Try
                _aspect = _aspectResource.GetAspect

                'Get hash to track changes
                _originalHash = _aspect.GetMD5Hash()

            Catch ex As ORMEntityOutOfSyncException
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                _aspect = Nothing
            End Try
        End If

    End Sub

    Private Sub BindControls()
        AspectPropertyEditor.Initialize(_aspect, _aspectResource.BankId, _resourceManager, _aspectResource)
        AspectPropertyEditor.ContextIdentifier = _contextIdentifierForItemViewer
        AspectResourceMetaData.ResourceEntity = _aspectResource
        AspectGeneral.Aspect = _aspect
        SetScoreTranslationEditor(_aspect.MaxScore, _aspect.AspectScoreTranslationTable)

        ValidateAspect(False)
    End Sub

    Private Sub RefreshScoreTranslationEditor(ByVal sender As Object, ByVal e As EventArgs)
        Dim newMaxScore As Integer = 0
        Integer.TryParse(DirectCast(e, Questify.Builder.UI.EventArgs(Of Integer)).Value.ToString(), newMaxScore)
        SetScoreTranslationEditor(newMaxScore, AspectScoreTranslationTableEditor.DataSource)
        AspectScoreTranslationTableEditor.Refresh()
    End Sub

    Private Sub SetScoreTranslationEditor(maxRawScore As Integer, currentScoreTranslationTable As AspectScoreTranslationTable)
        If currentScoreTranslationTable Is Nothing Then
            currentScoreTranslationTable = New AspectScoreTranslationTable()
        End If
        Dim calc = New AspectScoreTranslationTableCalculator(maxRawScore, currentScoreTranslationTable)
        AspectScoreTranslationTableEditor.DataSource = CType(calc.SynchronizeScoreTranslationTableWithMaxRawScore(), AspectScoreTranslationTable)
    End Sub

    Private Sub EnsuresFormUpdateAfterEditing()
        AspectPropertyEditor.Validate()
        AspectScoreTranslationTableEditor.Validate()
    End Sub

#End Region 'Constructors

#Region "Protected Properties"

    ''' <summary>
    ''' Gets a value indicating whether this instance is dirty.
    ''' </summary>
    ''' <value><c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
    Protected ReadOnly Property IsDirty() As Boolean
        Get
            Return _aspectResource.IsNew OrElse ResourceDataIsDirty() OrElse _aspectResource.IsDirty OrElse AspectResourceMetaData.IsDirty()
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether [resourse data is dirty].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [resourse data is dirty]; otherwise, <c>false</c>.
    ''' </value>
    Protected ReadOnly Property ResourceDataIsDirty() As Boolean
        Get
            Dim currentEntityHash As Byte() = If(_aspect IsNot Nothing, _aspect.GetMD5Hash(), Nothing)
            Return Not ArrayHelper.CompareByteArray(_originalHash, currentEntityHash)
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether this instance is saving allowed.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if this instance is saving allowed; otherwise, <c>false</c>.
    ''' </value>
    Protected ReadOnly Property IsSavingAllowed() As Boolean
        Get
            Return Not AspectResourceMetaData.CanNotUpdate
        End Get
    End Property


#End Region 'Protected Properties

#Region "Public Properties"

    ''' <summary>
    ''' Gets or sets the resource.
    ''' </summary>
    ''' <value>The resource.</value>
    Public Property Resource() As AspectResourceEntity
        Get
            Return _aspectResource
        End Get
        Set(ByVal value As AspectResourceEntity)
            _aspectResource = value
        End Set
    End Property

#End Region 'Public Properties

#Region "Private Methods"

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

    ''' <summary>
    ''' Handles the FormClosing event of the AspectEditor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs" /> instance containing the event data.</param>
    Private Sub AspectEditor_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        EnsuresFormUpdateAfterEditing()
        If Not _forceClose Then
            'Check for changes; if true ask for save, else just close
            e.Cancel = Not SaveIfNecessary()
        End If
    End Sub

    Private Sub AspectEditor_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
        _resourceManager.Dispose()
        _resourceManager = Nothing
        RemoveHandler TestSessionContext.ResourceNeeded, AddressOf GenericHandler_ResourceNeeded
        RemoveHandler AspectPropertyEditor.MaxScoreChanged, AddressOf RefreshScoreTranslationEditor
    End Sub

    ''' <summary>
    ''' Handles the Load event of the AspectEditor control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub AspectEditor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If _aspect Is Nothing Then
            'Close the form as is; can't do anything without the aspect entity
            _forceClose = True
            Close()
            Exit Sub
        End If

        BindControls()

        'Add form.text databinding for form caption
        Dim titleBarBinding As New Binding("text", _aspect, "identifier")
        AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
        DataBindings.Add(titleBarBinding)

        SaveAsToolStripButton.Enabled = Not _aspectResource.IsNew
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ExitToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
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
                        _aspectResource.Version = String.Empty 'Reset version
                        _aspect.Identifier = _aspectResource.Name
                        returnValue = SaveAspect(False)
                        If (returnValue) Then
                            _aspectResource = ResourceFactory.Instance.GetAspect(_aspectResource)
                            OnAspectChanged(New Questify.Builder.UI.RefreshEventArgs(_aspectResource, True))
                        Else
                            'The save has failed; restoring entities and rebind!
                            _aspectResource = orgAspectResource
                            SetupAspect()

                            'Rebind
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

    ''' <summary>
    ''' Handles the Click event of the SaveAsToolStripButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
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

    ''' <summary>
    ''' Validates if the name is valid. More refactoring can be done because this function also exists in other editors.
    ''' </summary>
    ''' <param name="bankId">The bank</param>
    ''' <param name="resourceName">The name of the resource to validate</param>
    Private Function IsNewResourceValid(ByVal bankId As Integer, ByVal resourceName As String) As Boolean
        Return Not String.IsNullOrEmpty(resourceName) AndAlso ResourceFactory.Instance.GetResourceByNameWithOption(bankId, resourceName, New ResourceRequestDTO()) Is Nothing
    End Function

    ''' <summary>
    ''' Saves the aspect.
    ''' </summary>
    ''' <param name="force">if set to <c>true</c> [force].</param>
    Private Function SaveAspect(Optional ByVal force As Boolean = True) As Boolean
        Dim success As Boolean = True
        AspectResourceMetaData.EndEdit()
        AspectGeneral.ValidateChildren()
        If ValidateAspect(True) Then
            'Make the property editor updates the aspect's Description property before saving.
            AspectPropertyEditor.UpdateDescription()

            If Not force AndAlso Not IsDirty Then
                Return False
            End If

            'Keep certain properties of the aspect and aspectResource in sync.
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

    ''' <summary>
    ''' Handles the Click event of the SaveCloseToolStripButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub SaveCloseToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveCloseToolStripButton.Click, SaveCloseToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()

        If SaveAspect() Then
            _forceClose = True
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Saves if necessary.
    ''' </summary>
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

    ''' <summary>
    ''' Handles the Click event of the SaveToolStripButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripButton.Click, SaveToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()
        SaveAspect(False)
    End Sub

    ''' <summary>
    ''' Titles to form title formatter.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.ConvertEventArgs" /> instance containing the event data.</param>
    Private Sub TitleToFormTitleFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = String.Format(My.Resources.AspectEditor_TitleBar, e.Value)
    End Sub

#End Region 'Private Methods

    ''' <summary>
    ''' Raises the <see cref="Questify.Builder.UI.RefreshEventArgs" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Protected Overridable Sub OnAspectChanged(ByVal e As UI.RefreshEventArgs)
        RaiseEvent AspectChanged(Me, e)
    End Sub

End Class
