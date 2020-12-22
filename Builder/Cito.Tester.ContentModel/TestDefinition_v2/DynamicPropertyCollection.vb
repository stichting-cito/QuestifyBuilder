Imports Cito.Tester.Common


<Serializable> _
Public Class DynamicPropertyCollection
    Inherits List(Of DynamicProperty)



    Public Sub New()
    End Sub



    Public Overloads Sub Add(prop As DynamicProperty)
        If prop Is Nothing Then
            Throw New ArgumentNullException("prop", "property is null.")
        End If
        If Me.ContainsProperty(prop.Name) Then
            Throw New DuplicateNameException($"Property with '{prop.Name}' already exists in collection.")
        End If
        MyBase.Add(prop)
    End Sub


    Public Overloads Sub Remove(prop As DynamicProperty)
        If prop Is Nothing Then
            Throw New ArgumentNullException("prop", "property is null.")
        End If
        If Not Me.ContainsProperty(prop.Name) Then
            Throw New ContentModelException($"Property with '{prop.Name}' does not exist in this collection.")
        End If
        MyBase.Remove(prop)
    End Sub


    Public Function ContainsProperty(name As String) As Boolean

        For Each settings As DynamicProperty In Me
            If settings.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) Then
                return True
            End If
        Next

        Return False
    End Function


    Default Public Overloads ReadOnly Property Item(name As String) As DynamicProperty
        Get
            For Each prop As DynamicProperty In Me
                If prop.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) Then
                    return prop
                End If
            Next

            Return Nothing
        End Get
    End Property


End Class

