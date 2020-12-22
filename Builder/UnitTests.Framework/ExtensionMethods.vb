Imports System.Runtime.CompilerServices
Imports System.Xml.Linq
Imports System.Linq
Imports System.Xml
Imports System.IO
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports System.Diagnostics
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Xml.Serialization

Public Module ExtensionMethods
    <Extension()>
    Public Function Deserialize(Of T As New)(ByVal xml As System.Xml.Serialization.XmlSerializer, ByVal data As String) _
        As T

        Dim ret As T
        Using strm As New System.IO.StringReader(data)
            ret = DirectCast(xml.Deserialize(strm), T)
        End Using

        Return ret
    End Function

    Public Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    <Extension()>
    Function DoSerialize(Of T)(obj As T) As XElement
        Dim s = New XmlSerializer(GetType(T))
        Dim ret As XElement = Nothing
        Using m As New StringWriter()
            s.Serialize(m, obj)
            ret = XElement.Parse(m.ToString())
        End Using
        Return ret
    End Function

    <Extension()>
    Public Function Serialize(Of T)(ByVal xml As XmlSerializer, ByVal data As T) As String

        Dim ret As String
        Using strm As New StringWriter
            xml.Serialize(strm, data)
            ret = strm.ToString()
        End Using
        Return ret
    End Function

    <Extension()>
    Function ToXmlNode(ByVal element As XElement) As XmlNode
        Using xmlReader As XmlReader = element.CreateReader()
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(xmlReader)
            Return xmlDoc.FirstChild
        End Using
    End Function

    <Extension>
    Public Function SetXmlAsBinData(resource As ResourceEntity, data As XElement) As Boolean
        If (resource.ResourceData Is Nothing) Then resource.ResourceData = New ResourceDataEntity With {.FileExtension = ".xml"}

        Using stream = New MemoryStream()
            SerializeHelper.XmlSerializeToStream(stream, data)
            resource.ResourceData.BinData = stream.ToArray()
        End Using
        Return True
    End Function

    <Extension>
    Public function SetAssessmentToAssessmentResource(resource As AssessmentTestResourceEntity, assessmentTest As AssessmentTest2) As Boolean
        If (resource.ResourceData Is Nothing) Then
            resource.ResourceData = New ResourceDataEntity With {.FileExtension = ".xml"}
        End If
        resource.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(assessmentTest)
        Return True
    End function

    <Extension>
    Public Function AddSubParameters(Of TScorePrm As ScoringParameter)(scoreParam As TScorePrm,
                                                                        ParamArray ids As String()) As TScorePrm
        scoreParam.Value = New ParameterSetCollection()
        For Each id In ids
            scoreParam.Value.Add(New ParameterCollection() With {.Id = id})
        Next
        Return scoreParam
    End Function

    <Extension>
    Public Function AsCombinedScoringMap(scoringParameter As ScoringParameter, ParamArray setNumbers As Integer()) _
        As CombinedScoringMapKey
        Dim ret = New List(Of ScoringMapKey)()
        For Each k In scoringParameter.Value
            ret.Add(New ScoringMapKey(scoringParameter, k.Id))
        Next
        Return CombinedScoringMapKey.Create(ret, setNumbers)
    End Function

    <Extension>
    Public Function AsCombinedScoringMap(scoringParameter As ScoringParameter, solution As Solution) _
        As CombinedScoringMapKey
        Return New ScoringMap(New ScoringParameter() {scoringParameter}, solution).GetMap().First()
    End Function

    <Extension>
    Public Function AsScoringMapKey(scoringParameter As ScoringParameter, factId As String) As ScoringMapKey
        Debug.Assert(scoringParameter.Value.Any(Function(subParamSet) subParamSet.Id = factId),
                     "FactId was not found!?!?")
        Return New ScoringMapKey(scoringParameter, factId)
    End Function

    <Extension>
    Public Function [To](Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    <Extension>
    Public Sub WriteToDebug(s As Solution, stateName As String)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub
End Module