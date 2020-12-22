
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.HelperClasses

Namespace FakeAppTemplate

    Class Mapper

        Public Shared Iterator Function GetMany(Of T As {ResourceDto})(ByVal entityCollection As EntityCollection) As IEnumerable(Of T)
            Throw New NotImplementedException
        End Function

    End Class
End NameSpace