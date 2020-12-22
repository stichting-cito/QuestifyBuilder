Imports Cito.Tester.Common

Public Class ResourceManifestMetadataDefinitionCollection
    Inherits List(Of ResourceManifestMetadataDefinitionBase)


    Public Overloads Property Item(name As String, metadatatype As ResourceManifestMetadataDefinitionBase.enumMetaDataType) As ResourceManifestMetadataDefinitionBase
        Get
            For i As Integer = 0 To Me.Count - 1
                If Me.Item(i).Name.Equals(name, StringComparison.OrdinalIgnoreCase) AndAlso Me.Item(i).MetaDataType = metadatatype Then
                    Return Me.Item(i)
                End If
            Next

            Return Nothing
        End Get
        Set(value As ResourceManifestMetadataDefinitionBase)
            Me.Item(name, metadatatype) = value
        End Set
    End Property

    Public Sub AddSharedMetaDataCollection(customBankPropertyMetaDataCollection As MetaDataCollection)

        If customBankPropertyMetaDataCollection Is Nothing Then
            Exit Sub
        End If

        For Each md As MetaData In customBankPropertyMetaDataCollection
            Select Case md.MetaDatatype
                Case MetaData.enumMetaDataType.BankCustomProperty
                    Select Case md.GetType.ToString
                        Case GetType(MetaDataMultiValue).ToString
                            Dim multiValue As MetaDataMultiValue = CType(md, MetaDataMultiValue)
                            Dim multiValueDefinition As New ResourceManifestMetadataMultiValueDefinition

                            With multiValueDefinition
                                .Name = multiValue.Name
                                .Title = multiValue.Title
                                .Code = multiValue.Code
                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty
                                .ApplicableTo = md.ApplicableTo
                                .Publishable = md.Publishable
                                .Scorable = md.Scorable

                                Select Case multiValue.MetaDataSubType
                                    Case MetaDataMultiValue.enumMetaDataSubType.MultiSelect
                                        .MultiSelect = True
                                    Case MetaDataMultiValue.enumMetaDataSubType.SingleSelect
                                        .MultiSelect = False
                                End Select
                            End With

                            For Each mditem As MetaData In multiValue.ListValues
                                Debug.Assert(TypeOf mditem Is MetaDataCode, "This should have been a MetaDataCode")
                                Dim metacode As MetaDataCode = DirectCast(mditem, MetaDataCode)

                                multiValueDefinition.ListValues.Add(New ResourceManifestMetadataListValue(metacode.Name, metacode.Title, "", metacode.Code))
                            Next

                            If Me.Item(multiValueDefinition.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty) Is Nothing Then
                                Me.Add(multiValueDefinition)
                            End If

                        Case GetType(MetaDataConceptStructure).ToString()
                            Dim conceptStructureMetaData As MetaDataConceptStructure = DirectCast(md, MetaDataConceptStructure)
                            Dim conceptStructureDefinition As New ResourceManifestMetaDataConceptStructureDefinition

                            With conceptStructureDefinition
                                .Name = conceptStructureMetaData.Name
                                .Title = conceptStructureMetaData.Title
                                .Code = conceptStructureMetaData.Code
                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty
                                .ApplicableTo = conceptStructureMetaData.ApplicableTo
                                .Publishable = conceptStructureMetaData.Publishable
                                .Scorable = conceptStructureMetaData.Scorable
                            End With

                            For Each csPartMetaData As MetaDataConceptStructurePart In conceptStructureMetaData.StructureParts
                                Dim conceptStructurePartDefinition As New ResourceManifestMetaDataConceptStructurePartDefinition() With
                                    {.Name = csPartMetaData.Name, .Title = csPartMetaData.Title, .Code = csPartMetaData.Code, .ConceptTypeId = csPartMetaData.ConceptTypeId}

                                For Each csChildPartMetaData As MetaDataConceptStructurePart In csPartMetaData.StructureParts
                                    Dim childConceptStructurePartDefinition As New ResourceManifestMetaDataConceptStructurePartDefinition() With
                                        {.Name = csChildPartMetaData.Name, .Code = csChildPartMetaData.Code, .VisualOrder = csChildPartMetaData.VisualOrder}

                                    conceptStructurePartDefinition.ChildConceptStructureParts.Add(childConceptStructurePartDefinition)
                                Next

                                conceptStructureDefinition.ConceptStructurePartDefinitions.Add(conceptStructurePartDefinition)
                            Next

                            If Me.Item(conceptStructureDefinition.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty) Is Nothing Then
                                Me.Add(conceptStructureDefinition)
                            End If

                        Case GetType(MetaDataTreeStructure).ToString()
                            Dim treeStructureMetaData As MetaDataTreeStructure = DirectCast(md, MetaDataTreeStructure)
                            Dim treeStructureDefinition As New ResourceManifestMetaDataTreeStructureDefinition

                            With treeStructureDefinition
                                .Name = treeStructureMetaData.Name
                                .Title = treeStructureMetaData.Title
                                .Code = treeStructureMetaData.Code
                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty
                                .ApplicableTo = treeStructureMetaData.ApplicableTo
                                .Publishable = treeStructureMetaData.Publishable
                                .Scorable = treeStructureMetaData.Scorable
                            End With

                            For Each csPartMetaData As MetaDataTreeStructurePart In treeStructureMetaData.StructureParts
                                Dim treeStructurePartDefinition As New ResourceManifestMetaDataTreeStructurePartDefinition() With
                                    {.Name = csPartMetaData.Name, .Title = csPartMetaData.Title, .Code = csPartMetaData.Code}

                                For Each csChildPartMetaData As MetaDataTreeStructurePart In csPartMetaData.StructureParts
                                    Dim childTreeStructurePartDefinition As New ResourceManifestMetaDataTreeStructurePartDefinition() With
                                        {.Name = csChildPartMetaData.Name, .Code = csChildPartMetaData.Code, .VisualOrder = csChildPartMetaData.VisualOrder}

                                    treeStructurePartDefinition.ChildTreeStructureParts.Add(childTreeStructurePartDefinition)
                                Next

                                treeStructureDefinition.TreeStructurePartDefinitions.Add(treeStructurePartDefinition)
                            Next

                            If Me.Item(treeStructureDefinition.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty) Is Nothing Then
                                Me.Add(treeStructureDefinition)
                            End If

                        Case GetType(MetaDataRichText).ToString, GetType(MetaData).ToString
                            Dim singleValue As MetaData = md
                            Dim singleValueDefinition As New ResourceManifestMetadataSingleValueDefinition

                            With singleValueDefinition
                                .Name = singleValue.Name
                                .Title = singleValue.Title
                                .MetaDataType = ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty
                                .ApplicableTo = md.ApplicableTo
                                .Publishable = md.Publishable
                                .Scorable = md.Scorable
                                .RichText = (TypeOf md Is MetaDataRichText)
                            End With

                            If Me.Item(singleValueDefinition.Name, ResourceManifestMetadataDefinitionBase.enumMetaDataType.BankCustomProperty) Is Nothing Then
                                Me.Add(singleValueDefinition)
                            End If
                        Case Else
                            Throw New NotImplementedException($"metadata of type {md.GetType.ToString} not implemented")
                    End Select
                Case Else
                    Throw New Exception(String.Empty)
            End Select
        Next
    End Sub

End Class
