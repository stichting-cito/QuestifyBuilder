Imports System.Linq
Imports Questify.Builder.Configuration
Imports Cito.Tester.ContentModel
Imports MakarovDev.ExpandCollapsePanel

Public Class ParameterGroupBox

    Private _rows As Integer
    Private _toolTips As New List(Of ToolTip)
    Private _groupName As String = String.Empty

    Public Property GroupName() As String
        Get
            Return _groupName
        End Get
        Set(ByVal value As String)
            _groupName = value
            Dim name = StripNumber(_groupName.Trim, "_"c)
            name = StripNumber(name.Trim, " "c).Trim
            Me.Text = name
        End Set
    End Property

    Public Sub New()

        InitializeComponent()

        ResetLayoutPanel()
    End Sub

    Public Sub SetExpandedState()
        IsExpanded = ItemEditorGroupSettings.IsExpanded(_groupName)
        Me.AutoSize = IsExpanded
    End Sub

    Public Sub SetTabIndex(ByRef tabIndex As Integer)
        Me.TabIndex = tabIndex
        tabIndex += 1

        for each control as control in Me.Controls
            control.TabStop = False
        Next

        For Each editorControl In Me.TableLayoutControl.Controls
            If TypeOf editorControl Is ParameterEditorControlBase OrElse TypeOf editorControl Is CollectionEditorControlBase then
                editorControl.TabIndex = tabIndex
                tabIndex += 1
            End If
        Next
    End Sub

    Public Sub AddParameterEditorToGroup(ByVal editorControl As ParameterEditorControlBase, ByVal parameter As ParameterBase)
        Guard(editorControl, parameter)

        TableLayoutControl.RowStyles.Add(New RowStyle(SizeType.AutoSize))

        Dim labelName As String = parameter.DesignerSettings.GetSettingValueByKey("label")
        Dim description As String = parameter.DesignerSettings.GetSettingValueByKey("description")

        If Not String.IsNullOrEmpty(labelName) Then
            Dim paramLabel As New Label With {.Text = labelName, .AutoSize = True, .TextAlign = ContentAlignment.MiddleLeft, .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right}
            AddToolTip(labelName, description, paramLabel)

            If LabelHasToBePlacedAboveControl(parameter) Then
                AddParameterControlWithLableAbove(editorControl, parameter, paramLabel)
            Else
                AddParameterControlWithLabel(editorControl, paramLabel)
            End If
        Else
            AddParameterControlNoParameter(editorControl)
        End If

        editorControl.ParentTabEnabledContainerControl = Me
        TableLayoutControl.RowCount = _rows
        Me.ExpandedHeight = TableLayoutControl.Height
        Me.Height = TableLayoutControl.Height
        _rows += 1
    End Sub

    Private Sub ResetLayoutPanel()
        TableLayoutControl.RowCount = 0
        TableLayoutControl.RowStyles.Clear()
        TableLayoutControl.Controls.Clear()
        _rows = 0
    End Sub

    Private Sub AddParameterControlNoParameter(ByVal editorControl As ParameterEditorControlBase)
        TableLayoutControl.Controls.Add(editorControl, 0, _rows)
        TableLayoutControl.SetColumnSpan(editorControl, 2)
    End Sub

    Private Sub AddParameterControlWithLabel(ByVal editorControl As ParameterEditorControlBase, ByVal paramLabel As Label)
        TableLayoutControl.Controls.Add(paramLabel, 0, _rows)
        TableLayoutControl.Controls.Add(editorControl, 1, _rows)
    End Sub

    Private Function StripNumber(name As String, c As Char) As String
        If name.Contains(c) AndAlso IsNumeric(name.Split(c)(0).Trim) Then
            name = name.Substring(name.IndexOf(c), name.Length - name.IndexOf(c)).Trim
        End If
        Return name
    End Function

    Private Sub AddParameterControlWithLableAbove(ByVal editorControl As ParameterEditorControlBase, ByVal parameter As ParameterBase, ByVal paramLabel As Label)
        TableLayoutControl.Controls.Add(paramLabel, 0, _rows)

        _rows += 1
        TableLayoutControl.RowStyles.Add(New RowStyle(SizeType.AutoSize))

        If TypeOf parameter Is CollectionParameter Then
            editorControl.Margin = New Padding(20, 3, 15, 3)
            paramLabel.Margin = New Padding(20, 3, 15, 3)
        End If

        TableLayoutControl.Controls.Add(editorControl, 0, _rows)
        TableLayoutControl.SetColumnSpan(editorControl, 2)
    End Sub

    Private Sub AddToolTip(ByVal labelName As String, ByVal description As String, ByVal paramLabel As Label)
        Dim toolTipControl = ParameterEditorHelper.AddToolTip(labelName, description, paramLabel)
        If toolTipControl IsNot Nothing Then _toolTips.Add(toolTipControl)
    End Sub

    Private Function LabelHasToBePlacedAboveControl(ByVal parameter As ParameterBase) As Boolean
        Return (TypeOf parameter Is XHtmlParameter OrElse TypeOf parameter Is CollectionParameter)
    End Function

    Private Sub Guard(ByVal editorControl As ParameterEditorControlBase, ByVal parameter As ParameterBase)
        If editorControl Is Nothing Then
            Throw New ArgumentNullException("editorControl")
        End If

        If parameter Is Nothing Then
            Throw New ArgumentNullException("parameter")
        End If
    End Sub

    Private Sub ParameterGroupBox_ExpandCollapse(sender As Object, e As ExpandCollapseEventArgs) Handles Me.ExpandCollapse
        TableLayoutControl.Visible = e.IsExpanded
        If Me.Parent IsNot Nothing Then
            Me.Parent.ResumeLayout()
        End If
        If Not String.IsNullOrEmpty(_groupName) Then
            ItemEditorGroupSettings.ChangeCollapseState(_groupName, Not IsExpanded)
        End If
    End Sub

    Private Sub ParameterGroupBox_BeforeExpandCollapse(sender As Object, e As ExpandCollapseEventArgs) Handles Me.BeforeExpandCollapse
        If Me.Parent IsNot Nothing Then
            Me.Parent.SuspendLayout()
        End If
    End Sub

End Class
