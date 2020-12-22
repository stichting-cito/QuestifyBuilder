Imports HelperClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

Namespace Faketory.interface

    Public Interface IEntityMaker

        Function GetItemResourceEntities(ByVal ids As IEnumerable(Of String)) As ItemResourceEntityCollection

        Function GetItemResourceEntities2(ByVal ParamArray ids As String()) As EntityCollection

    End Interface
End NameSpace