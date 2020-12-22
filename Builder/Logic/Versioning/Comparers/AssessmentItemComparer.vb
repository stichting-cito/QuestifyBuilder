Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemProcessing
Imports System.Linq
Imports Cito.Tester.Common
Imports Versioning

Class AssessmentItemComparer
    Inherits MetaDataComparerBase(Of AssessmentItem)

    Private ReadOnly _resourceManager As ResourceManagerBase

    Public Sub New(ByVal resourceManager As ResourceManagerBase)
        MyBase.New()

        _resourceManager = resourceManager
    End Sub

    Public Overrides Function Compare(t1 As AssessmentItem, t2 As AssessmentItem) _
        As IEnumerable(Of MetaDataCompareResult)
        _results.AddRange(CompareSolution(t1.Solution, t2.Solution))

        AddDesignerSettingsFromItemLayoutTemplateToAssessmentItem(t1)
        AddDesignerSettingsFromItemLayoutTemplateToAssessmentItem(t2)

        _results.AddRange(ParameterHandler.Compare(t2.Parameters, t1.Parameters))

        For Each result As MetaDataCompareResult In ParameterHandler.Compare(t1.Parameters, t2.Parameters)
            If _
                _results.FirstOrDefault(
                    Function(i) i.PropertyName = result.PropertyName AndAlso i.Category = result.Category) Is Nothing _
                Then
                _results.Add(result)
            End If
        Next

        Return _results
    End Function

    Private Sub AddDesignerSettingsFromItemLayoutTemplateToAssessmentItem(ByRef assessmentItem As AssessmentItem)
        If Not String.IsNullOrEmpty(assessmentItem.LayoutTemplateSourceName) Then
            Dim request = New ResourceRequestDTO()
            Dim adapter As New ItemLayoutAdapter(assessmentItem.LayoutTemplateSourceName, Nothing,
                                          Sub(sender As Object, e As ResourceNeededEventArgs)
                                              Dim resource As BinaryResource = Nothing

                                              If e.TypedResourceType IsNot Nothing Then
                                                  resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
                                              Else
                                                  resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
                                              End If
                                              e.BinaryResource = resource
                                          End Sub)

            Dim extractedParameterSets As ParameterSetCollection = adapter.CreateParameterSetsFromItemTemplate()

            For Each parameterCollectionFromAssessmentItem As ParameterCollection In assessmentItem.Parameters
                Dim parameterCollectionFromILT = extractedParameterSets.FirstOrDefault(Function(i) i.Id = parameterCollectionFromAssessmentItem.Id)
                If parameterCollectionFromILT IsNot Nothing Then
                    For Each parameterFromAssessmentItem As ParameterBase In parameterCollectionFromAssessmentItem.InnerParameters
                        Dim parameterFromILT As ParameterBase = CType(parameterCollectionFromILT.GetParameterByName(parameterFromAssessmentItem.Name, False), ParameterBase)
                        If parameterFromILT IsNot Nothing Then
                            parameterFromAssessmentItem.DesignerSettings = parameterFromILT.DesignerSettings
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Function CompareSolution(s1 As Solution, s2 As Solution) As IEnumerable(Of MetaDataCompareResult)
        Dim result As New List(Of MetaDataCompareResult)()

        If (s1.MaxSolutionRawScore <> s2.MaxSolutionRawScore) Then _
            result.Add(New MetaDataCompareResult(My.Resources.Property_MaxSolutionRawScore, Nothing,
                                                 s1.MaxSolutionRawScore.ToString(), s2.MaxSolutionRawScore.ToString(),
                                                 My.Resources.Category_Solution, Nothing))

        CompareScoreTranslationTables(s1, s2, result)
        CompareFindings(s1, s2, result)
        CompareAspectReferenceSet(s1, s2, result)
        CompareConceptFindings(s1.ConceptFindings, s2.ConceptFindings, result)
        Return result
    End Function

    Private Sub CompareAspectReferenceSet(s1 As Solution, s2 As Solution, result As List(Of MetaDataCompareResult))
        If _
    (s1.AspectReferenceSetCollection Is Nothing OrElse s1.AspectReferenceSetCollection.Count = 0) AndAlso
    (s2.AspectReferenceSetCollection Is Nothing OrElse s2.AspectReferenceSetCollection.Count = 0) Then Return

        For i As Integer = 0 To _
            Math.Max(s1.AspectReferenceSetCollection.Count, s2.AspectReferenceSetCollection.Count) - 1 Step 1
            If (s1.AspectReferenceSetCollection.Count <= i) Then

            ElseIf s2.AspectReferenceSetCollection.Count <= i Then

            Else
                Dim asrs1 = s1.AspectReferenceSetCollection(i)
                Dim asrs2 = s2.AspectReferenceSetCollection(i)
                CompareAspectReferenceSet(asrs1, asrs2, result)
            End If
        Next
    End Sub

    Private Sub CompareAspectReferenceSet(asrs1 As AspectReferenceCollection, asrs2 As AspectReferenceCollection,
                                          result As List(Of MetaDataCompareResult))
        If _
            (asrs1.Items Is Nothing OrElse asrs1.Items.Count = 0) AndAlso
            (asrs2.Items Is Nothing OrElse asrs2.Items.Count = 0) Then Return

        For Each aspref As AspectReference In asrs1.Items
            Dim aspref2 As AspectReference =
        asrs2.Items.FirstOrDefault(Function(ar As AspectReference) aspref.SourceName = ar.SourceName)
            If aspref2 IsNot Nothing Then
                If aspref.Description <> aspref2.Description Then _
                    AddNewMetaDataCompareResult(result, My.Resources.Property_Description, aspref.Description,
                                                aspref2.Description, My.Resources.Category_Solution)
                If aspref.MaxScore <> aspref2.MaxScore Then _
                    AddNewMetaDataCompareResult(result, My.Resources.Property_MaxScore, aspref.MaxScore.ToString,
                                                aspref2.MaxScore.ToString, My.Resources.Category_Solution)
                If asrs1.Items.IndexOf(aspref) <> asrs2.Items.IndexOf(aspref2) Then _
                    AddNewMetaDataCompareResult(result, My.Resources.Property_Index,
                                                asrs1.Items.IndexOf(aspref).ToString,
                                                asrs2.Items.IndexOf(aspref2).ToString, My.Resources.Category_Solution)
            Else
                result.Add(New MetaDataCompareResult(My.Resources.Property_AspectReference, Nothing, aspref.SourceName,
                                                     "", My.Resources.Category_Solution, Nothing))
                result.Add(New MetaDataCompareResult(My.Resources.Property_AspectReference, Nothing, aspref.Description,
                                                     "", My.Resources.Category_Solution, Nothing))
                result.Add(New MetaDataCompareResult(My.Resources.Property_AspectReference, Nothing,
                                                     aspref.MaxScore.ToString, "", My.Resources.Category_Solution,
                                                     Nothing))
            End If
        Next

        For Each aspref As AspectReference In
    asrs2.Items.Where(Function(ar) Not asrs1.Items.Any(Function(oldAr) oldAr.SourceName = ar.SourceName))
            AddNewMetaDataCompareResult(result, My.Resources.Property_AspectReference, "", aspref.SourceName,
                                        My.Resources.Category_Solution)
            AddNewMetaDataCompareResult(result, My.Resources.Property_AspectReference, "", aspref.Description,
                                        My.Resources.Category_Solution)
            AddNewMetaDataCompareResult(result, My.Resources.Property_AspectReference, "", aspref.MaxScore.ToString,
                                        My.Resources.Category_Solution)
        Next
    End Sub

    Private Sub CompareScoreTranslationTables(s1 As Solution, s2 As Solution, result As List(Of MetaDataCompareResult))
        If (Not s1.ItemScoreTranslationTableSpecified AndAlso Not s2.ItemScoreTranslationTableSpecified) Then Return

        For Each table As ItemScoreTranslationTableEntry In s1.ItemScoreTranslationTable
            Dim t2 = s2.ItemScoreTranslationTable.FirstOrDefault(Function(t) table.RawScore = t.RawScore)
            If (t2 Is Nothing) Then
                AddNewMetaDataCompareResult(result, My.Resources.Property_RawScore, table.RawScore.ToString,
                                            String.Empty, My.Resources.Category_Solution)
                AddNewMetaDataCompareResult(result, My.Resources.Property_TranslatedScore,
                                            table.TranslatedScore.ToString, String.Empty, My.Resources.Category_Solution)
            Else
                If table.TranslatedScore <> t2.TranslatedScore Then _
                    AddNewMetaDataCompareResult(result, My.Resources.Property_TranslatedScore,
                                                table.TranslatedScore.ToString, t2.TranslatedScore.ToString,
                                                My.Resources.Category_Solution)
            End If
        Next

        For Each newEntry As ItemScoreTranslationTableEntry In
            s2.ItemScoreTranslationTable.Where(
                Function(t) Not s1.ItemScoreTranslationTable.Any(Function(t2) t2.RawScore = t.RawScore))
            AddNewMetaDataCompareResult(result, My.Resources.Property_RawScore, String.Empty, newEntry.RawScore.ToString,
                            My.Resources.Category_Solution)
            AddNewMetaDataCompareResult(result, My.Resources.Property_TranslatedScore, String.Empty,
                                        newEntry.TranslatedScore.ToString, My.Resources.Category_Solution)
        Next
    End Sub

    Private Sub CompareFindings(s1 As Solution, s2 As Solution, result As List(Of MetaDataCompareResult))
        If _
            (s1.Findings Is Nothing OrElse s1.Findings.Count = 0) AndAlso
            (s2.Findings Is Nothing OrElse s2.Findings.Count = 0) Then Return

        Dim s1String = GetFindingString(s1.Findings)
        Dim s2String = GetFindingString(s2.Findings)

        If s1String <> s2String Then
            AddNewMetaDataCompareResult(result, My.Resources.Property_Findings, s1String, s2String,
                                        My.Resources.Category_Solution)
        End If
    End Sub

    Private Sub CompareConceptFindings(cfCol1 As ConceptFindingCollection, cfCol2 As ConceptFindingCollection,
                                       result As List(Of MetaDataCompareResult))
        If (cfCol1 Is Nothing OrElse cfCol1.Count = 0) AndAlso (cfCol2 Is Nothing OrElse cfCol2.Count = 0) Then Return

        If cfCol1 IsNot Nothing Then
            For Each cf1 In cfCol1
                Dim cf2 As ConceptFinding = Nothing
                If (cfCol2 IsNot Nothing) Then
                    cf2 = cfCol2.FirstOrDefault(Function(c) c.Id = cf1.Id)
                End If

                If (cf2 Is Nothing) Then
                    AddNewMetaDataCompareResult(result, My.Resources.Property_Concept, cf1.ToString(), String.Empty,
                                                My.Resources.Category_Solution)
                Else
                    If cf1.ToString <> cf2.ToString Then _
                        AddNewMetaDataCompareResult(result, My.Resources.Property_Concept, cf1.ToString, cf2.ToString,
                                                    My.Resources.Category_Solution)
                End If
            Next
        End If

        If cfCol2 IsNot Nothing Then
            For Each cf2 In
                cfCol2.Where(Function(c2) cfCol1 Is Nothing OrElse Not cfCol1.Any(Function(c1) c1.Id = c2.Id))
                AddNewMetaDataCompareResult(result, My.Resources.Property_Concept, String.Empty, cf2.ToString,
                                            My.Resources.Category_Solution)
            Next
        End If
    End Sub

    Private Function GetFindingString(fc As KeyFindingCollection) As String
        If fc Is Nothing OrElse fc.Count = 0 Then Return String.Empty

        Return String.Join(";", fc.Select(Function(f) f.ToString).ToArray())
    End Function

    Private Sub AddNewMetaDataCompareResult(list As List(Of MetaDataCompareResult), propertyName As String,
                                            oldValue As String, newValue As String, category As String)
        list.Add(New MetaDataCompareResult(propertyName, Nothing, oldValue, newValue, category, Nothing))
    End Sub
End Class
