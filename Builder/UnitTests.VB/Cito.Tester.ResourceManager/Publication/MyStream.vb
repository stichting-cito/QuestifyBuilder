Imports System.IO

Public Class MyStream
    Inherits Stream

    Public Sub New()

    End Sub


    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides Sub Flush()

    End Sub

    Public Overrides ReadOnly Property Length As Long
        Get
            Return 0
        End Get
    End Property

    Public Overrides Property Position As Long
        Get
            Return 0
        End Get
        Set(value As Long)

        End Set
    End Property

    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Return 0
    End Function

    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
        Return 0
    End Function

    Public Overrides Sub SetLength(value As Long)

    End Sub

    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)

    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        DisposedHasBeenCalled() 'for test purposes
    End Sub

    Public Overridable Sub DisposedHasBeenCalled()

    End Sub


End Class
