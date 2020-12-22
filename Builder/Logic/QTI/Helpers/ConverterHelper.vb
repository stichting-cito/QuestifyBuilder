Imports Questify.Builder.Logic.QTI.Converters.XhtmlConverter.QTI_Base
Imports Questify.Builder.Logic.QTI.Converters.XhtmlConverter.QTI30
Imports Questify.Builder.Logic.QTI.Interfaces
Imports Questify.Builder.Logic.QTI.Requests.BaseClasses
Imports Questify.Builder.Logic.QTI.Requests.QTI22
Imports Questify.Builder.Logic.QTI.Requests.QTI30

Namespace QTI.Helpers
    Public Class ConverterHelper(Of T As PublicationRequestBase)

        Public Sub New()

        End Sub

        Public Function GetXhtmlConverter(uniqueId As String) As IXhtmlConverter
            Dim returnValue As IXhtmlConverter
            Select Case GetType(T).ToString
                Case GetType(QTI22PublicationRequest).ToString
                    returnValue = DirectCast(Activator.CreateInstance(GetType(QTIXhtmlConverter)), IXhtmlConverter)
                Case GetType(PublicationRequest).ToString
                    returnValue = DirectCast(Activator.CreateInstance(GetType(QTI30XhtmlConverter)), IXhtmlConverter)
                Case Else
                    returnValue = Nothing
            End Select
            If returnValue IsNot Nothing Then
                returnValue.Initialise(uniqueId)
            End If
            Return returnValue
        End Function

    End Class
End Namespace