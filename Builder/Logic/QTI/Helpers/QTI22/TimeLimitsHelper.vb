Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22

    Public Class TimeLimitsHelper

        Public Shared Function GetTimeLimitsType(timeLimits As TimeLimits) As TimeLimitsType
            If Not ChainHandlerHelper.TimeLimitsIsEmpty(timeLimits) Then
                Dim newTimeListType As New TimeLimitsType
                newTimeListType.maxTime = timeLimits.MaxTime
                newTimeListType.minTime = timeLimits.MinTime
                newTimeListType.maxTimeSpecified = (timeLimits.MaxTime > 0)
                newTimeListType.minTimeSpecified = (timeLimits.MinTime > 0)
                Return newTimeListType
            End If
            Return Nothing
        End Function

    End Class

End Namespace