Imports System.ComponentModel

Namespace Enums


    <TypeConverter(GetType(LocalizedEnumConverter))> _
    Public Enum CustomBankPropertyType
        ListMultipleSelect
        ListSingleSelect
        FreeValue
        FreeValueRichText
        Concept
        Tree
    End Enum
End NameSpace