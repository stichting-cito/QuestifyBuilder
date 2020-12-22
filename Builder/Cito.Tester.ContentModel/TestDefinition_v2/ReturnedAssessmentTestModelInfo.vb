Public Class ReturnedAssessmentTestModelInfo
    Private _assessmentTestv2 As AssessmentTest2


    Public Sub New(assessmentTestv2 As AssessmentTest2)
        _assessmentTestv2 = assessmentTestv2
    End Sub

    Public ReadOnly Property AssessmentTestv2 As AssessmentTest2
        Get
            Return _assessmentTestv2
        End Get
    End Property


End Class
