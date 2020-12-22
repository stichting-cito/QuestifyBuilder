Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot(ElementName:="AssessmentTest2")> _
Public Class AssessmentTest2
    Inherits AssessmentTestNode

    Public Sub New()
        MyBase.New()
        Me.TestParts = New TestPartCollection2
        Me.IncludedViews = New List(Of String)
        Me.CutOffScoreConditions = New CutOffScoreConditions()
    End Sub


    <XmlArray("IncludedViews"),
    XmlArrayItem("ViewType", GetType(String))>
    Public ReadOnly Property IncludedViews As List(Of String)

    Public Overrides ReadOnly Property IsPickable As Boolean
        Get
            For Each part As TestPart2 In Me.TestParts
                If part.IsPickable Then
                    return True
                End If
            Next

            Return false
        End Get
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property MaxScore As Double
        Get
            Dim cumulativeMaxScore As Double = 0
            For Each part As TestPart2 In Me.TestParts
                cumulativeMaxScore += part.MaxScore
            Next

            Return cumulativeMaxScore
        End Get
    End Property

    <XmlElement("CutOffScoreConditions")> _
    Public Property CutOffScoreConditions As CutOffScoreConditions

    <XmlArray("TestParts"), _
    XmlArrayItem("Part", GetType(TestPart2))> _
    Public ReadOnly Property TestParts As TestPartCollection2


    Private Shared Function FindItemReferenceInSection(section As TestSection2, nameToFind As String) As ItemReference2
        Dim result As ItemReference2 = Nothing

        For Each comp As TestComponent2 In section.Components
            If TypeOf comp Is ItemReference2 Then
                Dim item As ItemReference2 = DirectCast(comp, ItemReference2)
                If item.SourceName = nameToFind Then
                    return item
                End If
            Else
                Dim innerSection As TestSection2 = DirectCast(comp, TestSection2)
                result = FindItemReferenceInSection(innerSection, nameToFind)
                If result IsNot Nothing Then
                    Return result

                End If
            End If
        Next

        Return result
    End Function

    Public Function GetItemReferenceByName(name As String) As ItemReference2
        Dim result As ItemReference2 = Nothing

        For Each part As TestPart2 In Me.TestParts
            For Each section As TestSection2 In part.Sections
                result = FindItemReferenceInSection(section, name)
                If result IsNot Nothing Then
                    Return result
                End If
            Next
        Next

        Return result
    End Function

    Public Function FindNodeByIdentifier(id As String) As AssessmentTestNode
        Dim result As AssessmentTestNode = Nothing

        For Each part As TestPart2 In Me.TestParts
            result = part.FindNodeByIdentifier(id)
            If result IsNot Nothing Then
                Return result
            End If
        Next

        Return result
    End Function

    Public Function GetAllItemReferencesInTest() As ReadOnlyCollection(Of ItemReference2)
        Dim allItemsInTestCollection As New List(Of ItemReference2)

        For Each part As TestPart2 In Me.TestParts
            For Each section As TestSection2 In part.Sections
                allItemsInTestCollection.AddRange(section.GetAllItemReferencesInSection(True))
            Next
        Next

        Return New ReadOnlyCollection(Of ItemReference2)(allItemsInTestCollection)
    End Function

    Public Function GetAllSectionsInTest() As ReadOnlyCollection(Of TestSection2)
        Dim allSectionsInTestCollection As New List(Of TestSection2)

        For Each part As TestPart2 In Me.TestParts
            For Each section As TestSection2 In part.Sections
                allSectionsInTestCollection.Add(section)
                allSectionsInTestCollection.AddRange(section.GetAllSectionsInSection())
            Next
        Next

        Return New ReadOnlyCollection(Of TestSection2)(allSectionsInTestCollection)
    End Function

End Class