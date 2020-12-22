Imports System.IO

Public Class StreamResource
    Implements IDisposable


    Private ReadOnly _name As String
    Private ReadOnly _type As String
    Private ReadOnly _cachelocal As Boolean
    Private _length As Long
    Private _resourceStream As Stream
    Private ReadOnly _dependentResources As DependentResourceCollection
    Private ReadOnly _metaData As New MetaDataCollection
    Private ReadOnly _version As Integer = 1
    Private ReadOnly _originalVersion As Integer = 0
    Private ReadOnly _originalName As String = String.Empty
    Private ReadOnly _state As Integer = 0



    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Version() As Integer
        Get
            Return _version
        End Get
    End Property

    Public ReadOnly Property OriginalVersion() As Integer
        Get
            Return _originalVersion
        End Get
    End Property

    Public ReadOnly Property OriginalName() As String
        Get
            Return _originalName
        End Get
    End Property
    Public ReadOnly Property Type() As String
        Get
            Return _type
        End Get
    End Property

    Public ReadOnly Property CacheLocal() As Boolean
        Get
            Return _cachelocal
        End Get
    End Property

    Public Property Length() As Long
        Get
            Return _length
        End Get
        Set(value As Long)
            _length = value
        End Set
    End Property

    Public Property ResourceObject() As Stream
        Get
            Return _resourceStream
        End Get
        Set(value As Stream)
            _resourceStream = value
        End Set
    End Property

    Public ReadOnly Property DependentResources() As DependentResourceCollection
        Get
            Return _dependentResources
        End Get
    End Property

    Public ReadOnly Property MetaData() As MetaDataCollection
        Get
            Return _metaData
        End Get
    End Property

    Public ReadOnly Property State As Integer
        Get
            Return _state
        End Get
    End Property



    Protected Sub New()
        _dependentResources = New DependentResourceCollection
    End Sub

    Public Sub New(obj As Stream)
        Me.New()
        _resourceStream = obj

        If obj IsNot Nothing AndAlso obj.CanSeek Then
            _length = obj.Length
        Else
            _length = 0
        End If
    End Sub

    Public Sub New(name As String, version As Integer, type As String, cacheLocal As Boolean, obj As Stream, dependentResources As IEnumerable(Of DependentResource),
               state As Integer)
        Me.New(obj)
        _name = name
        _type = type
        _cachelocal = cacheLocal
        _version = version
        If dependentResources IsNot Nothing Then
            _dependentResources.AddRange(dependentResources)
        End If
        _state = state
    End Sub

    Public Sub New(name As String, version As Integer, type As String, cacheLocal As Boolean, obj As Stream,
               dependentResources As IEnumerable(Of DependentResource), originalName As String, originalVersion As Integer,
               state As Integer)
        Me.New(obj)
        _name = name
        _type = type
        _cachelocal = cacheLocal
        _version = version
        If dependentResources IsNot Nothing Then
            _dependentResources.AddRange(dependentResources)
        End If
        _originalName = originalName
        _originalVersion = originalVersion
        _state = state
    End Sub

    Public Sub New(name As String, type As String, cacheLocal As Boolean, obj As Stream, dependentResources As IEnumerable(Of DependentResource))
        Me.New(name, 1, type, cacheLocal, obj, dependentResources, 0)
    End Sub
    Public Sub New(stream As Stream, streamLength As Long)
        _resourceStream = stream
        _length = streamLength
    End Sub



    Public Sub CloseStream()
        If _resourceStream IsNot Nothing Then
            _resourceStream.Close()
            _resourceStream.Dispose()
            _resourceStream = Nothing
        End If
    End Sub


    Private _disposedValue As Boolean = False

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                CloseStream()

                _dependentResources.Clear()
                _metaData.Clear()
            End If
        End If
        _disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub


End Class
