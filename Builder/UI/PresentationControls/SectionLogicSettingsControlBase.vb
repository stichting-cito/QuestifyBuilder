Imports System.ComponentModel
Imports Questify.Builder.Logic

Namespace PresentationControls

    Public Class SectionLogicSettingsControlBase


        <Description("This event will be raised when a dependent resource is added in this control."),
            Category("SectionLogicSettingsControl events")>
        Public Event DependentResourceAdded As EventHandler(Of SectionLogicSettingsDependencyChangedEventArgs)

        <Description("This event will be raised when a dependent resource is removed in this control."),
            Category("SectionLogicSettingsControl events")>
        Public Event DependentResourceRemoved As EventHandler(Of SectionLogicSettingsDependencyChangedEventArgs)

        <Description("This event will be raised when this control needs the 'select resource' dialog."),
    Category("SectionLogicSettingsControl events")>
        Public Event ResourceDialogRequest As EventHandler(Of ResourceDialogRequestEventArgs)



        Protected Sub OnDependentResourceAdded(e As SectionLogicSettingsDependencyChangedEventArgs)
            RaiseEvent DependentResourceAdded(Me, e)
        End Sub

        Protected Sub OnDependentResourceRemoved(e As SectionLogicSettingsDependencyChangedEventArgs)
            RaiseEvent DependentResourceRemoved(Me, e)
        End Sub

        Protected Sub OnResourceDialogRequest(e As ResourceDialogRequestEventArgs)
            RaiseEvent ResourceDialogRequest(Me, e)
        End Sub


    End Class
End Namespace