Imports System.Xml.Serialization
Imports System.Linq

Public Class GraphGapMatchScoringParameter : Inherits ScoringParameter : Implements ITransformable(Of GraphGapMatchScoringParameter)

    Private _hotspots As AreaParameter
    Private _isTransformed As Boolean
    Private _isCategorizationVariant As Boolean = True
    Private ReadOnly _gaps As New Dictionary(Of String, Dictionary(Of String, String))

    Public Overrides ReadOnly Property AlternativesCount As Integer?
        Get
            If Value Is Nothing Then Return 0
            Return Value.Count
        End Get
    End Property

    Public Overrides Function GetParametersWithDesignerSettings() As IEnumerable(Of ParameterBase)
        Return New ParameterBase() {Area}
    End Function

    Public ReadOnly Property CanTransform As Boolean Implements ITransformable.CanTransform
        Get
            Return Not _isCategorizationVariant
        End Get
    End Property

    Public ReadOnly Property IsTransformed As Boolean Implements ITransformable.IsTransformed
        Get
            Return _isTransformed
        End Get
    End Property

    <XmlAttribute("iscategorizationvariant")> _
    Public Property IsCategorizationVariant As Boolean
        Get
            Return _isCategorizationVariant
        End Get
        Set
            _isCategorizationVariant = value
        End Set
    End Property

    <ParameterControlAttribute>
    <XmlElement("areaparameter")>
    Public Property Area As AreaParameter
        Get
            Return _hotspots
        End Get
        Set(shapes As AreaParameter)
            _hotspots = shapes
        End Set
    End Property

    Public Overrides ReadOnly Property IsSingleValue As Boolean
        Get
            Return True
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property Gaps As Dictionary(Of String, Dictionary(Of String, String))
        Get
            Return _gaps
        End Get
    End Property

    Public Function Transform() As GraphGapMatchScoringParameter Implements ITransformable(Of GraphGapMatchScoringParameter).Transform
        Dim transformedParam As New GraphGapMatchScoringParameter() With {.Name = Name, .ControllerId = ControllerId, .FindingOverride = FindingOverride, .IsCategorizationVariant = IsCategorizationVariant}
        For Each parameterCollection As ParameterCollection In Value.OrderBy(Function(x) AscW(x.Id))
            Dim innerParameter As GapImageParameter = parameterCollection.InnerParameters.OfType(Of GapImageParameter).FirstOrDefault()
            If innerParameter IsNot Nothing Then
                Dim labelPrm = parameterCollection.InnerParameters.FirstOrDefault(Function(p) p.Name IsNot Nothing AndAlso p.Name.Equals(ElementLabelParameterName, StringComparison.InvariantCultureIgnoreCase))
                transformedParam.Gaps.Add(parameterCollection.Id, New Dictionary(Of String, String) From {
                                           {GapMatchScoringParameter.GapMatchValue, innerParameter.Value},
                                           {GapMatchScoringParameter.GapMatchMax, innerParameter.MatchMax.ToString()},
                                           {GapMatchScoringParameter.GapMatchName, innerParameter.Name},
                                           {GapMatchScoringParameter.GapMatchLabel, If(labelPrm IsNot Nothing, DirectCast(labelPrm, PlainTextParameter).Value, String.Empty)}
                                       })
            End If
        Next

        transformedParam.Value = New ParameterSetCollection()
        For Each h In Area.ShapeList.OrderBy(Function(x) x.Identifier)
            Dim paramSet = New ParameterCollection() With {.Id = h.Identifier}
            paramSet.InnerParameters.Add(New GapImageParameter() With
                                         {.MatchMax = -1, .Id = h.Identifier, .Name = GapMatchScoringParameter.GapControlName})
            transformedParam.Value.Add(paramSet)
        Next

        transformedParam.DesignerSettings = DesignerSettings

        transformedParam._isTransformed = True

        Return transformedParam
    End Function
End Class