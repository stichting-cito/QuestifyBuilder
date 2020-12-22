Imports System.Collections.Concurrent
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Interfaces

Public MustInherit Class HarmonizerDecoratorBase
    Implements IHarmonizer

    Private ReadOnly _decoree As IHarmonizer

    Public Sub New(harmonizer As IHarmonizer)
        _decoree = harmonizer
    End Sub


    Public Overridable Function Harmonize(item As ItemResourceEntity) As Boolean Implements IHarmonizer.Harmonize
        Return _decoree.Harmonize(item)
    End Function

    Public Overridable Function Harmonize(parametersetCollections As ConcurrentDictionary(Of String, ParameterSetCollection), item As ItemResourceEntity) As Boolean Implements IHarmonizer.Harmonize
        Return _decoree.Harmonize(parametersetCollections, item)
    End Function

    Public Overridable Function Harmonize(templates As IEnumerable(Of String), item As ItemResourceEntity) As Boolean Implements IHarmonizer.Harmonize
        Return _decoree.Harmonize(templates, item)
    End Function

    Public Function Harmonize(item As ItemResourceEntity, template As String) As Boolean Implements IHarmonizer.Harmonize
        Return _decoree.Harmonize(item, template)
    End Function

End Class
