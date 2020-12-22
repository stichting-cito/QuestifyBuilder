Imports System.ComponentModel
Imports Cito.Tester.ContentModel

Public Interface ITestResourcesEvents
    <Description("This event will be raised when a dependent resource is added in this control."),
Category("AssessmentTestPropertiesEditor events")>
    Event DependentResourceAdded As EventHandler(Of ResourceEventArgs)

    <Description("This event will be raised when a dependent resource is removed in this control."),
Category("AssessmentTestPropertiesEditor events")>
    Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs)

    <Description("This event will be raised when the control needs a resource from the bank."),
    Category("AssessmentTestPropertiesEditor events")>
    Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
End Interface
