Imports System.ComponentModel
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class TestResourceEventBase
    Inherits System.Windows.Forms.UserControl
    Implements ITestResourcesEvents
    <Description("This event will be raised when a dependent resource is added in this control."),
Category("AssessmentTestPropertiesEditor events")>
    Public Event DependentResourceAdded As EventHandler(Of ResourceEventArgs) Implements ITestResourcesEvents.DependentResourceAdded

    <Description("This event will be raised when a dependent resource is removed in this control."),
Category("AssessmentTestPropertiesEditor events")>
    Public Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs) Implements ITestResourcesEvents.DependentResourceRemoved

    <Description("This event will be raised when the control needs a resource from the bank."),
Category("AssessmentTestPropertiesEditor events")>
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs) Implements ITestResourcesEvents.ResourceNeeded

    Protected Sub OnDependentResourceAdded(ByVal e As ResourceEventArgs)
        RaiseEvent DependentResourceAdded(Me, e)
    End Sub

    Protected Sub OnDependentResourceRemoved(ByVal e As ResourceNameEventArgs)
        RaiseEvent DependentResourceRemoved(Me, e)
    End Sub

    Protected Sub OnResourceNeeded(ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub
End Class
