Imports Cito.Tester.ContentModel.Datasources
Imports FakeItEasy
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.UnitTests.Framework.Faketory.customMocks.datasources
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl


    Friend Class DataSourceMaker
        Implements IDataSourceMaker


        Public Function GetInclusionGroup(ByVal name As String, ByVal idents As IEnumerable(Of String)) As EntityCollection Implements IDataSourceMaker.GetInclusionGroup
            Dim ret As New EntityCollection
            ret.Add(MakeResource(name, idents, DataSourceBehaviourEnum.Inclusion))
            Return ret
        End Function

        Function GetInclusionGroups(ByVal groupDefs As IEnumerable(Of KeyValuePair(Of String, IEnumerable(Of String)))) As EntityCollection Implements IDataSourceMaker.GetInclusionGroups
            Dim ret As New EntityCollection
            For Each e As KeyValuePair(Of String, IEnumerable(Of String)) In groupDefs
                ret.Add(MakeResource(e.Key, e.Value, DataSourceBehaviourEnum.Inclusion))
            Next
            Return ret
        End Function

        Public Function GetExclusionGroup(ByVal name As String, ByVal idents As IEnumerable(Of String)) As EntityCollection Implements IDataSourceMaker.GetExclusionGroup
            Dim ret As New EntityCollection
            ret.Add(MakeResource(name, idents, DataSourceBehaviourEnum.Exclusion))
            Return ret
        End Function

        Private Function MakeResource(ByVal name As String, ByVal idents As IEnumerable(Of String), ByVal datasourceBehaviour As DataSourceBehaviourEnum) As DataSourceResourceEntity
            Dim fake = New Fake(Of DataSourceResourceEntity)()
            fake.FakedObject.Name = name
            fake.CallsTo(Function(x) x.ResourceData).Returns(MakeInclusiveDataSourceSettings(idents, datasourceBehaviour))
            Return fake.FakedObject
        End Function

        Private Function MakeInclusiveDataSourceSettings(ByVal idents As IEnumerable(Of String), ByVal datasourceBehaviour As DataSourceBehaviourEnum) As ResourceDataEntity
            Dim ret As New ResourceDataEntity
            Dim tp As Type = Type.GetType(GetType(MockedItemDataSourceConfig).FullName)
            Dim toSerialize As New DataSourceSettings(GetType(MockedItemDataSource).AssemblyQualifiedName,
                                                      GetType(MockedItemDataSourceConfig).AssemblyQualifiedName,
                                                      String.Empty)


            toSerialize.Behaviour = datasourceBehaviour

            Dim data As New MockedItemDataSourceConfig
            data.GroupDefinition.AddRange(idents)
            toSerialize.DataSourceConfig = data
            ret.BinData = Cito.Tester.Common.SerializeHelper.XmlSerializeToByteArray(toSerialize)
            Return ret
        End Function

    End Class
End NameSpace