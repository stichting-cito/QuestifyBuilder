Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.Common.Logging

Public Class SessionContext
    Implements ISessionContext


    Private _currentItem As AssessmentItem
    Private _currentItemIndex As Integer
    Private _isTestPaused As Boolean
    Private _pickedItemsReference As PickedItemCollectionV2
    Private _responses As TestResponseCollection
    Private _suspendingTest As Boolean
    Private _testEndInitializeTime As DateTime
    Private _testStartInitializeTime As DateTime
    Private _totalItemCount As Integer



    Public Event BrowserAltF4Pressed(sender As Object, e As EventArgs) Implements ISessionContext.BrowserAltF4Pressed

    Public Event BrowserPausePressed(sender As Object, e As EventArgs) Implements ISessionContext.BrowserPausePressed

    Public Event CurrentItemChanged(sender As Object, e As EventArgs) Implements ISessionContext.CurrentItemChanged

    Public Event GetTitleBarButtonIsSupported(sender As Object, e As TitleBarButtonStateEventArgs) Implements ISessionContext.GetTitleBarButtonIsSupported

    Public Event GotoItemAtIndex(sender As Object, e As GotoItemEventArgs) Implements ISessionContext.GotoItemAtIndex

    Public Event GoToNextItem(sender As Object, e As EventArgs) Implements ISessionContext.GoToNextItem

    Public Event GoToNextItemInTest(sender As Object, e As EventArgs) Implements ISessionContext.GoToNextItemInTest

    Public Event GoToPreviousItem(sender As Object, e As EventArgs) Implements ISessionContext.GoToPreviousItem

    Public Event HandInTest(sender As Object, e As HandInTestEventArgs) Implements ISessionContext.HandInTest

    Public Event ReloadItem(sender As Object, e As EventArgs) Implements ISessionContext.ReloadItem

    Public Event ReportValue(sender As Object, e As ReportValueEventArgs) Implements ISessionContext.ReportValue

    Public Event ResourceNeeded(sender As Object, e As ResourceNeededEventArgs) Implements ISessionContext.ResourceNeeded

    Public Event SetItemResponse(sender As Object, e As ResponseEvenArgs) Implements ISessionContext.SetItemResponse

    Public Event SuspendTest(sender As Object, e As EventArgs) Implements ISessionContext.SuspendTest

    Public Event ToggleNavigationBar(sender As Object, e As ToggleNavigationBarEventArgs) Implements ISessionContext.ToggleNavigationBar

    Public Event ToggleTitleBar(sender As Object, e As ToggleNavigationBarEventArgs) Implements ISessionContext.ToggleTitleBar

    Public Event ToggleTitleBarButtonEnabled(sender As Object, e As TitleBarButtonStateEventArgs) Implements ISessionContext.ToggleTitleBarButtonEnabled

    Public Event ToggleTitleBarButtonVisible(sender As Object, e As TitleBarButtonStateEventArgs) Implements ISessionContext.ToggleTitleBarButtonVisible

    Public Event UIButtonClickedCommand(sender As Object, e As UIButtonClickedEventArgs) Implements ISessionContext.UIButtonClickedCommand

    Public Event UpdateResponse(sender As Object, e As ResponseEvenArgs) Implements ISessionContext.UpdateResponse



    Public Property CurrentItem As AssessmentItem Implements ISessionContext.CurrentItem
        Get
            Return _currentItem
        End Get
        Set
            _currentItem = value
            OnCurrentItemChanged(Nothing, New EventArgs())
        End Set
    End Property

    Public Property CurrentItemIndex As Integer Implements ISessionContext.CurrentItemIndex
        Get
            Return _currentItemIndex
        End Get
        Set
            _currentItemIndex = value
        End Set
    End Property

    Public Property IsTestPaused As Boolean Implements ISessionContext.IsTestPaused
        Get
            Return _isTestPaused
        End Get
        Set
            _isTestPaused = value
        End Set
    End Property

    Public Property PickedItemsV2 As PickedItemCollectionV2 Implements ISessionContext.PickedItemsV2
        Get
            Return _pickedItemsReference
        End Get
        Set
            _pickedItemsReference = value
        End Set
    End Property

    Public ReadOnly Property Responses As TestResponseCollection Implements ISessionContext.Responses
        Get
            Return _responses
        End Get
    End Property

    Public Property SuspendingTest As Boolean Implements ISessionContext.SuspendingTest
        Get
            Return _suspendingTest
        End Get
        Set
            _suspendingTest = value
        End Set
    End Property

    Public Property TestEndInitializeTime As Date Implements ISessionContext.TestEndInitializeTime
        Get
            Return _testEndInitializeTime
        End Get
        Set
            _testEndInitializeTime = value
        End Set
    End Property

    Public Property TestStartInitializeTime As Date Implements ISessionContext.TestStartInitializeTime
        Get
            Return _testStartInitializeTime
        End Get
        Set
            _testStartInitializeTime = value
        End Set
    End Property

    Public Property TotalScoreableItemCount As Integer Implements ISessionContext.TotalScoreableItemCount
        Get
            Return _totalItemCount
        End Get
        Set
            _totalItemCount = value
        End Set
    End Property



    Public Function GetMetaDataForCurrentItem() As MetaDataCollection Implements ISessionContext.GetMetaDataForCurrentItem
        If CurrentItem IsNot Nothing Then
            Return GetResourceMetaData(CurrentItem.Identifier)
        Else
            Return New MetaDataCollection
        End If
    End Function

    Public Function GetMetaDataValueForCurrentItem(metaDataName As String) As String Implements ISessionContext.GetMetaDataValueForCurrentItem
        If CurrentItem IsNot Nothing Then
            Return GetResourceMetaDataValue(CurrentItem.Identifier, metaDataName)
        Else
            Return String.Empty
        End If
    End Function

    Public Function GetResourceMetaData(resourceName As String) As MetaDataCollection Implements ISessionContext.GetResourceMetaData
        Dim e As New ResourceNeededEventArgs(Uri.UnescapeDataString(resourceName), Nothing, ResourceNeededCommand.MetaData)

        Try
            OnResourceNeeded(e)
            Return e.Metadata

        Catch ex As Exception
            Return New MetaDataCollection()
        End Try
    End Function

    Public Function GetResourceMetaDataValue(resourceName As String, metaDataName As String) As String Implements ISessionContext.GetResourceMetaDataValue
        Dim e As New ResourceNeededEventArgs(Uri.UnescapeDataString(resourceName), Nothing, ResourceNeededCommand.MetaData)
        Dim mdc As MetaDataCollection

        OnResourceNeeded(e)
        mdc = e.Metadata

        For Each md As MetaData In
            From md1 In mdc
            Where
                    md1.MetaDatatype = MetaData.enumMetaDataType.BankCustomProperty AndAlso
                    md1.Name.Equals(metaDataName, StringComparison.InvariantCultureIgnoreCase)
            Return md.Value
        Next

        Return String.Empty
    End Function

    Public Function GetResourceObject(resourceName As String,
        streamProcessingDelegate As ResourceProcessingFunction) As Object Implements ISessionContext.GetResourceObject
        Dim e As New ResourceNeededEventArgs(Uri.UnescapeDataString(resourceName), streamProcessingDelegate)

        OnResourceNeeded(e)

        If e.BinaryResource IsNot Nothing Then
            Return e.BinaryResource.ResourceObject
        End If

        Return Nothing
    End Function

    Public Sub OnBrowserAltF4Pressed(sender As Object) Implements ISessionContext.OnBrowserAltF4Pressed
        RaiseEvent BrowserAltF4Pressed(sender, EventArgs.Empty)
    End Sub

    Public Sub OnBrowserPausePressed(sender As Object) Implements ISessionContext.OnBrowserPausePressed
        RaiseEvent BrowserPausePressed(sender, EventArgs.Empty)
    End Sub

    Public Sub OnCurrentItemChanged(sender As Object, e As EventArgs) Implements ISessionContext.OnCurrentItemChanged
        RaiseEvent CurrentItemChanged(sender, e)
    End Sub

    Public Function OnGetTitleBarButtonIsSupported(sender As Object, buttonName As String) As Boolean Implements ISessionContext.OnGetTitleBarButtonIsSupported
        Dim eventArgs As New TitleBarButtonStateEventArgs(buttonName)

        RaiseEvent GetTitleBarButtonIsSupported(sender, eventArgs)

        Return eventArgs.StateValue
    End Function

    Public Sub OnGotoItemAtIndex(sender As Object, e As GotoItemEventArgs) Implements ISessionContext.OnGotoItemAtIndex
        RaiseEvent GotoItemAtIndex(sender, e)
    End Sub

    Public Sub OnGoToNextItem(sender As Object) Implements ISessionContext.OnGoToNextItem
        RaiseEvent GoToNextItem(sender, EventArgs.Empty)
    End Sub

    Public Sub OnGoToNextItemInTest(sender As Object) Implements ISessionContext.OnGoToNextItemInTest
        RaiseEvent GoToNextItemInTest(sender, EventArgs.Empty)
    End Sub

    Public Sub OnGoToPreviousItem(sender As Object) Implements ISessionContext.OnGoToPreviousItem
        RaiseEvent GoToPreviousItem(sender, EventArgs.Empty)
    End Sub

    Public Sub OnHandInTest(sender As Object, e As HandInTestEventArgs) Implements ISessionContext.OnHandInTest
        RaiseEvent HandInTest(sender, e)
    End Sub

    Public Sub OnReloadItem(sender As Object) Implements ISessionContext.OnReloadItem
        RaiseEvent ReloadItem(sender, EventArgs.Empty)
    End Sub

    Public Sub OnReportValue(sender As Object, e As ReportValueEventArgs) Implements ISessionContext.OnReportValue
        RaiseEvent ReportValue(sender, e)
    End Sub

    Public Sub OnResourceNeeded(e As ResourceNeededEventArgs) Implements ISessionContext.OnResourceNeeded
        If e IsNot Nothing Then
            RaiseEvent ResourceNeeded(Nothing, e)

            If e.Command = ResourceNeededCommand.Resource AndAlso (e.BinaryResource Is Nothing OrElse e.BinaryResource.ResourceObject Is Nothing) Then
                Throw New ArgumentException($"Resource with name [{e.ResourceName}] could not be resolved")
            End If
        Else
            Throw New ArgumentNullException("e")
        End If
    End Sub

    Public Sub OnSetItemResponse(sender As Object, e As ResponseEvenArgs) Implements ISessionContext.OnSetItemResponse
        RaiseEvent SetItemResponse(sender, e)
    End Sub

    Public Sub OnSuspendTest(sender As Object) Implements ISessionContext.OnSuspendTest
        RaiseEvent SuspendTest(sender, EventArgs.Empty)
    End Sub

    Public Sub OnToggleNavigationBar(sender As Object, e As ToggleNavigationBarEventArgs) Implements ISessionContext.OnToggleNavigationBar
        RaiseEvent ToggleNavigationBar(sender, e)
    End Sub

    Public Sub OnToggleTitleBar(sender As Object, e As ToggleNavigationBarEventArgs) Implements ISessionContext.OnToggleTitleBar
        RaiseEvent ToggleTitleBar(sender, e)
    End Sub

    Public Sub OnToggleTitleBarButtonEnabled(sender As Object, e As TitleBarButtonStateEventArgs) Implements ISessionContext.OnToggleTitleBarButtonEnabled
        RaiseEvent ToggleTitleBarButtonEnabled(sender, e)
    End Sub

    Public Sub OnToggleTitleBarButtonVisible(sender As Object, e As TitleBarButtonStateEventArgs) Implements ISessionContext.OnToggleTitleBarButtonVisible
        RaiseEvent ToggleTitleBarButtonVisible(sender, e)
    End Sub

    Public Sub OnUpdateResponse(sender As Object, e As ResponseEvenArgs) Implements ISessionContext.OnUpdateResponse
        RaiseEvent UpdateResponse(sender, e)
    End Sub

    Public Sub ScoreTest() Implements ISessionContext.ScoreTest
    End Sub

    Public Sub SendUIButtonClickedCommand(commandToExec As UICommand, commandText As String) Implements ISessionContext.SendUIButtonClickedCommand
    End Sub

    Public Sub SendUIButtonClickedCommand(commandText As String) Implements ISessionContext.SendUIButtonClickedCommand
        SendUIButtonClickedCommand(UICommand.CustomCommand, commandText)
    End Sub


End Class