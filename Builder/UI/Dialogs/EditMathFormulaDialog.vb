Imports Cito.Tester.Common
Imports System.Net
Imports System.Xml
Imports System.Xml.Linq
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.HelperClasses
Imports System.Linq

Public Class EditMathFormulaDialog

    Public Event EditFormula As EventHandler(Of FormulaEventArgs)

    Private _mathMlEditorControl As IMathMlEditorControl
    Private _mathMlEditorPlugin As IMathMlEditorPlugin
    Private ReadOnly _mathMLImage As Byte()
    Private ReadOnly _mathMl_value As String
    Private _imageName As String = String.Empty
    Private _font As Font = Nothing
    Private Const PNG_MIME As String = "image/png"
    Private _isInContentMode As Boolean = False

    Public Sub New(
            ByVal mathMLImage As Byte(),
            ByVal imageName As String,
            ByVal font As Font,
            ByVal isInContentMode As Boolean,
            Optional ByVal mathMlValue As String = "")
        InitializeComponent()

        _isInContentMode = isInContentMode
        _mathMLImage = mathMLImage
        _mathMl_value = mathMlValue
        _imageName = imageName
        _font = font

        GetMathMlEditor()
    End Sub

    Public Sub New(ByVal mathMLImage As Byte(), ByVal imageName As String, ByVal isInContentMode As Boolean)
        Me.New(mathMLImage, imageName, MathMLHelper.GetBaseFont(), isInContentMode, String.Empty)
    End Sub

    Private Sub GetMathMlEditor()
        _mathMlEditorPlugin = Logic.PluginHelper.MathMlPlugin
        If _mathMlEditorPlugin IsNot Nothing Then
            _mathMlEditorControl = _mathMlEditorPlugin.GetMathMlEditorControl(_isInContentMode)
            ContentPanel.Controls.Add(_mathMlEditorControl)
            ContentPanel.Controls(0).Dock = DockStyle.Fill
            EditMathMlInEditor()
        Else
            MessageBox.Show(My.Resources.MathML_NoPlugin)
            Me.OKButton.Enabled = False
        End If
    End Sub

    Private Sub EditMathMlInEditor()
        If _mathMLImage IsNot Nothing AndAlso ValidateMimeType(_mathMLImage) Then
            Dim metadata As String = _mathMl_value
            If (metadata = String.Empty) Then
                metadata = MathMLHelper.GetMetaDataFromPngImage(_mathMLImage)
            End If
            If Not String.IsNullOrEmpty(metadata) Then
                _mathMlEditorControl.EditMathMl(metadata)
            End If
        End If
    End Sub

    Private Function ValidateMimeType(ByVal mathMLImage As Byte()) As Boolean
        Dim parsedUri As Uri = Nothing
        Dim mimeType As String = String.Empty

        If Uri.TryCreate(_imageName, UriKind.Absolute, parsedUri) AndAlso parsedUri.Scheme = "file" Then
            _imageName = parsedUri.LocalPath
        End If

        mimeType = FileHelper.GetMimeFromByteArray(New Uri(TempStorageHelper.GetTempFilename()).ToString(), mathMLImage)

        If Not (mimeType = PNG_MIME) Then
            Throw New ArgumentException(String.Format("'{0}' is an unsupported image format.", mimeType))
        End If

        Return True
    End Function

    Private Function GetMetaDataFromImage(ByVal mathMLImage As Byte()) As String
        If _mathMLImage IsNot Nothing AndAlso ValidateMimeType(_mathMLImage) Then
            Return MathMLHelper.GetMetaDataFromPngImage(mathMLImage)
        End If
        Return String.Empty
    End Function

    Protected Overrides Function OnOk() As Boolean
        Dim mathMl = _mathMlEditorControl.GetMathMl()

        If _isInContentMode Then
            Dim doc As New XmlDocument()
            Dim namespaceManager As New XmlNamespaceManager(doc.NameTable)
            namespaceManager.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML")
            doc.LoadXml(mathMl)

            If doc.SelectSingleNode("//mml:merror", namespaceManager) IsNot Nothing Then
                MessageBox.Show(My.Resources.MathML_Syntax_Error & vbNewLine & doc.SelectSingleNode("//mml:mtext", namespaceManager).InnerXml)
                Return False
            End If

            If String.IsNullOrEmpty(mathMl) Then
                MessageBox.Show(My.Resources.MathML_LayoutError)
                Return False
            End If
        End If

        SetMathMLDataInImage(mathMl)

        Me.DialogResult = DialogResult.OK

        Return True
    End Function

    Friend Function SetMathMLDataInImage(mathMl As String) As Byte()
        Dim image As Byte() = Nothing
        Dim imageWithMetaData As Byte() = Nothing

        Dim verticalAlignValue As Double
        If String.IsNullOrEmpty(mathMl) Then
            Dim metaData As String = GetMetaDataFromImage(_mathMLImage)
            image = CreateImageFromMathML(metaData, False, verticalAlignValue)
            If image IsNot Nothing AndAlso image.Length > 0 Then
                imageWithMetaData = MathMLHelper.SetMathMLMetaDataInImage(image, metaData)
            End If
        Else
            image = CreateImageFromMathML(mathMl, True, verticalAlignValue)
            If image IsNot Nothing AndAlso image.Length > 0 Then
                imageWithMetaData = MathMLHelper.SetMathMLMetaDataInImage(image, mathMl)
            End If
        End If

        If imageWithMetaData IsNot Nothing AndAlso imageWithMetaData.Length > 0 Then
            Dim xDoc = XDocument.Parse(mathMl)
            MathMLHelper.SetMathMlNamespace(xDoc)
            Dim formulaEvents = New FormulaEventArgs(imageWithMetaData, _imageName, WebUtility.HtmlEncode(xDoc.Root.OuterXml()))
            formulaEvents.VerticalAlignValue = verticalAlignValue
            RaiseEvent EditFormula(Me, formulaEvents)
        End If

        Return imageWithMetaData
    End Function

    Private Function CreateImageFromMathML(ByVal mathML As String, getFontFromMml As Boolean, ByRef verticalAlignValue As Double) As Byte()
        If _mathMlEditorPlugin IsNot Nothing Then
            Return _mathMlEditorPlugin.RenderPng(mathML, If(getFontFromMml, MathMLHelper.CreateImageOptions(mathML, _font), MathMLHelper.CreateImageOptions(_font)), verticalAlignValue)
        End If

        Return Nothing
    End Function

End Class
