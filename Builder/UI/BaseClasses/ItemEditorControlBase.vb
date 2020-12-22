Public Class ItemEditorControlBase


    Private _assessmentItem As Cito.Tester.ContentModel.AssessmentItem



    Public Overridable Property AssessmentItem() As Cito.Tester.ContentModel.AssessmentItem
        Get
            Return _assessmentItem
        End Get
        Set(ByVal value As Cito.Tester.ContentModel.AssessmentItem)
            If value IsNot Nothing Then
                Dim refresh As Boolean
                If _assessmentItem IsNot Nothing Then
                    refresh = True
                End If

                _assessmentItem = value
                AssessmentItemBindingSource.DataSource = _assessmentItem

                If refresh Then
                    AssessmentItemBindingSource.ResetCurrentItem()
                End If
            End If
        End Set
    End Property



    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(ByVal assessmentItem As Cito.Tester.ContentModel.AssessmentItem)
        InitializeComponent()


        Me.AssessmentItem = assessmentItem
    End Sub


End Class
