Imports Cito.Tester.Common

<Obsolete("This class is obsolete, use an instance of SessionContext (or another implementation of ISessionContext) instead.")>
Public NotInheritable Class TestSessionContext

    Private Shared ReadOnly _currentSessionContext As ISessionContext = New SessionContext()

    Friend Shared ReadOnly Property CurrentSessionContext As ISessionContext
        Get
            Dim currentOnStack As ISessionContext = SessionContextProvider.CurrentSession
            If (currentOnStack IsNot Nothing) Then
                Return currentOnStack
            End If
            Return _currentSessionContext
        End Get
    End Property


    Private Shared _isInEditor As Boolean



    Public Shared Property CurrentItem As AssessmentItem
        Get
            Return CurrentSessionContext.CurrentItem
        End Get
        Set
            CurrentSessionContext.CurrentItem = value
        End Set
    End Property


    Public Shared Property CurrentItemIndex As Integer
        Get
            Return CurrentSessionContext.CurrentItemIndex
        End Get
        Set
            CurrentSessionContext.CurrentItemIndex = value
        End Set
    End Property


    Public Shared Property PickedItemsV2 As PickedItemCollectionV2
        Get
            Return CurrentSessionContext.PickedItemsV2
        End Get
        Set
            CurrentSessionContext.PickedItemsV2 = value
        End Set
    End Property





    Public Shared Sub OnBrowserAltF4Pressed(sender As Object)
        CurrentSessionContext.OnBrowserAltF4Pressed(sender)
    End Sub


    Public Shared Sub OnSuspendTest(sender As Object)
        CurrentSessionContext.OnSuspendTest(sender)
    End Sub


    Public Shared Sub OnBrowserPausePressed(sender As Object)
        CurrentSessionContext.OnBrowserPausePressed(sender)
    End Sub


    Public Shared Sub OnGoToNextItem(sender As Object)
        CurrentSessionContext.OnGoToNextItem(sender)
    End Sub


    Public Shared Sub OnGoToNextItemInTest(sender As Object)
        CurrentSessionContext.OnGoToNextItemInTest(sender)
    End Sub


    Public Shared Sub OnGotoItemAtIndex(sender As Object, e As GotoItemEventArgs)
        CurrentSessionContext.OnGotoItemAtIndex(sender, e)
    End Sub


    Public Shared Sub OnReloadItem(sender As Object)
        CurrentSessionContext.OnReloadItem(sender)
    End Sub


    Public Shared Sub OnGoToPreviousItem(sender As Object)
        CurrentSessionContext.OnGoToPreviousItem(sender)
    End Sub



    Public Shared Sub OnHandInTest(sender As Object, e As HandInTestEventArgs)
        CurrentSessionContext.OnHandInTest(sender, e)
    End Sub


    Public Shared Sub OnReportValue(sender As Object, e As ReportValueEventArgs)
        CurrentSessionContext.OnReportValue(sender, e)
    End Sub


    Public Shared Sub OnCurrentItemChanged(sender As Object, e As EventArgs)
        CurrentSessionContext.OnCurrentItemChanged(sender, e)
    End Sub


    Public Shared Sub OnUpdateResponse(sender As Object, e As ResponseEvenArgs)
        CurrentSessionContext.OnUpdateResponse(sender, e)
    End Sub


    Public Shared Sub OnSetItemResponse(sender As Object, e As ResponseEvenArgs)
        CurrentSessionContext.OnSetItemResponse(sender, e)
    End Sub


    Public Shared Sub OnToggleNavigationBar(sender As Object, e As ToggleNavigationBarEventArgs)
        CurrentSessionContext.OnToggleNavigationBar(sender, e)
    End Sub


    Public Shared Sub OnToggleTitleBar(sender As Object, e As ToggleNavigationBarEventArgs)
        CurrentSessionContext.OnToggleTitleBar(sender, e)
    End Sub


    Public Shared Sub OnToggleTitleBarButtonVisible(sender As Object, e As TitleBarButtonStateEventArgs)
        CurrentSessionContext.OnToggleTitleBarButtonVisible(sender, e)
    End Sub


    Public Shared Sub OnToggleTitleBarButtonEnabled(sender As Object, e As TitleBarButtonStateEventArgs)
        CurrentSessionContext.OnToggleTitleBarButtonEnabled(sender, e)
    End Sub


    Public Shared Function OnGetTitleBarButtonIsSupported(sender As Object, buttonName As String) As Boolean
        Return CurrentSessionContext.OnGetTitleBarButtonIsSupported(sender, buttonName)
    End Function


    Public Shared Sub ScoreTest()
        CurrentSessionContext.ScoreTest()
    End Sub


    Public Shared Sub SendUIButtonClickedCommand(commandText As String)
        CurrentSessionContext.SendUIButtonClickedCommand(UICommand.CustomCommand, commandText)
    End Sub


    Public Shared Sub SendUIButtonClickedCommand(commandToExec As UICommand, commandText As String)
        CurrentSessionContext.SendUIButtonClickedCommand(commandToExec, commandText)
    End Sub



    Public Shared Function GetResourceMetaData(resourceName As String) As MetaDataCollection
        Return CurrentSessionContext.GetResourceMetaData(resourceName)
    End Function



    Public Shared Function GetResourceObject(resourceName As String,
                                             streamProcessingDelegate As ResourceProcessingFunction) As Object
        Return CurrentSessionContext.GetResourceObject(resourceName, streamProcessingDelegate)
    End Function


    Public Shared Function GetMetaDataForCurrentItem() As MetaDataCollection
        Return CurrentSessionContext.GetMetaDataForCurrentItem()
    End Function



    Public Shared Function GetResourceMetaDataValue(resourceName As String, metaDataName As String) _
         As String
        Return CurrentSessionContext.GetResourceMetaDataValue(resourceName, metaDataName)
    End Function



    Public Shared Function GetMetaDataValueForCurrentItem(metaDataName As String) As String
        Return CurrentSessionContext.GetMetaDataValueForCurrentItem(metaDataName)
    End Function



    Public Shared Custom Event BrowserAltF4Pressed As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.BrowserAltF4Pressed, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.BrowserAltF4Pressed, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnBrowserAltF4Pressed(sender)
        End RaiseEvent
    End Event

    Public Shared Custom Event BrowserPausePressed As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.BrowserPausePressed, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.BrowserPausePressed, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnBrowserPausePressed(sender)
        End RaiseEvent
    End Event

    Public Shared Custom Event ReloadItem As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.ReloadItem, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.ReloadItem, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnReloadItem(sender)
        End RaiseEvent
    End Event

    Public Shared Custom Event GoToNextItem As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.GoToNextItem, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.GoToNextItem, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnGoToNextItem(sender)
        End RaiseEvent
    End Event

    Public Shared Custom Event GoToNextItemInTest As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.GoToNextItemInTest, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.GoToNextItemInTest, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnGoToNextItemInTest(sender)
        End RaiseEvent
    End Event

    Public Shared Custom Event GotoItemAtIndex As EventHandler(Of GotoItemEventArgs)
        AddHandler(value As EventHandler(Of GotoItemEventArgs))
            AddHandler CurrentSessionContext.GotoItemAtIndex, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of GotoItemEventArgs))
            RemoveHandler CurrentSessionContext.GotoItemAtIndex, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As GotoItemEventArgs)
            CurrentSessionContext.OnGotoItemAtIndex(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event GoToPreviousItem As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.GoToPreviousItem, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.GoToPreviousItem, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnGoToPreviousItem(sender)
        End RaiseEvent
    End Event

    Public Shared Custom Event ToggleNavigationBar As EventHandler(Of ToggleNavigationBarEventArgs)
        AddHandler(value As EventHandler(Of ToggleNavigationBarEventArgs))
            AddHandler CurrentSessionContext.ToggleNavigationBar, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of ToggleNavigationBarEventArgs))
            RemoveHandler CurrentSessionContext.ToggleNavigationBar, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ToggleNavigationBarEventArgs)
            CurrentSessionContext.OnToggleNavigationBar(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event ToggleTitleBar As EventHandler(Of ToggleNavigationBarEventArgs)
        AddHandler(value As EventHandler(Of ToggleNavigationBarEventArgs))
            AddHandler CurrentSessionContext.ToggleTitleBar, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of ToggleNavigationBarEventArgs))
            RemoveHandler CurrentSessionContext.ToggleTitleBar, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ToggleNavigationBarEventArgs)
            CurrentSessionContext.OnToggleTitleBar(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event ToggleTitleBarButtonVisible As EventHandler(Of TitleBarButtonStateEventArgs)
        AddHandler(value As EventHandler(Of TitleBarButtonStateEventArgs))
            AddHandler CurrentSessionContext.ToggleTitleBarButtonVisible, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of TitleBarButtonStateEventArgs))
            RemoveHandler CurrentSessionContext.ToggleTitleBarButtonVisible, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As TitleBarButtonStateEventArgs)
            CurrentSessionContext.OnToggleTitleBarButtonVisible(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event ToggleTitleBarButtonEnabled As EventHandler(Of TitleBarButtonStateEventArgs)
        AddHandler(value As EventHandler(Of TitleBarButtonStateEventArgs))
            AddHandler CurrentSessionContext.ToggleTitleBarButtonEnabled, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of TitleBarButtonStateEventArgs))
            RemoveHandler CurrentSessionContext.ToggleTitleBarButtonEnabled, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As TitleBarButtonStateEventArgs)
            CurrentSessionContext.OnToggleTitleBarButtonEnabled(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event GetTitleBarButtonIsSupported As EventHandler(Of TitleBarButtonStateEventArgs)
        AddHandler(value As EventHandler(Of TitleBarButtonStateEventArgs))
            AddHandler CurrentSessionContext.GetTitleBarButtonIsSupported, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of TitleBarButtonStateEventArgs))
            RemoveHandler CurrentSessionContext.GetTitleBarButtonIsSupported, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As TitleBarButtonStateEventArgs)
            CurrentSessionContext.OnGetTitleBarButtonIsSupported(sender, e.ButtonName)
        End RaiseEvent
    End Event

    Public Shared Custom Event HandInTest As EventHandler(Of HandInTestEventArgs)
        AddHandler(value As EventHandler(Of HandInTestEventArgs))
            AddHandler CurrentSessionContext.HandInTest, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of HandInTestEventArgs))
            RemoveHandler CurrentSessionContext.HandInTest, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As HandInTestEventArgs)
            CurrentSessionContext.OnHandInTest(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event SuspendTest As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.SuspendTest, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.SuspendTest, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnSuspendTest(sender)
        End RaiseEvent
    End Event

    Public Shared Custom Event ReportValue As EventHandler(Of ReportValueEventArgs)
        AddHandler(value As EventHandler(Of ReportValueEventArgs))
            AddHandler CurrentSessionContext.ReportValue, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of ReportValueEventArgs))
            RemoveHandler CurrentSessionContext.ReportValue, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ReportValueEventArgs)
            CurrentSessionContext.OnReportValue(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event CurrentItemChanged As EventHandler
        AddHandler(value As EventHandler)
            AddHandler CurrentSessionContext.CurrentItemChanged, value
        End AddHandler

        RemoveHandler(value As EventHandler)
            RemoveHandler CurrentSessionContext.CurrentItemChanged, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As EventArgs)
            CurrentSessionContext.OnCurrentItemChanged(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event UIButtonClickedCommand As EventHandler(Of UIButtonClickedEventArgs)
        AddHandler(value As EventHandler(Of UIButtonClickedEventArgs))
            AddHandler CurrentSessionContext.UIButtonClickedCommand, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of UIButtonClickedEventArgs))
            RemoveHandler CurrentSessionContext.UIButtonClickedCommand, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As UIButtonClickedEventArgs)
            CurrentSessionContext.SendUIButtonClickedCommand(e.UICommand, e.CommandText)
        End RaiseEvent
    End Event

    Public Shared Custom Event UpdateResponse As EventHandler(Of ResponseEvenArgs)
        AddHandler(value As EventHandler(Of ResponseEvenArgs))
            AddHandler CurrentSessionContext.UpdateResponse, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of ResponseEvenArgs))
            RemoveHandler CurrentSessionContext.UpdateResponse, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ResponseEvenArgs)
            CurrentSessionContext.OnUpdateResponse(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event SetItemResponse As EventHandler(Of ResponseEvenArgs)
        AddHandler(value As EventHandler(Of ResponseEvenArgs))
            AddHandler CurrentSessionContext.SetItemResponse, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of ResponseEvenArgs))
            RemoveHandler CurrentSessionContext.SetItemResponse, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ResponseEvenArgs)
            CurrentSessionContext.OnSetItemResponse(sender, e)
        End RaiseEvent
    End Event

    Public Shared Custom Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
        AddHandler(value As EventHandler(Of ResourceNeededEventArgs))
            AddHandler CurrentSessionContext.ResourceNeeded, value
        End AddHandler

        RemoveHandler(value As EventHandler(Of ResourceNeededEventArgs))
            RemoveHandler CurrentSessionContext.ResourceNeeded, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ResourceNeededEventArgs)
            CurrentSessionContext.OnResourceNeeded(e)
        End RaiseEvent
    End Event

End Class