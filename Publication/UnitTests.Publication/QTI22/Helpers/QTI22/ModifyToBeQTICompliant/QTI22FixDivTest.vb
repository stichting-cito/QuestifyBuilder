
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.UnitTests.Framework

Namespace QTI22.Helpers

    <TestClass()>
    Public Class QTI22FixDivTest

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub FixPopupInStrong()
            'Arrange 
            Dim xDoc = <?xml version="1.0" encoding="utf-8"?><assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" adaptive="false" identifier="ITM-300504" title="Aanzichten" timeDependent="false" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1"><strong>Welke van de drie aanzichten A, B of C is het rechter zijaanzicht van het huis?<br/><br/><div id="QTIWCT_e44a92e3-6f7f-40f6-a1a6-cc6333ad2747"><img id="Id-IMG_39b39e8f-9c1a-4cb7-b220-142b81569f1c" src="img/aanzichten_2.jpg" width="1095" height="807" alt=""/></div></strong></assessmentItem>
            Dim xpectedOutcome = <?xml version="1.0" encoding="utf-8"?><assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" adaptive="false" identifier="ITM-300504" title="Aanzichten" timeDependent="false" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1"><strong>Welke van de drie aanzichten A, B of C is het rechter zijaanzicht van het huis?<br/><br/></strong><div id="QTIWCT_e44a92e3-6f7f-40f6-a1a6-cc6333ad2747"><img id="Id-IMG_39b39e8f-9c1a-4cb7-b220-142b81569f1c" src="img/aanzichten_2.jpg" width="1095" height="807" alt=""/></div><strong></strong></assessmentItem>
            Dim xmlDoc = xDoc.ToXmlDocument
            Dim fp As New FixDiv
            'Act
            fp.Modify(xmlDoc, New QTI22DocumentHelper())
            'Assert
            Dim xOutcome = XDocument.Parse(xmlDoc.OuterXml)
            Assert.IsTrue(UnitTestHelper.AreSame(xpectedOutcome, xOutcome))
        End Sub

    End Class

End Namespace