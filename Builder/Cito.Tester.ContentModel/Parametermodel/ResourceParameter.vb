Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Public Class ResourceParameter
    Inherits ResizableParameter

    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
    Private _resource As Byte()

    <XmlIgnore>
    Public ReadOnly Property Resource As Byte()
        Get
            Return _resource
        End Get
    End Property

    Protected Sub SetResource(resrc As Byte())
        _resource = resrc
    End Sub

    Public Overridable Sub RefreshResource()
        _resource = Nothing
        If Not String.IsNullOrEmpty(Me.Value) Then
            Dim e As New ResourceNeededEventArgs(Me.Value, AddressOf StreamConverters.ConvertStreamToByteArray)
            OnResourceNeeded(Me, e)
            If e.BinaryResource IsNot Nothing AndAlso e.BinaryResource.ResourceObject IsNot Nothing Then
                _resource = DirectCast(e.BinaryResource.ResourceObject, Byte())
            End If
        End If
    End Sub

    Protected Overridable Sub OnResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(sender, e)
    End Sub

    Overridable Sub PreFetchResource()
    End Sub

End Class
