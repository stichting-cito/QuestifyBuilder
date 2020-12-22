
Public Class BackgroundWorkerTask

    Public Sub New(ByVal task As TaskType, ByVal parameter As Object)
        MyClass.New(task)
        _inputParameter = parameter
    End Sub

    Public Sub New(ByVal task As TaskType)
        _taskType = task
    End Sub


    Private ReadOnly _taskType As TaskType
    Private ReadOnly _inputParameter As Object
    Private _result As Object


    Public ReadOnly Property WorkerTask() As TaskType
        Get
            Return _taskType
        End Get
    End Property

    Public ReadOnly Property InputParameter() As Object
        Get
            Return _inputParameter
        End Get
    End Property

    Public Property Result() As Object
        Get
            Return _result
        End Get
        Set(ByVal value As Object)
            _result = value
        End Set
    End Property


End Class

