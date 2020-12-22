Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel.Datasources
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Text
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Validating
    Class ItemInDatasourceTargetValidation
        Inherits ChainHandlerBase(Of TestConstructionRequest)


        Private ReadOnly _indent As Char = vbTab(0)
        Private ReadOnly _test As AssessmentTest2
        Private ReadOnly _targetSection As TestSection2
        Private ReadOnly _resourceManager As DataBaseResourceManager
        Private _inclusionGroups As New Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))



        Sub New(ByVal resourceManager As DataBaseResourceManager, ByVal targetSection As TestSection2,
        ByVal test As AssessmentTest2)
            _test = test
            _targetSection = targetSection
            _resourceManager = resourceManager
        End Sub



        Public Overrides Function ProcessRequest(requestData As TestConstructionRequest) As ChainHandlerResult
            Dim suspected As Dictionary(Of ResourceRef, DataSourceSettings)
            Dim usedDatasources As Dictionary(Of String, TestSection2)

            _inclusionGroups = ItemHelpers.GetItemsPerGroup(_resourceManager, New String() {"inclusion"})

            suspected = GetAllDataSourcesIntersectingWithCurrentItems(requestData.Items, requestData.OverridenTarget)

            usedDatasources = GetAllUsedDatasourcesInAssessment()

            For Each itm As ResourceRef In suspected.Keys
                Dim ds As String = suspected(itm).Identifier
                If usedDatasources.ContainsKey(ds) Then
                    Dim section As TestSection2 = usedDatasources(ds)
                    If (section.Identifier <> _targetSection.Identifier) Then
                        Dim ex As ItemDatasourceUsedElsewhereException
                        ex = ConstraintException(ds, section)
                        Throw ex
                    End If
                End If
            Next


            Return ChainHandlerResult.RequestHandled
        End Function



        Private Function GetAllDataSourcesIntersectingWithCurrentItems(ByVal items As IEnumerable(Of ResourceRef),
                                                                       ByVal notSuspect As _
                                                                          IDictionary(Of ResourceRef, TestSection2)) _
            As Dictionary(Of ResourceRef, DataSourceSettings)
            Dim ret As Dictionary(Of ResourceRef, DataSourceSettings)

            Dim kvpLst As IEnumerable(Of KeyValuePair(Of ResourceRef, DataSourceSettings)) =
        From k In _inclusionGroups.Keys
        From v In _inclusionGroups(k)
        Where items.Contains(v) AndAlso (Not notSuspect.ContainsKey(v))
        Select New KeyValuePair(Of ResourceRef, DataSourceSettings)(v, k)

            Try
                ret = kvpLst.ToDictionary(Function(k) k.Key, Function(v) v.Value)
            Catch ex As ArgumentException
                Debug.Assert(False)
                Trace.TraceError(String.Format("Duplicate Items found in multiple Inclusion Groups"))
                Throw ex
            End Try

            Return ret
        End Function

        Private Function ConstraintException(ds As String, section As TestSection2) As ItemDatasourceUsedElsewhereException
            Dim ret As ItemDatasourceUsedElsewhereException
            Dim confictingItems As IEnumerable(Of ResourceRef) = (From k In _inclusionGroups.Keys Where k.Identifier = ds
                                                                  Select _inclusionGroups(k)).First

            ret = New ItemDatasourceUsedElsewhereException(ds, confictingItems, section, _targetSection)

            Return ret
        End Function



        Private Function GetAllUsedDatasourcesInAssessment() As Dictionary(Of String, TestSection2)
            Dim ret As Dictionary(Of String, TestSection2)

            Dim allSections As IEnumerable(Of TestSection2) = _test.GetAllSectionsInTest()

            Try
                ret = (From s In allSections
                       Where Not String.IsNullOrEmpty(s.ItemDataSource)
                       Select s).ToDictionary(Function(k) k.ItemDataSource, Function(v) v)

            Catch ex As ArgumentException
                Debug.Assert(False)
                Trace.TraceError(String.Format("Duplicate datasources found in multiple sections"))
                Throw New ChainHandlerException(My.Resources.Message_TestConstruction_DuplicateDataSource)
            End Try

            Return ret
        End Function


        Friend Function DumpAssesmentTree(ByVal test As AssessmentTest2) As String
            Dim sb As New StringBuilder

            sb.AppendLine($"TEST [{test.Title}]")
            For Each testPart As TestPart2 In test.TestParts
                sb.Append(_indent, 1)
                sb.AppendLine($"TestPart [{testPart.Title}]")
                For Each section As TestSection2 In testPart.Sections
                    sb.AppendLine(EnumerateSection(section, 2))
                Next
            Next

            Return sb.ToString
        End Function

        Friend Function EnumerateSection(section As TestSection2, indentDepth As Integer) As String
            Dim sb As New StringBuilder
            sb.Append(_indent, indentDepth)
            sb.AppendLine($"Section [{section.Title}]")

            For Each tstcomp As TestComponent2 In section.Components
                If (TypeOf tstcomp Is TestSection2) Then
                    sb.AppendLine(EnumerateSection(DirectCast(tstcomp, TestSection2), indentDepth + 1))
                ElseIf (TypeOf tstcomp Is ItemReference2) Then
                    sb.Append(_indent, indentDepth + 1)
                    sb.AppendLine($"Item [{tstcomp.Title}]")
                Else
                    Debug.Assert(False, "unhandled type.")
                End If

            Next
            Return sb.ToString()
        End Function

    End Class

End Namespace