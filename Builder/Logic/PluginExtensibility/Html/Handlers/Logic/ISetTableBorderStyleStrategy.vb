Imports System.Drawing

Namespace PluginExtensibility.Html.Handlers.Logic
    Public Interface ISetTableBorderStyleStrategy

        Sub SetLeft_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean)
        Sub SetTop_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean)
        Sub SetRight_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean)
        Sub SetBottom_BorderStyle(style As CssBorder, forCell As TableCell, borders As Boolean)
        Sub SetBackgroundColor(color As Color?, forCell As TableCell)

    End Interface
End Namespace