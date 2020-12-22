Imports System.Text
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel.ParamValidator
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior

Public Class XhtmlGenericResourceViewer

    Private ReadOnly _isEditable As Boolean
    Private ReadOnly _contextIdentifier As Nullable(Of Integer)
    Private ReadOnly _resourceEntity As GenericResourceEntity
    Private _initialValueHash As Integer
    Private ReadOnly _htmlContentValidator As New HtmlContentValidator


    Public Sub New(ByVal editable As Boolean, ByVal contextIdentifier As Nullable(Of Integer),
               ByVal resourceManager As ResourceManagerBase,
               ByVal resourceEntity As GenericResourceEntity)

        _isEditable = editable
        _contextIdentifier = contextIdentifier
        _resourceEntity = resourceEntity
        InitializeComponent()

        MyBase.ResourceManager = resourceManager

        ReparentHtmlEditor1.isReadOnly = Not editable
        ReparentHtmlEditor1.XHtmlViewer1.ScriptErrorsSuppressed = True
    End Sub

    Public Overrides ReadOnly Property HasChangesToPropagate As Boolean
        Get
            Dim data As String = Encoding.UTF8.GetString(Me.DataSource.ResourceData.BinData())
            Return _InitialValueHash <> data.GetHashCode()
        End Get
    End Property

    Protected Overrides Sub DataBind()
        Dim data As String = Encoding.UTF8.GetString(Me.DataSource.ResourceData.BinData())
        _InitialValueHash = data.GetHashCode()

        ReparentHtmlEditor1.isReadOnly = Not Me._isEditable

        ReparentHtmlEditor1.Initialize(New GenericResourceEditorBehaviour(_resourceEntity, ResourceManager, _contextIdentifier, False))
        GenericResourceEntityBindingSource.DataSource = Me.DataSource
    End Sub

    Private Sub handle_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If Me._isEditable Then
            Dim data As String = Encoding.UTF8.GetString(Me.DataSource.ResourceData.BinData())
            If Not _htmlContentValidator.HtmlContainsValue(data) Then
                MessageBox.Show(String.Format(My.Resources.MandatoryParameterMessage, String.Empty))
                e.Cancel = True
            Else
                _InitialValueHash = data.GetHashCode()
            End If
        End If
    End Sub

End Class
