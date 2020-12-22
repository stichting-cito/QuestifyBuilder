Imports System.ComponentModel
Imports System.Reflection
Imports System.Xml.Serialization
Imports Cito.Tester.Common


<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum ComponentState
    Pickable
    Picked
    TimeCriteriaMet
End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum ItemResponseState
    Missing
    Invalid
    Valid
End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum ItemFunctionalType
    Regular
    System
    Informational
    Seeding
End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum HandInTestState
    UserExitedTest
    AllItemsAnswered
    AllItemsAnsweredNoConfirmation
End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum UICommand
    CustomCommand

    UserExitTest

    SuspendTest

    PauseTest

    Calculator

    Notepad

    Overview

    Clock

    Help

    Volume

    PlayItemSound

    SoundOnOff

End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum enumSectionType
    normal = 0
    insertionGrp = 1
    insertionCol = 2
    categoryGrp = 3
    BumperGrp = 4
End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
<Flags>
Public Enum NavigationBarPart

    EntireNavigationBar = 0

    GotoPreviousItem = 1

    GotoNextItem = 2

    ItemSelectionArea = 4
End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum ToolType

    None = 0

    Calculator = 1

    GeoTriangle = 2

    SimpleCalculator = 3

End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum Qti21NavigationMode
    linear = 0
    nonlinear = 1
    automatic = 2
End Enum



<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum SubmissionMode
    individual = 0
    simultaneous = 1
End Enum

<Flags, TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum CesItemUsageType
    <XmlIgnore>
    none = 0

    [default] = 1

    seeding = 2

    informational = 4
End Enum

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum PreProcessingRuleId
    HLKL
    YIJ
    VAS
    VAP
    VDT
    VKT
    VSBE
End Enum

Public Class LocalizedEnumConverter
    Inherits ResourceEnumConverter

    Public Sub New(sType As Type)
        MyBase.New(sType, My.Resources.ResourceManager)
    End Sub
End Class

Public Class ENums
    Public Shared Function EnumDescription(EnumConstant As [Enum]) As String
        Dim fi As FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
        If aattr.Length > 0 Then
            Return aattr(0).Description
        Else
            Return EnumConstant.ToString()
        End If
    End Function
End Class
