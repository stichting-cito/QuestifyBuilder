Imports System.ComponentModel
Imports Enums

<TypeConverter(GetType(LocalizedEnumConverter))>
Public Enum PrintFormType
    UserDefinedBooklet = -1
    QuestionBooklet = 0
    CorrectionBooklet = 1
    SourceBooklet = 2
    MultiMediaInstructionBooklet = 3
End Enum