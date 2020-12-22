Imports System.Linq
Imports System.ComponentModel
Imports Cito.Tester.Common

<Serializable>
Public Class ParameterSetCollection
    Inherits BindingList(Of ParameterCollection)

    Public Property ShouldSort As Boolean = False

    Private Const DynamicCollDefaultIdentifier As String = "dynamicCollectionId"
    Private Const DynamicCollAutoScoringIdentifier As String = "autoScoring"

    Public Function GetParamCollectionByControlId(id As String) As ParameterCollection
        For Each [set] As ParameterCollection In Me
            If [set].Id = id Then
                Return [set]
            End If
        Next
        Return Nothing
    End Function

    Public Sub AddRange(collection As ParameterSetCollection)
        For Each paramCollection As ParameterCollection In collection
            Me.Add(paramCollection)
        Next
        ShouldSort = ShouldSort OrElse collection.ShouldSort
    End Sub

    Public Shared Function DeepClone(paramSetCollection As ParameterSetCollection) As ParameterSetCollection
        Dim result As New ParameterSetCollection()

        For Each paramCollection As ParameterCollection In paramSetCollection
            result.Add(ParameterCollection.DeepClone(paramCollection))
        Next

        Return result
    End Function

    Public Overloads Function Contains(id As String) As Boolean

        For i As Integer = 0 To Me.Count - 1
            If Me.Item(i).Id.Equals(id) Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Function GetParentCollectionByParameter(parameter As ParameterBase) As ParameterCollection
        Dim worker As New ParameterSetCollectionFlattener(Me)
        Return worker.GetParentCollection(parameter)
    End Function

    Public Function GetParameterSetCollectionNameByParameter(parameter As ParameterBase) As String

        For i As Integer = 0 To Me.Count - 1
            If Me.Item(i).InnerParameters.Contains(parameter) Then
                Return Me.Item(i).Id
            End If
        Next
        Return String.Empty
    End Function

    Public Function GetParameter(parameterId As String, parameterSetCollectionId As String) As ParameterBase

        For i As Integer = 0 To Me.Count - 1
            If Me.Item(i).Id.Equals(parameterSetCollectionId) Then
                For Each parameter As ParameterBase In Me.Item(i).InnerParameters
                    If parameter.Name.Equals(parameterId) Then
                        Return parameter
                    End If
                Next
                Return Nothing
            End If
        Next

        Return Nothing
    End Function


    Public Function GetParameters() As List(Of ParameterBase)
        Return GetParameters(Function(parameterCollection) True)
    End Function

    Public Function GetParameters(parameterCollectionFilter As Func(Of ParameterCollection, Boolean)) As List(Of ParameterBase)
        Dim returnValue As New List(Of ParameterBase)
        Dim sortedParameters As New SortedDictionary(Of String, ParameterBase)

        Dim collectionsToEvaluate = From collection In Me Where parameterCollectionFilter(collection)

        For Each paramSet As ParameterCollection In collectionsToEvaluate

            If ShouldSort Then
                For Each parameter As ParameterBase In paramSet.InnerParameters
                    Dim sortKey As String = parameter.DesignerSettings.GetSettingValueByKey("sortkey")
                    Dim sortKeyValue As Integer
                    If Not String.IsNullOrEmpty(sortKey) AndAlso Integer.TryParse(sortKey, sortKeyValue) Then
                        Dim sortKeyModified As String = $"{sortKeyValue:d3}"
                        sortedParameters.Add($"{sortKeyModified}-{parameter.Name}-{paramSet.Id}", parameter)
                    Else
                        Dim label As String = String.Empty
                        If parameter.DesignerSettings.GetDesignerSettingByKey("label") IsNot Nothing Then
                            label = parameter.DesignerSettings.GetDesignerSettingByKey("label").Value
                        End If
                        sortedParameters.Add($"{label}-{parameter.Name}-{paramSet.Id}", parameter)
                    End If
                Next
            Else
                returnValue.AddRange(paramSet.InnerParameters)
            End If

        Next

        If ShouldSort Then
            returnValue.AddRange(sortedParameters.Values)
        End If
        Return returnValue
    End Function

    Public Function UniqueFlattenedParameters(Optional includeDynamicParameters As Boolean = True) As IEnumerable(Of ParameterBase)
        Dim worker As New ParameterSetCollectionFlattener(Me)
        Return worker.FlattenDefinition(includeDynamicParameters)
    End Function

    Public Function UniqueFlattenedParametersNames(Optional includeCollectionName As Boolean = False) As IEnumerable(Of String)
        Dim worker As New ParameterSetCollectionFlattener(Me)
        Return worker.FlattenDefinitionName(includeCollectionName).Distinct()
    End Function

    Public Function FlattenParameters(Optional includeSpecialPrmsFromScoringPrms As Boolean = False) As IEnumerable(Of ParameterBase)
        Dim worker As New ParameterSetCollectionFlattener(Me)
        Return worker.Flatten(includeSpecialPrmsFromScoringPrms)
    End Function

    Public Function DeepFetchScoringParameters() As HashSet(Of ScoringParameter)
        Return DoDeepFetchScoringParameters(Me, New Dictionary(Of String, HashSet(Of ScoringParameter)), New HashSet(Of ScoringParameter)())
    End Function

    Public Function DeepFetchInlineScoringParameters() As HashSet(Of ScoringParameter)
        Return DoDeepFetchScoringParameters(Me, New Dictionary(Of String, HashSet(Of ScoringParameter)), New HashSet(Of ScoringParameter)())
    End Function

    Private Shared Function DoDeepFetchScoringParameters(collection As ParameterSetCollection, ByRef scoringPrmsInDynamicCollections As Dictionary(Of String, HashSet(Of ScoringParameter)), ByRef result As HashSet(Of ScoringParameter)) As HashSet(Of ScoringParameter)
        Dim direct = collection.GetParameters(Function(parameterCollection) Not parameterCollection.IsDynamicCollection).OfType(Of ScoringParameter)()
        Dim inlineElementCollections = collection.FlattenParameters(True).Where(Function(p) TypeOf p Is XHtmlParameter).Select(Function(p) DirectCast(p, XHtmlParameter).GetInlineElements().Values)
        Dim hasScoringPrmsInDynCollection As Boolean = False

        If collection.Any(Function(c) c.IsDynamicCollection) Then
            Dim prmsToCheck As New Dictionary(Of String, IEnumerable(Of ScoringParameter))
            collection.Where(Function(parameterCollection) parameterCollection.IsDynamicCollection).ToList().ForEach(Sub(c)
                                                                                                                         prmsToCheck.Add(c.Id, c.InnerParameters().OfType(Of ScoringParameter)())
                                                                                                                     End Sub)
            EnrichScoringParamsOfDynamicCollections(prmsToCheck, inlineElementCollections, scoringPrmsInDynamicCollections)
        End If

        For Each d In direct
            result.Add(TransformIfNecessary(d))
        Next

        If scoringPrmsInDynamicCollections.ContainsKey(DynamicCollDefaultIdentifier) Then
            For Each dyn In scoringPrmsInDynamicCollections(DynamicCollDefaultIdentifier)
                result.Add(TransformIfNecessary(dyn))
                hasScoringPrmsInDynCollection = True
            Next
        ElseIf scoringPrmsInDynamicCollections.ContainsKey(DynamicCollAutoScoringIdentifier) Then
            For Each dyn In scoringPrmsInDynamicCollections(DynamicCollAutoScoringIdentifier)
                result.Add(TransformIfNecessary(dyn))
            Next
        End If

        Dim prms = collection.FlattenParameters(True).Where(Function(p) TypeOf p Is CollectionParameter OrElse TypeOf p Is PlainTextParameter OrElse TypeOf p Is XHtmlParameter OrElse TypeOf p Is CustomInteractionResourceParameter)
        If prms IsNot Nothing Then
            For Each prm In prms
                If TypeOf prm Is CollectionParameter AndAlso DirectCast(prm, CollectionParameter).Value IsNot Nothing AndAlso DirectCast(prm, CollectionParameter).Value.Any() Then
                    Dim collPrm = DirectCast(prm, CollectionParameter)
                    For idx = 0 To collPrm.Value.Count - 1
                        Dim scoringPrms = collPrm.Value(idx).InnerParameters.OfType(Of ScoringParameter)()
                        For Each scoringPrm In scoringPrms
                            scoringPrm.CollectionIdx = collPrm.Value(idx).Id
                            result.Add(scoringPrm)
                        Next
                    Next
                ElseIf TypeOf prm Is CustomInteractionResourceParameter AndAlso Not DirectCast(prm, CustomInteractionResourceParameter).InlineUsage Then
                    If scoringPrmsInDynamicCollections.ContainsKey(IdentifierHelper.CI_FindingName) Then
                        For Each sp In scoringPrmsInDynamicCollections(IdentifierHelper.CI_FindingName)
                            result.Add(sp)
                        Next
                    End If
                ElseIf TypeOf prm Is PlainTextParameter Then
                    Dim plainTextPrm = DirectCast(prm, PlainTextParameter)
                    If hasScoringPrmsInDynCollection AndAlso Not String.IsNullOrEmpty(plainTextPrm.Value) AndAlso IdentifierHelper.CheckIdentifierIsGuid(plainTextPrm.Value) Then
                        For Each dynScoringPrm In scoringPrmsInDynamicCollections(DynamicCollDefaultIdentifier).Where(Function(sp) sp.Name.Equals(plainTextPrm.Value, StringComparison.InvariantCultureIgnoreCase)).ToList()
                            Dim scoringPrm = result.FirstOrDefault(Function(r) r.Equals(dynScoringPrm))
                            If scoringPrm IsNot Nothing AndAlso scoringPrm.InlineId Is Nothing Then
                                scoringPrm.InlineId = plainTextPrm.Value
                            End If
                        Next
                    End If
                ElseIf TypeOf prm Is XHtmlParameter Then
                    Dim xhtmlPrm = DirectCast(prm, XHtmlParameter)
                    For Each inlineElement In xhtmlPrm.GetInlineElements().Values
                        Dim indirect = DoDeepFetchScoringParameters(inlineElement.Parameters, scoringPrmsInDynamicCollections, New HashSet(Of ScoringParameter)())
                        For Each i In indirect
                            If i.InlineId Is Nothing AndAlso Not scoringPrmsInDynamicCollections.Any(Function(d) d.Key.Equals(DynamicCollDefaultIdentifier, StringComparison.InvariantCultureIgnoreCase) AndAlso d.Value.Contains(i)) Then
                                i.InlineId = inlineElement.Identifier
                            End If
                            result.Add(i)
                        Next

                        If scoringPrmsInDynamicCollections.ContainsKey(inlineElement.Identifier) Then
                            For Each sp In scoringPrmsInDynamicCollections(inlineElement.Identifier)
                                result.Add(sp)
                            Next
                        End If
                    Next
                End If
            Next
        End If

        Return result
    End Function

    Private Shared Function TransformIfNecessary(Of T As ScoringParameter)(scoringParameter As T) As T
        Dim transformableParam As ITransformable = TryCast(scoringParameter, ITransformable)
        If (transformableParam IsNot Nothing) Then
            Dim doTransform As ITransformable(Of T) = DirectCast(transformableParam, ITransformable(Of T))
            If transformableParam.CanTransform Then
                Return doTransform.Transform()
            End If
        End If
        Return scoringParameter
    End Function

    Private Shared Sub EnrichScoringParamsOfDynamicCollections(dynamicCollectionScoringParams As Dictionary(Of String, IEnumerable(Of ScoringParameter)), inlineElementCollections As IEnumerable(Of Dictionary(Of String, InlineElement).ValueCollection), ByRef result As Dictionary(Of String, HashSet(Of ScoringParameter)))
        For Each dynColl In dynamicCollectionScoringParams.Keys
            For Each sp In dynamicCollectionScoringParams(dynColl).Where(Function(s) s.ControllerId IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.ControllerId))
                Dim responseIdentifier As String = String.Empty
                Dim scoringPrms As New HashSet(Of ScoringParameter)
                If IdentifierHelper.MatchesInlineCustomInteractionIdentifier(sp.ControllerId, responseIdentifier) Then
                    For Each inlineElementCollection In inlineElementCollections
                        If inlineElementCollection.Any(Function(ie) ie.Identifier.Equals(responseIdentifier, StringComparison.OrdinalIgnoreCase)) Then
                            sp.InlineId = sp.ControllerId
                            If result.ContainsKey(responseIdentifier) Then
                                result(responseIdentifier).Add(TransformIfNecessary(sp))
                            Else
                                scoringPrms.Add(TransformIfNecessary(sp))
                                result.Add(responseIdentifier, scoringPrms)
                            End If
                            Exit For
                        End If
                    Next
                ElseIf IdentifierHelper.MatchesCustomInteractionIdentifier(sp.ControllerId) AndAlso Not String.IsNullOrEmpty(sp.FindingOverride) Then
                    If result.ContainsKey(sp.FindingOverride) Then
                        result(sp.FindingOverride).Add(TransformIfNecessary(sp))
                    Else
                        scoringPrms.Add(TransformIfNecessary(sp))
                        result.Add(sp.FindingOverride, scoringPrms)
                    End If
                ElseIf TypeOf sp Is AspectScoringParameter AndAlso dynColl.Equals(DynamicCollAutoScoringIdentifier, StringComparison.InvariantCultureIgnoreCase) Then
                    DirectCast(sp, AspectScoringParameter).AutoScoringOffPrm = True

                    If result.ContainsKey(DynamicCollAutoScoringIdentifier) Then
                        result(DynamicCollAutoScoringIdentifier).Add(TransformIfNecessary(sp))
                    Else
                        scoringPrms.Add(TransformIfNecessary(sp))
                        result.Add(DynamicCollAutoScoringIdentifier, scoringPrms)
                    End If
                Else
                    If result.ContainsKey(DynamicCollDefaultIdentifier) Then
                        result(DynamicCollDefaultIdentifier).Add(TransformIfNecessary(sp))
                    Else
                        scoringPrms.Add(TransformIfNecessary(sp))
                        result.Add(DynamicCollDefaultIdentifier, scoringPrms)
                    End If
                End If
            Next
        Next
    End Sub

End Class
