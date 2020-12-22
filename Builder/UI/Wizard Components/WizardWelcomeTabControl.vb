Public Class WizardWelcomeTabControl

    Public Sub New()

        InitializeComponent()


    End Sub


    Public Property Title() As String
        Get
            Return TitleLabel.Text
        End Get
        Set(ByVal value As String)
            TitleLabel.Text = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return DescriptionLabel.Text
        End Get
        Set(ByVal value As String)
            DescriptionLabel.Text = value
        End Set
    End Property


    Public Property HideWelcomeTab() As Boolean
        Get
            Return Me.HideWelcomeTabCheckBox.Checked
        End Get
        Set(ByVal value As Boolean)
            Me.HideWelcomeTabCheckBox.Checked = value
        End Set
    End Property



    Public Property LeftPanelBackgroundImage() As Image
        Get
            Return LeftPanel.BackgroundImage
        End Get
        Set(ByVal value As Image)
            LeftPanel.BackgroundImage = value
        End Set
    End Property

    Public Property LeftPanelBackgroundImageLayout() As ImageLayout
        Get
            Return LeftPanel.BackgroundImageLayout
        End Get
        Set(ByVal value As ImageLayout)
            LeftPanel.BackgroundImageLayout = value
        End Set
    End Property

    Public Property LeftPanelBackColor() As Color
        Get
            Return LeftPanel.BackColor
        End Get
        Set(ByVal value As Color)
            LeftPanel.BackColor = value
        End Set
    End Property

    Public Property LeftPanelWidth() As Integer
        Get
            Return LeftPanel.Width
        End Get
        Set(ByVal value As Integer)
            LeftPanel.Width = value
        End Set
    End Property



    Public Property RightPanelBackgroundImage() As Image
        Get
            Return RightPanel.BackgroundImage
        End Get
        Set(ByVal value As Image)
            RightPanel.BackgroundImage = value
        End Set
    End Property

    Public Property RightPanelBackgroundImageLayout() As ImageLayout
        Get
            Return RightPanel.BackgroundImageLayout
        End Get
        Set(ByVal value As ImageLayout)
            RightPanel.BackgroundImageLayout = value
        End Set
    End Property

    Public Property RightPanelBackColor() As Color
        Get
            Return RightPanel.BackColor
        End Get
        Set(ByVal value As Color)
            RightPanel.BackColor = value
        End Set
    End Property


End Class
