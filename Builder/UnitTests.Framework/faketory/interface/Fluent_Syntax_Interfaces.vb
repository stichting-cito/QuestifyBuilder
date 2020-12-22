Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Faketory.interface

    Public Interface IAddAll

        Inherits IAddRootObjects
        Inherits IAddResources
        Inherits IAddCustomProperties

    End Interface


    Public Interface IAddRootObjects
        Function Aspect(name As String, postAction As Action(Of AspectResourceEntity)) As IActionsAfter
        Function Aspect(name As String) As IActionsAfter
        Function Item(name As String, postAction As Action(Of ItemResourceEntity)) As IActionsAfter
        Function Item(name As String) As IActionsAfter
        Function ItemTemplate(name As String, postAction As Action(Of ItemLayoutTemplateResourceEntity)) As IActionsAfter
        Function ItemTemplate(name As String) As IActionsAfter
        Function ControlTemplate(name As String, postAction As Action(Of ControlTemplateResourceEntity)) As IActionsAfter
        Function ControlTemplate(name As String) As IActionsAfter
        Function SourceText(name As String, postAction As Action(Of GenericResourceEntity)) As IActionsAfter
        Function SourceText(name As String) As IActionsAfter
        Function AssessmentTest(name As String) As IActionsAfter
        Function AssessmentTest(id As Guid, name As String) As IActionsAfter
        Function AssessmentTest(name As String, postAction As Action(Of AssessmentTestResourceEntity)) As IActionsAfter
        Function AssessmentTest(name As String, someTest As AssessmentTest2) As IActionsAfter
        Function AssessmentTest(id As Guid, name As String, someTest As AssessmentTest2) As IActionsAfter
    End Interface

    Public Interface IAddCustomProperties
        Function SingleListProperty(code As Guid, name As String, listitems As List(Of KeyValuePair(Of String, String))) As IAddAll
        Function TreeProperty(code As Guid, name As String) As IAddTreePropertyKeyword
    End Interface
    Public Interface IAddResources
        Function GenericResource(name As String, data As Byte()) As IAddResourcesKeyword
        Function StyleSheet(name As String, stylesheetData As String) As IAddResourcesKeyword
        Function StyleSheet(name As String, stylesheetData As String, postAction As Action(Of GenericResourceEntity)) As IAddResourcesKeyword
        Function Image(name As String) As IAddResourcesKeyword
        Function Image(name As String, postAction As Action(Of GenericResourceEntity)) As IAddResourcesKeyword
        Function Text(name As String, HtmlTxt As XElement) As IAddResourcesKeyword
        Function Text(name As String, HtmlTxt As XElement, postAction As Action(Of GenericResourceEntity)) As IAddResourcesKeyword
    End Interface

    Public Interface IActionsAfter
        Inherits IAddResourcesKeyword
        ReadOnly Property DependsOn As IAddRootObjects
        ReadOnly Property IsUsedBy As IAddRootObjects
    End Interface

    Public Interface IAddTreePropertyKeyword
        Function Root(name As String, title As String) As IAddTreePropertyKeyword
        Function Node(name As String, title As String) As IAddTreePropertyKeyword
    End Interface

    Public Interface IAddResourcesKeyword
        ReadOnly Property AddResource As IAddResources
    End Interface

End NameSpace