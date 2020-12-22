Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.IO

Namespace ContentModel.Scoring.Validator

    Friend Class OtherGroupWithSameInteractionsExists
        Inherits ValidationRuleProcessor

        Protected Overrides Sub Validate(item As AssessmentItem)

            Dim solution As Solution = item.Solution

            For Each finding In solution.Findings
                If Not AtLeastOneOtherFactSetHaveSameInteractions(finding) Then
                    Throw New Exception()
                End If
            Next

        End Sub

        Private Function AtLeastOneOtherFactSetHaveSameInteractions(keyFinding As KeyFinding) As Boolean

            Dim findingClone As KeyFinding = CloneFinding(keyFinding)

            Dim factSetListA As List(Of Factset) = ToListOfSimpleFactSet(keyFinding.KeyFactsets)
            Dim factSetListB As List(Of Factset) = ToListOfSimpleFactSet(findingClone.KeyFactsets)

            For Each factSetA In factSetListA

                Dim match As New List(Of Factset)

                For Each factSetB In factSetListB
                    If factSetA.Facts.SetEquals(factSetB.Facts) Then
                        match.Add(factSetB)
                    End If
                Next

                If match.Count < 2 Then
                    Return False
                End If

            Next

            Return True
        End Function

        Private Function CloneFinding(finding As KeyFinding) As KeyFinding
            Dim xmlSerializer As New XmlSerializer(GetType(KeyFinding))
            Dim clone As New KeyFinding()
            Using stream As New MemoryStream()
                xmlSerializer.Serialize(stream, finding)
                stream.Seek(0, SeekOrigin.Begin)
                clone = DirectCast(xmlSerializer.Deserialize(stream), KeyFinding)
            End Using
            Return clone
        End Function

        Private Function ToListOfSimpleFactSet(factSets As List(Of KeyFactSet)) As List(Of Factset)
            Dim rFactSets As New List(Of Factset)()
            For Each factset In factSets
                Dim simpleFactSet As New Factset()
                For Each fact In factset.Facts
                    Dim simpleFact As New Fact()
                    For Each value In fact.Values
                        Dim simpleValue As New value() With {.domain = value.Domain}
                        simpleFact.values.Add(simpleValue)
                    Next
                    simpleFactSet.facts.Add(simpleFact)
                Next
                rFactSets.Add(simpleFactSet)
            Next
            Return rFactSets
        End Function


    End Class

End Namespace