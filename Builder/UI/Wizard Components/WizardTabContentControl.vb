Imports System.ComponentModel
Imports System.ComponentModel.Design

<Designer("System.Windows.Forms.Design.ParentControlDesigner,System.Design", GetType(IDesigner))> _
Public Class WizardTabContentControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property Task() As String
        Get
            Return TaskLabel.Text
        End Get
        Set(ByVal value As String)
            TaskLabel.Text = value
        End Set
    End Property

    Public Property TaskDescription() As String
        Get
            Return TaskDescriptionLabel.Text
        End Get
        Set(ByVal value As String)
            TaskDescriptionLabel.Text = value
        End Set
    End Property

    Public Property TaskPanelBackgroundImage() As Image
        Get
            Return TaskPanel.BackgroundImage
        End Get
        Set(ByVal value As Image)
            TaskPanel.BackgroundImage = value
        End Set
    End Property

    Public Property TaskPanelBackgroundImageLayout() As ImageLayout
        Get
            Return TaskPanel.BackgroundImageLayout
        End Get
        Set(ByVal value As ImageLayout)
            TaskPanel.BackgroundImageLayout = value
        End Set
    End Property

    Public Property TaskPanelBackColor() As Color
        Get
            Return TaskPanel.BackColor
        End Get
        Set(ByVal value As Color)
            TaskPanel.BackColor = value
        End Set
    End Property

    Public Property TaskPanelHeight() As Integer
        Get
            Return TaskPanel.Height
        End Get
        Set(ByVal value As Integer)
            TaskPanel.Height = value
        End Set
    End Property

End Class
