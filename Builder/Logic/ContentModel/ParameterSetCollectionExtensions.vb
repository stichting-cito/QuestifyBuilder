Imports Cito.Tester.ContentModel
Imports System.Runtime.CompilerServices
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ItemProcessing

Namespace ContentModel

    Public Module ParameterSetCollectionExtensions

        <Extension()>
        Public Function GetReferencesFromResourceParameters(parametersetCollection As ParameterSetCollection) As XhtmlReferenceList
            Dim xhtmlReferences As New XhtmlReferenceList()
            For Each parameterBase As ParameterBase In parametersetCollection.GetParameters()
                Dim xhtmlResourceParameter = TryCast(parameterBase, XhtmlResourceParameter)
                If (xhtmlResourceParameter IsNot Nothing) Then
                    xhtmlReferences = xhtmlResourceParameter.References
                End If
            Next
            Return xhtmlReferences
        End Function

        <Extension>
        Public Function Merge(currectParameterSetCollection As ParameterSetCollection, newParameterSetCollection As ParameterSetCollection) As WarningsAndErrors
            Dim returnValue = ParameterHandler.Merge(newParameterSetCollection, currectParameterSetCollection)
            currectParameterSetCollection.Clear()
            newParameterSetCollection.FixReferencedAttributes()
            currectParameterSetCollection.AddRange(newParameterSetCollection)
            Return returnValue
        End Function


        <Extension>
        Public Function AreEqual(currentParameterSetCollection As ParameterSetCollection, newParameterSetCollection As ParameterSetCollection) As Boolean
            Return currentParameterSetCollection.UniqueFlattenedParametersNames(True).OrderBy(Function(p) p).SequenceEqual(newParameterSetCollection.UniqueFlattenedParametersNames(True).OrderBy(Function(p) p))
        End Function

        <Extension()>
        Public Function GetParametersWithResources(parametersetCollection As ParameterSetCollection) As List(Of ParameterBase)
            Dim parameters As New List(Of ParameterBase)
            For Each pSet As ParameterCollection In parametersetCollection
                LoopThroughParameters(pSet.InnerParameters, parameters)
            Next
            Return parameters
        End Function

        <Extension()>
        Public Function GetXhtmlParameters(parametersetCollection As ParameterSetCollection) As List(Of ParameterBase)
            Dim parameters As New List(Of ParameterBase)
            For Each pSet As ParameterCollection In parametersetCollection
                LoopThroughParameters(pSet.InnerParameters, parameters, True)
            Next
            Return parameters
        End Function

        <Extension()>
        Public Function GetCollectionParameters(parametersetCollection As ParameterSetCollection) As List(Of ParameterBase)
            Dim parameters As New List(Of ParameterBase)
            For Each pSet As ParameterCollection In parametersetCollection
                LoopThroughParameters(pSet.InnerParameters, parameters, False, True)
            Next
            Return parameters
        End Function

        <Extension()>
        Public Function GetParameterCollectionByName(parameterSetCollection As ParameterSetCollection, name As String) As ParameterCollection
            Return parameterSetCollection.FirstOrDefault(Function(ps) ps.Id = name)
        End Function

        Private Sub LoopThroughParameters(parameterList As IEnumerable(Of ParameterBase), ByRef parameters As List(Of ParameterBase))
            LoopThroughParameters(parameterList, parameters, False)
        End Sub

        Private Sub LoopThroughParameters(parameterList As IEnumerable(Of ParameterBase), ByRef parameters As List(Of ParameterBase), xhtmlOnly As Boolean)
            LoopThroughParameters(parameterList, parameters, xhtmlOnly, False)
        End Sub

        Private Sub LoopThroughParameters(parameterList As IEnumerable(Of ParameterBase), ByRef parameters As List(Of ParameterBase), xhtmlOnly As Boolean, collectionsOnly As Boolean)
            For Each p As ParameterBase In parameterList
                If TypeOf p Is XHtmlParameter AndAlso Not parameters.Contains(p) AndAlso Not collectionsOnly Then
                    parameters.Add(p)
                ElseIf TypeOf p Is ResourceParameter AndAlso Not parameters.Contains(p) AndAlso Not xhtmlOnly AndAlso Not collectionsOnly Then
                    parameters.Add(p)
                ElseIf TypeOf p Is CollectionParameter Then
                    Dim cp = DirectCast(p, CollectionParameter).Value
                    If cp IsNot Nothing Then
                        LoopThroughParameters(cp.FlattenParameters().ToList, parameters, xhtmlOnly, collectionsOnly)
                    End If
                    If TypeOf p Is ScoringParameter Then
                        Dim properties = p.GetType().GetProperties().Where(Function(prop) prop.IsDefined(GetType(ParameterControlAttribute), False))
                        LoopThroughParameters((From propertyInfo In properties Select CType(propertyInfo.GetValue(p), ParameterBase)).ToList(), parameters, xhtmlOnly, collectionsOnly)
                    End If
                    If collectionsOnly Then parameters.Add(p)
                End If
            Next
        End Sub

        <Extension()>
        Public Sub FixReferencedAttributes(newParameters As ParameterSetCollection)
            Dim flattenedParameters = newParameters.FlattenParameters.ToList
            Dim collectionParameters = flattenedParameters.OfType(Of CollectionParameter).Where(Function(p) p.AttributeReferences.Count > 0)
            For Each collectionParameter In collectionParameters
                For Each attrRef In collectionParameter.AttributeReferences
                    Dim sourceParam = flattenedParameters.FirstOrDefault(Function(p) p.Name.Equals(attrRef.Value, StringComparison.OrdinalIgnoreCase))
                    Dim targetProp = collectionParameter.GetType().GetProperties().FirstOrDefault(Function(prop) prop.Name.Equals(attrRef.Name, StringComparison.OrdinalIgnoreCase))
                    If attrRef.WhatToCopy = AttributeReference.WhatToCpy.Value Then
                        Dim sourceValue = sourceParam.GetType().GetProperty("Value")
                        If targetProp.PropertyType.IsEnum Then
                            Dim newConvertedValue = [Enum].Parse(targetProp.PropertyType, sourceValue.GetValue(sourceParam).ToString)
                            targetProp.SetValue(collectionParameter, newConvertedValue)
                        Else
                            Dim newConvertedValue = Convert.ChangeType(sourceValue.GetValue(sourceParam), targetProp.PropertyType)
                            targetProp.SetValue(collectionParameter, newConvertedValue)
                        End If
                    ElseIf attrRef.WhatToCopy = AttributeReference.WhatToCpy.Parameter Then
                        targetProp.SetValue(collectionParameter, sourceParam)
                    End If
                Next
            Next
        End Sub

        <Extension()>
        Public Function AsListOfParameters(parameterSetCollection As ParameterSetCollection) As IEnumerable(Of ParameterBase)
            Return From collection In parameterSetCollection From param In collection.InnerParameters Select param
        End Function


    End Module
End Namespace
