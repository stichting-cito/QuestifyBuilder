
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Cito.Tester.Common
Imports Questify.Builder.Logic

<TestClass()>
Public Class MetaDataDeserializerTest

    <TestMethod(), Description("Tests the deserialization of binary data written by an XmlSerializer."), TestCategory("Logic")>
    Public Sub TestSerializedMetaDataWithXmlSerializer()
        'Arrange
        Dim versionableEntity As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Nothing)
        Dim binary As Byte() = SerializeHelper.XmlSerializeToByteArray(Helper.CreateMetaData(CType(versionableEntity, IPropertyEntity), "remcor"))
        Dim deserializer As New XmlSerializerMetaDataDeserializer(Nothing)

        'Act
        Dim result As Versioning.MetaData = deserializer.DeserializeMetaData(binary)

        'Assert
        Assert.IsTrue(True) 'When you arrive at this point (no exception was thrown), everything is fine.
    End Sub

    <TestMethod(), Description("Tests the deserialization of invalid xml not suitable for XmlSerializer."), TestCategory("Logic")>
    <ExpectedException(GetType(MetaDataDeserializationException))>
    Public Sub TestSerializedMetaDataWithDataContractSerializer_InvalidOperationException()
        'Arrange
        Dim versionableEntity As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), Nothing)
        Dim binary As Byte() = System.Text.Encoding.UTF8.GetBytes("<root><anElement>Some data.</anElement></root>")
        Dim deserializer As New XmlSerializerMetaDataDeserializer(Nothing)

        'Act
        Dim metaData As Versioning.MetaData = deserializer.DeserializeMetaData(binary)

        'Assert
    End Sub

End Class
