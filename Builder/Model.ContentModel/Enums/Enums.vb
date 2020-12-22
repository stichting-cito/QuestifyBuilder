Imports System.ComponentModel

Namespace Enums


    <TypeConverter(GetType(LocalizedEnumConverter))> _
    Public Enum ItemTypeEnum
        Choice
        Essay
        ShortAnswer
        Likert
        Composite
        Informational
        Pause
        System
        Inline
        Order
        Hotspot
        [Error]
    End Enum

    <TypeConverter(GetType(LocalizedEnumConverter))> _
    Public Enum TestPartNavigationModeEnum
        Linear
        Nonlinear
        Automatic
    End Enum

    Public Enum ValidationResult
        Valid = 0
        Warning = 1
        NotValid = 2
    End Enum

    <TypeConverter(GetType(LocalizedEnumConverter))> _
    Public Enum ResourceTypeEnum
        None = 0
        AllResources = ResourceTypeEnum.AssessmentTestResource Or ItemResource Or ItemLayoutTemplateResource Or ControlTemplateResource Or GenericResource Or TestPackageResource Or DeliveryResource
        AssessmentTestResource = 1
        ItemResource = 2
        ItemLayoutTemplateResource = 4
        ControlTemplateResource = 8
        GenericResource = 16
        TestPackageResource = 32
        DeliveryResource = 64
    End Enum

    <TypeConverter(GetType(LocalizedEnumConverter))> _
    Public Enum AuthenticationType As Integer
        [Default]
        ActiveDirectory
    End Enum

    Public Class LocalizedEnumConverter
        Inherits Cito.Tester.Common.ResourceEnumConverter

        Public Sub New(ByVal sType As Type)
            MyBase.New(sType, My.Resources.ResourceManager)
        End Sub
    End Class
End NameSpace