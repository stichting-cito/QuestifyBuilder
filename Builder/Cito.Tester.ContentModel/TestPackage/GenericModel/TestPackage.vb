Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot(ElementName:="TestPackage")> _
Public Class TestPackage
    Inherits TestPackageNode



    Private _testSets As TestsetCollection
    Private _includedViews As List(Of String)



    Public Sub New()
        MyBase.New()
        Me._testSets = New TestsetCollection(Me)
        Me._includedViews = New List(Of String)
    End Sub


    <XmlArray("TestSets"), _
   XmlArrayItem("Set", GetType(TestSet))> _
    Public ReadOnly Property TestSets As TestsetCollection
        Get
            Return Me._testSets
        End Get
    End Property



    <XmlArray("IncludedViews"),
    XmlArrayItem("ViewType", GetType(String))>
    Public ReadOnly Property IncludedViews As List(Of String)
        Get
            Return _includedViews
        End Get
    End Property







    Public Function GetTestReferenceByName(name As String) As TestReference
        Dim result As TestReference = Nothing

        For Each testset As TestSet In Me.TestSets
            result = FindTestReferenceInTestset(testset, name)
            If result IsNot Nothing Then
                Return result
            End If
        Next

        Return result
    End Function


    Public Function FindNodeByIdentifier(identifier As String) As TestPackageNode
        Dim result As TestPackageNode = Nothing

        For Each testset As TestSet In Me.TestSets
            result = testset.FindNodeByIdentifier(identifier)
            If result IsNot Nothing Then
                return result
            End If
        Next

        Return result
    End Function


    Public Function GetAllTestReferencesInTestPackage() As ReadOnlyCollection(Of TestReference)
        Dim allTestsInTestPackageCollection As New List(Of TestReference)

        For Each testset As TestSet In Me.TestSets
            allTestsInTestPackageCollection.AddRange(testset.GetAllTestReferencesInTestSet())

        Next

        Return New ReadOnlyCollection(Of TestReference)(allTestsInTestPackageCollection)
    End Function






    Private Shared Function FindTestReferenceInTestset(testSet As TestSet, nameToFind As String) As TestReference
        Dim result As TestReference = Nothing

        For Each comp As TestPackageComponent In testSet.Components
            If TypeOf comp Is TestReference Then
                Dim test As TestReference = DirectCast(comp, TestReference)
                If test.SourceName = nameToFind Then
                    return test
                End If
            End If
        Next
        Return result
    End Function

End Class
