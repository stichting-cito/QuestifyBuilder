
Imports System.ComponentModel

Public Class HandleImportConflictDialog


    Private _choosenConflictResolution As ImportFormWizard.ResourceConflictResolution



    Friend ReadOnly Property ChoosenConflictResolution() As ImportFormWizard.ResourceConflictResolution
        Get
            Return _ChoosenConflictResolution
        End Get
    End Property



    Private Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        _ChoosenConflictResolution = DirectCast(DirectCast(sender, Button).Tag, ImportFormWizard.ResourceConflictResolution)
        Close()
    End Sub

    Private Sub AddBottomPanelButton(ByVal componentResources As ComponentResourceManager, ByVal associatedConflictResolution As ImportFormWizard.ResourceConflictResolution)
        Dim buttonToAdd As New Button

        buttonToAdd.Tag = associatedConflictResolution
        buttonToAdd.Width = 100

        Select Case associatedConflictResolution
            Case ImportFormWizard.ResourceConflictResolution.LeaveCurrentInstance
                buttonToAdd.Text = componentResources.GetString("LeaveResourceButtonText")
                buttonToAdd.Name = "LeaveResource"
            Case ImportFormWizard.ResourceConflictResolution.LeaveThisAndFollowingInstances
                buttonToAdd.Text = componentResources.GetString("LeaveAllResourcesButtonText")
                buttonToAdd.Name = "LeaveAllResources"
            Case ImportFormWizard.ResourceConflictResolution.ReplaceCurrentInstance
                buttonToAdd.Text = componentResources.GetString("ReplaceResourceButtonText")
                buttonToAdd.Name = "ReplaceResource"
            Case ImportFormWizard.ResourceConflictResolution.ReplaceThisAndFollowingInstances
                buttonToAdd.Text = componentResources.GetString("ReplaceAllResourcesButtonText")
                buttonToAdd.Name = "Replace all"
            Case Else
                Throw New ArgumentException(
                    $"No support for resource conflict resolution {associatedConflictResolution.ToString()}.")
        End Select


        AddHandler buttonToAdd.Click, AddressOf Button_Click

        MyBase.BottomPanel.Controls.Add(buttonToAdd)

        buttonToAdd.Left = BottomPanel.Width - (BottomPanel.Controls.Count * buttonToAdd.Width) - ((BottomPanel.Controls.Count - 1) * 3)
    End Sub


    Public Sub New()
        InitializeComponent()

        _ChoosenConflictResolution = ImportFormWizard.ResourceConflictResolution.LeaveCurrentInstance
    End Sub

    Public Sub New(
              ByVal conflictLabelText As String,
              ByVal resourceConflictResolutions As ImportFormWizard.ResourceConflictResolution,
              newWindowTitle As String,
              Optional ByVal newReplaceResourceQuestionLabel As String = Nothing)
        Me.New()

        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(HandleImportConflictDialog))

        MyBase.BottomPanel.Controls.Clear()

        If (ResourceConflictResolutions And ImportFormWizard.ResourceConflictResolution.LeaveCurrentInstance) = ImportFormWizard.ResourceConflictResolution.LeaveCurrentInstance Then AddBottomPanelButton(resources, ImportFormWizard.ResourceConflictResolution.LeaveCurrentInstance)
        If (ResourceConflictResolutions And ImportFormWizard.ResourceConflictResolution.LeaveThisAndFollowingInstances) = ImportFormWizard.ResourceConflictResolution.LeaveThisAndFollowingInstances Then AddBottomPanelButton(resources, ImportFormWizard.ResourceConflictResolution.LeaveThisAndFollowingInstances)
        If (ResourceConflictResolutions And ImportFormWizard.ResourceConflictResolution.ReplaceCurrentInstance) = ImportFormWizard.ResourceConflictResolution.ReplaceCurrentInstance Then AddBottomPanelButton(resources, ImportFormWizard.ResourceConflictResolution.ReplaceCurrentInstance)
        If (ResourceConflictResolutions And ImportFormWizard.ResourceConflictResolution.ReplaceThisAndFollowingInstances) = ImportFormWizard.ResourceConflictResolution.ReplaceThisAndFollowingInstances Then AddBottomPanelButton(resources, ImportFormWizard.ResourceConflictResolution.ReplaceThisAndFollowingInstances)

        ResourceNameLabel.Text = conflictLabelText

        If newReplaceResourceQuestionLabel IsNot Nothing Then
            ReplaceResourceQuestionLabel.Text = newReplaceResourceQuestionLabel
        End If

        If newWindowTitle IsNot Nothing Then
            Text = newWindowTitle
        End If
    End Sub


End Class
