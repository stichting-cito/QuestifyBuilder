Imports Cito.Tester.Common

Namespace ResourceManager

    Public Class DatabaseResourceManagerWrapper
        Implements IResourceManagerWrapper

        Private ReadOnly _resourceManager As DataBaseResourceManager
        Private _disposedValue As Boolean

        Public Sub New(ByVal bankId As Integer, Optional retrieveCustomBankProperties As Boolean = False)
            _resourceManager = New DataBaseResourceManager(bankId, retrieveCustomBankProperties)
        End Sub


        Public ReadOnly Property ResourceManager As ResourceManagerBase Implements IResourceManagerWrapper.ResourceManager
            Get
                Return _resourceManager
            End Get
        End Property


        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me._disposedValue Then
                If disposing Then
                    _resourceManager.Dispose()
                End If
            End If
            _disposedValue = True
        End Sub

        Protected Overrides Sub Finalize()
            Dispose(False)
            MyBase.Finalize()
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class
End NameSpace