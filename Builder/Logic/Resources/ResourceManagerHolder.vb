Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel

Public Class ResourceManagerHolder
    Implements IDisposable
    Private ReadOnly _bankId As Integer
    Private ReadOnly _resourceManager As DataBaseResourceManager
    Private _disposed As Boolean


    Public Sub New(bankId As Integer)
        _bankId = bankId
        _resourceManager = New DataBaseResourceManager(_bankId)
    End Sub


    Public ReadOnly Property ResourceNeeded() As EventHandler(Of ResourceNeededEventArgs)
        Get
            Return AddressOf ResourcesNeeded
        End Get
    End Property

    Public ReadOnly Property Bankid As Integer
        Get
            Return _bankId
        End Get
    End Property


    Private Sub ResourcesNeeded(sender As Object, e As ResourceNeededEventArgs)
        If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
            Dim resource As BinaryResource
            Dim request = new ResourceRequestDTO()
            If e.TypedResourceType IsNot Nothing Then
                resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
            Else
                resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
            End If
            e.BinaryResource = resource
        Else
            e.BinaryResource = New BinaryResource(New Object())
        End If
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If _disposed Then
            Return
        End If

        If disposing Then
            If (_resourceManager IsNot Nothing) Then
                _resourceManager.Dispose()
            End If
        End If
        _disposed = True
    End Sub

    Public Sub IDisposable_Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
