Imports Questify.Builder.Model.ContentModel.HelperClasses

Namespace Faketory.interface

    Public Interface IDataSourceMaker

        Function GetInclusionGroup(ByVal name As String, ByVal idents As IEnumerable(Of String)) As EntityCollection

        Function GetInclusionGroups(ByVal groupDefs As IEnumerable(Of KeyValuePair(Of String, IEnumerable(Of String)))) As EntityCollection

        Function GetExclusionGroup(ByVal name As String, ByVal idents As IEnumerable(Of String)) As EntityCollection

    End Interface
End NameSpace