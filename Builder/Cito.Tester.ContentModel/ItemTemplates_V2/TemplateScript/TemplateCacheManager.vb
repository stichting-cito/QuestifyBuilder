Imports System.Reflection


Public NotInheritable Class TemplateCacheManager


    Private Shared _cachedTemplates As New Dictionary(Of String, TemplateCacheItem)




    Private Sub New()
    End Sub



    Public Shared CacheLock As Object = New Object()


    Public Shared ReadOnly Property CachedTemplates As Dictionary(Of String, TemplateCacheItem)
        Get
            Return _cachedTemplates
        End Get
    End Property




    Public Shared Sub CacheTemplate(templateCacheKey As String, templateAssembly As Assembly)
        Dim Template As New TemplateCacheItem
        With Template
            .CacheKey = templateCacheKey
            .CacheDateTime = DateTime.Now
            .TemplateAssembly = templateAssembly
        End With
        CachedTemplates.Add(templateCacheKey, Template)
    End Sub


    Public Shared Sub ClearCache()
        _cachedTemplates.Clear()
    End Sub



    Public Shared Function GetCachedTemplate(templateCacheKey As String) As Assembly
        Dim Template As TemplateCacheItem = Nothing

        If CachedTemplates.TryGetValue(templateCacheKey, Template) Then
            Return Template.TemplateAssembly
        Else
            Return Nothing
        End If
    End Function


End Class