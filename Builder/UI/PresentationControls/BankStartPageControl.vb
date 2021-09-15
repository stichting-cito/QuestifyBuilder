
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Factories
Imports CustomClasses
Imports Questify.Builder.Security

Public Class BankStartPageControl

#Region "Enums"
    ''' <summary>
    ''' datasource contains multiple resultset as input
    ''' this is converted to a dataset with multiple datatables
    ''' the index of the datatable in coupled to a understandalbe name in this enumeration.
    ''' </summary>
    Private Enum resultSetDatatableName
        totalNumberOfItems = 0
        numberOfItemsCreatedByMe = 1
        numberOfUnusedItems = 2
        topModifiedItems = 3
        totalNumberOfTest = 4
        numberOfTestCreatedByMe = 5
        totalNumberOfMedia = 6
        numberOfUnusedMedia = 7
        totalNumberOfItemTemplates = 8
        numberOfUnusedItemsTemplates = 9
        numberOfItemTemplatesCreatedByMe = 10
        totalNumberOfTestTemplates = 11
        numberOfTestTemplatesCreatedByMe = 12
        totalNumberOfControlTemplates = 13
        numberOfUnusedControlTemplates = 14
        numberOfControlTemplates = 15
        itemStateInformation = 16
    End Enum
#End Region

#Region "Fields"
    Private _bankStatistics As BankStatistics
    Private _bankInfo As String
    Private _bankId As Integer

#End Region

#Region "Properties"
    ''' <summary>
    ''' Datasource where the grid binds to.
    ''' </summary>
    <Description("Bank entity where this control binds to"), Category("Data"), Browsable(False)>
    Public Property DataSource() As BankStatistics
        Get
            Return _bankStatistics
        End Get
        Set(ByVal value As BankStatistics)
            _bankStatistics = value
            If _bankStatistics IsNot Nothing Then
                FillControls()
            End If
        End Set
    End Property


    ''' <summary>
    ''' Gets or sets the name of the bank.
    ''' </summary>
    ''' <value>The name of the bank.</value>
    Public Property BankInfo() As String
        Get
            Return _bankInfo
        End Get
        Set(ByVal value As String)
            _bankInfo = value
            WelcomeLabel.Text = _bankInfo
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the bank id.
    ''' </summary>
    ''' <value>The bank id.</value>
    Public Property BankId() As Integer
        Get
            Return _bankId
        End Get
        Set(ByVal value As Integer)
            _bankId = value
        End Set
    End Property
#End Region

#Region "Private methods"
    ''' <summary>
    ''' Labels the formatter.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.ConvertEventArgs" /> instance containing the event data.</param>
    Private Sub LabelFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = String.Format("Welcome to bank '{0}'", e.Value)
    End Sub

    ''' <summary>
    ''' Fills the controls.
    ''' </summary>
    Private Sub FillControls()
        '---------------------------------------------------
        '----------------------ITEMS------------------------
        '---------------------------------------------------
        Dim allowToViewItems As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.ItemEntity, _bankId)
        GroupboxItem.Visible = allowToViewItems
        If allowToViewItems Then
            'Get number of items
            Dim totalNumberOfItems As Integer = 0
            totalNumberOfItems = DataSource.TotalNumberOfItems
            Me.ItemsLabel.Text = String.Concat(totalNumberOfItems.ToString, "   ", My.Resources.Items)

            'Get number of items created by me
            Dim numberOfItemsCreatedByMe As Integer = 0
            numberOfItemsCreatedByMe = DataSource.NumberOfItemsCreatedByMe

            Me.ItemsCreatedByMeLabel.Text = String.Concat(numberOfItemsCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)

            'Get unused items
            Dim totalUnUsedItems As Integer = 0
            totalUnUsedItems = DataSource.NumberOfUnusedItems
            Me.UnusedItemsLabel.Text = String.Concat(totalUnUsedItems.ToString, "   ", My.Resources.NotUsed)

            'Latest modified items by me
            Me.RecentlyModifiedItemsGrid.DataSource = DataSource.LastModifiedItems


            'ItemState Information
            Me.ItemStateInformationGrid.DataSource = DataSource.ItemStatus
        End If
        '---------------------------------------------------
        '----------------------TEST-------------------------
        '---------------------------------------------------
        Dim allowToViewTest As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.TestEntity, _bankId)
        GroupboxTest.Visible = allowToViewTest
        If allowToViewTest Then
            'Get number of test
            Dim totalNumberOfTests As Integer = 0
            totalNumberOfTests = DataSource.TotalNumberOfTest
            Me.TestsLabel.Text = String.Concat(totalNumberOfTests.ToString, "   ", My.Resources.Tests)

            'My Tests
            Dim numberOfTestsCreatedByMe As Integer = 0
            numberOfTestsCreatedByMe = DataSource.NumberOfTestCreatedByMe
            Me.TestsCreatedByMeLabel.Text = String.Concat(numberOfTestsCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
        '---------------------------------------------------
        ' ----------------------MEDIA------------------------
        '---------------------------------------------------
        Dim allowToViewMedia As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.MediaEntity, _bankId)
        GroupboxMedia.Visible = allowToViewMedia
        If allowToViewMedia Then
            If PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.MediaEntity, _bankId) Then
                'Get number of media files
                Dim totalNumberOfMedia As Integer = 0
                totalNumberOfMedia = DataSource.TotalNumberOfMedia
                Me.MediaLabel.Text = String.Concat(totalNumberOfMedia.ToString, "   ", My.Resources.MediaFiles)

                'Get unused media files
                Dim numberOfUnusedMediaFiles As Integer = 0
                numberOfUnusedMediaFiles = DataSource.NumberOfUnusedMedia

                Me.UnusedMediaFilesLabel.Text = String.Concat(numberOfUnusedMediaFiles.ToString, "   ", My.Resources.NotUsed)
            End If
        End If
        '---------------------------------------------------
        '-------------------ITEM_LAYOUT_TEMPLATES------------------
        '---------------------------------------------------
        Dim allowToViewItemTemplate As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.ItemLayoutTemplateEntity, _bankId)
        GroupboxItemTemplates.Visible = allowToViewItemTemplate
        If allowToViewItemTemplate Then
            'Get number item templates
            Dim numberOfItemtemplates As Integer = 0
            numberOfItemtemplates = DataSource.TotalNumberOfItemTemplates '- DataSource.NumberOfUnusedItemsTemplates
            Me.ItemTemplatesLabel.Text = String.Concat(numberOfItemtemplates.ToString, "   ", My.Resources.ItemTemplates)

            'Get number of unused itemtemplates
            Dim numberOfUnusedItemTemplates As Integer = 0
            numberOfUnusedItemTemplates = DataSource.NumberOfUnusedItemsTemplates
            Me.UnusedItemTemplatesLabel.Text = String.Concat(numberOfUnusedItemTemplates.ToString, "   ", My.Resources.NotUsed)

            'Get number of item templates created by me.
            Dim numberOfItemTemplatesCreatedByMe As Integer = 0
            numberOfItemTemplatesCreatedByMe = DataSource.NumberOfTestTemplatesCreatedByMe
            Me.ItemTemplatesCreatedByMeLabel.Text = String.Concat(numberOfItemTemplatesCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
        '---------------------------------------------------
        '-------------------TEST_TEMPLATES------------------
        '---------------------------------------------------
        Dim allowToViewTestTemplate As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.TestTemplateEntity, _bankId)
        GroupboxTestTemplate.Visible = allowToViewTestTemplate
        If allowToViewTestTemplate Then
            'Get number test templates
            Dim numberOfTestTemplates As Integer = 0
            numberOfTestTemplates = DataSource.TotalNumberOfTestTemplates
            Me.TestTemplatesLabel.Text = String.Concat(numberOfTestTemplates.ToString, "   ", My.Resources.TestTemplates)

            'Get number of test templates created by me.
            Dim numberOfTestTemplatesCreatedByMe As Integer = 0
            numberOfTestTemplatesCreatedByMe = DataSource.NumberOfTestTemplatesCreatedByMe
            Me.TestTemplatesCreatedByMeLabel.Text = String.Concat(numberOfTestTemplatesCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
        '---------------------------------------------------
        '----------------CONTROL_TEMPLATES------------------
        '---------------------------------------------------
        Dim allowToViewControlTemplate As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.ControlTemplateEntity, _bankId)
        GroupboxControlTemplate.Visible = allowToViewControlTemplate
        If allowToViewControlTemplate Then
            'Get number control templates
            Dim numberOfControltemplates As Integer = 0
            numberOfControltemplates = DataSource.TotalNumberOfControlTemplates
            Me.ControlTemplatesLabel.Text = String.Concat(numberOfControltemplates.ToString, "   ", My.Resources.ControlTemplate)

            'Get number of unused control templates
            Dim numberOfUnusedControlTemplates As Integer = 0
            numberOfUnusedControlTemplates = DataSource.NumberOfUnusedControlTemplates
            Me.UnusedControlTemplatesLabel.Text = String.Concat(numberOfUnusedControlTemplates.ToString, "   ", My.Resources.NotUsed)

            'Get number of control templates created by me.
            Dim numberOfControlTemplatesCreatedByMe As Integer = 0
            numberOfControlTemplatesCreatedByMe = DataSource.NumberOfControlTemplatesCreatedByMe
            Me.ControlTemplatesCreatedByMeLabel.Text = String.Concat(numberOfControlTemplatesCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
    End Sub



#End Region

#Region "Open Item"
    ''' <summary>
    ''' Occurs when [open item].
    ''' </summary>
    Public Event OpenItem As EventHandler(Of Questify.Builder.UI.EntityActionEventArgs)

    ''' <summary>
    ''' Handles the RowDoubleClick event of the RecentlyModifiedItemsGrid control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Janus.Windows.GridEX.RowActionEventArgs" /> instance containing the event data.</param>
    Private Sub RecentlyModifiedItemsGrid_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles RecentlyModifiedItemsGrid.RowDoubleClick
        If e.Row.DataRow IsNot Nothing Then
            Dim itemId As System.Guid = DirectCast(e.Row.DataRow, ModifiedItems).resourceId
            Dim item = DtoFactory.Item.Get(itemId)
            Dim openItemEvent As New EntityActionEventArgs(item)
            RaiseEvent OpenItem(sender, openItemEvent)
        End If
    End Sub
#End Region

#Region "ActionCommand support"
    
    ''' <summary>
    ''' Return False as start tab has no specific Add New action. 
    ''' </summary>
    Public Overrides ReadOnly Property AllowAddNew As Boolean
        Get
            Return False
        End Get
    End Property
    ''' <summary>
    ''' Add new is permitted as from the start tab is it allowed to add new specific things like items, templates
    ''' </summary>
    ''' <param name="bankId"></param>
    Public Overrides Function AddNewIsPermitted(bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property AllowPublish As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether [allow reports].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [allow reports]; otherwise, <c>false</c>.
    ''' </value>
    Public Overrides ReadOnly Property AllowReports() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Function PublishIsPermitted(bankId As Integer) As Boolean
        Return False
    End Function

    Public Overrides ReadOnly Property AllowSynchronize As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides Function SynchronizeIsPermitted(bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides Sub AddNew()
    End Sub

    Public Overrides Sub Publish()
        OnPublishBank(New EventArgs())
    End Sub

    ''' <summary>
    ''' This event will be raised when the 'Publish' option in the toolbar is clicked
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    <Description("This event will be raised when the 'Publish' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event PublishBank As EventHandler(Of EventArgs)

    ''' <summary>
    ''' Raises the <see cref="E:PublisBank" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Public Sub OnPublishBank(ByVal e As EventArgs)
        RaiseEvent PublishBank(Me, e)
    End Sub

#End Region
End Class
