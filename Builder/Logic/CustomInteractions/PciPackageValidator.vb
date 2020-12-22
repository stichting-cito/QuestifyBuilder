Imports System.IO
Imports System.IO.Compression
Imports System.Linq


Namespace CustomInteractions
    Public Class PciPackageValidator : Inherits CiPackageValidator
        Public Sub New(path As String)
            MyBase.New(path)
        End Sub

        Public Overrides Function TryValidate(ByRef errorMessages As List(Of String)) As Boolean
            Return TryValidate(errorMessages, New MetadataRoot())
        End Function

        Public Overrides Function TryValidate(ByRef errorMessages As List(Of String), ByRef metaData As MetadataRoot) As Boolean
            errorMessages = Validate(errorMessages, metaData, False)
            Return Not errorMessages.Any() AndAlso metadata IsNot Nothing AndAlso metadata.Modules.Any() AndAlso Not String.IsNullOrEmpty(metadata.TypeIdentifier)
        End Function
    End Class
End Namespace