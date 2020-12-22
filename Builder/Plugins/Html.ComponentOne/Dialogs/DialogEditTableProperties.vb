
Imports System.Windows.Forms
Imports C1.Win.C1Editor
Imports C1.Win.C1Editor.UICustomization
Imports System.Xml

Public Class DialogEditTableProperties
    Implements ITableItemDialog

    Private _tableAlignment As Web.UI.WebControls.HorizontalAlign
    Private _item As XHTMLTableItem
    Private _editor As XHtmlEditor

    Public Sub New(editor As XHtmlEditor, ByVal namespaceManager As XmlNamespaceManager)

        InitializeComponent()

        _editor = editor
    End Sub

    Public Property TableAlignment() As Web.UI.WebControls.HorizontalAlign
        Get
            Dim tconverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(_tableAlignment.GetType)
            Return DirectCast(tconverter.ConvertFromString(Nothing, System.Threading.Thread.CurrentThread.CurrentUICulture, ComboBoxAlignment.SelectedItem.ToString()), Web.UI.WebControls.HorizontalAlign)
        End Get
        Set(ByVal value As Web.UI.WebControls.HorizontalAlign)
            _tableAlignment = value
        End Set
    End Property

    Public Property TableStyle() As String
        Get
            Return ConstructTableStyle()
        End Get
        Set(ByVal value As String)
            ComboBoxWidthUnit.SelectedIndex = 0

            If value IsNot Nothing Then
                Dim KeyWordIndex As Integer

                KeyWordIndex = value.IndexOf("WIDTH:")
                If (_item IsNot Nothing AndAlso _item.UseWidth) OrElse KeyWordIndex > -1 Then
                    RadioButtonFixedWidth.Checked = True

                    Dim widhtValue As String
                    Dim widthUnitIndex As Integer = 0

                    If _item IsNot Nothing AndAlso _item.UseWidth Then
                        widhtValue = _item.WidthValue.ToString()
                        If _item.WidthType = UICustomization.SizeType.Pixel Then
                            widthUnitIndex = 1
                        End If
                    Else
                        Dim valueOffset As Integer = KeyWordIndex + "WIDTH:".Length
                        Dim semiColonOffset As Integer = value.IndexOf(";", valueOffset)

                        If semiColonOffset = -1 Then
                            semiColonOffset = value.Length
                        End If

                        widhtValue = value.Substring(valueOffset, semiColonOffset - valueOffset)

                        If widhtValue.EndsWith("px") Then
                            widthUnitIndex = 1
                            widhtValue = widhtValue.Substring(0, widhtValue.Length - 2)
                        Else
                            widhtValue = widhtValue.Substring(0, widhtValue.Length - 1)
                        End If
                    End If

                    TextBoxWidth.Text = widhtValue
                    ComboBoxWidthUnit.SelectedIndex = widthUnitIndex
                Else
                    RadioButtonAutoAdjustWidth.Checked = True
                End If

                KeyWordIndex = value.IndexOf("HEIGHT:")
                If KeyWordIndex > -1 Then
                    RadioButtonFixedHeight.Checked = True

                    Dim valueOffset As Integer = KeyWordIndex + "HEIGHT:".Length
                    Dim semiColonOffset As Integer = value.IndexOf(";", valueOffset)

                    If semiColonOffset = -1 Then
                        semiColonOffset = value.Length
                    End If

                    Dim heightValue As String = value.Substring(valueOffset, semiColonOffset - valueOffset)

                    If heightValue.EndsWith("px") Then
                        heightValue = heightValue.Substring(0, heightValue.Length - 2)
                    End If

                    TextBoxHeight.Text = heightValue
                Else
                    RadioButtonAutoAdjustHeight.Checked = True
                End If
            End If
        End Set
    End Property

    Private Function ConstructTableStyle() As String
        Dim Style As String

        Style = "BORDER-COLLAPSE: collapse; "

        If RadioButtonFixedHeight.Checked Then
            Style += String.Format(" HEIGHT: {0}px;", TextBoxHeight.Text)
        End If

        If RadioButtonFixedWidth.Checked Then
            _item.WidthType = If(ComboBoxWidthUnit.SelectedIndex = 0, UICustomization.SizeType.Percent, UICustomization.SizeType.Pixel)
            _item.WidthValue = Integer.Parse(TextBoxWidth.Text)
            _item.UseWidth = True
            Style += String.Format(" WIDTH: {0}", TextBoxWidth.Text)
            If ComboBoxWidthUnit.SelectedIndex = 0 Then
                Style += "%"
            Else
                Style += "px"
            End If

            Style += ";"
        Else
            _item.UseWidth = False
        End If

        Return Style
    End Function

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Try
            _editor.BeginTransaction()
            _item.Style = TableStyle

            If TableAlignment = Web.UI.WebControls.HorizontalAlign.NotSet Then
                DirectCast(_item.Node, XmlElement).RemoveAttribute("align")
            Else
                DirectCast(_item.Node, XmlElement).SetAttribute("align", [Enum].GetName(GetType(Web.UI.WebControls.HorizontalAlign), TableAlignment).ToLower())
            End If
            _editor.CommitTransaction()
        Catch ex As Exception
            _editor.RollbackTransaction()
            Throw ex
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DialogEditTableProperties_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        System.ComponentModel.TypeDescriptor.AddAttributes(_tableAlignment.GetType(), New System.ComponentModel.TypeConverterAttribute(GetType(LocalEnumLocalizer)))

        ComboBoxAlignment.Items.Clear()
        For Each kv As KeyValuePair(Of System.Enum, String) In LocalEnumLocalizer.GetValues(_tableAlignment.GetType())
            ComboBoxAlignment.Items.Add(kv.Value)
        Next

        Dim tconverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(_tableAlignment.GetType())
        Dim SelectedText As String = String.Empty
        SelectedText = tconverter.ConvertToString(Nothing, Threading.Thread.CurrentThread.CurrentUICulture, _tableAlignment)
        Dim SelectedIndex As Integer = 0

        For Each cbItem As Object In ComboBoxAlignment.Items
            Dim stringCBItem As String = DirectCast(cbItem, String)

            If stringCBItem.Equals(SelectedText) Then
                ComboBoxAlignment.SelectedIndex = SelectedIndex
                ComboBoxAlignment.SelectedItem = cbItem
                Exit For
            End If

            SelectedIndex += 1
        Next

        TableStyle = _item.Style
    End Sub

    Private Sub RadioButtonFixedWidth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonFixedWidth.CheckedChanged
        TextBoxWidth.Enabled = RadioButtonFixedWidth.Checked
        ComboBoxWidthUnit.Enabled = RadioButtonFixedWidth.Checked
    End Sub

    Private Sub RadioButtonFixedHeight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonFixedHeight.CheckedChanged
        TextBoxHeight.Enabled = RadioButtonFixedHeight.Checked
    End Sub

    Private Function GetAlignmentFromTable(ByVal tableElement As XmlElement) As Web.UI.WebControls.HorizontalAlign
        If tableElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(tableElement.GetAttribute("align")) Then
            Return DirectCast([Enum].Parse(GetType(Web.UI.WebControls.HorizontalAlign), tableElement.GetAttribute("align"), True), Web.UI.WebControls.HorizontalAlign)
        End If

        Return Web.UI.WebControls.HorizontalAlign.NotSet
    End Function

    Private Class LocalEnumLocalizer
        Inherits Cito.Tester.Common.ResourceEnumConverter

        Public Sub New(ByVal sType As System.Type)
            MyBase.New(sType, New System.ComponentModel.ComponentResourceManager(GetType(DialogEditTableProperties)))
        End Sub
    End Class

    Private Sub BindData(ByVal item As XHTMLTableItem) Implements ITableItemDialog.BindData
        _item = item
        _tableAlignment = GetAlignmentFromTable(CType(item.Node, XmlElement))

    End Sub

    Private Function Show(ByVal ownerWindow As IWin32Window) As Boolean Implements ITableItemDialog.Show
        Return ShowDialog(ownerWindow) = DialogResult.OK
    End Function

End Class
