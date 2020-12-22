Imports System.Xml.Serialization

Public Class MatrixScoringParameter : Inherits ScoringParameter

    Private _columnWidthFirstColumn As IntegerParameter
    Private _matrixColumnsDefinition As MultiChoiceScoringParameter


    <ParameterControlAttribute> _
    <XmlElement("matrixcolumnsdefinition")> _
    Public Property MatrixColumnsDefinition As MultiChoiceScoringParameter
        Get
            Return _matrixColumnsDefinition
        End Get
        Set(definition As MultiChoiceScoringParameter)
            _matrixColumnsDefinition = definition
        End Set
    End Property

    <ParameterControlAttribute> _
    <XmlElement("linelabelcolumnwidth")> _
    Public Property LineLabelColumnWidth As IntegerParameter
        Get
            Return _columnWidthFirstColumn
        End Get
        Set
            _columnWidthFirstColumn = value
        End Set
    End Property


    Public Overrides Function GetParametersWithDesignerSettings() As IEnumerable(Of ParameterBase)
        Return New ParameterBase() {MatrixColumnsDefinition, LineLabelColumnWidth}
    End Function

    <XmlIgnore> _
    Public Overrides ReadOnly Property AlternativesCount As Nullable(Of Integer)
        Get
            If MatrixColumnsDefinition IsNot Nothing AndAlso MatrixColumnsDefinition.Value IsNot Nothing AndAlso MatrixColumnsDefinition.Value.Count > 0 Then
                Return MatrixColumnsDefinition.Value.Count
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Overrides ReadOnly Property IsSingleValue As Boolean
        Get
            Return True
        End Get
    End Property

End Class
