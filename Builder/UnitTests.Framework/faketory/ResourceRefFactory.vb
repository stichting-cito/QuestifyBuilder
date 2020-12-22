Imports Cito.Tester.ContentModel.Datasources

Namespace Faketory

    Public Module ResourceRefFactory

        Public Function MakeMultiple(ByVal ParamArray ids() As String) As IEnumerable(Of ResourceRef)
            Return From a In ids Select New ResourceRef(a)
        End Function

        Public Function Make(id As String) As ResourceRef
            Return New ResourceRef(id)
        End Function

    End Module

End NameSpace