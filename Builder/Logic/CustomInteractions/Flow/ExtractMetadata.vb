Imports System.Activities

Namespace CustomInteractions.Flow

    Public NotInheritable Class ExtractMetadata
        Inherits CodeActivity(Of MetadataRoot)
        Public Property FileName As InArgument(Of String)

        Public Property IsGeogebraFile As InArgument(Of Boolean)

        Protected Overrides Function Execute(context As CodeActivityContext) As MetadataRoot
            Dim path As String = context.GetValue(FileName)
            Dim isGeogebra As Boolean = context.GetValue(IsGeogebraFile)

            If Not isGeogebra Then
                Return New CiPackageValidator(path).GetMetaData()
            Else
                Return New GeogebraPackageValidator(path).GetMetaData()
            End If
        End Function
    End Class
End NameSpace