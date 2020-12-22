
Imports Cito.Tester.Common
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.Service.Interfaces
Imports System.IO

Namespace HelperClasses
    Public Class GeneralHelper

        Public Shared Function GetNameFromVariable(o As Object) As String
            If o IsNot Nothing Then
                Dim resource = TryCast(o, ResourceEntity)
                If resource IsNot Nothing Then
                    Return $"type: {resource.GetType.Name}, bank: {resource.BankId}, name: {resource.Name}"
                Else
                    Return o.ToString
                End If
            End If
            Return ""
        End Function


        Public Shared Function CreateItemPreviewHandlers(ByVal resourceManager As ResourceManagerBase) As List(Of IItemPreviewHandler)
            Dim itemPreviewHandlerNames As ItemPreviewHandlerCollection = DirectCast(ConfigPluginHelper.GetListOfPluginHandlersBySectionName("itemPreviewHandlers"), ItemPreviewHandlerCollection)
            Dim itemPreviewHandlers As New List(Of IItemPreviewHandler)()

            For Each itemPreviewHandler As ItemPreviewHandler In itemPreviewHandlerNames
                Dim previewer = TryCreateInstanceOfItemPreviewHandler(itemPreviewHandler, resourceManager)
                If previewer IsNot Nothing Then
                    itemPreviewHandlers.Add(previewer)
                End If
            Next

            Return itemPreviewHandlers
        End Function

        Private Shared Function TryCreateInstanceOfItemPreviewHandler(itemPreviewHandler As ItemPreviewHandler, resourceManager As ResourceManagerBase) As IItemPreviewHandler
            Try
                Return DirectCast(Activator.CreateInstance(Type.GetType(itemPreviewHandler.Type, True), itemPreviewHandler.HandlerConfig, resourceManager), IItemPreviewHandler)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function


        Public Shared Function GetViewsWithoutGeneral(includedViews As List(Of String)) As List(Of String)
            Dim returnValue As New List(Of String)(includedViews)
            If returnValue.Contains(GenericTestModelPlugin.PLUGIN_NAME) Then
                returnValue.Remove(GenericTestModelPlugin.PLUGIN_NAME)
            End If

            Return returnValue
        End Function


        Public Shared Function GetSerializedEntityFromEntityToPublish(ByVal resourceId As Guid) As Byte()
            Return ResourceFactory.Instance.GetResourceDataByResourceId(resourceId).BinData
        End Function


        Public Shared Function DoesTestContainsItems(ByVal test As AssessmentTest2) As Boolean
            Dim returnValue As Boolean = False

            For Each testPart As TestPart2 In test.TestParts
                For Each section As TestSection2 In testPart.Sections
                    If DoesTestSectionContainsItems(section) Then
                        returnValue = True
                        Exit For
                    End If
                Next

                If returnValue Then Exit For
            Next

            Return returnValue
        End Function




        Private Shared Function DoesTestSectionContainsItems(ByVal section As TestSection2) As Boolean
            Dim returnValue As Boolean = False

            For Each component As TestComponent2 In section.Components
                If TypeOf component Is ItemReference2 Then
                    returnValue = True
                    Exit For
                ElseIf TypeOf component Is TestSection2 Then
                    If DoesTestSectionContainsItems(DirectCast(component, TestSection2)) Then
                        returnValue = True
                        Exit For
                    End If
                End If
            Next

            Return returnValue
        End Function

    End Class
End Namespace