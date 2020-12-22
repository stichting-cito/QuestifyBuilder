Imports Questify.Builder.Logic.Service.Interfaces

Public Class SelectTargetAndColumns

    Public Property SelectedColumns As IList(Of String)
        Get
            Return Me.SelectColumns.SelectedColumns
        End Get
        Set(value As IList(Of String))
            Me.SelectColumns.SelectedColumns = value
        End Set
    End Property

    Public Property SelectedTarget As String
        Get
            Return DirectCast(Me.SelectTargetControl.SelectTargetComboBox.Items(Me.SelectTargetControl.SelectTargetComboBox.SelectedIndex), KeyValuePair(Of String, IItemPreviewHandler)).Key
        End Get
        Set(value As String)
            Me.SelectTargetControl.SelectTargetComboBox.SelectedIndex = Me.SelectTargetControl.SelectTargetComboBox.FindString(value)
        End Set
    End Property

    Public Sub New()
        Me.SelectTargetControl = New SelectTarget()

        Me.InitializeComponent()

    End Sub

    Public Sub New(selectTargetControl As SelectTarget)
        Me.SelectTargetControl = selectTargetControl

        Me.InitializeComponent()

    End Sub

    Private Sub SelectTargetAndColumns_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating

        Me.SelectColumns.SelectColumns_Validating(sender, e)
        Me.SelectTargetControl.SelectTarget_Validating(sender, e)

        If Not e.Cancel Then
            Me.OnValidated(e)
        End If
    End Sub

End Class
