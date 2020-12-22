Imports System.Diagnostics.CodeAnalysis
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Factories

Public Class WizardBase

    Private _bankId As Integer
    Private _bankName As String
    Private _previousTab As TabPage
    Private _progressMinimumstepsRequired As Integer = 2
    Private ReadOnly _stateStack As New StateStack
    Private _supportsCancelation As Boolean = False
    Private _aSyncProcessing As Boolean = False
    Private ReadOnly _wizardTabStack As New Stack(Of TabPage)



    Public Property BankId As Integer
        Get
            Return _bankId
        End Get
        Set(value As Integer)
            _bankId = value
        End Set
    End Property

    Public ReadOnly Property BankName As String
        Get
            If String.IsNullOrEmpty(_bankName) Then
                _bankName = BankFactory.Instance.GetBankName(_bankId)
            End If
            Return _bankName
        End Get

    End Property

    Public Property BankBreadCrumb As String

    Public Property CurrentTabTitle As String

    <SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")>
    Public Property ProgressMinimumStepsRequired As Integer
        Get
            Return _progressMinimumstepsRequired
        End Get
        Set(value As Integer)
            If value < 0 Then
                Throw New UIException("Invalid value.")
            End If
            _progressMinimumstepsRequired = value
        End Set
    End Property

    Public Property SupportsCancelation As Boolean
        Get
            Return _supportsCancelation
        End Get
        Set(value As Boolean)
            CancelBtn.Enabled = value
            _supportsCancelation = value
        End Set
    End Property

    Public Overridable ReadOnly Property ASyncProcessing As Boolean
        Get
            Return _aSyncProcessing
        End Get

    End Property




    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        If CancelBtn.Text = My.Resources.Close Then
            Me.DialogResult = DialogResult.OK
            OnCloseWizard()
        Else
            OnCancelWizard()
        End If
    End Sub

    Private Sub HelpBtn_Click(sender As Object, e As EventArgs) Handles HelpBtn.Click
        OnHelp()
    End Sub

    Private Function IsLoaded() As Boolean
        Return WelcomeTabPageControl IsNot Nothing
    End Function

    Private Sub NextButton_Click(sender As Object, e As EventArgs) Handles NextButton.Click
        OnGotoNextTab()
    End Sub

    Private Sub PreviousButton_Click(sender As Object, e As EventArgs) Handles PreviousButton.Click
        OnGotoPreviousTab()
    End Sub

    Private Sub ProcessButton_Click(sender As Object, e As EventArgs) Handles ProcessButton.Click
        OnGotoNextTab()
    End Sub

    Protected Sub SetASyncProcessing(asyncValue As Boolean)
        _aSyncProcessing = asyncValue
    End Sub

    Protected Sub SetButtons()
        PreviousButton.Enabled = (Not TabControlMain.SelectedTab.Equals(WelcomeTabPageControl)) AndAlso (Not TabControlMain.SelectedTab.Equals(ResultTabPageControl)) AndAlso (Not TabControlMain.SelectedTab.Equals(ProcessTabPageControl))
        NextButton.Enabled = (Not TabControlMain.SelectedTab.Equals(ProcessTabPageControl)) AndAlso (Not TabControlMain.SelectedTab.Equals(ResultTabPageControl)) AndAlso (Not TabControlMain.SelectedTab.Equals(OverviewTabPageControl))
        ProcessButton.Enabled = Not NextButton.Enabled AndAlso Not TabControlMain.SelectedTab.Equals(ProcessTabPageControl)
        HelpBtn.Visible = WizardHelpEvent IsNot Nothing
        If TabControlMain.SelectedTab.Equals(ResultTabPageControl) Then
            Me.CancelBtn.Text = My.Resources.Close
        Else
            Me.CancelBtn.Text = My.Resources.Cancel
        End If
        PreviousButton.Visible = (Not TabControlMain.SelectedTab.Equals(ResultTabPageControl))
        NextButton.Visible = (Not TabControlMain.SelectedTab.Equals(ResultTabPageControl))
        ProcessButton.Visible = (Not TabControlMain.SelectedTab.Equals(ResultTabPageControl))

        If ProcessButton.Enabled Then
            Me.AcceptButton = ProcessButton
        End If

        CancelBtn.Enabled = True
        CancelBtn.Visible = True
    End Sub


    Private Sub WizardBase_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsLoaded() Then
            If TabControlMain.TabPages.IndexOf(WelcomeTabPageControl) > 0 Then
                TabControlMain.TabPages.Remove(WelcomeTabPageControl)
                TabControlMain.TabPages.Insert(0, WelcomeTabPageControl)
            End If

            If TabControlMain.TabPages.IndexOf(OverviewTabPageControl) <> TabControlMain.TabPages.Count - 3 Then
                TabControlMain.TabPages.Remove(OverviewTabPageControl)
                TabControlMain.TabPages.Add(OverviewTabPageControl)
            End If

            If TabControlMain.TabPages.IndexOf(ProcessTabPageControl) <> TabControlMain.TabPages.Count - 2 Then
                TabControlMain.TabPages.Remove(ProcessTabPageControl)
                TabControlMain.TabPages.Add(ProcessTabPageControl)
            End If

            If TabControlMain.TabPages.IndexOf(ResultTabPageControl) <> TabControlMain.TabPages.Count - 1 Then
                TabControlMain.TabPages.Remove(ResultTabPageControl)
                TabControlMain.TabPages.Add(ResultTabPageControl)
            End If

            TabControlMain.SelectedTab = WelcomeTabPageControl
            SetButtons()
        End If
    End Sub

    Private Sub TabControlMain_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControlMain.Deselecting
        _previousTab = e.TabPage
    End Sub

    Private Sub TabControlMain_SelectedIndexChanged(sender As Object, e As TabControlEventArgs) Handles TabControlMain.Selected
        Dim selected As TabPage = e.TabPage

        If IsLoaded() Then
            OnTabChanged(selected, _previousTab)

            If selected.Equals(ProcessTabPageControl) Then
                Me.BeginInvoke(New MethodInvoker(AddressOf AfterTabChange))
            End If
        End If
    End Sub

    Private Sub AfterTabChange()
        If Not ASyncProcessing Then
            Dim myArgs As New WizardProcessEventArgs(_previousTab, TabControlMain.SelectedTab)

            OnBeforeProcess(myArgs)
            If Not myArgs.Cancel Then
                myArgs = New WizardProcessEventArgs(_previousTab, TabControlMain.SelectedTab)
                OnDoProcess(myArgs)
                If Not ASyncProcessing Then
                    If myArgs.Cancel Then
                        SelectTab(myArgs.PreviousTab)
                    Else
                        SelectTab(ResultTabPageControl)
                    End If
                End If
            Else
                SelectTab(myArgs.PreviousTab)
            End If
        End If

        If TabControlMain.SelectedTab.Equals(ResultTabPageControl) Then
            OnWizardCompleted()
        End If
    End Sub


    Protected Sub SelectTab(tab As TabPage)
        If InvokeRequired Then
            TabControlMain.Invoke(Sub() SelectTab(tab))
        Else
            TabControlMain.SelectTab(tab)
        End If
    End Sub



    Protected Overridable Function GetProcessingTab() As TabPage
        Return Me.TabControlMain.TabPages("ProcessTabPageControl")
    End Function

    <SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")>
    Protected Sub InitTabControl()

        If WelcomeTabPageControl Is Nothing Then
            Me.WelcomeTabPageControl = New TabPage()
            WelcomeTabPageControl.Text = String.Empty
            WelcomeTabPageControl.Tag = "WelcomeTab"
            Me.WelcomeTabPageControl.Name = "WelcomeTabPageControl"
            Me.WelcomeTabContent = New WizardWelcomeTabControl
            Me.WelcomeTabPageControl.Controls.Add(Me.WelcomeTabContent)
            Me.WelcomeTabContent.Description = "Description"
            Me.WelcomeTabContent.Dock = DockStyle.Fill
            Me.WelcomeTabContent.Name = "WelcomeTabContent"

        End If


        If OverviewTabPageControl Is Nothing Then
            Me.OverviewTabPageControl = New TabPage()
            OverviewTabPageControl.Text = String.Empty
            OverviewTabPageControl.Tag = "OverviewTab"
            Me.OverviewTabPageControl.Name = "OverviewTabPageControl"
            Me.OverviewTabContent = New WizardOverviewTabControl
            Me.OverviewTabPageControl.Controls.Add(Me.OverviewTabContent)
            Me.OverviewTabContent.Dock = DockStyle.Fill
            Me.OverviewTabContent.Name = "OverviewTabContent"
        End If

        If ProcessTabPageControl Is Nothing Then
            Me.ProcessTabPageControl = New TabPage()
            ProcessTabPageControl.Text = String.Empty
            ProcessTabPageControl.Tag = "ProcessTab"
            Me.ProcessTabPageControl.Name = "ProcessTabPageControl"
            Me.ProcessTabContent = New WizardProcessTabControl
            Me.ProcessTabPageControl.Controls.Add(Me.ProcessTabContent)
            Me.ProcessTabContent.Dock = DockStyle.Fill
            Me.ProcessTabContent.Name = "ProcessTabContent"
        End If

        If ResultTabPageControl Is Nothing Then
            ResultTabPageControl = New TabPage
            ResultTabPageControl.Text = String.Empty
            ResultTabPageControl.Tag = "ResultTab"
            Me.ResultTabPageControl.Name = "ResultTabPageControl"
            Me.ResultTabContent = New WizardResultTabControl
            Me.ResultTabPageControl.Controls.Add(Me.ResultTabContent)
            Me.ResultTabContent.Dock = DockStyle.Fill
            Me.ResultTabContent.Name = "ResultTabContent"
        End If

        If Me.TabControlMain.Controls.IndexOf(WelcomeTabPageControl) = -1 Then
            Dim pages As TabPage() = New TabPage() {WelcomeTabPageControl, OverviewTabPageControl, ProcessTabPageControl, ResultTabPageControl}
            TabControlMain.TabPages.AddRange(pages)
        End If
    End Sub


    Protected Overridable Sub OnBeforeProcess(e As WizardProcessEventArgs)
        RaiseEvent WizardBeforeProcess(Me, e)
    End Sub

    Protected Overridable Sub OnBeforeTabChange(e As WizardBeforeTabChangeEventArgs)
        If Not WizardBeforeTabChangeEvent Is Nothing Then
            RaiseEvent WizardBeforeTabChange(Me, e)
        End If
    End Sub

    Protected Overridable Sub OnCancelWizard()
        Dim cancel As Boolean

        If WizardCancelEvent Is Nothing Then
            OnCloseWizard()
        Else
            Dim e As New WizardCancelEventArgs(cancel)
            RaiseEvent WizardCancel(Me, e)
            cancel = e.Cancel
        End If

        If cancel = False Then
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Protected Overridable Sub OnCloseWizard()
        If WizardCloseEvent Is Nothing Then
            Me.CausesValidation = False
            Me.Close()
        Else
            Dim e As New WizardCancelEventArgs(False)
            RaiseEvent WizardClose(Me, e)
            If e.Cancel Then
                Me.Close()
            End If
        End If
    End Sub

    Protected Overridable Sub OnDoProcess(e As WizardProcessEventArgs)
        If Not Me.SupportsCancelation Then
            Me.CancelBtn.Enabled = False

            Application.DoEvents()
        End If

        RaiseEvent WizardDoProcess(Me, e)

        If Not Me.SupportsCancelation Then
            Me.CancelBtn.Enabled = True
        End If
    End Sub

    Protected Overridable Sub OnGotoNextTab()
        If TabControlMain.SelectedTab Is Nothing Then
            Return
        End If
        Dim myArg As New WizardBeforeTabChangeEventArgs(TabControlMain.SelectedTab, TabControlMain.TabPages(TabControlMain.SelectedIndex + 1))

        OnBeforeTabChange(myArg)

        If Not myArg.Cancel Then
            If TabControlMain.SelectedTab.Tag.ToString <> "InitialiseProgressTab" Then
                _wizardTabStack.Push(TabControlMain.SelectedTab)
            End If

            SelectTab(myArg.NextTab)
        End If
    End Sub

    Protected Overridable Sub OnGotoPreviousTab()
        Dim prevTab As TabPage = If(_wizardTabStack.Count > 0, _wizardTabStack.Pop, Nothing)

        If prevTab IsNot Nothing Then
            TabControlMain.SelectedTab = prevTab
        End If
    End Sub

    Protected Sub OnHelp()
        If Not WizardHelpEvent Is Nothing Then RaiseEvent WizardHelp(Me, Nothing)
    End Sub

    Protected Overridable Sub OnTabChanged(currentTab As TabPage, previousTab As TabPage)
        SetButtons()
        If Not WizardTabChangedEvent Is Nothing Then
            Dim e As New WizardTabChangedEventArgs(currentTab, previousTab)
            RaiseEvent WizardTabChanged(Me, e)
        End If
    End Sub

    Protected Overridable Sub OnWizardCompleted()
        RaiseEvent WizardCompleted(Me, Nothing)

        If Not TabControlMain.SelectedTab.Equals(ResultTabPageControl) Then
            SelectTab(ResultTabPageControl)
        End If
    End Sub

    Protected Sub PopButtonState()
        Dim state As ButtonState

        If Me._stateStack.Count > 0 Then
            state = Me._stateStack.Pop
            state.SetWizardState(Me)
        End If
    End Sub

    Protected Sub PushButtonState()
        Me._stateStack.Push(New ButtonState(Me))
    End Sub

    Protected Sub DisableAllButtons()
        Me.HelpBtn.Enabled = False
        Me.PreviousButton.Enabled = False
        Me.NextButton.Enabled = False
        Me.ProcessButton.Enabled = False
        Me.CancelBtn.Enabled = False
    End Sub

    Protected Sub EnableAllButtons()
        Me.HelpBtn.Enabled = True
        Me.PreviousButton.Enabled = True
        Me.NextButton.Enabled = True
        Me.ProcessButton.Enabled = True
        Me.CancelBtn.Enabled = True
    End Sub


    Public Event WizardBeforeProcess As EventHandler(Of WizardProcessEventArgs)
    Public Event WizardBeforeTabChange As EventHandler(Of WizardBeforeTabChangeEventArgs)
    Public Event WizardCancel As EventHandler(Of WizardCancelEventArgs)
    Public Event WizardClose As EventHandler(Of WizardCancelEventArgs)
    Public Event WizardCompleted As EventHandler
    Public Event WizardDoProcess As EventHandler(Of WizardProcessEventArgs)
    Public Event WizardHelp As EventHandler(Of EventArgs)
    Public Event WizardTabChanged As EventHandler(Of WizardTabChangedEventArgs)


    Private Class ButtonState

        Private _CancelBtn As Boolean
        Private _FinishBtn As Boolean
        Private _HelpBtn As Boolean
        Private _NextBtn As Boolean
        Private _PreviousBtn As Boolean


        Public Sub New(wizard As WizardBase)
            Me.HelpBtn = wizard.HelpBtn.Enabled
            Me.PreviousBtn = wizard.PreviousButton.Enabled
            Me.NextBtn = wizard.NextButton.Enabled
            Me.FinishBtn = wizard.ProcessButton.Enabled
            Me.CancelBtn = wizard.CancelBtn.Enabled
        End Sub



        Public Property CancelBtn As Boolean
            Get
                Return _CancelBtn
            End Get
            Set(value As Boolean)
                _CancelBtn = value
            End Set
        End Property

        Public Property FinishBtn As Boolean
            Get
                Return _FinishBtn
            End Get
            Set(value As Boolean)
                _FinishBtn = value
            End Set
        End Property

        Public Property HelpBtn As Boolean
            Get
                Return _HelpBtn
            End Get
            Set(value As Boolean)
                _HelpBtn = value
            End Set
        End Property

        Public Property NextBtn As Boolean
            Get
                Return _NextBtn
            End Get
            Set(value As Boolean)
                _NextBtn = value
            End Set
        End Property

        Public Property PreviousBtn As Boolean
            Get
                Return _PreviousBtn
            End Get
            Set(value As Boolean)
                _PreviousBtn = value
            End Set
        End Property



        Public Sub SetWizardState(wizard As WizardBase)
            wizard.HelpBtn.Enabled = Me.HelpBtn
            wizard.PreviousButton.Enabled = Me.PreviousBtn
            wizard.NextButton.Enabled = Me.NextBtn
            wizard.ProcessButton.Enabled = Me.FinishBtn
            wizard.CancelBtn.Enabled = Me.CancelBtn
        End Sub


    End Class

    Private Class StateStack
        Inherits Stack(Of ButtonState)
    End Class



End Class

Public Class WizardBeforeTabChangeEventArgs
    Inherits EventArgs


    Private _cancel As Boolean
    Private ReadOnly _currentTab As TabPage
    Private _nextTab As TabPage



    Public Sub New(currentTab As TabPage)
        _currentTab = currentTab
    End Sub

    Public Sub New(currentTab As TabPage, nextTab As TabPage)
        Me.New(currentTab)
        _nextTab = nextTab
    End Sub



    Public Property Cancel As Boolean
        Get
            Return _cancel
        End Get
        Set(value As Boolean)
            _cancel = value
        End Set
    End Property

    Public ReadOnly Property CurrentTab As TabPage
        Get
            Return _currentTab
        End Get
    End Property

    Public Property NextTab As TabPage
        Get
            Return _nextTab
        End Get
        Set(value As TabPage)
            _nextTab = value
        End Set
    End Property


End Class

Public Class WizardCancelEventArgs
    Inherits EventArgs

    Private _cancel As Boolean

    Public Sub New(cancel As Boolean)
        _cancel = cancel
    End Sub


    Public Property Cancel As Boolean
        Get
            Return _cancel
        End Get
        Set(value As Boolean)
            _cancel = value
        End Set
    End Property


End Class

Public Class WizardProcessEventArgs
    Inherits EventArgs


    Private _cancel As Boolean
    Private ReadOnly _currentTab As TabPage

    Private _previousTab As TabPage



    Public Sub New(previousTab As TabPage, currentTab As TabPage)
        _currentTab = currentTab
        _previousTab = previousTab
    End Sub



    Public Property Cancel As Boolean
        Get
            Return _cancel
        End Get
        Set(value As Boolean)
            _cancel = value
        End Set
    End Property

    Public ReadOnly Property CurrentTab As TabPage
        Get
            Return _currentTab
        End Get
    End Property

    Public Property PreviousTab As TabPage
        Get
            Return _previousTab
        End Get
        Set(value As TabPage)
            _previousTab = value
        End Set
    End Property


End Class

Public Class WizardTabChangedEventArgs
    Inherits EventArgs

    Private ReadOnly _currentTab As TabPage
    Private ReadOnly _previousTab As TabPage

    Public Sub New(currentTab As TabPage, previousTab As TabPage)
        _currentTab = currentTab
        _previousTab = previousTab
    End Sub

    Public ReadOnly Property CurrentTab As TabPage
        Get
            Return _currentTab
        End Get
    End Property

    Public ReadOnly Property PreviousTab As TabPage
        Get
            Return _previousTab
        End Get
    End Property
End Class