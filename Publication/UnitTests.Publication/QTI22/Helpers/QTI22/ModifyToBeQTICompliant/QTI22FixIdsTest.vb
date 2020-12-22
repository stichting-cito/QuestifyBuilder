
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper
Imports Questify.Builder.Logic.QTI.Helpers.QTI22

<TestClass()> Public Class QTI22FixIdsTest
    <TestMethod(), TestCategory("FACET Document Tweaks")>
    Public Sub FixDoubleIds()
        Dim xDoc = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" adaptive="false" identifier="ITM-300504" title="Aanzichten" timeDependent="false" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                       <strong>A, B of C ?</strong>
                       <mediaInteraction responseIdentifier="AUDIORESPONSE" autostart="true" maxPlays="2147483647" minPlays="0" loop="false" id="qtiAudio">
                           <object type="audio/mpeg" data="../audio/C_SX_FR_Hoe_A0024_00008-00018_Audio1.mp3" height="24" width="200"/>
                       </mediaInteraction>
                       <mediaInteraction responseIdentifier="AUDIORESPONSE2" autostart="true" maxPlays="2147483647" minPlays="0" loop="false" id="qtiAudio">
                           <object type="audio/mpeg" data="../audio/C_SX_FR_Hoe_A0024_00008-00018_Audio2.mp3" height="24" width="200"/>
                       </mediaInteraction>
                       <mediaInteraction responseIdentifier="AUDIORESPONSE3" autostart="true" maxPlays="2147483647" minPlays="0" loop="false" id="qtiAudio">
                           <object type="audio/mpeg" data="../audio/C_SX_FR_Hoe_A0024_00008-00018_Audio3.mp3" height="24" width="200"/>
                       </mediaInteraction>
                       <mediaInteraction responseIdentifier="AUDIORESPONSE4" autostart="true" maxPlays="2147483647" minPlays="0" loop="false" id="qtiAudio">
                           <object type="audio/mpeg" data="../audio/C_SX_FR_Hoe_A0024_00008-00018_Audio4.mp3" height="24" width="200"/>
                       </mediaInteraction>
                   </assessmentItem>
        Dim xmlDoc = xDoc.ToXmlDocument
        Dim fp As New FixIds
        fp.Modify(xmlDoc, New QTI22DocumentHelper())
        Assert.IsTrue(AreIdsUniques(xmlDoc))
    End Sub

    Private Function AreIdsUniques(xmlDoc As XmlDocument) As Boolean
        Dim isUnique As Boolean = True
        Dim listOfIds As New List(Of String)
        For Each node As XmlNode In xmlDoc.SelectNodes("//*[@id]")
            Dim id = node.Attributes("id").Value
            If Not listOfIds.Contains(id) Then
                listOfIds.Add(id)

            Else
                isUnique = False
                Exit For
            End If
        Next
        Return isUnique
    End Function


End Class
