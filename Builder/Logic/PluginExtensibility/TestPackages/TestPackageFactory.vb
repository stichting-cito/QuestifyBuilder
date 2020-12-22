Imports System.IO
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class TestPackageFactory

    Public Shared Property Plugins As IEnumerable(Of ITestPackageModelPlugin)

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

    Public Shared Function CreateTestReferenceAndViews(viewTypes As List(Of String)) As CreatedTestPackageNodeAndViews(Of TestReference, TestReferenceViewBase)
        Dim newTestReference As New TestReference()

        Dim returnValue As New CreatedTestPackageNodeAndViews(Of TestReference, TestReferenceViewBase)(newTestReference)

        For Each viewToCreate As String In viewTypes
            Dim plugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewToCreate))

            If (plugin Is Nothing) Then
                Throw New NotSupportedException("Unknown view type!")
            End If

            returnValue.Views.Add(plugin.ConstructTestReferenceView(newTestReference))
        Next

        Return returnValue
    End Function

    Public Shared Function CreateTemporaryTestSetView(testSetModel As TestSet, viewType As String) As TestSetViewBase
        Dim plugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        Return plugin.GetTestSet(testSetModel)
    End Function

    Public Shared Function CreateTestSetAndViews(viewTypes As List(Of String)) As CreatedTestPackageNodeAndViews(Of TestSet, TestSetViewBase)
        Dim newTestSet As New TestSet()

        Dim returnValue As New CreatedTestPackageNodeAndViews(Of TestSet, TestSetViewBase)(newTestSet)

        For Each viewToCreate In viewTypes
            Dim plugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewToCreate))

            If (plugin Is Nothing) Then
                Throw New NotSupportedException("Unknown view type!")
            End If

            returnValue.Views.Add(plugin.ConstructTestSetView(newTestSet))
        Next

        Return returnValue
    End Function


    Public Shared Function CreateView(Of TView As {New, TestPackageViewBase})(model As TestPackage) As TView
        Dim returnValue As New TView()
        returnValue.AddDynamicPropertiesFromModel(model)
        returnValue.ValidateAllProperties()

        Dim viewType = DetermineViewType(GetType(TView))

        Dim plugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        For Each testset As TestSet In model.TestSets
            returnValue.TestSets.Add(plugin.ConstructTestSetView(testset))
        Next

        If Not model.IncludedViews.Contains(viewType.ToString()) Then
            model.IncludedViews.Add(viewType.ToString())
        End If

        If Not model.IncludedViews.Contains(GenericTestModelPlugin.PLUGIN_NAME) Then
            model.IncludedViews.Add(GenericTestModelPlugin.PLUGIN_NAME)
        End If

        Return returnValue
    End Function

    Public Shared Function CreateView(model As TestPackage, viewType As String) As TestPackageViewBase
        Dim plugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedView(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        Return plugin.CreateView(model)
    End Function

    Public Shared Function DeleteViewFromModel(testPackageModel As TestPackage, viewToDelete As String) As Boolean
        Dim propertiesToDelete As New List(Of DynamicProperty)()

        If testPackageModel.IncludedViews.Contains(viewToDelete) Then
            For Each prop As DynamicProperty In testPackageModel.Properties
                If prop.MappedToViews.Contains(viewToDelete) Then
                    If prop.MappedToViews.Count = 1 Then
                        propertiesToDelete.Add(prop)
                    Else
                        prop.MappedToViews.Remove(viewToDelete)
                    End If
                End If
            Next

            For Each deleteProp As DynamicProperty In propertiesToDelete
                testPackageModel.Properties.Remove(deleteProp)
            Next

            testPackageModel.IncludedViews.Remove(viewToDelete)
        Else
            Throw New Exception("Cannot delete view from testpackage model because the view does not exist.")
        End If
    End Function

    Public Shared Function DetermineViewType(viewType As Type) As String
        Dim plugin = Plugins.FirstOrDefault(Function(p) p.IsSupportedTestPackage(viewType))

        If (plugin Is Nothing) Then
            Throw New NotSupportedException("Unknown view type!")
        End If

        Return plugin.Name
    End Function

    Public Shared Function GetIncludedViews(model As TestPackage) As List(Of String)
        Return model.IncludedViews
    End Function

    Public Shared Function ReturnTestPackageModelFromByteArray(serializedBytes As Byte()) As TestPackage
        Dim returnValue As TestPackage = Nothing

        Using objectStream As New MemoryStream(serializedBytes)
            returnValue = ReturnTestPackageModelFromByteArray(objectStream)
        End Using

        Return returnValue
    End Function

    Public Shared Function ReturnTestPackageModelFromByteArray(serializedObjectStream As Stream) As TestPackage
        Dim testPackageModel As TestPackage = Nothing

        If Not TryDeserialize(Of TestPackage)(serializedObjectStream, testPackageModel) Then
            Throw New ContentModelException("Serialized object isn't an testPackageModel!")
        End If

        Return testPackageModel
    End Function

End Class