

Namespace Commanding

    Public Class ControlTagParameterBinding
        Inherits ParameterBinding


        Overrides Function GetCommandParameter(source As Object) As Object
            If TypeOf source Is Control Then
                Return CType(source, Control).Tag
            ElseIf TypeOf source Is ToolStripItem Then
                Return CType(source, ToolStripItem).Tag
            Else
                Throw New ArgumentException("type of source is not control", "source")
            End If
        End Function


    End Class

End Namespace

