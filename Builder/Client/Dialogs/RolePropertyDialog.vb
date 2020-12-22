
Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class RolePropertyDialog


    <Description("This event will be raised when the data in the control has been changed."), Category("Roleproperty events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)


    Private ReadOnly _role As RoleEntity


    Public Property IsDirty As Boolean



    Public Sub New(ByVal role As RoleEntity)
        InitializeComponent()

        _role = role
        BindData()
    End Sub


    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub


    Private Sub BindData()
        ApplyEnabled = False

        AddHandler _role.PropertyChanged, AddressOf RoleDataChanged

        MetaData.Datasource = _role
        MetaData.IsEditable = True

        Dim titleBarBinding As New Binding("Text", _role, "Name")
        AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
        DataBindings.Add(titleBarBinding)
    End Sub


    Private Sub TitleToFormTitleFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        e.Value = String.Format(My.Resources.RolePropertyTitle, e.Value)
    End Sub

    Private Sub RoleDataChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        IsDirty = True
        OnDataChanged(New EventArgs())
    End Sub


End Class
