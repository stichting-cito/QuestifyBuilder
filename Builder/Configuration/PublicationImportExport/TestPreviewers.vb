Imports System.Configuration
Imports System.Diagnostics.CodeAnalysis

<SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")> _
Public NotInheritable Class TestPreviewers
    Inherits ConfigurationElementCollection

    Protected Overloads Overrides Function CreateNewElement() As ConfigurationElement
        Return New TestPreviewer()
    End Function

    Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
        Return DirectCast(element, TestPreviewer).Name
    End Function
End Class

