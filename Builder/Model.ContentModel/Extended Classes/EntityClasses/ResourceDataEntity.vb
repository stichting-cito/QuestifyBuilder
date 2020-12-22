Imports System.IO
Imports Cito.Tester.Common

Namespace Questify.Builder.Model.ContentModel.EntityClasses
    Partial Public Class ResourceDataEntity
        Public Function GetStream() As StreamResource
            Dim streamResourceToReturn As StreamResource
            Dim stream As New MemoryStream(BinData)

            streamResourceToReturn = New StreamResource(stream, stream.Length)

            Return streamResourceToReturn
        End Function
    End Class
End Namespace