
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses.Workers
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.Interfaces

<TestClass>
Public MustInherit Class GenericMetaDataGenerationTest(Of T As {ResourceEntity})

    <TestMethod>
    Public Sub Entity_ShouldHave_NoResourceIdMetadata()
        Dim entity As T = GetEntity()
        Dim generator = New MetaDataGeneratorForEntity(entity)

        Dim result = generator.GetEntitySpecificMetadata().ToList()

        Assert.IsFalse(result.Exists(Function(m)
                                         Return m.Name.Equals("resourceid", StringComparison.CurrentCultureIgnoreCase)
                                     End Function))

    End Sub

    <TestMethod>
    Public Sub OnlyVersionableEntities_ShouldHave_VersionMetaData()
        Dim entity As T = GetEntity()

        TryAndSetVersion(entity)

        Dim generator = New MetaDataGeneratorForEntity(entity)

        Dim isVersionable = TryCast(entity, IVersionable) IsNot Nothing
        Dim result = generator.DefaultMetadata().ToList()

        Dim metaDataVersionExists = result.Exists(Function(m)
                                                      Return m.Name.Equals("Version", StringComparison.CurrentCultureIgnoreCase)
                                                  End Function)

        If (isVersionable) Then
            Assert.AreEqual(isVersionable, metaDataVersionExists, String.Format("For Type {0}", GetType(T).Name))
        End If
    End Sub

    Private Sub TryAndSetVersion(ByVal entity As T)
        Dim prop = GetType(T).GetProperties().Where(Function(propertyInfo) propertyInfo.Name.Equals("version", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()

        If (prop IsNot Nothing) Then
            prop.SetValue(entity, "1")
        End If
    End Sub

    Protected MustOverride Function GetEntity() As T

End Class
