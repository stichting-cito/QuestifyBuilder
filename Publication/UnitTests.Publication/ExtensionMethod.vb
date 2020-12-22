Imports System.Runtime.CompilerServices
Imports System.Xml
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports System.IO

Module ExtensionMethod
    <Extension()>
    Function ToXmlDocument(ByVal element As XElement) As XmlDocument
        Using xmlReader As XmlReader = element.CreateReader()
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(xmlReader)
            Return xmlDoc
        End Using
    End Function

    <Extension()>
    Function ToXmlDocument(ByVal xDocument As XDocument) As XmlDocument
        Using xmlReader As XmlReader = xDocument.CreateReader()
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(xmlReader)
            Return xmlDoc
        End Using
    End Function

    <Extension>
    Function ToXDocument(xElement As XElement) As XDocument
        Return XDocument.Parse(xElement.ToString())
    End Function

    <Extension>
    Function ToXDocument(xmlDocument As XmlDocument) As XDocument
        Return XDocument.Parse(xmlDocument.OuterXml)
    End Function


    <Extension>
    Public Sub SetAssessmentToAssessmentResource(resource As AssessmentTestResourceEntity, assessmentTest As AssessmentTest2)
        If (resource.ResourceData Is Nothing) Then
            resource.ResourceData = New ResourceDataEntity
        End If
        resource.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(assessmentTest)
    End Sub

    <Extension>
    Public Sub SetXmlAsBinData(resource As ResourceEntity, data As XElement)

        If (resource.ResourceData Is Nothing) Then resource.ResourceData = New ResourceDataEntity

        Using stream = New MemoryStream()
            Cito.Tester.Common.SerializeHelper.XmlSerializeToStream(stream, data)
            resource.ResourceData.BinData = stream.ToArray()
        End Using

    End Sub

    <Extension>
    Public Function AddSubParameters(Of TScorePrm As ScoringParameter)(scoreParam As TScorePrm,
                                                                        ParamArray ids As String()) As TScorePrm
        scoreParam.Value = New ParameterSetCollection()
        For Each id In ids
            scoreParam.Value.Add(New ParameterCollection() With {.Id = id})
        Next
        Return scoreParam
    End Function

End Module
