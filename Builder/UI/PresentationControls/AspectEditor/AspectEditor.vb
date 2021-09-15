
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Linq
Imports System.Text
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Logic.ContentModel.Scoring

Public Class AspectEditor

    Private _aspect As Aspect
    Private _bankId As Integer
    Private _aspectResourceEntity As AspectResourceEntity
    Private _contextIdentifier As Nullable(Of Integer)
    Private _resourceManager As ResourceManagerBase
    Private _behaviour As AspectEditorBehavior

    Public Event MaxScoreChanged As EventHandler(Of EventArgs(Of Integer))

    ''' <summary>
    ''' Initializes the specified aspect.
    ''' </summary>
    ''' <param name="aspect">The aspect.</param>
    ''' <param name="bankId">The bank.</param>
    Public Sub Initialize(ByVal aspect As Aspect, ByVal bankId As Integer, ByVal resourceManager As ResourceManagerBase, ByVal aspectResourceEntity As AspectResourceEntity)
        _aspect = aspect
        _bankId = bankId
        _resourceManager = resourceManager
        _aspectResourceEntity = aspectResourceEntity
        _behaviour = New AspectEditorBehavior(_aspectResourceEntity, _resourceManager, ContextIdentifier, _aspect)

        AspectBindingSource.DataSource = _aspect

        InitEditor()
    End Sub

    ''' <summary>
    ''' Gets or sets the context identifier.
    ''' </summary>
    ''' <value>The context identifier.</value>
    Public Property ContextIdentifier() As Nullable(Of Integer)
        Get
            Return _contextIdentifier
        End Get
        Set(ByVal value As Nullable(Of Integer))
            _contextIdentifier = value
        End Set
    End Property

    ''' <summary>
    ''' Updates the description.
    ''' </summary>
    Public Sub UpdateDescription()
        DescriptionEditor.HtmlEditor.StopEditor()
    End Sub

    ''' <summary>
    ''' Removes the unused dependent resources.
    ''' </summary>
    Public Sub RemoveUnusedDependentResources()
        Dim depencies As New List(Of DependentResourceEntity)

        For Each resource As DependentResourceEntity In _aspectResourceEntity.DependentResourceCollection
            If Not Me.IsStyleSheetUsedInAspect(resource.DependentResource) AndAlso Not Me.IsResourceUsedInAspect(resource.DependentResource) Then
                depencies.Add(resource)
            End If
        Next

        For Each entity As DependentResourceEntity In depencies
            _aspectResourceEntity.DependentResourceCollection.Remove(entity)
        Next
    End Sub

    Private Function IsStyleSheetUsedInAspect(resource As ResourceEntity) As Boolean
        If resource Is Nothing Then Return False
        Return resource.GetType() Is GetType(GenericResourceEntity) AndAlso String.Compare((CType(resource, GenericResourceEntity)).MediaType, "text/css", False) = 0 AndAlso CType(Me.AspectBindingSource.Current, Aspect).Stylesheet.Split(";"c).Contains(resource.Name)
    End Function

    Private Function IsResourceUsedInAspect(resource As ResourceEntity) As Boolean
        If resource Is Nothing Then Return False
        Dim html As String = _behaviour.GetHtml()
        Return HtmlResourceExtractor.GetAllResourcesInHtml(_behaviour.InlineElements.Values.Select(Function(v) v.Item1).ToList, html).Contains(resource.Name)
    End Function

    Private Sub DescriptionEditor_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DescriptionEditor.Leave
        ValidateChildren()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the selectStylesheetButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub selectStylesheetButton_Click(sender As System.Object, e As System.EventArgs) Handles selectStylesheetButton.Click
        Using dialog As New SelectMediaResourceDialog(_bankId)
            dialog.MediaGridControl.MultiSelect = True
            dialog.Filter = "text/css"
            dialog.CanPickFiles = False

            If dialog.ShowDialog() = DialogResult.OK Then
                If dialog.SelectedEntities.Count >= 1 Then
                    Dim formattedEntitynames As String = FormatEntityNames(dialog.SelectedEntities)

                    If stylesheetTextBox.Text <> formattedEntitynames Then
                        stylesheetTextBox.Text = formattedEntitynames

                        For Each entity In dialog.SelectedEntities
                            If Not dialog.EntitiesProhibitedToSelect.Contains(entity.ResourceId) Then
                                CreateDependentResources(entity)
                            End If
                        Next
                    End If
                End If

                If dialog.SelectedEntities.Count = 1 Then
                    Me.AspectBindingSource.DataSource = _aspect
                    InitEditor()
                End If
            End If
        End Using
    End Sub

    Private Sub CreateDependentResources(ByVal entity As GenericResourceDto)
        Dim matches As List(Of Integer) = _aspectResourceEntity.DependentResourceCollection.FindMatches(DependentResourceFields.DependentResourceId = entity.ResourceId)

        If matches.Count = 0 Then
            Dim dependentResourceEntity As DependentResourceEntity = _aspectResourceEntity.DependentResourceCollection.AddNew()

            With dependentResourceEntity
                .Resource = _aspectResourceEntity
                .DependentResourceId = entity.ResourceId
            End With

            _aspectResourceEntity.DependentResourceCollection.Add(dependentResourceEntity)
        End If
    End Sub

    Private Function FormatEntityNames(ByVal entities As IEnumerable(Of GenericResourceDto)) As String
        Dim builder As New StringBuilder()

        For Each entity In entities
            builder.Append(entity.Name)
            builder.Append(";")
        Next

        Return builder.ToString().TrimEnd(";"c)
    End Function

    Private Sub InitEditor()
        _behaviour = New AspectEditorBehavior(_aspectResourceEntity, _resourceManager, ContextIdentifier, _aspect)
        DescriptionEditor.Initialize(_behaviour)
    End Sub

    Private Sub TextBoxScore_TextChanged(sender As Object, e As EventArgs) Handles TextBoxScore.TextChanged
        Dim newMaxScore As Integer = 0
        Integer.TryParse(TextBoxScore.Text, newMaxScore)
        OnMaxScoreChanged(New EventArgs(Of Integer)(newMaxScore))
    End Sub

    Protected Overridable Sub OnMaxScoreChanged(ByVal e As EventArgs(Of Integer))
        RaiseEvent MaxScoreChanged(Me, e)
    End Sub
End Class
