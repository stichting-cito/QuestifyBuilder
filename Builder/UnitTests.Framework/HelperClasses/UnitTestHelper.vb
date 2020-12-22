Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Drawing
Imports System.Text
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

Public Class UnitTestHelper

    Public Shared Sub CompareFiles(output As XDocument, expectedOutput As XDocument)
#If DEBUG Then
        Dim file1 As String = Path.Combine(Path.GetTempPath, "output.xml")
        Dim file2 As String = Path.Combine(Path.GetTempPath, "expected.xml")
        output.Save(file1)
        expectedOutput.Save(file2)
        Dim p = New Process()
        p.StartInfo.FileName = "devenv"
        p.StartInfo.Arguments = String.Format(" /diff ""{0}"" ""{1}""", file1, file2)
        p.Start()
#End If
    End Sub

    Public Shared Function AreSame(ByVal output As ItemResourceEntity, ByVal expectedResult As XElement, Optional setFixedIdsBeforeComparing As Boolean = True) As Boolean
        Dim expected As New XDocument()
        expected.Add(expectedResult)
        If setFixedIdsBeforeComparing Then SetFixedIdsToCompare(expected)

        Dim result As XDocument = XDocument.Parse(Encoding.UTF8.GetString(output.ResourceData.BinData))
        If setFixedIdsBeforeComparing Then SetFixedIdsToCompare(result)

        Dim compareResult = XmlTools.DeepEqualsWithNormalization(expected, result, Nothing)
        If Not compareResult Then
            CompareFiles(result, expected)
        End If

        Return compareResult
    End Function

    Public Shared Function AreSame(expectedXml As String, actualXml As String) As Boolean
        Dim expected = XDocument.Parse(expectedXml)
        Dim actual = XDocument.Parse(actualXml)
        Dim areEqual = DocumentsAreSame(expected, actual)
        If (Not areEqual) Then CompareFiles(actual, expected)
        Return areEqual
    End Function

    Public Shared Function AreSame(expectedXml As XElement, actualXml As XmlDocument) As Boolean
        Dim expected = New XDocument(expectedXml)
        Dim actual = actualXml.GetXDocument()
        Dim areEqual = DocumentsAreSame(expected, actual)
        If (Not areEqual) Then CompareFiles(actual, expected)
        Return areEqual
    End Function

    Public Shared Function AreSame(expectedXml As XElement, actualXml As XElement) As Boolean
        Dim expected = New XDocument(expectedXml)
        Dim actual = New XDocument(actualXml)
        Dim areEqual = DocumentsAreSame(expected, actual)
        If (Not areEqual) Then CompareFiles(actual, expected)
        Return areEqual
    End Function

    Public Shared Function AreSame(expectedXml As XDocument, actualXml As XDocument) As Boolean
        Dim areEqual = DocumentsAreSame(expectedXml, actualXml)
        If (Not areEqual) Then CompareFiles(actualXml, expectedXml)
        Return areEqual
    End Function

    Public Shared Sub SetFixedIdsToCompare(xdoc As XDocument)
        For Each el In xdoc.Descendants.Where(Function(x) x.Attributes("id").Count = 1)
            el.Attributes("id")(0).Value = "fixedid"
        Next

        For Each el In xdoc.Descendants.Where(Function(x) x.Name.LocalName.Equals("keyValue") AndAlso x.Attributes("domain").Any())
            el.Attributes("domain")(0).Value = "fixedid"
        Next

        For Each el in xdoc.Descendants.Where(Function(x) x.Name.LocalName.Equals("plaintextparameter") AndAlso x.Attributes("name").FirstOrDefault(Function(y) y.Value = "gapId") IsNot Nothing)
            el.Value = "fixedid"
        Next

        For Each el In xdoc.Descendants.Where(Function(x) x.Name.LocalName.EndsWith("ScoringParameter"))
            If (el.Attributes("name").Any())
                el.Attributes("name").First().Value = "fixedid"
            End If

            If el.Attributes("ControllerId").Any()
                el.Attributes("ControllerId").First().Value = "fixedid"
            End If
        Next
    End Sub

    Public Shared Function SetFixedIdsToCompare(html As String) As String
        Dim xdoc = XDocument.Parse(html)
        SetFixedIdsToCompare(xdoc)
        Return xdoc.ToString()
    End Function

    Public Shared Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Private Shared Function DocumentsAreSame(expectedXml As XDocument, actualXml As XDocument) As Boolean
        Return XmlTools.DeepEqualsWithNormalization(actualXml, expectedXml, Nothing)
    End Function

    Public Shared Function ImageToByte2(img As Image) As Byte()
        Dim byteArray As Byte() = New Byte(-1) {}
        Using stream As New MemoryStream()
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
            stream.Close()

            byteArray = stream.ToArray()
        End Using
        Return byteArray
    End Function

End Class

