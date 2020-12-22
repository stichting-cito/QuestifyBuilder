Imports System.ComponentModel

<Serializable> _
<TypeConverter(GetType(LocalizedEnumConverter))> _
Public Enum CutOffScoreConditionUnit
    <Description("Score")> _
    Score

    <Description("Percentage")> _
    Percentage
End Enum
