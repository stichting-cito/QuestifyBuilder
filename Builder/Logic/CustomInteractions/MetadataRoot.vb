Imports System.ComponentModel
Imports System.Linq
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters

Namespace CustomInteractions

    Public Enum CommunicationType
        Answer
        State
        None
    End Enum

    <JsonObject>
    Public Class MetadataRoot

        <JsonProperty(PropertyName:="title", Required:=Required.Default)>
        Public Property Title As String

        <JsonProperty(PropertyName:="width", Required:=Required.Default)>
        Public Property Width As Integer

        <JsonProperty(PropertyName:="height", Required:=Required.Default)>
        Public Property Height As Integer

        <JsonProperty(PropertyName:="scalable", Required:=Required.Default)>
        Public Property Scalable As Boolean

        <JsonProperty(PropertyName:="communicationtype", Required:=Required.Default)>
        <JsonConverter(GetType(StringEnumConverter))>
        <DefaultValue(CustomInteractions.CommunicationType.Answer)>
        Public Property CommunicationType As CommunicationType

        <JsonProperty(PropertyName:="scoring", Required:=Required.Default)>
        Public Property Scoring As IList(Of MetadataScoring)

        <JsonProperty(PropertyName:="modules", Required:=Required.Default)>
        Public Property Modules As IList(Of MetadataPciModule)

        <JsonProperty(PropertyName:="typeIdentifier", Required:=Required.Default)>
        Public Property TypeIdentifier As String

        Public Function GetScoring() As IEnumerable(Of ScoringTypeBase)
            If Scoring IsNot Nothing Then
                Return (From scoring In Scoring From s In scoring.GetType().GetProperties() Select s.GetValue(scoring, Nothing)).OfType(Of ScoringTypeBase)().ToList()
            End If
            Return Nothing
        End Function

        Public Function IsValid(errorMessage As List(Of String)) As Boolean
            Dim valid = True

            If Scoring IsNot Nothing Then
                If GetScoring.Any() Then
                    For Each scoringTypeBase As ScoringTypeBase In GetScoring()
                        valid = valid And scoringTypeBase.IsValid(errorMessage)
                    Next
                Else
                    errorMessage.Add(My.Resources.ScoringEmpty)
                    Return False
                End If
            End If

            Return valid
        End Function

    End Class

End Namespace