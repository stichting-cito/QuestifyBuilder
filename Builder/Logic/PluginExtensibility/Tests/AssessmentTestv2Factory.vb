
Imports System.IO
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports System.Linq

Public Class AssessmentTestv2Factory

    Public Shared Property Plugins As IEnumerable(Of ITestModelPlugin)

    Private Shared Function TryDeserialize(Of T)(serializedObjectStream As Stream, ByRef deserializedObject As T) As Boolean
        Dim returnValue As Boolean = False

        Try
            serializedObjectStream.Seek(0, SeekOrigin.Begin)

            deserializedObject = DirectCast(SerializeHelper.XmlDeserializeFromStream(serializedObjectStream, GetType(T)), T)
            returnValue = True
        Catch ex As Exception
        End Try

        Return returnValue
    End Function

    Public Shared Function ContainsView(model As AssessmentTest2, viewType As String) As Boolean
        Return model.IncludedViews.Contains(viewType)
    End Function

    Public Shared Function CreateItemReferenceAndViews(viewTypes As List(Of String)) As CreatedTestNodeAndViews(Of ItemReference2, ItemReferenceViewBase)
        Dim newItemReference As New ItemReference2()

        Dim returnValue As New CreatedTestNodeAndViews(Of ItemReference2, ItemReferenceViewBase)(newItemReference)

        For Each viewToCreate In viewTypes
            Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewToCreate))

            If (plugin Is Nothing) Then
                Throw New NotSupportedException("Unknown view type!")
            End If

            returnValue.Views.Add(plugin.GetItemReference(newItemReference))
        Next

        Return returnValue
    End Function

    Public Shared Function CreateNewItemReference(genericTestSection As TestSectionViewBase, addToSection As Boolean) As ItemReferenceViewBase
        Dim newItemReference As ItemReferenceViewBase = genericTestSection.CreateNewItemReference()

        If addToSection Then
            genericTestSection.Components.Add(newItemReference)
        End If

        Return newItemReference
    End Function

    Public Shared Function CreateNewTestPart(genericTest As AssessmentTestViewBase, addToTest As Boolean) As TestPartViewBase
        Dim newTestPart As TestPartViewBase = genericTest.CreateNewTestPart()
        If newTestPart Is Nothing Then
            Throw New NotSupportedException("Not possible to create test part of unknown view type")
        End If

        If addToTest Then
            genericTest.TestParts.Add(newTestPart)
        End If

        Return newTestPart
    End Function

    Public Shared Function CreateNewTestSection(genericTestPart As TestPartViewBase, addToTestPart As Boolean) As TestSectionViewBase
        Dim newTestSection As TestSectionViewBase = genericTestPart.CreateNewTestSection()

        If addToTestPart Then
            genericTestPart.Sections.Add(newTestSection)
        End If

        Return newTestSection
    End Function

    Public Shared Function CreateNewTestSection(genericTestSection As TestSectionViewBase, addToSection As Boolean) As TestSectionViewBase

        Dim newTestSection As TestSectionViewBase = genericTestSection.CreateNewTestSection()

        If addToSection Then
            genericTestSection.Components.Add(newTestSection)
        End If

        Return newTestSection
    End Function

    Public Shared Function CreateTemporaryTestPartView(testPartModel As TestPart2, viewType As String, assessmentTest As AssessmentTest2) As TestPartViewBase
        Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        Return plugin.GetTestPart(testPartModel, assessmentTest)
    End Function

    Public Shared Function CreateTemporaryTestSectionView(testSectionModel As TestSection2, viewType As String) As TestSectionViewBase
        Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        Return plugin.GetTestSection(testSectionModel)
    End Function

    Public Shared Function CreateTestPartAndViews(viewTypes As List(Of String), assessmentTest As AssessmentTest2) As CreatedTestNodeAndViews(Of TestPart2, TestPartViewBase)
        Dim newTestPart As New TestPart2()

        Dim returnValue As New CreatedTestNodeAndViews(Of TestPart2, TestPartViewBase)(newTestPart)

        For Each viewToCreate In viewTypes

            Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewToCreate))

            If (plugin Is Nothing) Then
                Throw New NotSupportedException("Unknown view type!")
            End If

            returnValue.Views.Add(plugin.ConstructTestPartView(newTestPart))
        Next

        Return returnValue
    End Function

    Public Shared Function CreateTestSectionAndViews(viewTypes As List(Of String), assessmentTest As AssessmentTest2) As CreatedTestNodeAndViews(Of TestSection2, TestSectionViewBase)
        Dim newTestSection As New TestSection2()

        Dim returnValue As New CreatedTestNodeAndViews(Of TestSection2, TestSectionViewBase)(newTestSection)

        For Each viewToCreate In viewTypes
            Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewToCreate))

            If (plugin Is Nothing) Then
                Throw New NotSupportedException("Unknown view type!")
            End If

            returnValue.Views.Add(plugin.ConstructTestSectionView(newTestSection))
        Next

        Return returnValue
    End Function

    Public Shared Function CreateView(Of TView As {New, AssessmentTestViewBase})(model As AssessmentTest2) As TView
        Dim returnValue As New TView()
        returnValue.AddDynamicPropertiesFromModel(model)
        returnValue.ValidateAllProperties()

        Dim viewType = DetermineViewType(GetType(TView))
        Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        For Each part As TestPart2 In model.TestParts
            returnValue.TestParts.Add(plugin.ConstructTestPartView(part))
        Next


        If Not model.IncludedViews.Contains(viewType) Then
            model.IncludedViews.Add(viewType)
        End If

        Return returnValue
    End Function

    Public Shared Function CreateView(model As AssessmentTest2, viewType As String) As AssessmentTestViewBase
        Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        Return plugin.CreateView(model)
    End Function

    Public Shared Function CreateViewFromOtherView(Of TView As {New, AssessmentTestViewBase})(model As AssessmentTestViewBase) As TView
        Return CreateView(Of TView)(model.TestModel)
    End Function


    Public Shared Sub DeleteViewFromModel(assessmentTestModel As AssessmentTest2, viewToDelete As String)
        If assessmentTestModel.IncludedViews.Contains(viewToDelete) Then
            DeleteViewPropertiesFromTestNode(assessmentTestModel, viewToDelete)
            For Each testPart In assessmentTestModel.TestParts
                DeleteViewPropertiesFromTestNode(testPart, viewToDelete)
            Next
            For Each section In assessmentTestModel.GetAllSectionsInTest()
                DeleteViewPropertiesFromTestNode(section, viewToDelete)
            Next
            assessmentTestModel.IncludedViews.Remove(viewToDelete)
        Else
            Throw New Exception("Cannot delete view from assessment test model because the view does not exist.")
        End If
    End Sub


    Private Shared Sub DeleteViewPropertiesFromTestNode(testNode As TestNodeBase, viewToDelete As String)
        Dim propertiesToDelete As New List(Of DynamicProperty)()

        For Each prop As DynamicProperty In testNode.Properties
            If prop.MappedToViews.Contains(viewToDelete) Then
                If prop.MappedToViews.Count = 1 Then
                    propertiesToDelete.Add(prop)
                Else
                    prop.MappedToViews.Remove(viewToDelete)
                End If
            End If
        Next

        For Each deleteProp As DynamicProperty In propertiesToDelete
            testNode.Properties.Remove(deleteProp)
        Next
    End Sub


    Public Shared Function DetermineViewType(viewType As Type) As String
        Dim plugin As ITestModelPlugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedTest(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        Return plugin.Name
    End Function

    Public Shared Function GetIncludedViews(model As AssessmentTest2) As List(Of String)
        Return model.IncludedViews
    End Function

    Public Shared Function GetParentTestpartOfTestSectionView(section As TestSectionViewBase) As TestPartViewBase
        Dim testPart As TestPartViewBase = Nothing
        Dim isTestpart As Boolean = False
        Dim recurseTestSection As TestSectionViewBase = section
        While isTestpart = False
            If TypeOf recurseTestSection.Parent Is TestSectionViewBase Then
                recurseTestSection = DirectCast(recurseTestSection.Parent, TestSectionViewBase)
            ElseIf TypeOf recurseTestSection.Parent Is TestPartViewBase Then
                isTestpart = True
                testPart = DirectCast(recurseTestSection.Parent, TestPartViewBase)
            End If
        End While
        Return testPart
    End Function


    Public Shared Function ReturnAssessmentTestv2ModelFromByteArray(serializedBytes As Byte(), allowTransformationOfOldModel As Boolean) As ReturnedAssessmentTestModelInfo
        Dim returnValue As ReturnedAssessmentTestModelInfo = Nothing

        Using objectStream As New MemoryStream(serializedBytes)
            returnValue = ReturnAssessmentTestv2ModelFromStream(objectStream, allowTransformationOfOldModel)
        End Using

        Return returnValue
    End Function

    Public Shared Function ReturnAssessmentTestv2ModelFromStream(serializedObjectStream As Stream, allowTransformationOfOldModel As Boolean) As ReturnedAssessmentTestModelInfo
        Dim testModelv2 As AssessmentTest2 = Nothing
        If Not TryDeserialize(Of AssessmentTest2)(serializedObjectStream, testModelv2) Then
            Throw New ContentModelException("Serialized object isn't an assessment test model (old or new)!")
        End If

        Return New ReturnedAssessmentTestModelInfo(testModelv2)
    End Function

End Class