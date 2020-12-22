Imports System.ComponentModel
Imports System.Linq
Imports System.Reflection
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic

Public Class DataSourceSettingsEditor


    Private _loadedAssemblies As New Dictionary(Of String, Assembly)
    Private _embeddedEditors As IList(Of DataSourceUIControlBase) = New List(Of DataSourceUIControlBase)


    Public ReadOnly Property EmbeddedEditors() As IList(Of DataSourceUIControlBase)
        Get
            Return _embeddedEditors
        End Get
    End Property



    Private Function CreateConfigUIFactory(settings As DataSourceSettings) As IDataSourceSettingsDesignerFactory
        Dim ConfigUIFactoryType As Type
        Dim ConfigUIFactoryInstance As IDataSourceSettingsDesignerFactory

        ConfigUIFactoryType = Type.GetType(settings.DataSourceConfigUIFactoryType)
        ConfigUIFactoryInstance = CType(Activator.CreateInstance(ConfigUIFactoryType), IDataSourceSettingsDesignerFactory)

        Return ConfigUIFactoryInstance
    End Function

    Private Sub DataSourceSettingsUIControl_DependentResourceAdded(sender As Object, e As ResourceNameEventArgs)
        OnDependentResourceAdded(e)
    End Sub

    Private Sub DataSourceSettingsUIControl_DependentResourceRemoved(sender As Object, e As ResourceNameEventArgs)
        OnDependentResourceRemoved(e)
    End Sub

    Private Sub OnDependentResourceAdded(e As ResourceNameEventArgs)
        RaiseEvent DependentResourceAdded(Me, e)
    End Sub

    Private Sub OnDependentResourceRemoved(e As ResourceNameEventArgs)
        RaiseEvent DependentResourceRemoved(Me, e)
    End Sub

    Private Sub RemoveDataSourceEditors()
        Dim controlsToBeDeleted As New List(Of Control)

        For Each control As Control In Me.Controls
            If TypeOf control Is DataSourceUIControlBase Then
                controlsToBeDeleted.Add(control)
            End If
        Next

        For Each control As Control In controlsToBeDeleted
            Me.EmbeddedEditors.Remove(CType(control, DataSourceUIControlBase))
            Me.Controls.Remove(control)
        Next
    End Sub



    Public Overridable Sub Initialize(mode As IDataSourceSettingsDesignerFactory.DesignerMode, dataSourceSettingsList As IEnumerable(Of DataSourceSettings), resourceManager As ResourceManagerBase)
        RemoveDataSourceEditors()
        NoConfigLabel.Visible = True

        Dim count = 0
        For Each settings As DataSourceSettings In dataSourceSettingsList
            count += 1
        Next

        For Each settings As DataSourceSettings In dataSourceSettingsList
            Dim configUIFactory As IDataSourceSettingsDesignerFactory = CreateConfigUIFactory(settings)
            Dim configUI As DataSourceUIControlBase = configUIFactory.CreateDesigner(mode, settings, settings.DataSourceConfig, resourceManager)
            If configUI IsNot Nothing Then
                Me.Controls.Add(configUI)
                Me.EmbeddedEditors.Add(configUI)

                configUI.Mode = mode

                If count = 1 Then
                    configUI.Dock = DockStyle.Fill
                Else
                    configUI.Dock = DockStyle.Top
                End If

                AddHandler configUI.DependentResourceAdded, AddressOf DataSourceSettingsUIControl_DependentResourceAdded
                AddHandler configUI.DependentResourceRemoved, AddressOf DataSourceSettingsUIControl_DependentResourceRemoved

                configUI.BringToFront()

                NoConfigLabel.Visible = False
            End If
        Next
    End Sub

    Public Function ValidateAllEditors() As Boolean
        Return Me.Controls.OfType(Of DataSourceUIControlBase).All(Function(controlUI) controlUI.ValidateUI())
    End Function

    Public Function GetErrorMessage() As String
        Return Me.Controls.OfType(Of DataSourceUIControlBase).Aggregate(String.Empty, Function(current, controlUI) String.Concat(current, controlUI.GetValidationMessage()))
    End Function



    <Browsable(True), Bindable(True), Description("This event will be raised when a dependent resource is added."), Category("Dependency Management Events")>
    Public Event DependentResourceAdded As EventHandler(Of ResourceNameEventArgs)

    <Browsable(True), Bindable(True), Description("This event will be raised when a dependent resource is removed."), Category("Dependency Management Events")>
    Public Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs)


End Class