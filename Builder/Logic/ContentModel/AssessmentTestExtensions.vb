Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel
    Public Module AssessmentTestExtensions

        <Extension>
        Public Function CanPropose(test As AssessmentTest2, bankId As Integer) As Boolean
            Return test.GetDataSourceSettingsForSectionsInTest(bankId).ToDictionary(Function(s) (s.Key), Function(s) TryCast(s.Value.CreateGetDataSource(), ItemDataSource)).Values.OfType(Of ItemDataSourceManyOutput).Any
        End Function



        <Extension>
        Public Function GetDataSourceSettingsForSectionsInTest(ByVal test As AssessmentTest2, ByVal bankId As Integer) As Dictionary(Of TestSection2, DataSourceSettings)
            Dim testSectionsInTemplate As IList(Of TestSection2) = test.GetAllSectionsInTest()
            Dim foundTestSectionsWithDataSource As New Dictionary(Of TestSection2, DataSourceSettings)
            For Each section As TestSection2 In testSectionsInTemplate
                If Not String.IsNullOrEmpty(section.ItemDataSource) Then
                    Dim resource As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(bankId, section.ItemDataSource, New ResourceRequestDTO())
                    Debug.Assert(resource IsNot Nothing, String.Format(CultureInfo.InvariantCulture, "Unable to retrieve datasource with name '{0}' from bank.", section.ItemDataSource))
                    If resource IsNot Nothing Then
                        Dim settings As DataSourceSettings = Parsers.ParseItemDataSourceSettingsFromResourceEntity(resource)
                        foundTestSectionsWithDataSource.Add(section, settings)
                    End If
                End If
            Next
            Return foundTestSectionsWithDataSource
        End Function
    End Module

End Namespace
