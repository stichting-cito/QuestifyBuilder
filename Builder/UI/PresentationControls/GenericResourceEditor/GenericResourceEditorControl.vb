Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Public Class GenericResourceEditorControl

    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Private _viewer As GenericResourceViewerBase
    Private _genericResource As GenericResourceEntity

    Public Sub New()

        InitializeComponent()


    End Sub

    Public ReadOnly Property Viewer As GenericResourceViewerBase
        Get
            Return _viewer
        End Get
    End Property

    Public Property ResourceManager As ResourceManagerBase

    Public Property DataSource() As GenericResourceEntity
        Get
            Return _genericResource
        End Get
        Set(ByVal value As GenericResourceEntity)
            _genericResource = value
            Me.DataBind()
        End Set
    End Property

    <System.ComponentModel.DefaultValue(False)> _
    Public Property IsEditable As Boolean

    Public Property BankContextIdentifier As Nullable(Of Integer)

    Public Sub PreSave()
        _viewer.PreSave()
    End Sub


    Private Sub DataBind()
        For Each c As Control In Controls
            c.Dispose()
        Next

        Me.Controls.Clear()

        If _genericResource IsNot Nothing AndAlso Not String.IsNullOrEmpty(_genericResource.MediaType) Then
            If _genericResource.MediaType.ToLower().Contains("image") Then
                If _genericResource.MediaType = "image/svg+xml" Then
                    _viewer = New SvgImageViewer()
                Else
                    _viewer = New ImageGenericResourceViewer()
                End If
            ElseIf _genericResource.MediaType = "video/mpeg" Then
                _viewer = New MpegVideoViewer()
            ElseIf _genericResource.MediaType = "text/plain" OrElse
                    _genericResource.MediaType.ToLower = "application/xhtml+xml" OrElse
                    _genericResource.MediaType.ToLower = "text/html" Then
                _viewer = New XhtmlGenericResourceViewer(Me.IsEditable, Me.BankContextIdentifier, ResourceManager, _genericResource)
            ElseIf _genericResource.MediaType.ToLowerInvariant = "text/css" Then
                _viewer = New SourceGenericResourceEditor()
            ElseIf _genericResource.MediaType.ToLowerInvariant = "application/x-zip-compressed" OrElse _
               _genericResource.MediaType.ToLowerInvariant = "application/vnd.openxmlformats-officedocument.wordprocessingml.document" OrElse _
             _genericResource.MediaType.ToLowerInvariant = "application/vnd.openxmlformats-officedocument.wordprocessingml.template" Then
                _viewer = New WordGenericResourceViewer
            Else
                _viewer = New NoPreviewResourceViewer()
            End If

            _viewer.Dock = DockStyle.Fill
            If not TypeOf _viewer Is NoPreviewResourceViewer Then
                Dim resourceData As ResourceDataEntity = _genericResource.ResourceData
                If resourceData Is Nothing Then
                    resourceData = ResourceFactory.Instance.GetResourceData(_genericResource)
                    _genericResource.ResourceData = resourceData
                End If
            End If
            _viewer.DataSource = _genericResource

            Me.Controls.Add(_viewer)
        End If
    End Sub

    Private Sub Viewer_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub


End Class
