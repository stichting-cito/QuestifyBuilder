Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Helpers.QTI30

    Public Class ResponseDeclarationHelper

        Public Shared Function GetStringInterpretationOfValueTypes(values As List(Of ValueType)) As List(Of String)
            Dim result As New List(Of String)
            For Each valueType As ValueType In values
                result.Add(valueType.Value)
            Next
            Return result
        End Function

        Public Shared Function CreateEmptyResponseDeclaration(cardinality As ResponseDeclarationTypeCardinality, identifier As String) As ResponseDeclarationType
            Dim responseDeclarationType As New ResponseDeclarationType
            responseDeclarationType.identifier = identifier
            responseDeclarationType.cardinality = cardinality
            responseDeclarationType.baseType = ResponseDeclarationTypeBaseType.string
            responseDeclarationType.baseTypeSpecified = True
            Return responseDeclarationType
        End Function
    End Class
End NameSpace