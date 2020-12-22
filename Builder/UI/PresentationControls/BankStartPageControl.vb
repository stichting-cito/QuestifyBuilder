
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Factories
Imports CustomClasses
Imports Questify.Builder.Security

Public Class BankStartPageControl

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

    Private _bankStatistics As BankStatistics
    Private _bankInfo As String
    Private _bankId As Integer


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


    Public Property BankInfo() As String
        Get
            Return _bankInfo
        End Get
        Set(ByVal value As String)
            _bankInfo = value
            WelcomeLabel.Text = _bankInfo
        End Set
    End Property

    Public Property BankId() As Integer
        Get
            Return _bankId
        End Get
        Set(ByVal value As Integer)
            _bankId = value
        End Set
    End Property

    Private Sub LabelFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = String.Format("Welcome to bank '{0}'", e.Value)
    End Sub

    Private Sub FillControls()
        Dim allowToViewItems As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.ItemEntity, _bankId)
        GroupboxItem.Visible = allowToViewItems
        If allowToViewItems Then
            Dim totalNumberOfItems As Integer = 0
            totalNumberOfItems = DataSource.TotalNumberOfItems
            Me.ItemsLabel.Text = String.Concat(totalNumberOfItems.ToString, "   ", My.Resources.Items)

            Dim numberOfItemsCreatedByMe As Integer = 0
            numberOfItemsCreatedByMe = DataSource.NumberOfItemsCreatedByMe

            Me.ItemsCreatedByMeLabel.Text = String.Concat(numberOfItemsCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)

            Dim totalUnUsedItems As Integer = 0
            totalUnUsedItems = DataSource.NumberOfUnusedItems
            Me.UnusedItemsLabel.Text = String.Concat(totalUnUsedItems.ToString, "   ", My.Resources.NotUsed)

            Me.RecentlyModifiedItemsGrid.DataSource = DataSource.LastModifiedItems


            Me.ItemStateInformationGrid.DataSource = DataSource.ItemStatus
        End If
        Dim allowToViewTest As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.TestEntity, _bankId)
        GroupboxTest.Visible = allowToViewTest
        If allowToViewTest Then
            Dim totalNumberOfTests As Integer = 0
            totalNumberOfTests = DataSource.TotalNumberOfTest
            Me.TestsLabel.Text = String.Concat(totalNumberOfTests.ToString, "   ", My.Resources.Tests)

            Dim numberOfTestsCreatedByMe As Integer = 0
            numberOfTestsCreatedByMe = DataSource.NumberOfTestCreatedByMe
            Me.TestsCreatedByMeLabel.Text = String.Concat(numberOfTestsCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
        Dim allowToViewMedia As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.MediaEntity, _bankId)
        GroupboxMedia.Visible = allowToViewMedia
        If allowToViewMedia Then
            If PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.MediaEntity, _bankId) Then
                Dim totalNumberOfMedia As Integer = 0
                totalNumberOfMedia = DataSource.TotalNumberOfMedia
                Me.MediaLabel.Text = String.Concat(totalNumberOfMedia.ToString, "   ", My.Resources.MediaFiles)

                Dim numberOfUnusedMediaFiles As Integer = 0
                numberOfUnusedMediaFiles = DataSource.NumberOfUnusedMedia

                Me.UnusedMediaFilesLabel.Text = String.Concat(numberOfUnusedMediaFiles.ToString, "   ", My.Resources.NotUsed)
            End If
        End If
        Dim allowToViewItemTemplate As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.ItemLayoutTemplateEntity, _bankId)
        GroupboxItemTemplates.Visible = allowToViewItemTemplate
        If allowToViewItemTemplate Then
            Dim numberOfItemtemplates As Integer = 0
            numberOfItemtemplates = DataSource.TotalNumberOfItemTemplates
            Me.ItemTemplatesLabel.Text = String.Concat(numberOfItemtemplates.ToString, "   ", My.Resources.ItemTemplates)

            Dim numberOfUnusedItemTemplates As Integer = 0
            numberOfUnusedItemTemplates = DataSource.NumberOfUnusedItemsTemplates
            Me.UnusedItemTemplatesLabel.Text = String.Concat(numberOfUnusedItemTemplates.ToString, "   ", My.Resources.NotUsed)

            Dim numberOfItemTemplatesCreatedByMe As Integer = 0
            numberOfItemTemplatesCreatedByMe = DataSource.NumberOfTestTemplatesCreatedByMe
            Me.ItemTemplatesCreatedByMeLabel.Text = String.Concat(numberOfItemTemplatesCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
        Dim allowToViewTestTemplate As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.TestTemplateEntity, _bankId)
        GroupboxTestTemplate.Visible = allowToViewTestTemplate
        If allowToViewTestTemplate Then
            Dim numberOfTestTemplates As Integer = 0
            numberOfTestTemplates = DataSource.TotalNumberOfTestTemplates
            Me.TestTemplatesLabel.Text = String.Concat(numberOfTestTemplates.ToString, "   ", My.Resources.TestTemplates)

            Dim numberOfTestTemplatesCreatedByMe As Integer = 0
            numberOfTestTemplatesCreatedByMe = DataSource.NumberOfTestTemplatesCreatedByMe
            Me.TestTemplatesCreatedByMeLabel.Text = String.Concat(numberOfTestTemplatesCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
        Dim allowToViewControlTemplate As Boolean = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.View, TestBuilderPermissionTarget.ControlTemplateEntity, _bankId)
        GroupboxControlTemplate.Visible = allowToViewControlTemplate
        If allowToViewControlTemplate Then
            Dim numberOfControltemplates As Integer = 0
            numberOfControltemplates = DataSource.TotalNumberOfControlTemplates
            Me.ControlTemplatesLabel.Text = String.Concat(numberOfControltemplates.ToString, "   ", My.Resources.ControlTemplate)

            Dim numberOfUnusedControlTemplates As Integer = 0
            numberOfUnusedControlTemplates = DataSource.NumberOfUnusedControlTemplates
            Me.UnusedControlTemplatesLabel.Text = String.Concat(numberOfUnusedControlTemplates.ToString, "   ", My.Resources.NotUsed)

            Dim numberOfControlTemplatesCreatedByMe As Integer = 0
            numberOfControlTemplatesCreatedByMe = DataSource.NumberOfControlTemplatesCreatedByMe
            Me.ControlTemplatesCreatedByMeLabel.Text = String.Concat(numberOfControlTemplatesCreatedByMe.ToString, "   ", My.Resources.CreatedByMe)
        End If
    End Sub




    Public Event OpenItem As EventHandler(Of Questify.Builder.UI.EntityActionEventArgs)

    Private Sub RecentlyModifiedItemsGrid_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles RecentlyModifiedItemsGrid.RowDoubleClick
        If e.Row.DataRow IsNot Nothing Then
            Dim itemId As System.Guid = DirectCast(e.Row.DataRow, ModifiedItems).resourceId
            Dim item = DtoFactory.Item.Get(itemId)
            Dim openItemEvent As New EntityActionEventArgs(item)
            RaiseEvent OpenItem(sender, openItemEvent)
        End If
    End Sub


    Public Overrides ReadOnly Property AllowAddNew As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides Function AddNewIsPermitted(bankId As Integer) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property AllowPublish As Boolean
        Get
            Return True
        End Get
    End Property

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

    <Description("This event will be raised when the 'Publish' option in the toolbar is clicked"), Category("GridBase events")>
    Public Event PublishBank As EventHandler(Of EventArgs)

    Public Sub OnPublishBank(ByVal e As EventArgs)
        RaiseEvent PublishBank(Me, e)
    End Sub

End Class
