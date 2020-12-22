Public NotInheritable Class ArrayHelper

    Private Sub New()

    End Sub


    Public Shared Function CompareByteArray(dataArrayA As Byte(), dataArrayB As Byte()) As Boolean
        Dim isEqual As Boolean = ReferenceEquals(dataArrayA, dataArrayB)

        If Not isEqual AndAlso dataArrayA IsNot Nothing AndAlso dataArrayB IsNot Nothing AndAlso dataArrayA.Length = dataArrayB.Length Then
            Dim index As Integer = 0
            While index < dataArrayA.Length AndAlso dataArrayA(index) = dataArrayB(index)
                index += 1
            End While

            isEqual = (index = dataArrayA.Length)
        End If

        Return isEqual
    End Function
End Class
