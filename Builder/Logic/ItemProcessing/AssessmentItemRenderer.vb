Imports Cito.Tester.ContentModel
Imports System.Xml
Imports Cito.Tester.Common

Namespace ItemProcessing

    Public Class AssessmentItemRenderer

        Private ReadOnly _item As AssessmentItem
        Private ReadOnly _resourceManager As ResourceManagerBase

        Private _adapter As ItemLayoutAdapter
        Dim handler As System.EventHandler(Of ResourceNeededEventArgs)


        Public Sub New(item As AssessmentItem, resourceManager As ResourceManagerBase)
            _item = item
            _resourceManager = resourceManager
        End Sub



        Public Function GetAvailableTargets() As IEnumerable(Of String)
            init()
            Return _adapter.Template.GetEnabledTargetNames()
        End Function

        Public Function GetXmlData(targetName As String) As XmlDocument
            Dim ret = _adapter.ParseTemplate(targetName, False)
            Return ret
        End Function


        Private Sub init()
            If (_adapter Is Nothing) Then
                handler = AddressOf actualHandler
                _adapter = New ItemLayoutAdapter(_item.LayoutTemplateSourceName, _item.Parameters, handler)
            End If
        End Sub

        Private Sub actualHandler(o As Object, e As ResourceNeededEventArgs)
            Dim _resource As BinaryResource = Nothing
            Dim request = new ResourceRequestDTO()

            If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then

                If e.TypedResourceType IsNot Nothing Then
                    _resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
                Else
                    _resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
                End If
                e.BinaryResource = _resource
            Else
                e.BinaryResource = New BinaryResource(New Object)
            End If
        End Sub

    End Class

End Namespace
