
Imports System.Drawing

Namespace PluginExtensibility.Html.Handlers.Logic

    Public Interface ICellStyleDeterminator

        Function GetStyleLeft(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?)
        Function GetStyleTop(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?)
        Function GetStyleRight(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?)
        Function GetStyleBottom(cel As TableCell) As Tuple(Of LineStyle, Integer, Color?)

    End Interface

End Namespace