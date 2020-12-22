Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Exceptions

Public Class BackGroundWorkerPool
    Inherits Component


    Private ReadOnly _workers As Dictionary(Of String, BackgroundWorker)
    Private _maxWorkers As Integer
    Private _statusbarListener As StatusBarListener



    Public Event DoWork As DoWorkEventHandler
    Public Event ProgressChanged As ProgressChangedEventHandler
    Public Event RunWorkerCompleted As RunWorkerCompletedEventHandler

    Protected Sub OnDoWork(ByVal sender As BackgroundWorker, ByVal e As DoWorkEventArgs)
        RaiseEvent DoWork(sender, e)
    End Sub

    Protected Sub OnProgressChanged(ByVal sender As BackgroundWorker, ByVal e As ProgressChangedEventArgs)
        RaiseEvent ProgressChanged(sender, e)
    End Sub

    Protected Sub OnRunWorkerCompleted(ByVal sender As BackgroundWorker, ByVal e As RunWorkerCompletedEventArgs)
        RaiseEvent RunWorkerCompleted(sender, e)
    End Sub



    <Description("The number of workers that may execute tasks"), Bindable(True), Category("BackGroundWorkerPool Specific"), DefaultValue(2)> _
    Public Property MaxWorkers() As Integer
        Get
            Return _maxWorkers
        End Get
        Set(ByVal value As Integer)
            _maxWorkers = value
        End Set
    End Property

    Public Property StatusbarListener() As StatusBarListener
        Get
            Return _statusbarListener
        End Get
        Set(ByVal value As StatusBarListener)
            _statusbarListener = value
        End Set
    End Property




    Public Sub New()
        _workers = New Dictionary(Of String, BackgroundWorker)
        _maxWorkers = 2

    End Sub



    Public Sub StartNewTask(ByVal task As BackgroundWorkerTask, ByVal key As String)

        If _workers.ContainsKey(key) Then
            Dim worker As BackgroundWorker = _workers(key)
            If worker.IsBusy Then worker.CancelAsync()

            If _workers.Count = _maxWorkers Then
                Dim count As Integer
                Do Until Not worker.IsBusy
                    count += 1
                    Threading.Thread.Sleep(1000)
                    If count = 10 Then
                        Exit Do
                    End If
                Loop
            End If

            worker.Dispose()
            _workers.Remove(key)
        End If

        If _maxWorkers > _workers.Count Then
            Dim newWorker As New BackgroundWorker
            newWorker.WorkerReportsProgress = True
            newWorker.WorkerSupportsCancellation = True
            AddHandler newWorker.DoWork, AddressOf Worker_DoWork
            AddHandler newWorker.ProgressChanged, AddressOf Worker_ProgressChanged
            AddHandler newWorker.RunWorkerCompleted, AddressOf Worker_RunWorkerCompleted

            _workers.Add(key, newWorker)

            newWorker.RunWorkerAsync(task)
        Else
            Throw New UIException("Cannot execute more tasks at this moment, all workers are in use!")
        End If
    End Sub

    Public Sub StopAllTasks()
        For Each item As KeyValuePair(Of String, BackgroundWorker) In _workers
            If item.Value.IsBusy() Then item.Value.CancelAsync()

            item.Value.Dispose()
        Next

        _workers.Clear()
    End Sub



    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        OnDoWork(DirectCast(sender, BackgroundWorker), e)
    End Sub

    Private Sub Worker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        If _statusbarListener IsNot Nothing Then
            _statusbarListener.PublishMessage(Me, String.Format(My.Resources.StatusBar_GettingData, _workers.Count))
        End If

        OnProgressChanged(DirectCast(sender, BackgroundWorker), e)
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        OnRunWorkerCompleted(worker, e)

        If _workers.ContainsValue(worker) Then
            For Each w As KeyValuePair(Of String, BackgroundWorker) In _workers
                If w.Value.Equals(worker) Then
                    _workers.Remove(w.Key)
                    Exit For
                End If
            Next
        End If

        worker.Dispose()

        If _statusbarListener IsNot Nothing Then
            If _workers.Count > 0 Then
                _statusbarListener.PublishMessage(Me, String.Format(My.Resources.StatusBar_GettingData, _workers.Count))
            Else
                _statusbarListener.PublishMessage(Me, My.Resources.StatusBar_Ready)
            End If
        End If
    End Sub


End Class
