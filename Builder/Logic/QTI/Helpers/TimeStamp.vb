Namespace QTI.Helpers
    Public Class TimeStamp
        Implements ITimeStamp

        Private _lastValue As Long
        Public Function GetUniqueSequenceNumberFromCurrentDateTime() As Long Implements ITimeStamp.GetUniqueSequenceNumberFromCurrentDateTime
            Dim startDate As New DateTime(2013, 1, 1)
            _lastValue = CInt((DateTime.Now() - startDate).TotalSeconds)
            Return _lastValue
        End Function

        Public Function GetLastValue() As Long Implements ITimeStamp.GetLastValue
            Return _lastValue
        End Function

    End Class
End NameSpace