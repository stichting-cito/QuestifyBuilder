Imports System.Xml
Imports System.Xml.Schema
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq

Public NotInheritable Class MathMLValidator

    Private errorsAndWarnings As New List(Of String)()

    Public Shared Function IsValidMathML(mathML As String) As Boolean
        Dim validator As New MathMLValidator()
        Return validator.Validate(mathML).Count() = 0
    End Function

    Public Shared Function ValidateMathML(mathML As String) As IEnumerable(Of String)
        Dim validator As New MathMLValidator()
        Return validator.Validate(mathML)
    End Function

    Friend Function Validate(mathML As String) As IEnumerable(Of String)
        ValidateMathMLNamespace(mathML)
        ValidateMathMLAgainstSchema(mathML)
        Return errorsAndWarnings
    End Function

    Private Sub ValidateMathMLNamespace(mathML As String)

        Try
            Dim xel As XElement = XElement.Parse(mathML)
            Dim ns As String = xel.GetDefaultNamespace().NamespaceName
            If String.IsNullOrEmpty(ns) AndAlso xel.GetNamespaceOfPrefix("m") IsNot Nothing Then
                ns = xel.GetNamespaceOfPrefix("m").NamespaceName
            End If
            Dim mathMLNamespace As String = "http://www.w3.org/1998/Math/MathML"
            If String.IsNullOrEmpty(ns) OrElse ns <> mathMLNamespace Then
                errorsAndWarnings.Add($"ERROR: MathML namespace {mathMLNamespace} not found.")
            End If
        Catch ex As Exception
            errorsAndWarnings.Add($"ERROR: {ex.Message}")
        End Try

    End Sub

    Private Sub ValidateMathMLAgainstSchema(mathML As String)
        If errorsAndWarnings.Count > 0 Then Return

        Dim xmlReaderSettings As XmlReaderSettings = CreateXMLReaderSettingsWithMathMLSchema()
        Dim mathMLXmlReader As XmlReader = XmlReader.Create(New StringReader(mathML), xmlReaderSettings)

        Try
            While mathMLXmlReader.Read()
            End While
        Catch ex As Exception
            errorsAndWarnings.Add($"ERROR: {ex.Message}")
        End Try
    End Sub

    Private Function CreateXMLReaderSettingsWithMathMLSchema() As XmlReaderSettings

        Dim xmlReaderSettings As XmlReaderSettings = New XmlReaderSettings()
        xmlReaderSettings.Schemas.Add("http://www.w3.org/1998/Math/MathML", XmlReader.Create(New StringReader(My.Resources.mathml3_strict_content)))
        xmlReaderSettings.Schemas.Add("http://www.w3.org/1998/Math/MathML", XmlReader.Create(New StringReader(My.Resources.mathml3_content)))
        xmlReaderSettings.Schemas.Add("http://www.w3.org/1998/Math/MathML", XmlReader.Create(New StringReader(My.Resources.mathml3_presentation)))
        xmlReaderSettings.Schemas.Add("http://www.w3.org/1998/Math/MathML", XmlReader.Create(New StringReader(My.Resources.mathml3_common)))
        xmlReaderSettings.Schemas.Add("http://www.w3.org/1998/Math/MathML", XmlReader.Create(New StringReader(My.Resources.mathml3)))

        xmlReaderSettings.Schemas.Compile()

        xmlReaderSettings.ValidationType = ValidationType.Schema
        AddHandler xmlReaderSettings.ValidationEventHandler, New ValidationEventHandler(AddressOf mathMLValidationEventHandler)

        Return xmlReaderSettings
    End Function

    Private Sub mathMLValidationEventHandler(sender As Object, e As ValidationEventArgs)

        If e.Severity = XmlSeverityType.Warning Then
            errorsAndWarnings.Add($"WARNING: {e.Message}")
        ElseIf e.Severity = XmlSeverityType.Error Then
            errorsAndWarnings.Add($"ERROR: {e.Message}")
        End If

    End Sub

End Class
