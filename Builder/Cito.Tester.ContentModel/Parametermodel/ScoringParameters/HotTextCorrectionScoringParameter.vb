Imports System.Xml.Serialization


Public Class HotTextCorrectionScoringParameter : Inherits StringScoringParameter

    Private _correctionIsApplicable As Boolean
    Private _relatedControlLabelParameter As PlainTextParameter
    Private _lastTextToCorrectUsedToCalculateValue As String

    <XmlIgnore> _
    Public Overrides ReadOnly Property ShouldSuffix As Boolean
        Get
            Return False
        End Get
    End Property

    <XmlIgnore> _
    Public Overrides Property InlineId As String
        Get
            If MyBase.InlineId IsNot Nothing AndAlso Not MyBase.InlineId.StartsWith("Input_") Then
                Return String.Concat("Input_", MyBase.InlineId)
            Else
                Return MyBase.InlineId
            End If
        End Get
        Set
            MyBase.InlineId = value
        End Set
    End Property

    <XmlElement("relatedControlLabel")> _
    Public Property RelatedControlLabelParameter As PlainTextParameter
        Get
            Return _relatedControlLabelParameter
        End Get
        Set
            _relatedControlLabelParameter = value
        End Set
    End Property

    <XmlIgnore> _
    Public ReadOnly Property TextToCorrect As String
        Get
            Return If(_relatedControlLabelParameter IsNot Nothing, _relatedControlLabelParameter.Value, String.Empty)
        End Get
    End Property

    <XmlAttribute("correctionIsApplicable")>
    Public Property CorrectionIsApplicable As Boolean
        Get
            Return _correctionIsApplicable
        End Get
        Set
            _correctionIsApplicable = value
        End Set
    End Property


    <XmlElement("subparameterset", GetType(ParameterCollection))> _
    Public Overrides Property Value As ParameterSetCollection
        Get
            If CorrectionIsApplicable Then
                If String.IsNullOrEmpty(_lastTextToCorrectUsedToCalculateValue) AndAlso Not String.IsNullOrEmpty(TextToCorrect) Then
                    If Not TextToCorrect.Equals(_lastTextToCorrectUsedToCalculateValue) Then
                        _lastTextToCorrectUsedToCalculateValue = TextToCorrect

                        If MyBase.Value IsNot Nothing Then
                            MyBase.Value.Clear()
                        End If
                        Dim paramSet = New ParameterCollection() With {.Id = "1"}
                        If MyBase.Value Is Nothing Then
                            MyBase.Value = New ParameterSetCollection()
                        End If
                        MyBase.Value.Add(paramSet)
                    End If
                End If
            End If
            Return MyBase.Value
        End Get
        Set
            MyBase.Value = value
        End Set
    End Property


    Public Overrides Function GetLabelFor(scoreKey As String) As String
        Return TextToCorrect
    End Function

End Class
