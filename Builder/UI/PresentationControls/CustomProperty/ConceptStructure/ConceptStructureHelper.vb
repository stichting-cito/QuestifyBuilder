Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories

Friend Class ConceptStructureHelper

    Friend Shared Function CreateLocalizedConceptTypes() As List(Of KeyValuePair(Of ConceptTypeEntity, String))
        Dim localizedConceptTypes As New List(Of KeyValuePair(Of ConceptTypeEntity, String))
        Dim allConceptTypes As EntityCollection = BankFactory.Instance.GetAllConceptTypes()

        For Each conceptType As ConceptTypeEntity In allConceptTypes
            Dim conceptTypeFromResourceFile As String = My.Resources.ResourceManager.GetString(String.Concat("ConceptType_", conceptType.Name))

            If conceptTypeFromResourceFile IsNot Nothing Then
                localizedConceptTypes.Add(New KeyValuePair(Of ConceptTypeEntity, String)(conceptType, conceptTypeFromResourceFile))
            Else
                localizedConceptTypes.Add(New KeyValuePair(Of ConceptTypeEntity, String)(conceptType, conceptType.Name))
            End If
        Next

        localizedConceptTypes.Sort(New Comparison(Of KeyValuePair(Of ConceptTypeEntity, String))(Function(i As KeyValuePair(Of ConceptTypeEntity, String), j As KeyValuePair(Of ConceptTypeEntity, String))
                                                                                                     Return i.Value.CompareTo(j.Value)
                                                                                                 End Function))

        Return localizedConceptTypes
    End Function
End Class
