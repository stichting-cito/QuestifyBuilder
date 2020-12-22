Imports System.Xml.Serialization

Public Class HotspotScoringParameter : Inherits ChoiceScoringParameter

    Private _hotspots As AreaParameter
    Private _numberOfResponses As IntegerParameter

    <XmlElement("subparameterset", GetType(ParameterCollection))> _
    Public Overrides Property Value As ParameterSetCollection
        Get
            If _hotspots Is Nothing OrElse (MyBase.Value IsNot Nothing AndAlso _hotspots.ShapeList.Count <> MyBase.Value.Count) Then

                If MyBase.Value IsNot Nothing Then
                    MyBase.Value.Clear()
                End If
                If _hotspots IsNot Nothing Then
                    For Each h In _hotspots.ShapeList
                        Dim paramSet = New ParameterCollection() With {.Id = h.Identifier}

                        If MyBase.Value Is Nothing Then
                            MyBase.Value = New ParameterSetCollection()
                        End If
                        MyBase.Value.Add(paramSet)
                    Next
                End If
            End If

            Return MyBase.Value
        End Get
        Set
            MyBase.Value = value
        End Set
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property IsSingleChoice As Boolean
        Get
            Return False
        End Get
    End Property

    <ParameterControlAttribute> _
    <XmlElement("areaparameter")> _
    Public Property Area As AreaParameter
        Get
            Return _hotspots
        End Get
        Set(shapes As AreaParameter)
            _hotspots = shapes
        End Set
    End Property

    Public Overrides Function GetParametersWithDesignerSettings() As IEnumerable(Of ParameterBase)
        Return New ParameterBase() {Area}
    End Function

End Class
