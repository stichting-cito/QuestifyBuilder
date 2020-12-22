Imports Cito.Tester.ContentModel

Namespace ContentModel

    Friend Class ParameterCollectionResourceExtractor


        Private ReadOnly _arg As ParameterCollection

        Public Sub New(arg As ParameterCollection)
            _arg = arg
        End Sub

        Public Function ExtractResources() As HashSet(Of String)
            Dim res As New HashSet(Of String)

            For Each p As ParameterBase In _arg.InnerParameters
                If (TypeOf p Is ResourceParameter) Then
                    Dim r = DirectCast(p, ResourceParameter)
                    Dim toAdd As String = r.Value.Trim().Replace("%20", " ")
                    If Not String.IsNullOrEmpty(toAdd) Then res.Add(toAdd)
                End If
            Next

            Return res
        End Function

    End Class
End Namespace
