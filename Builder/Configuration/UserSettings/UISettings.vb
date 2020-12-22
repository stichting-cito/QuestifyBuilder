Imports System.Drawing

Public NotInheritable Class UISettings

    Public Shared Property ItemPreviewTarget As String
        Get
            Return My.Settings.UI_itemPreviewTarget
        End Get
        Set
            My.Settings.UI_itemPreviewTarget = value
        End Set
    End Property

    Public Shared Property ItemPreviewResolution As Integer
        Get
            Return My.Settings.UI_itemPreviewResolution
        End Get
        Set
            My.Settings.UI_itemPreviewResolution = value
        End Set
    End Property

    Public Shared Property FormulaEditorFont As Font
        Get
            Return My.Settings.UI_formulaEditorFont
        End Get
        Set
            My.Settings.UI_formulaEditorFont = value
        End Set
    End Property

End Class
