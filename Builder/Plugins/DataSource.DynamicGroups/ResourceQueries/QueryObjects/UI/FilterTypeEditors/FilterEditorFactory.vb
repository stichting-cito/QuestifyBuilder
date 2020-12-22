Imports Questify.Builder.Logic.ResourceManager

Public NotInheritable Class FilterEditorFactory


    Private Shared _editorMap As Dictionary(Of Type, Type)



    Shared Sub New()
        _editorMap = New Dictionary(Of Type, Type)

        With _editorMap
            .Add(GetType(NOOPFilterPredicate), GetType(EmptyFilterEditor))
            .Add(GetType(NotFilterPredicate), GetType(MonadicFilterEditor))
            .Add(GetType(AndFilterPredicate), GetType(DyadicFilterEditor))
            .Add(GetType(OrFilterPredicate), GetType(DyadicFilterEditor))
            .Add(GetType(ResourcePropertyFilterPredicate), GetType(PropertyFilterEditor))
            .Add(GetType(ItemInTestFilterPredicate), GetType(ItemInTestFilterEditor))
        End With
    End Sub

    Private Sub New()
    End Sub



    Public Shared ReadOnly Property EditorMap() As Dictionary(Of Type, Type)
        Get
            Return _editorMap
        End Get
    End Property



    Public Shared Function CreateEditorFor(filter As FilterPredicate, resourceManager As DataBaseResourceManager) As FilterEditorBase
        Dim filterEditor As FilterEditorBase = Nothing

        If EditorMap.ContainsKey(filter.GetType) Then
            filterEditor = Activator.CreateInstance(EditorMap(filter.GetType))
            filterEditor.ResourceManager = resourceManager
        End If

        Return filterEditor
    End Function


End Class