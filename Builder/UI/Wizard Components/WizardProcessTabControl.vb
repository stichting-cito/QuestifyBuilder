Public Class WizardProcessTabControl

    Public Sub New()

        InitializeComponent()

        ProcessProgressOverallBar.Visible = False
        ProcessStepInfoLabelOverall.Visible = False
        ProcessProgressDetailBar.Top += 35
        ProcessStepInfoLabelDetail.Height += 35
    End Sub

    Private _progressStepOffsetOverall As Integer
    Private _progressStepOffsetDetail As Integer



    Public Property PublishMultiple As Boolean
        Get
            Return ProcessProgressOverallBar.Visible AndAlso ProcessStepInfoLabelOverall.Visible
        End Get
        Set(value As Boolean)
            ProcessProgressOverallBar.Visible = value
            ProcessStepInfoLabelOverall.Visible = value
            If value Then
                ProcessProgressDetailBar.Top -= 35
                ProcessStepInfoLabelDetail.Height -= 35
            End If
        End Set
    End Property


    Public Property ProcessInfoTextOverall() As String
        Get
            Return ProcessStepInfoLabelOverall.Text
        End Get
        Set(ByVal value As String)
            ProcessStepInfoLabelOverall.Text = value
        End Set
    End Property


    Public Property ProcessInfoTextDetail() As String
        Get
            Return ProcessStepInfoLabelDetail.Text
        End Get
        Set(ByVal value As String)
            ProcessStepInfoLabelDetail.Text = value
        End Set
    End Property

    Public Property ProgressMaximumValueOverAll() As Integer
        Get
            Return ProcessProgressOverallBar.Maximum
        End Get
        Set(ByVal value As Integer)
            ProcessProgressOverallBar.Maximum = value
        End Set
    End Property

    Public Property ProgressMaximumValueDetail() As Integer
        Get
            Return ProcessProgressDetailBar.Maximum
        End Get
        Set(ByVal value As Integer)
            If value >= 0 Then
                ProcessProgressDetailBar.Maximum = value
            End If
            If value = 0 Then
                ProcessProgressDetailBar.Style = ProgressBarStyle.Marquee
                ProcessProgressDetailBar.Maximum = 1
            Else
                ProcessProgressDetailBar.Style = ProgressBarStyle.Blocks
            End If
        End Set
    End Property

    Public Property ProgressMinimumValueOverAll() As Integer
        Get
            Return ProcessProgressOverallBar.Minimum
        End Get
        Set(ByVal value As Integer)
            ProcessProgressOverallBar.Minimum = value
        End Set
    End Property

    Public Property ProgressMinimumValueDetail() As Integer
        Get
            Return ProcessProgressDetailBar.Minimum
        End Get
        Set(ByVal value As Integer)
            ProcessProgressDetailBar.Minimum = value
        End Set
    End Property

    Public Property ProgressStepOffsetOverAll() As Integer
        Get
            Return _progressStepOffsetOverall
        End Get
        Set(ByVal value As Integer)
            _progressStepOffsetOverall = value
        End Set
    End Property

    Public Property ProgressStepOffsetDetail() As Integer
        Get
            Return _progressStepOffsetDetail
        End Get
        Set(ByVal value As Integer)
            _progressStepOffsetDetail = value
        End Set
    End Property

    Public Property ProgressValueOverAll() As Integer
        Get
            Return ProcessProgressOverallBar.Value
        End Get
        Set(ByVal value As Integer)
            ProcessProgressOverallBar.Value = value
        End Set
    End Property

    Public Property ProgressValueDetail() As Integer
        Get
            Return ProcessProgressDetailBar.Value
        End Get
        Set(ByVal value As Integer)
            If Not value > ProcessProgressDetailBar.Maximum Then
                ProcessProgressDetailBar.Value = value
            End If
        End Set
    End Property


    Public Sub ResetProgressValueOverAll(ByVal stepText As String)
        ProcessInfoTextOverall = stepText
        ProgressValueOverAll = ProcessProgressOverallBar.Minimum
    End Sub

    Public Sub SetProgressCompletedOverAll(ByVal stepText As String)
        ProcessInfoTextOverall = stepText
        ProgressValueOverAll = ProcessProgressOverallBar.Maximum
    End Sub

    Public Sub NextStepOverAll(ByVal stepText As String)
        ProcessInfoTextOverall = stepText

        If (ProgressValueOverAll + ProgressStepOffsetOverAll) > ProgressMaximumValueOverAll Then
            ProgressValueOverAll = ProgressMaximumValueOverAll
        Else
            ProgressValueOverAll += ProgressStepOffsetOverAll
        End If

    End Sub

    Public Sub ResetProgressValueDetail(ByVal stepText As String)
        ProcessInfoTextDetail = stepText
        ProcessProgressDetailBar.Value = ProcessProgressDetailBar.Minimum
    End Sub

    Public Sub SetProgressCompletedDetail(ByVal stepText As String)
        ProcessInfoTextDetail = stepText
        ProcessProgressDetailBar.Value = ProcessProgressDetailBar.Maximum
    End Sub

    Public Sub NextStepDetail(ByVal stepText As String)
        ProcessInfoTextDetail = stepText

        If (ProgressValueDetail + ProgressStepOffsetDetail) > ProgressMaximumValueDetail Then
            ProgressValueDetail = ProgressMaximumValueDetail
        Else
            ProgressValueDetail += ProgressStepOffsetDetail
        End If

    End Sub


End Class
