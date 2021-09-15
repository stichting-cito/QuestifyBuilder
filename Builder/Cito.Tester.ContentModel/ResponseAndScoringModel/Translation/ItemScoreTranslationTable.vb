Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization

<SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")>
<Serializable>
<XmlRoot("itemScoreTranslationTable")>
Public Class ItemScoreTranslationTable
    Inherits ScoreTranslationTable(Of ItemScoreTranslationTableEntry)

End Class
