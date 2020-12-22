Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.Common

Namespace ItemProcessing

    <CLSCompliant(False)>
    Public Class ParameterSetCollectionHelper
        Implements IDisposable

        Private ReadOnly _resourceManager As ResourceManagerBase
        Private ReadOnly _itemLayoutTemplateName As String
        Private _isTransformed As Boolean?
        Protected _adapter As ItemLayoutAdapter
        Protected _extractedParameters As ParameterSetCollection


        Public Sub New(resourceManager As ResourceManagerBase, itemLayoutTemplateName As String)
            _resourceManager = resourceManager
            _itemLayoutTemplateName = itemLayoutTemplateName
        End Sub

        Public Sub New(adapter As ItemLayoutAdapter, itemLayoutTemplateName As String)
            _adapter = adapter
            _itemLayoutTemplateName = itemLayoutTemplateName
        End Sub

        Public Sub New(itemLayoutTemplateName As String)
            _itemLayoutTemplateName = itemLayoutTemplateName
        End Sub

        Public Property CachingStrategy As IITemSetupCacheHelper

        Public ReadOnly Property IsTransFormedTemplate As Boolean
            Get
                If CachingStrategy IsNot Nothing Then
                    _isTransformed = CachingStrategy.GetCachedIsTransformed(_itemLayoutTemplateName)
                End If
                If Not _isTransformed.HasValue Then
                    CreateAdapterIfNeeded(_adapter)
                    _isTransformed = _adapter.Template.IsTransformed
                End If
                Return _isTransformed.Value
            End Get
        End Property




        Public Function GetInlineMediaTemplates() As Dictionary(Of String, String)

            CreateAdapterIfNeeded(_adapter)
            Return _adapter.GetInlineMediaTemplates()
        End Function


        Public Function GetExtractedParameters() As ParameterSetCollection
            If CachingStrategy IsNot Nothing Then
                _extractedParameters = CachingStrategy.GetCachedExtractedParameters(_itemLayoutTemplateName)
                If (_extractedParameters IsNot Nothing) Then
                    _extractedParameters = _extractedParameters.DeepCloneWithDesignerSettingsAndAttributeReferences()
                    _extractedParameters.OverrideAttributeReferences()
                End If
            End If
            If _extractedParameters Is Nothing Then
                CreateAdapterIfNeeded(_adapter)
                _extractedParameters = _adapter.CreateParameterSetsFromItemTemplate()
                _extractedParameters.ShouldSort = ShouldSort()
                If CachingStrategy IsNot Nothing Then
                    CachingStrategy.ReadyForCaching(_itemLayoutTemplateName, _extractedParameters.DeepCloneWithDesignerSettingsAndAttributeReferences(), IsTransFormedTemplate)
                End If
            End If

            Return _extractedParameters
        End Function



        Private Function ShouldSort() As Boolean
            CreateAdapterIfNeeded(_adapter)
            Dim returnValue As Boolean = False
            If _adapter IsNot Nothing AndAlso
                         _adapter.Template IsNot Nothing AndAlso
                         _adapter.Template.DesignerSettings IsNot Nothing AndAlso
                                                  _adapter.Template.DesignerSettings.GetDesignerSettingByKey("sort") IsNot Nothing Then
                Boolean.TryParse(_adapter.Template.DesignerSettings.GetDesignerSettingByKey("sort").Value, returnValue)
            End If
            Return returnValue
        End Function

        Private Sub GenericHandler_ResourceNeeded(ByVal sender As Object, ByVal e As Cito.Tester.ContentModel.ResourceNeededEventArgs)
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

        Protected Sub CreateAdapterIfNeeded(adapter As ItemLayoutAdapter)
            If adapter Is Nothing Then
                _adapter = New ItemLayoutAdapter(_itemLayoutTemplateName, Nothing, AddressOf GenericHandler_ResourceNeeded)
            End If
        End Sub


        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    _extractedParameters = Nothing
                End If

            End If
            Me.disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class

End Namespace
