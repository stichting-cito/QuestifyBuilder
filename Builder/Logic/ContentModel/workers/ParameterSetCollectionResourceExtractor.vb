Imports Cito.Tester.ContentModel

Namespace ContentModel
    Friend Class ParameterSetCollectionResourceExtractor

        Private ReadOnly _arg As ParameterSetCollection

        Public Sub New(arg As ParameterSetCollection)
            _arg = arg
        End Sub

        Public Function ExtractResources() As HashSet(Of String)
            Dim res As New HashSet(Of String)
            For Each c As ParameterCollection In _arg
                Dim parameterColl As New ParameterCollectionResourceExtractor(c)
                res.UnionWith(parameterColl.ExtractResources())
            Next
            Return res
        End Function

    End Class
End Namespace