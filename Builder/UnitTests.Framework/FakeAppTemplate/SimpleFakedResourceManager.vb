Imports System.IO
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses

Namespace FakeAppTemplate

    Public Class SimpleFakedResourceManager
        Inherits ResourceManagerBase

        Private ReadOnly _resourceCollection As EntityCollection

        Sub New(resourceCollection As EntityCollection)
            Debug.Assert(resourceCollection IsNot Nothing)
            _resourceCollection = resourceCollection
        End Sub

        Public ReadOnly Property ResourceCollection As EntityCollection
            Get
                Return _resourceCollection
            End Get
        End Property

        Public Overloads Overrides Function GetResource(name As String, processingMethod As ResourceProcessingFunction, Request As ResourceRequestDTO) As BinaryResource

            Select Case name
                Case "InlineVideoLayoutTemplate"
                    Debug.Assert(False)
                Case "InlineAudioLayoutTemplate"
                    Debug.Assert(False)

                Case Else
                    For Each e As ResourceEntity In ResourceCollection
                        If e.Name = name Then
                            Dim _2ret As BinaryResource

                            Using stream = New MemoryStream(e.ResourceData.BinData)
                                Dim strmResource = New StreamResource(stream, stream.Length)
                                Dim tmp = processingMethod.Invoke(name, strmResource, Nothing)
                                _2ret = New BinaryResource(name, Nothing, tmp, Nothing)

                            End Using
                            Return _2ret

                        End If
                    Next
                    Debug.Assert(False)
            End Select
            Return Nothing
        End Function

        Public Overrides Function GetResource(name As String) As StreamResource
            Select Case name
                Case "InlineVideoLayoutTemplate",
                    "InlineAudioLayoutTemplate"
                    Return New StreamResource(New IO.MemoryStream(New Byte() {1}), 1)

                Case Else
                    For Each e As ResourceEntity In ResourceCollection
                        If e.Name = name Then
                            Return New StreamResource(New IO.MemoryStream(New Byte() {1}), 1)
                        End If
                    Next

                    Debug.Assert(False)
            End Select
            Return Nothing
        End Function

        Public Overrides Function GetResource(name As String, request As ResourceRequestDTO) As StreamResource
            Return GetResource(name)
        End Function

        Public Overrides Function GetResourceMetaData(name As String) As MetaDataCollection
            Return New MetaDataCollection() From {New MetaData("ItemType", "Choice", MetaData.enumMetaDataType.BankMetaData)}
        End Function

        Public Overrides Function GetResourcesOfType(type As String) As ResourceEntryCollection
            Throw New NotImplementedException
        End Function

        Public Overrides Function GetTypedResource(name As String, usingType As Type, request As ResourceRequestDTO) As BinaryResource
            Throw New NotImplementedException()
        End Function

    End Class
End NameSpace