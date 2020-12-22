Imports Cito.Tester.ContentModel
Imports FakeItEasy
Imports HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl

    Friend Class EntityMaker
        Implements IEntityMaker

        Friend Function GetItemResourceEntities(ByVal ids As IEnumerable(Of String)) As ItemResourceEntityCollection Implements IEntityMaker.GetItemResourceEntities
            Dim ret As New ItemResourceEntityCollection

            For Each e As String In ids
                Dim eInternal As String = e
                Dim fake As ItemResourceEntity = A.Fake(Of ItemResourceEntity)()
                fake.Name = e
                A.CallTo(Function() fake.ResourceData).ReturnsLazily(Function() CreateAssesmentItem(eInternal))
                ret.Add(fake)
            Next

            Return ret
        End Function

        Private Function CreateAssesmentItem(ByVal id As String) As ResourceDataEntity
            Dim ret As New ResourceDataEntity
            Dim toSerialize As New AssessmentItem() With {.Identifier = id, .Title = id}
            ret.BinData = Cito.Tester.Common.SerializeHelper.XmlSerializeToByteArray(toSerialize)
            Return ret
        End Function

        Function GetItemResourceEntities2(ByVal ParamArray ids As String()) As EntityCollection Implements IEntityMaker.GetItemResourceEntities2
            Dim ret As New EntityCollection

            For Each e As String In ids
                Dim add As New ItemResourceEntity()
                add.Name = e
                ret.Add(add)
            Next

            Return ret
        End Function

    End Class
End NameSpace