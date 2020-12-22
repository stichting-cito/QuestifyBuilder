Imports Cito.Tester.ContentModel

Namespace ItemProcessing

    Public Interface IITemSetupCacheHelper

        Function GetCachedExtractedParameters(iltName As String) As ParameterSetCollection

        Function GetCachedIsTransformed(iltName As String) As Boolean?


        Sub ReadyForCaching(iltName As String, extractedParameters As ParameterSetCollection, isTransformed As Boolean)

    End Interface

End Namespace

