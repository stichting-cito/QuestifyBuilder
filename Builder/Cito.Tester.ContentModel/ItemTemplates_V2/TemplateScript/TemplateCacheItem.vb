Imports System.Reflection


Public Class TemplateCacheItem


    Private _CacheDateTime As DateTime
    Private _CacheKey As String
    Private _TemplateAssembly As Assembly




    Public Property CacheDateTime As DateTime
        Get
            Return _CacheDateTime
        End Get
        Set
            _CacheDateTime = value
        End Set
    End Property


    Public Property CacheKey As String
        Get
            Return _CacheKey
        End Get
        Set
            _CacheKey = value
        End Set
    End Property


    Public Property TemplateAssembly As Assembly
        Get
            Return _TemplateAssembly
        End Get
        Set
            _TemplateAssembly = value
        End Set
    End Property


End Class