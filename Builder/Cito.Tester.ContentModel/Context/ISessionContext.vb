Imports Cito.Tester.Common

Public Interface ISessionContext


    Event BrowserAltF4Pressed As EventHandler

    Event BrowserPausePressed As EventHandler

    Event CurrentItemChanged As EventHandler

    Event GetTitleBarButtonIsSupported As EventHandler(Of TitleBarButtonStateEventArgs)

    Event GotoItemAtIndex As EventHandler(Of GotoItemEventArgs)

    Event GoToNextItem As EventHandler

    Event GoToNextItemInTest As EventHandler

    Event GoToPreviousItem As EventHandler

    Event HandInTest As EventHandler(Of HandInTestEventArgs)

    Event ReloadItem As EventHandler

    Event ReportValue As EventHandler(Of ReportValueEventArgs)

    Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Event SetItemResponse As EventHandler(Of ResponseEvenArgs)

    Event SuspendTest As EventHandler

    Event ToggleNavigationBar As EventHandler(Of ToggleNavigationBarEventArgs)

    Event ToggleTitleBar As EventHandler(Of ToggleNavigationBarEventArgs)

    Event ToggleTitleBarButtonEnabled As EventHandler(Of TitleBarButtonStateEventArgs)

    Event ToggleTitleBarButtonVisible As EventHandler(Of TitleBarButtonStateEventArgs)

    Event UIButtonClickedCommand As EventHandler(Of UIButtonClickedEventArgs)

    Event UpdateResponse As EventHandler(Of ResponseEvenArgs)



    Property CurrentItem As AssessmentItem

    Property CurrentItemIndex As Integer

    Property IsTestPaused As Boolean

    Property PickedItemsV2 As PickedItemCollectionV2

    ReadOnly Property Responses As TestResponseCollection

    Property SuspendingTest As Boolean

    Property TestEndInitializeTime As DateTime

    Property TestStartInitializeTime As DateTime

    Property TotalScoreableItemCount As Integer



    Function GetMetaDataForCurrentItem() As MetaDataCollection

    Function GetMetaDataValueForCurrentItem(metaDataName As String) As String

    Function GetResourceMetaData(resourceName As String) As MetaDataCollection

    Function GetResourceMetaDataValue(resourceName As String, metaDataName As String) As String

    Function GetResourceObject(resourceName As String, streamProcessingDelegate As ResourceProcessingFunction) As Object

    Sub OnBrowserAltF4Pressed(sender As Object)

    Sub OnBrowserPausePressed(sender As Object)

    Sub OnCurrentItemChanged(sender As Object, e As EventArgs)

    Function OnGetTitleBarButtonIsSupported(sender As Object, buttonName As String) As Boolean

    Sub OnGotoItemAtIndex(sender As Object, e As GotoItemEventArgs)

    Sub OnGoToNextItem(sender As Object)

    Sub OnGoToNextItemInTest(sender As Object)

    Sub OnGoToPreviousItem(sender As Object)

    Sub OnHandInTest(sender As Object, e As HandInTestEventArgs)

    Sub OnReloadItem(sender As Object)

    Sub OnReportValue(sender As Object, e As ReportValueEventArgs)

    Sub OnResourceNeeded(e As ResourceNeededEventArgs)

    Sub OnSetItemResponse(sender As Object, e As ResponseEvenArgs)

    Sub OnSuspendTest(sender As Object)

    Sub OnToggleNavigationBar(sender As Object, e As ToggleNavigationBarEventArgs)

    Sub OnToggleTitleBar(sender As Object, e As ToggleNavigationBarEventArgs)

    Sub OnToggleTitleBarButtonEnabled(sender As Object, e As TitleBarButtonStateEventArgs)

    Sub OnToggleTitleBarButtonVisible(sender As Object, e As TitleBarButtonStateEventArgs)

    Sub OnUpdateResponse(sender As Object, e As ResponseEvenArgs)

    Sub ScoreTest()

    Sub SendUIButtonClickedCommand(commandText As String)

    Sub SendUIButtonClickedCommand(commandToExec As UICommand, commandText As String)


End Interface