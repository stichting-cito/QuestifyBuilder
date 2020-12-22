
Imports System.IO
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.Linq
Imports Questify.Builder.Client
Imports Questify.Builder.Logic.Conversion
Imports Questify.Builder.UnitTests.Framework.Faketory
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

<TestClass()>
Public Class ExcelImportHandlerTest

    Private _listOfKnownColumns As List(Of String)
    Private _resourceIdColumnName As String

    Public Property FakeServices As IFakeServices

    <TestInitialize()>
    Public Sub Init()
        _resourceIdColumnName = "id"

        _listOfKnownColumns = New List(Of String)
        _listOfKnownColumns.Add("itm-nr")
        _listOfKnownColumns.Add("code")
        _listOfKnownColumns.Add("itm-nr")
        _listOfKnownColumns.Add("mk-sleutel")
        _listOfKnownColumns.Add("itm-naam")
        _listOfKnownColumns.Add("score-max")
        _listOfKnownColumns.Add("mk-alt")
        _listOfKnownColumns.Add("type")

        FakeServices = FakeFactory.FakeServices
        FakeServices.SetupFakeServices()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeServices.CleanFakeServices()
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub FreeValuecustomPropertyWithNoValueCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.FreeValue, importer, item)
        Dim existingProperties = New EntityCollection : existingProperties.Add(GetFreeValue())

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub FreeValuecustomPropertyWithValueCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.FreeValue, importer, item)
        Dim freeValue = GetFreeValue()
        Dim existingProperties = New EntityCollection : existingProperties.Add(freeValue)
        item.CustomBankPropertyValueCollection.Add(New FreeValueCustomBankPropertyValueEntity With {.Value = "test123", .CustomBankPropertyId = freeValue.CustomBankPropertyId})

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim fvProperty = CType(item.CustomBankPropertyValueCollection(0), FreeValueCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(fvProperty.Value = "testValue1")
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub SingleListValuecustomPropertyWithValuesCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.SingleListValue, importer, item)
        Dim singleValue = GetSingleList()
        Dim existingProperties = New EntityCollection : existingProperties.Add(singleValue)
        Dim listValue As New ListCustomBankPropertyValueEntity With {.CustomBankPropertyId = singleValue.CustomBankPropertyId}
        Dim selectedValue = New ListCustomBankPropertySelectedValueEntity
        Dim value1 = singleValue.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = "Value1").FirstOrDefault
        Dim value2 = singleValue.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = "Value2").FirstOrDefault
        selectedValue.CustomBankPropertyId = singleValue.CustomBankPropertyId
        selectedValue.ResourceId = item.ResourceId
        selectedValue.ListValueBankCustomPropertyId = value2.ListValueBankCustomPropertyId
        listValue.ListCustomBankPropertySelectedValueCollection.Add(selectedValue)
        item.CustomBankPropertyValueCollection.Add(listValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim listProperty = CType(item.CustomBankPropertyValueCollection(0), ListCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(listProperty.ListCustomBankPropertySelectedValueCollection.Count = 1)
        Assert.IsTrue(listProperty.ListCustomBankPropertySelectedValueCollection(0).CustomBankPropertyId = value1.CustomBankPropertyId)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub SingleListValuecustomPropertyMultiValuesWillbeIgnored()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.SingleListValueDoubleValue, importer, item)
        Dim singleValue = GetSingleList()
        Dim existingProperties = New EntityCollection : existingProperties.Add(singleValue)
        Dim value2 = singleValue.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = "Value2").FirstOrDefault

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 0)
    End Sub


    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub MultiListValuecustomPropertyWithValuesCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.MultiListValue, importer, item)
        Dim multiValue = GetMultiList(True)
        Dim existingProperties = New EntityCollection : existingProperties.Add(multiValue)
        Dim listValue As New ListCustomBankPropertyValueEntity With {.CustomBankPropertyId = multiValue.CustomBankPropertyId}
        Dim selectedValue = New ListCustomBankPropertySelectedValueEntity
        Dim value1 = multiValue.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = "Value1").FirstOrDefault
        Dim value2 = multiValue.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = "Value2").FirstOrDefault
        Dim value3 = multiValue.ListValueCustomBankPropertyCollection.Where(Function(v) v.Name = "Value3").FirstOrDefault
        selectedValue.CustomBankPropertyId = multiValue.CustomBankPropertyId
        selectedValue.ResourceId = item.ResourceId
        selectedValue.ListValueBankCustomPropertyId = value3.ListValueBankCustomPropertyId
        listValue.ListCustomBankPropertySelectedValueCollection.Add(selectedValue)
        item.CustomBankPropertyValueCollection.Add(listValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim listProperty = CType(item.CustomBankPropertyValueCollection(0), ListCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(listProperty.ListCustomBankPropertySelectedValueCollection.Count = 2)
        Assert.IsTrue(listProperty.ListCustomBankPropertySelectedValueCollection(0).CustomBankPropertyId = value1.CustomBankPropertyId)
        Assert.IsTrue(listProperty.ListCustomBankPropertySelectedValueCollection(1).CustomBankPropertyId = value2.CustomBankPropertyId)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub ConceptCustomPropertyCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1", .ResourceData = New ResourceDataEntity() With {.BinData = GetBytes(_asssessmentItem.ToString)}}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.Concept, importer, item)
        Dim conceptValue = GetConceptStructure()
        Dim existingProperties = New EntityCollection : existingProperties.Add(conceptValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim conceptProperty = CType(item.CustomBankPropertyValueCollection(0), ConceptStructureCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(conceptProperty.ConceptStructureCustomBankPropertySelectedPartCollection.Count = 1)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub ConceptCustomPropertyCanBeChanged()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1", .ResourceData = New ResourceDataEntity() With {.BinData = GetBytes(_asssessmentItem.ToString)}}
        Dim dummyConceptProperty = New ConceptStructureCustomBankPropertyValueEntity()
        item.CustomBankPropertyValueCollection.Add(dummyConceptProperty)
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.Concept, importer, item)
        Dim conceptValue = GetConceptStructure()
        Dim existingProperties = New EntityCollection : existingProperties.Add(conceptValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim conceptProperty = CType(item.CustomBankPropertyValueCollection(0), ConceptStructureCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(conceptProperty.ConceptStructureCustomBankPropertySelectedPartCollection.Count = 1)
        Assert.AreNotEqual(item.CustomBankPropertyValueCollection(0), dummyConceptProperty)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub ConceptCustomPropertyCanBeRemoved()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1", .ResourceData = New ResourceDataEntity() With {.BinData = GetBytes(_asssessmentItem.ToString)}}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.ConceptEmpty, importer, item)
        Dim conceptValue = GetConceptStructure()
        Dim conceptPropertyToRemove = New ConceptStructureCustomBankPropertyValueEntity()
        conceptPropertyToRemove.CustomBankProperty = conceptValue
        item.CustomBankPropertyValueCollection.Add(conceptPropertyToRemove)
        Dim existingProperties = New EntityCollection : existingProperties.Add(conceptValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 0)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub ConceptCustomPropertyCannotBeRemovedIfCoupled()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1", .ResourceData = New ResourceDataEntity() With {.BinData = GetBytes(_assessmentItemWithConcepts.ToString)}}

        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.ConceptEmpty, importer, item)
        Dim conceptValue = GetConceptStructure()
        Dim conceptPropertyToRemove = New ConceptStructureCustomBankPropertyValueEntity()
        conceptPropertyToRemove.CustomBankProperty = conceptValue
        item.CustomBankPropertyValueCollection.Add(conceptPropertyToRemove)
        Dim existingProperties = New EntityCollection : existingProperties.Add(conceptValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.AreEqual(item.CustomBankPropertyValueCollection(0), conceptPropertyToRemove)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub ConceptCustomPropertyCannotBeChangedIfConceptsAreAdded()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1", .ResourceData = New ResourceDataEntity() With {.BinData = GetBytes(_assessmentItemWithConcepts.ToString)}}
        Dim dummyConceptProperty = New ConceptStructureCustomBankPropertyValueEntity()
        item.CustomBankPropertyValueCollection.Add(dummyConceptProperty)
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.Concept, importer, item)
        Dim conceptValue = GetConceptStructure()
        Dim existingProperties = New EntityCollection : existingProperties.Add(conceptValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.AreEqual(item.CustomBankPropertyValueCollection(0), dummyConceptProperty)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub ConceptCustomPropertyNotapplicableToItemCanNotBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1", .ResourceData = New ResourceDataEntity() With {.BinData = GetBytes(_asssessmentItem.ToString)}}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.ConceptAttribute, importer, item)
        Dim conceptValue = GetConceptStructure()
        Dim existingProperties = New EntityCollection : existingProperties.Add(conceptValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 0)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub TreeStructureCustomPropertyCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.Tree, importer, item)
        Dim treeValue = GetTree()
        Dim existingProperties = New EntityCollection : existingProperties.Add(treeValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim conceptProperty = CType(item.CustomBankPropertyValueCollection(0), TreeStructureCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(conceptProperty.TreeStructureCustomBankPropertySelectedPartCollection.Count = 3)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub TreeStructureCustomPropertyCanBeRemoved()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.TreeEmpty, importer, item)
        Dim treeValue = GetTree()
        Dim treePropertyToRemove = New TreeStructureCustomBankPropertyValueEntity()
        treePropertyToRemove.CustomBankProperty = treeValue
        item.CustomBankPropertyValueCollection.Add(treePropertyToRemove)
        Dim existingProperties = New EntityCollection From {treeValue}

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 0)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub TreeStructureCustomPropertyCanBeRemoved2()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.TreeEmpty, importer, item)
        Dim treeValue = GetTree()
        Dim treePropertyToRemove = New TreeStructureCustomBankPropertyValueEntity()
        treePropertyToRemove.CustomBankProperty = treeValue
        item.CustomBankPropertyValueCollection.Add(treePropertyToRemove)
        Dim freeValueProperty = New FreeValueCustomBankPropertyValueEntity() With {.Value = "someFreeValue"}
        item.CustomBankPropertyValueCollection.Add(freeValueProperty)
        Dim existingProperties = New EntityCollection From {treeValue}

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.AreEqual(item.CustomBankPropertyValueCollection(0), freeValueProperty)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub TreeStructureCustomPropertyMultiCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.TreeMulti, importer, item)
        Dim treeValue = GetTree()
        Dim existingProperties = New EntityCollection : existingProperties.Add(treeValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim conceptProperty = CType(item.CustomBankPropertyValueCollection(0), TreeStructureCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(conceptProperty.TreeStructureCustomBankPropertySelectedPartCollection.Count = 4)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub TreeStructureCustomPropertyMultiWithExistingValuesCanBeImported()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.Tree, importer, item)
        Dim treeValue = GetTree()
        Dim existingProperties = New EntityCollection : existingProperties.Add(treeValue)

        Dim existingTreeValue As New TreeStructureCustomBankPropertyValueEntity With {.CustomBankPropertyId = treeValue.CustomBankPropertyId}

        Dim value1 = treeValue.TreeStructurePartCustomBankPropertyCollection.Where(Function(v) v.Name = "1.1.2").FirstOrDefault
        Dim value2 = treeValue.TreeStructurePartCustomBankPropertyCollection.Where(Function(v) v.Name = "1.1").FirstOrDefault
        Dim value3 = treeValue.TreeStructurePartCustomBankPropertyCollection.Where(Function(v) v.Name = "1").FirstOrDefault
        Dim value4 = treeValue.TreeStructurePartCustomBankPropertyCollection.Where(Function(v) v.Name = "1.1.1").FirstOrDefault
        existingTreeValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(New TreeStructureCustomBankPropertySelectedPartEntity With {.CustomBankPropertyId = treeValue.CustomBankPropertyId,
                                                                                        .ResourceId = item.ResourceId,
                                                                                        .TreeStructurePartId = value1.TreeStructurePartCustomBankPropertyId})

        existingTreeValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(New TreeStructureCustomBankPropertySelectedPartEntity With {.CustomBankPropertyId = treeValue.CustomBankPropertyId,
                                                                                              .ResourceId = item.ResourceId,
                                                                                              .TreeStructurePartId = value2.TreeStructurePartCustomBankPropertyId})

        existingTreeValue.TreeStructureCustomBankPropertySelectedPartCollection.Add(New TreeStructureCustomBankPropertySelectedPartEntity With {.CustomBankPropertyId = treeValue.CustomBankPropertyId,
                                                                                               .ResourceId = item.ResourceId,
                                                                                               .TreeStructurePartId = value3.TreeStructurePartCustomBankPropertyId})

        item.CustomBankPropertyValueCollection.Add(existingTreeValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim conceptProperty = CType(item.CustomBankPropertyValueCollection(0), TreeStructureCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)

        Assert.IsTrue(conceptProperty.TreeStructureCustomBankPropertySelectedPartCollection.Count = 3)
        Assert.IsTrue(conceptProperty.TreeStructureCustomBankPropertySelectedPartCollection.Where(Function(v) v.TreeStructurePartId = value1.TreeStructurePartCustomBankPropertyId).Count = 0)
        Assert.IsTrue(conceptProperty.TreeStructureCustomBankPropertySelectedPartCollection.Where(Function(v) v.TreeStructurePartId = value4.TreeStructurePartCustomBankPropertyId).Count = 1)
    End Sub


    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub TreeStructureCustomPropertyMultiCanBeImportedWithoutDoubleValues()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.TreeMulti, importer, item)
        Dim treeValue = GetTree()
        Dim existingProperties = New EntityCollection : existingProperties.Add(treeValue)

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Dim conceptProperty = CType(item.CustomBankPropertyValueCollection(0), TreeStructureCustomBankPropertyValueEntity)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 1)
        Assert.IsTrue(conceptProperty.TreeStructureCustomBankPropertySelectedPartCollection.Count = 4)
    End Sub

    <TestMethod(), TestCategory("ImportLogic")>
    Public Sub TreeStructureCustomPropertyJustOneTreePerItem()
        Dim importer As New ExcelImportHandler
        Dim item As New ItemResourceEntity With {.ResourceId = New Guid("77b934f6-9cc9-4fdb-91f3-a9015d19606b"), .BankId = 123, .Name = "Item1"}
        item.CustomBankPropertyValueCollection.Add(New TreeStructureCustomBankPropertyValueEntity() With {.ResourceId = Guid.NewGuid, .CustomBankProperty = New CustomBankPropertyEntity With {.Name = "Test"}})
        Dim customPropertyDictionary = GetCustomPropertyDictionary(My.Resources.TreeMulti, importer, item)
        Dim treeValue = GetTree()
        Dim existingProperties = New EntityCollection From {treeValue}

        Dim removedEventRaised As Boolean = False
        AddHandler importer.ImportHandlerCustomBankPropertiesRemoved, Sub()
                                                                          removedEventRaised = True
                                                                      End Sub

        importer.AddPropertiesToResource(customPropertyDictionary, item, existingProperties)

        Assert.IsTrue(removedEventRaised)
        Assert.IsTrue(item.CustomBankPropertyValueCollection.Count = 0)
    End Sub


    Private Function GetCustomPropertyDictionary(resource As Byte(), importer As ExcelImportHandler, item As ResourceEntity) As Dictionary(Of String, String)
        Dim excelReader As New OpenXmlExcelReader
        Dim customBankPropertyValues As New Dictionary(Of String, Dictionary(Of String, String))
        Using excelStream = New MemoryStream(resource)
            Dim excelImportErrors As List(Of String) = excelReader.ReadExcelDocument(excelStream, _listOfKnownColumns, _resourceIdColumnName)
            Dim customBankPropertyDefinition = excelReader.CustomPropertyDefinitions
            customBankPropertyValues = excelReader.CustomPropertyValues
        End Using

        Return customBankPropertyValues(item.ResourceId.ToString())
    End Function

    Private Function GetFreeValue() As FreeValueCustomBankPropertyEntity
        Dim s As New XmlSerializer(GetType(FreeValueCustomBankPropertyEntity))
        Return DirectCast(s.Deserialize(New StringReader(_freeValue.ToString)), FreeValueCustomBankPropertyEntity)
    End Function

    Private Function GetSingleList() As ListCustomBankPropertyEntity
        Dim s As New XmlSerializer(GetType(ListCustomBankPropertyEntity))
        Return DirectCast(s.Deserialize(New StringReader(_singleValueList.ToString)), ListCustomBankPropertyEntity)
    End Function

    Private Function GetMultiList(applicableToItem As Boolean) As ListCustomBankPropertyEntity
        Dim s As New XmlSerializer(GetType(ListCustomBankPropertyEntity))
        Dim applicable = "0"
        If applicableToItem Then applicable = "2"
        Dim multiListstring = String.Format(_multiListValue.ToString, applicable)
        Return DirectCast(s.Deserialize(New StringReader(multiListstring)), ListCustomBankPropertyEntity)
    End Function

    Private Function GetTree() As TreeStructureCustomBankPropertyEntity
        Dim s As New XmlSerializer(GetType(TreeStructureCustomBankPropertyEntity))
        Return DirectCast(s.Deserialize(New StringReader(_tree.ToString)), TreeStructureCustomBankPropertyEntity)
    End Function

    Private Function GetConceptStructure() As ConceptStructureCustomBankPropertyEntity
        Dim s As New XmlSerializer(GetType(ConceptStructureCustomBankPropertyEntity))
        Return DirectCast(s.Deserialize(New StringReader(_concept.ToString)), ConceptStructureCustomBankPropertyEntity)
    End Function

    Private Shared Function GetBytes(str As String) As Byte()
        Dim bytes As Byte() = New Byte(str.Length * 2 - 1) {}
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length)
        Return bytes
    End Function



    Private ReadOnly _freeValue As XElement = <FreeValueCustomBankPropertyEntity>
                                                  <FreeValueCustomBankPropertyEntity EntityType="19" ObjectID="001fb514-fc9b-4564-b823-07da90f53e5a" Format="Compact25">
                                                      <CustomBankPropertyId>c1ba361e-e3f8-4b3c-9fb4-414a7416e3a0</CustomBankPropertyId>
                                                      <BankId>26</BankId>
                                                      <ApplicableToMask>2</ApplicableToMask>
                                                      <Publishable>true</Publishable>
                                                      <Name>Free_Property1</Name>
                                                      <Title>Free_Property1</Title>
                                                      <CreationDate>2014-05-12T13:39:49.15</CreationDate>
                                                      <CreatedBy>1</CreatedBy>
                                                      <ModifiedDate>2014-05-12T13:39:52.28</ModifiedDate>
                                                      <ModifiedBy>1</ModifiedBy>
                                                      <Code>ead4381f-cad9-e311-b65e-005056c00008</Code>
                                                      <Version>0.2</Version>
                                                      <ResourceType>Free Value</ResourceType>
                                                      <ModifiedByFullName/>
                                                      <CreatedByFullName/>
                                                      <BankName/>
                                                      <StateName/>
                                                      <OriginalName/>
                                                      <OriginalVersion/>
                                                      <ResourceData/>
                                                      <CopiedFromString/>
                                                      <RemovedDependentEntities/>
                                                      <ApplicableToString>Items</ApplicableToString>
                                                      <CustomPropertyType>Vrije waarde</CustomPropertyType>
                                                      <_lps fs="ACAAAg==" es="1"/>
                                                  </FreeValueCustomBankPropertyEntity>
                                              </FreeValueCustomBankPropertyEntity>

    Private ReadOnly _singleValueList As XElement = <ListCustomBankPropertyEntity>
                                                        <ListCustomBankPropertyEntity EntityType="22" ObjectID="7b70a936-dc7e-41f4-9dd2-6f111dc7e845" Format="Compact25">
                                                            <CustomBankPropertyId>6c30a142-7fea-48f9-9a14-ce12dc816420</CustomBankPropertyId>
                                                            <BankId>26</BankId>
                                                            <ApplicableToMask>2</ApplicableToMask>
                                                            <Publishable>false</Publishable>
                                                            <Name>SingleListProperty</Name>
                                                            <Title>SingleListProperty</Title>
                                                            <CreationDate>2014-05-12T13:41:52.12</CreationDate>
                                                            <CreatedBy>1</CreatedBy>
                                                            <ModifiedDate>2014-05-12T13:45:28.607</ModifiedDate>
                                                            <ModifiedBy>1</ModifiedBy>
                                                            <Code>1c3d8368-cad9-e311-b65e-005056c00008</Code>
                                                            <Version>0.2</Version>
                                                            <MultipleSelect>false</MultipleSelect>
                                                            <ListValueCustomBankPropertyCollection>
                                                                <ListValueCustomBankPropertyEntity ObjectID="60bff7d8-766c-462d-8019-e1437c4c188a">
                                                                    <ListValueBankCustomPropertyId>21bc61c6-4043-4f62-be5b-1ea102cc3fce</ListValueBankCustomPropertyId>
                                                                    <CustomBankPropertyId>6c30a142-7fea-48f9-9a14-ce12dc816420</CustomBankPropertyId>
                                                                    <Name>Value1</Name>
                                                                    <Title>Value1</Title>
                                                                    <Code>b0fd214c-ac2d-4b50-9cc6-2b0b06f52ecb</Code>
                                                                    <ListCustomBankProperty Ref="7b70a936-dc7e-41f4-9dd2-6f111dc7e845"/>
                                                                    <_lps fs="AAA=" es="1"/>
                                                                </ListValueCustomBankPropertyEntity>
                                                                <ListValueCustomBankPropertyEntity ObjectID="9c9407cd-5315-4f65-a464-14e75794a937">
                                                                    <ListValueBankCustomPropertyId>c5fe6fe6-03d1-47bc-8f6b-273a132f8f5a</ListValueBankCustomPropertyId>
                                                                    <CustomBankPropertyId>6c30a142-7fea-48f9-9a14-ce12dc816420</CustomBankPropertyId>
                                                                    <Name>Value2</Name>
                                                                    <Title>Value2</Title>
                                                                    <Code>c173c78a-80f6-40be-953a-e8e109f4dded</Code>
                                                                    <ListCustomBankProperty Ref="7b70a936-dc7e-41f4-9dd2-6f111dc7e845"/>
                                                                    <_lps fs="AAA=" es="1"/>
                                                                </ListValueCustomBankPropertyEntity>
                                                                <_lps f="7"/>
                                                            </ListValueCustomBankPropertyCollection>
                                                            <ResourceType>List Property</ResourceType>
                                                            <BankName/>
                                                            <CreatedByFullName/>
                                                            <ModifiedByFullName/>
                                                            <StateName/>
                                                            <OriginalName/>
                                                            <OriginalVersion/>
                                                            <ResourceData/>
                                                            <CopiedFromString/>
                                                            <RemovedDependentEntities/>
                                                            <ApplicableToString>Items</ApplicableToString>
                                                            <CustomPropertyType>Lijstwaarde (enkelvoudig)</CustomPropertyType>
                                                            <_lps fs="AIAACAA=" es="1"/>
                                                        </ListCustomBankPropertyEntity>
                                                    </ListCustomBankPropertyEntity>

    Private ReadOnly _multiListValue As XElement = <ListCustomBankPropertyEntity>
                                                       <ListCustomBankPropertyEntity EntityType="22" ObjectID="74f11029-b8d6-405b-83ef-2560f57d52f7" Format="Compact25">
                                                           <CustomBankPropertyId>50459661-4f02-4c5d-92e3-a6c74c8d2205</CustomBankPropertyId>
                                                           <BankId>26</BankId>
                                                           <ApplicableToMask>{0}</ApplicableToMask>
                                                           <Publishable>false</Publishable>
                                                           <Name>MultiListProperty</Name>
                                                           <Title>MultiListProperty</Title>
                                                           <CreationDate>2014-05-12T13:44:56</CreationDate>
                                                           <CreatedBy>1</CreatedBy>
                                                           <ModifiedDate>2014-05-12T13:45:23.027</ModifiedDate>
                                                           <ModifiedBy>1</ModifiedBy>
                                                           <Code>b2151dd6-cad9-e311-b65e-005056c00008</Code>
                                                           <Version>0.2</Version>
                                                           <MultipleSelect>true</MultipleSelect>
                                                           <ListValueCustomBankPropertyCollection>
                                                               <ListValueCustomBankPropertyEntity ObjectID="9546f78b-4891-47a2-9728-db775d57eaeb">
                                                                   <ListValueBankCustomPropertyId>bda8c37e-25d8-4fb6-b10d-13895e49819d</ListValueBankCustomPropertyId>
                                                                   <CustomBankPropertyId>50459661-4f02-4c5d-92e3-a6c74c8d2205</CustomBankPropertyId>
                                                                   <Name>Value2</Name>
                                                                   <Title>Value2</Title>
                                                                   <Code>a1663233-ed59-43f2-a6dd-27c6b86e13d1</Code>
                                                                   <ListCustomBankProperty Ref="74f11029-b8d6-405b-83ef-2560f57d52f7"/>
                                                                   <_lps fs="AAA=" es="1"/>
                                                               </ListValueCustomBankPropertyEntity>
                                                               <ListValueCustomBankPropertyEntity ObjectID="de334a08-0d9f-4c79-bc59-94c4bbb1d40d">
                                                                   <ListValueBankCustomPropertyId>1b0654f7-c494-4418-81f4-4205e56c84fd</ListValueBankCustomPropertyId>
                                                                   <CustomBankPropertyId>50459661-4f02-4c5d-92e3-a6c74c8d2205</CustomBankPropertyId>
                                                                   <Name>Value3</Name>
                                                                   <Title>Value3</Title>
                                                                   <Code>c0964033-2c11-4d4a-a09d-0b8d34eb0738</Code>
                                                                   <ListCustomBankProperty Ref="74f11029-b8d6-405b-83ef-2560f57d52f7"/>
                                                                   <_lps fs="AAA=" es="1"/>
                                                               </ListValueCustomBankPropertyEntity>
                                                               <ListValueCustomBankPropertyEntity ObjectID="6b001c86-ea34-4c39-931a-3c57674f1745">
                                                                   <ListValueBankCustomPropertyId>892590c0-a1d2-4d78-b8d9-6981c94a0083</ListValueBankCustomPropertyId>
                                                                   <CustomBankPropertyId>50459661-4f02-4c5d-92e3-a6c74c8d2205</CustomBankPropertyId>
                                                                   <Name>Value1</Name>
                                                                   <Title>Value1</Title>
                                                                   <Code>3e52641a-d4ff-4725-8f4d-b71c8acbd9a2</Code>
                                                                   <ListCustomBankProperty Ref="74f11029-b8d6-405b-83ef-2560f57d52f7"/>
                                                                   <_lps fs="AAA=" es="1"/>
                                                               </ListValueCustomBankPropertyEntity>
                                                               <_lps f="7"/>
                                                           </ListValueCustomBankPropertyCollection>
                                                           <ResourceType>List Property</ResourceType>
                                                           <BankName/>
                                                           <CreatedByFullName/>
                                                           <ModifiedByFullName/>
                                                           <StateName/>
                                                           <OriginalName/>
                                                           <OriginalVersion/>
                                                           <ResourceData/>
                                                           <CopiedFromString/>
                                                           <RemovedDependentEntities/>
                                                           <ApplicableToString>Items</ApplicableToString>
                                                           <CustomPropertyType>Lijstwaarde (meervoudig)</CustomPropertyType>
                                                           <_lps fs="AIAACAA=" es="1"/>
                                                       </ListCustomBankPropertyEntity>
                                                   </ListCustomBankPropertyEntity>

    Private ReadOnly _tree As XElement = <TreeStructureCustomBankPropertyEntity>
                                             <TreeStructureCustomBankPropertyEntity EntityType="41" ObjectID="9203b00f-f0cb-468c-b70e-4974d0b7c655" Format="Compact25">
                                                 <CustomBankPropertyId>128250b1-0b07-4d51-b4e0-4776024cda47</CustomBankPropertyId>
                                                 <BankId>26</BankId>
                                                 <ApplicableToMask>2</ApplicableToMask>
                                                 <Publishable>false</Publishable>
                                                 <Name>TreeProperty</Name>
                                                 <Title>TreeProperty</Title>
                                                 <CreationDate>2014-05-12T13:48:57.293</CreationDate>
                                                 <CreatedBy>1</CreatedBy>
                                                 <ModifiedDate>2014-05-12T13:48:57.283</ModifiedDate>
                                                 <ModifiedBy>1</ModifiedBy>
                                                 <Code>5edced65-cbd9-e311-b65e-005056c00008</Code>
                                                 <Version>0.1</Version>
                                                 <TreeStructurePartCustomBankPropertyCollection>
                                                     <TreeStructurePartCustomBankPropertyEntity ObjectID="86b39ad8-e6b2-4754-bafb-7afef991e03c">
                                                         <TreeStructurePartCustomBankPropertyId>56ba9cdb-1df9-4aad-97b9-28b0f15dd1a7</TreeStructurePartCustomBankPropertyId>
                                                         <CustomBankPropertyId>128250b1-0b07-4d51-b4e0-4776024cda47</CustomBankPropertyId>
                                                         <Name>1.1.2</Name>
                                                         <Title>1.1.2</Title>
                                                         <Code>cee8ecd7-15b6-4d61-a06d-194d4aa6247b</Code>
                                                         <TreeStructureCustomBankProperty Ref="9203b00f-f0cb-468c-b70e-4974d0b7c655"/>
                                                         <_lps fs="AAA=" es="1"/>
                                                     </TreeStructurePartCustomBankPropertyEntity>
                                                     <TreeStructurePartCustomBankPropertyEntity ObjectID="5a0b6ffa-071d-4671-a07d-a24534314e2e">
                                                         <TreeStructurePartCustomBankPropertyId>7d6bbcad-1551-4263-a7b0-2d841bf5c63b</TreeStructurePartCustomBankPropertyId>
                                                         <CustomBankPropertyId>128250b1-0b07-4d51-b4e0-4776024cda47</CustomBankPropertyId>
                                                         <Name>1</Name>
                                                         <Title>1</Title>
                                                         <Code>f07845a4-1eb3-4788-9a3f-d6be43ad39b4</Code>
                                                         <TreeStructureCustomBankProperty Ref="9203b00f-f0cb-468c-b70e-4974d0b7c655"/>
                                                         <ChildTreeStructurePartCustomBankPropertyCollection>
                                                             <ChildTreeStructurePartCustomBankPropertyEntity ObjectID="ab6bb1d1-cdd2-4ef8-9015-3dc32383e7ff">
                                                                 <Id>2b6e47de-fa92-4116-97bf-9589e11204a4</Id>
                                                                 <ChildTreeStructurePartCustomBankPropertyId>3fa5aab7-624b-424e-a35e-ee50b25df5af</ChildTreeStructurePartCustomBankPropertyId>
                                                                 <TreeStructurePartCustomBankPropertyId>7d6bbcad-1551-4263-a7b0-2d841bf5c63b</TreeStructurePartCustomBankPropertyId>
                                                                 <VisualOrder>0</VisualOrder>
                                                                 <TreeStructurePartCustomBankProperty Ref="5a0b6ffa-071d-4671-a07d-a24534314e2e"/>
                                                                 <_lps fs="AA==" es="1"/>
                                                             </ChildTreeStructurePartCustomBankPropertyEntity>
                                                             <ChildTreeStructurePartCustomBankPropertyEntity ObjectID="abc1483a-1fb2-4b04-98bf-501d431dba91">
                                                                 <Id>9d7010f3-023f-498b-994e-ce1c6f293902</Id>
                                                                 <ChildTreeStructurePartCustomBankPropertyId>cd0914d1-2924-40bc-bef6-f60764c490c0</ChildTreeStructurePartCustomBankPropertyId>
                                                                 <TreeStructurePartCustomBankPropertyId>7d6bbcad-1551-4263-a7b0-2d841bf5c63b</TreeStructurePartCustomBankPropertyId>
                                                                 <VisualOrder>1</VisualOrder>
                                                                 <TreeStructurePartCustomBankProperty Ref="5a0b6ffa-071d-4671-a07d-a24534314e2e"/>
                                                                 <_lps fs="AA==" es="1"/>
                                                             </ChildTreeStructurePartCustomBankPropertyEntity>
                                                             <_lps f="7"/>
                                                         </ChildTreeStructurePartCustomBankPropertyCollection>
                                                         <_lps fs="AAA=" es="1"/>
                                                     </TreeStructurePartCustomBankPropertyEntity>
                                                     <TreeStructurePartCustomBankPropertyEntity ObjectID="961214fe-b55a-4034-a6c4-eee229ece835">
                                                         <TreeStructurePartCustomBankPropertyId>d75b7369-bf6a-45a6-a435-d57b82ad1b57</TreeStructurePartCustomBankPropertyId>
                                                         <CustomBankPropertyId>128250b1-0b07-4d51-b4e0-4776024cda47</CustomBankPropertyId>
                                                         <Name>1.1.1</Name>
                                                         <Title>1.1.1</Title>
                                                         <Code>0cdc99d9-5241-48d9-aa43-908182ef5bef</Code>
                                                         <TreeStructureCustomBankProperty Ref="9203b00f-f0cb-468c-b70e-4974d0b7c655"/>
                                                         <_lps fs="AAA=" es="1"/>
                                                     </TreeStructurePartCustomBankPropertyEntity>
                                                     <TreeStructurePartCustomBankPropertyEntity ObjectID="60e87160-6c99-42fd-b39f-f522a6024ec3">
                                                         <TreeStructurePartCustomBankPropertyId>3fa5aab7-624b-424e-a35e-ee50b25df5af</TreeStructurePartCustomBankPropertyId>
                                                         <CustomBankPropertyId>128250b1-0b07-4d51-b4e0-4776024cda47</CustomBankPropertyId>
                                                         <Name>1.1</Name>
                                                         <Title>1.1</Title>
                                                         <Code>cf01d7fc-84f4-471a-9ffb-0faf4b898449</Code>
                                                         <TreeStructureCustomBankProperty Ref="9203b00f-f0cb-468c-b70e-4974d0b7c655"/>
                                                         <ChildTreeStructurePartCustomBankPropertyCollection>
                                                             <ChildTreeStructurePartCustomBankPropertyEntity ObjectID="4f3e195f-5110-459b-9b38-8e74d73285d3">
                                                                 <Id>a3daf7c9-d3d5-4010-aea5-99ab082187dd</Id>
                                                                 <ChildTreeStructurePartCustomBankPropertyId>56ba9cdb-1df9-4aad-97b9-28b0f15dd1a7</ChildTreeStructurePartCustomBankPropertyId>
                                                                 <TreeStructurePartCustomBankPropertyId>3fa5aab7-624b-424e-a35e-ee50b25df5af</TreeStructurePartCustomBankPropertyId>
                                                                 <VisualOrder>1</VisualOrder>
                                                                 <TreeStructurePartCustomBankProperty Ref="60e87160-6c99-42fd-b39f-f522a6024ec3"/>
                                                                 <_lps fs="AA==" es="1"/>
                                                             </ChildTreeStructurePartCustomBankPropertyEntity>
                                                             <ChildTreeStructurePartCustomBankPropertyEntity ObjectID="cd5924b3-4b00-437e-80da-046f69bac872">
                                                                 <Id>9a2b4be3-05d8-4454-a2b4-efb4e740b040</Id>
                                                                 <ChildTreeStructurePartCustomBankPropertyId>d75b7369-bf6a-45a6-a435-d57b82ad1b57</ChildTreeStructurePartCustomBankPropertyId>
                                                                 <TreeStructurePartCustomBankPropertyId>3fa5aab7-624b-424e-a35e-ee50b25df5af</TreeStructurePartCustomBankPropertyId>
                                                                 <VisualOrder>0</VisualOrder>
                                                                 <TreeStructurePartCustomBankProperty Ref="60e87160-6c99-42fd-b39f-f522a6024ec3"/>
                                                                 <_lps fs="AA==" es="1"/>
                                                             </ChildTreeStructurePartCustomBankPropertyEntity>
                                                             <_lps f="7"/>
                                                         </ChildTreeStructurePartCustomBankPropertyCollection>
                                                         <_lps fs="AAA=" es="1"/>
                                                     </TreeStructurePartCustomBankPropertyEntity>
                                                     <TreeStructurePartCustomBankPropertyEntity ObjectID="338547ed-4428-40dc-bdcb-ca786cae7bac">
                                                         <TreeStructurePartCustomBankPropertyId>cd0914d1-2924-40bc-bef6-f60764c490c0</TreeStructurePartCustomBankPropertyId>
                                                         <CustomBankPropertyId>128250b1-0b07-4d51-b4e0-4776024cda47</CustomBankPropertyId>
                                                         <Name>1.2</Name>
                                                         <Title>1.2</Title>
                                                         <Code>14962922-e1f2-4cde-a0fb-8a690e7399c7</Code>
                                                         <TreeStructureCustomBankProperty Ref="9203b00f-f0cb-468c-b70e-4974d0b7c655"/>
                                                         <_lps fs="AAA=" es="1"/>
                                                     </TreeStructurePartCustomBankPropertyEntity>
                                                     <_lps f="7"/>
                                                 </TreeStructurePartCustomBankPropertyCollection>
                                                 <ResourceType>Tree Structure</ResourceType>
                                                 <ModifiedByFullName/>
                                                 <CreatedByFullName/>
                                                 <BankName/>
                                                 <StateName/>
                                                 <OriginalName/>
                                                 <OriginalVersion/>
                                                 <ResourceData/>
                                                 <CopiedFromString/>
                                                 <RemovedDependentEntities/>
                                                 <ApplicableToString>Items</ApplicableToString>
                                                 <CustomPropertyType>Boomstructuur</CustomPropertyType>
                                                 <_lps fs="ACAAAg==" es="1"/>
                                             </TreeStructureCustomBankPropertyEntity>
                                         </TreeStructureCustomBankPropertyEntity>

    Private ReadOnly _concept As XElement = <ConceptStructureCustomBankPropertyEntity>
                                                <ConceptStructureCustomBankPropertyEntity EntityType="8" ObjectID="168c55c0-988b-4e8a-bab7-c781019fb8dd" Format="Compact25">
                                                    <CustomBankPropertyId>da5e9ff6-c3f4-444c-a5de-0146dbb17334</CustomBankPropertyId>
                                                    <BankId>26</BankId>
                                                    <ApplicableToMask>2</ApplicableToMask>
                                                    <Publishable>false</Publishable>
                                                    <Name>ConceptProperty</Name>
                                                    <Title>ConceptProperty</Title>
                                                    <CreationDate>2014-05-12T13:47:26.67</CreationDate>
                                                    <CreatedBy>1</CreatedBy>
                                                    <ModifiedDate>2014-05-12T13:47:26.67</ModifiedDate>
                                                    <ModifiedBy>1</ModifiedBy>
                                                    <Code>3835ea2f-cbd9-e311-b65e-005056c00008</Code>
                                                    <Version>0.1</Version>
                                                    <ConceptStructurePartCustomBankPropertyCollection>
                                                        <ConceptStructurePartCustomBankPropertyEntity ObjectID="1c9a297f-ff3f-4b28-bd7b-91cbfca512b3">
                                                            <ConceptStructurePartCustomBankPropertyId>e036aa73-417b-4a51-acdb-0e9f50e133c3</ConceptStructurePartCustomBankPropertyId>
                                                            <CustomBankPropertyId>da5e9ff6-c3f4-444c-a5de-0146dbb17334</CustomBankPropertyId>
                                                            <Name>ValueAttribute</Name>
                                                            <Title>ValueAttribute</Title>
                                                            <Code>e0edfab4-03c6-436c-ad46-04231b2f4e03</Code>
                                                            <ConceptTypeId>2</ConceptTypeId>
                                                            <ConceptStructureCustomBankProperty Ref="168c55c0-988b-4e8a-bab7-c781019fb8dd"/>
                                                            <ConceptType ObjectID="44c98003-b94e-443d-96bb-be778f6ff87a">
                                                                <ConceptTypeId>2</ConceptTypeId>
                                                                <Name>Attribute</Name>
                                                                <ApplicableToMask>1</ApplicableToMask>
                                                                <ConceptStructurePartCustomBankPropertyCollection>
                                                                    <ConceptStructurePartCustomBankPropertyEntity Ref="1c9a297f-ff3f-4b28-bd7b-91cbfca512b3"/>
                                                                    <_lps f="7"/>
                                                                </ConceptStructurePartCustomBankPropertyCollection>
                                                                <_lps fs="AA==" es="1"/>
                                                            </ConceptType>
                                                            <ChildConceptStructurePartCustomBankPropertyCollection>
                                                                <ChildConceptStructurePartCustomBankPropertyEntity ObjectID="812a9eae-bfb3-4dfb-a0aa-8569fa6289dd">
                                                                    <Id>c8e2c8fe-e68d-471b-8af3-59723b82d1e8</Id>
                                                                    <ConceptStructurePartCustomBankPropertyId>e036aa73-417b-4a51-acdb-0e9f50e133c3</ConceptStructurePartCustomBankPropertyId>
                                                                    <ChildConceptStructurePartCustomBankPropertyId>d9c84fa6-9c3e-4c7a-a13b-53a7a112ece3</ChildConceptStructurePartCustomBankPropertyId>
                                                                    <VisualOrder>0</VisualOrder>
                                                                    <ChildConceptStructurePartCustomBankProperty ObjectID="db1d7c29-466c-4de3-9c38-db4773622be1">
                                                                        <ConceptStructurePartCustomBankPropertyId>d9c84fa6-9c3e-4c7a-a13b-53a7a112ece3</ConceptStructurePartCustomBankPropertyId>
                                                                        <CustomBankPropertyId>da5e9ff6-c3f4-444c-a5de-0146dbb17334</CustomBankPropertyId>
                                                                        <Name>ValueAttributePart</Name>
                                                                        <Title>ValueAttributePart</Title>
                                                                        <Code>542d9050-dc65-4b2d-a59f-9a3749192391</Code>
                                                                        <ConceptTypeId>3</ConceptTypeId>
                                                                        <ReferencedConceptStructurePartCustomBankPropertyCollection>
                                                                            <ChildConceptStructurePartCustomBankPropertyEntity Ref="812a9eae-bfb3-4dfb-a0aa-8569fa6289dd"/>
                                                                            <_lps f="7"/>
                                                                        </ReferencedConceptStructurePartCustomBankPropertyCollection>
                                                                        <_lps fs="AAA=" es="1"/>
                                                                    </ChildConceptStructurePartCustomBankProperty>
                                                                    <_lps fs="AA==" es="1"/>
                                                                </ChildConceptStructurePartCustomBankPropertyEntity>
                                                                <_lps f="7"/>
                                                            </ChildConceptStructurePartCustomBankPropertyCollection>
                                                            <_lps fs="AAA=" es="1"/>
                                                        </ConceptStructurePartCustomBankPropertyEntity>
                                                        <ConceptStructurePartCustomBankPropertyEntity ObjectID="54c6b08e-9772-4232-9462-4269ec1f8974">
                                                            <ConceptStructurePartCustomBankPropertyId>8a2bfde9-685f-4ef9-8d13-18e748100c28</ConceptStructurePartCustomBankPropertyId>
                                                            <CustomBankPropertyId>da5e9ff6-c3f4-444c-a5de-0146dbb17334</CustomBankPropertyId>
                                                            <Name>Value1</Name>
                                                            <Title>Value1</Title>
                                                            <Code>406218a9-5a25-41bd-8b96-aecfd8437cfd</Code>
                                                            <ConceptTypeId>5</ConceptTypeId>
                                                            <ConceptStructureCustomBankProperty Ref="168c55c0-988b-4e8a-bab7-c781019fb8dd"/>
                                                            <ConceptType ObjectID="ccc21fb7-9dd7-44a6-a9de-b63a05469b83">
                                                                <ConceptTypeId>5</ConceptTypeId>
                                                                <Name>Domain</Name>
                                                                <ApplicableToMask>2</ApplicableToMask>
                                                                <ConceptStructurePartCustomBankPropertyCollection>
                                                                    <ConceptStructurePartCustomBankPropertyEntity Ref="54c6b08e-9772-4232-9462-4269ec1f8974"/>
                                                                    <_lps f="7"/>
                                                                </ConceptStructurePartCustomBankPropertyCollection>
                                                                <_lps fs="AA==" es="1"/>
                                                            </ConceptType>
                                                            <ChildConceptStructurePartCustomBankPropertyCollection>
                                                                <ChildConceptStructurePartCustomBankPropertyEntity ObjectID="c94d6115-9df7-48de-a371-ffbdbf6e2383">
                                                                    <Id>8a3c3d68-6a0c-438a-aa06-d33aa9d04823</Id>
                                                                    <ConceptStructurePartCustomBankPropertyId>8a2bfde9-685f-4ef9-8d13-18e748100c28</ConceptStructurePartCustomBankPropertyId>
                                                                    <ChildConceptStructurePartCustomBankPropertyId>e036aa73-417b-4a51-acdb-0e9f50e133c3</ChildConceptStructurePartCustomBankPropertyId>
                                                                    <VisualOrder>0</VisualOrder>
                                                                    <ChildConceptStructurePartCustomBankProperty ObjectID="195cb2db-ad3b-4248-8a76-92616a8f5a85">
                                                                        <ConceptStructurePartCustomBankPropertyId>e036aa73-417b-4a51-acdb-0e9f50e133c3</ConceptStructurePartCustomBankPropertyId>
                                                                        <CustomBankPropertyId>da5e9ff6-c3f4-444c-a5de-0146dbb17334</CustomBankPropertyId>
                                                                        <Name>ValueAttribute</Name>
                                                                        <Title>ValueAttribute</Title>
                                                                        <Code>e0edfab4-03c6-436c-ad46-04231b2f4e03</Code>
                                                                        <ConceptTypeId>2</ConceptTypeId>
                                                                        <ReferencedConceptStructurePartCustomBankPropertyCollection>
                                                                            <ChildConceptStructurePartCustomBankPropertyEntity Ref="c94d6115-9df7-48de-a371-ffbdbf6e2383"/>
                                                                            <_lps f="7"/>
                                                                        </ReferencedConceptStructurePartCustomBankPropertyCollection>
                                                                        <_lps fs="AAA=" es="1"/>
                                                                    </ChildConceptStructurePartCustomBankProperty>
                                                                    <_lps fs="AA==" es="1"/>
                                                                </ChildConceptStructurePartCustomBankPropertyEntity>
                                                                <_lps f="7"/>
                                                            </ChildConceptStructurePartCustomBankPropertyCollection>
                                                            <_lps fs="AAA=" es="1"/>
                                                        </ConceptStructurePartCustomBankPropertyEntity>
                                                        <ConceptStructurePartCustomBankPropertyEntity ObjectID="dd8c7334-c321-4274-8708-ac72ad66227b">
                                                            <ConceptStructurePartCustomBankPropertyId>d9c84fa6-9c3e-4c7a-a13b-53a7a112ece3</ConceptStructurePartCustomBankPropertyId>
                                                            <CustomBankPropertyId>da5e9ff6-c3f4-444c-a5de-0146dbb17334</CustomBankPropertyId>
                                                            <Name>ValueAttributePart</Name>
                                                            <Title>ValueAttributePart</Title>
                                                            <Code>542d9050-dc65-4b2d-a59f-9a3749192391</Code>
                                                            <ConceptTypeId>3</ConceptTypeId>
                                                            <ConceptStructureCustomBankProperty Ref="168c55c0-988b-4e8a-bab7-c781019fb8dd"/>
                                                            <ConceptType ObjectID="b59543ec-19ab-4970-99ff-9f6a854132a6">
                                                                <ConceptTypeId>3</ConceptTypeId>
                                                                <Name>PartAttribute</Name>
                                                                <ApplicableToMask>3</ApplicableToMask>
                                                                <ConceptStructurePartCustomBankPropertyCollection>
                                                                    <ConceptStructurePartCustomBankPropertyEntity Ref="dd8c7334-c321-4274-8708-ac72ad66227b"/>
                                                                    <_lps f="7"/>
                                                                </ConceptStructurePartCustomBankPropertyCollection>
                                                                <_lps fs="AA==" es="1"/>
                                                            </ConceptType>
                                                            <_lps fs="AAA=" es="1"/>
                                                        </ConceptStructurePartCustomBankPropertyEntity>
                                                        <_lps f="7"/>
                                                    </ConceptStructurePartCustomBankPropertyCollection>
                                                    <ResourceType>Concept Structure</ResourceType>
                                                    <ModifiedByFullName/>
                                                    <CreatedByFullName/>
                                                    <BankName/>
                                                    <StateName/>
                                                    <OriginalName/>
                                                    <OriginalVersion/>
                                                    <ResourceData/>
                                                    <CopiedFromString/>
                                                    <RemovedDependentEntities/>
                                                    <ApplicableToString>Items</ApplicableToString>
                                                    <CustomPropertyType>Conceptstructuur</CustomPropertyType>
                                                    <_lps fs="ACAAAg==" es="1"/>
                                                </ConceptStructureCustomBankPropertyEntity>
                                            </ConceptStructureCustomBankPropertyEntity>

    ReadOnly _asssessmentItem As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="SomeCode" title="SomeTitle" layoutTemplateSrc="BO.Choice.SC">
                                                <solution>
                                                    <keyFindings/>
                                                    <aspectReferences/>
                                                </solution>
                                                <parameters>
                                                </parameters>
                                            </assessmentItem>

    ReadOnly _assessmentItemWithConcepts As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="dtt-en-hv-0172a-SC" title="appreciation" layoutTemplateSrc="Cito.Generic.Matrix.DC">
                                                           <solution>
                                                               <keyFindings>
                                                                   <keyFinding id="matrix" scoringMethod="Dichotomous">
                                                                       <keyFactSet>
                                                                           <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="matrix1" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>B</typedValue>
                                                                                   </stringValue>
                                                                               </keyValue>
                                                                           </keyFact>
                                                                       </keyFactSet>
                                                                   </keyFinding>
                                                               </keyFindings>
                                                               <conceptFindings>
                                                                   <conceptFinding id="matrix" scoringMethod="None">
                                                                       <conceptFactSet>
                                                                           <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <conceptValue domain="matrix1" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>B</typedValue>
                                                                                   </stringValue>
                                                                               </conceptValue>
                                                                           </conceptFact>
                                                                           <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <concept value="2" code="EN-SC-1"/>
                                                                               <concept value="2" code="EN-SC-1.1"/>
                                                                           </concepts>
                                                                       </conceptFactSet>
                                                                   </conceptFinding>
                                                               </conceptFindings>
                                                               <aspectReferences/>
                                                               <ItemScoreTranslationTable>
                                                                   <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                   <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                               </ItemScoreTranslationTable>
                                                           </solution>
                                                           <parameters>
                                                               <parameterSet id="entireItem"/>
                                                           </parameters>
                                                       </assessmentItem>


End Class
