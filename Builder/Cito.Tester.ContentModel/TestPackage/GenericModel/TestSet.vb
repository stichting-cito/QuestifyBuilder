
Imports System.Xml.Serialization


<Serializable, _
 XmlRoot(ElementName:="TestSet")> _
Public Class TestSet
    Inherits TestPackageComponent

    Private _components As TestPackageComponentCollection


    <XmlArray("Components"), _
XmlArrayItem("TestReference", GetType(TestReference))> _
    Public ReadOnly Property Components As TestPackageComponentCollection
        Get
            Return Me._components
        End Get
    End Property





    Public Function GetAllTestReferencesInTestSet() As List(Of TestReference)
        Dim returnValue As New List(Of TestReference)

        For Each comp As TestPackageComponent In Me.Components
            returnValue.Add(DirectCast(comp, TestReference))
        Next

        Return returnValue
    End Function


    Public Function FindNodeByIdentifier(identifier As String) As TestPackageNode
        Dim result As TestPackageNode = Nothing

        If Me.Identifier = identifier Then
            result = Me
        Else
            For Each component As TestPackageComponent In Me.Components
                If TypeOf component Is TestSet Then
                    result = DirectCast(component, TestSet).FindNodeByIdentifier(identifier)
                    If result IsNot Nothing Then
                        Return result
                    End If
                End If
            Next
        End If

        Return result
    End Function


    Public Sub New()
        MyBase.New()
        Me._components = New TestPackageComponentCollection(Me)
    End Sub

End Class
