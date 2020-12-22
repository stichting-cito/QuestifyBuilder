Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic


Public Class ParameterEditorControlBase


    Public Event AddingResource As EventHandler(Of ResourceNameEventArgs)
    Public Event RemovingResource As EventHandler(Of ResourceNameEventArgs)

    Protected _itemSaving As Boolean = False
    Protected _formClosing As Boolean = False
    Private _parentTabEnabledContainerControl As Control
    Protected _parent As ParameterSetsEditor
    Private _resourceManager As ResourceManagerBase
    Private _hasLoadedOldItemLayoutTemplate As Boolean = False


    Protected Property EditorParent() As ParameterSetsEditor
        Get
            Return _parent
        End Get
        Set(ByVal value As ParameterSetsEditor)
            _parent = value
        End Set
    End Property

    Public Property HasLoadedOldItemLayoutTemplate As Boolean
        Get
            Return _hasLoadedOldItemLayoutTemplate
        End Get
        Set(value As Boolean)
            _hasLoadedOldItemLayoutTemplate = value
        End Set
    End Property

    Public Overridable Property ItemSaving As Boolean
        Get
            Return _itemSaving
        End Get
        Set(value As Boolean)
            _itemSaving = value
        End Set
    End Property

    Public Overridable Property FormClosing As Boolean
        Get
            Return _formClosing
        End Get
        Set(value As Boolean)
            _formClosing = value
        End Set
    End Property

    Public Property ResourceManager() As ResourceManagerBase
        Get
            Return _resourceManager
        End Get
        Set(value As ResourceManagerBase)
            If (value IsNot Nothing) Then
                _resourceManager = value
            End If
        End Set
    End Property


    Public Property ParentTabEnabledContainerControl As Control
        Get
            Return _parentTabEnabledContainerControl
        End Get
        Set
            _parentTabEnabledContainerControl = Value
        End Set
    End Property




    Public Sub New(parent As ParameterSetsEditor)
        Me.New()

        _parent = parent
    End Sub

    Public Sub New()

        InitializeComponent()

    End Sub

    Public Overridable Function ValidateParameter() As String
        Throw New NotImplementedException
    End Function

    Public Function MandatoryParameterMessage(label As String, group As String) As String
        If Not String.IsNullOrEmpty(label) AndAlso Not String.IsNullOrEmpty(group) Then
            Return String.Format(My.Resources.MandatoryEmptyParameterMessage, label, group)
        Else
            Return My.Resources.MandatoryParameterMessage
        End If
    End Function

    Public Overridable Function ResourceUsedInThisParameter(resource As EntityClasses.ResourceEntity) As Boolean
#If DEBUG Then
        Debug.Assert(True)
#Else
        Throw New NotImplementedException()
#End If

    End Function

    Public Overridable Sub PreItemSave(hasLoadedOldItemLayoutTemplate As Boolean)
    End Sub

    Public Overridable Sub PostItemSave()
    End Sub

    Public Overridable Sub RemoveAllResources()
        Throw New NotImplementedException
    End Sub

    Public Sub SetConditionalEnabled(value As Boolean)
        Dim groupBox As ParameterGroupBox = GetGroupBox(Me)
        If value Then
            If groupBox IsNot Nothing Then
                groupBox.Visible = True
            End If
            Me.Visible = True
        End If
        Me.Enabled = value
        Dim tableLayoutPanel As TableLayoutPanel = GetTableLayoutPanel(Me)
        If tableLayoutPanel IsNot Nothing Then
            Dim tableLayoutPanelCellPosition As TableLayoutPanelCellPosition = tableLayoutPanel.GetPositionFromControl(Me)
            Dim rowIndex As Integer = tableLayoutPanelCellPosition.Row
            Dim labelcontrol As Control = tableLayoutPanel.GetControlFromPosition(0, rowIndex)
            If labelcontrol.GetType Is GetType(Label) Then
                labelcontrol.Enabled = value
            ElseIf Not rowIndex = 0 Then
                labelcontrol = tableLayoutPanel.GetControlFromPosition(0, rowIndex - 1)
                labelcontrol.Enabled = value
            End If
        End If
        If groupBox IsNot Nothing AndAlso Not groupBox.Visible = value Then
            groupBox.Visible = ControlContainsEnabledParameterEditors(groupBox)
        End If
    End Sub



    Protected Overridable Sub OnAddingResource(ByVal e As ResourceNameEventArgs)
        If e IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.ResourceName) Then
            RaiseEvent AddingResource(Me, e)
        End If
    End Sub

    Protected Overridable Sub OnRemovingResource(ByVal e As ResourceNameEventArgs)
        If e IsNot Nothing AndAlso Not String.IsNullOrEmpty(e.ResourceName) Then
            RaiseEvent RemovingResource(Me, e)
        End If
    End Sub



    Private Sub ParameterBindingSource_CurrentItemChanged(sender As Object, e As EventArgs) Handles ParameterBindingSource.CurrentItemChanged
        If Not (EditorParent Is Nothing) Then
            EditorParent.ValidateParameterEditors()
        End If
    End Sub


    Private Function ControlContainsEnabledParameterEditors(control As Control) As Boolean
        Dim returnValue = False
        If control.Controls IsNot Nothing Then
            For Each childControl As Control In control.Controls
                If TypeOf (childControl) Is ParameterEditorControlBase AndAlso childControl.Enabled Then
                    returnValue = True
                    Exit For
                ElseIf childControl.Controls IsNot Nothing Then
                    returnValue = ControlContainsEnabledParameterEditors(childControl)
                End If
            Next
        End If
        Return returnValue
    End Function

    Private Function GetTableLayoutPanel(control As Control) As TableLayoutPanel
        Dim returnValue As TableLayoutPanel
        If control.Parent IsNot Nothing Then
            If control.Parent.GetType Is GetType(TableLayoutPanel) Then
                returnValue = DirectCast(control.Parent, TableLayoutPanel)
            Else
                returnValue = GetTableLayoutPanel(control.Parent)
            End If
        Else
            returnValue = Nothing
        End If
        Return returnValue
    End Function

    Private Function GetGroupBox(control As Control) As ParameterGroupBox
        Dim returnValue As ParameterGroupBox
        If control.Parent IsNot Nothing Then
            If control.Parent.GetType Is GetType(ParameterGroupBox) Then
                returnValue = DirectCast(control.Parent, ParameterGroupBox)
            Else
                returnValue = GetGroupBox(control.Parent)
            End If
        Else
            returnValue = Nothing
        End If
        Return returnValue
    End Function




    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If (disposing) Then
                _resourceManager = Nothing
                ParameterBindingSource.Dispose()
            End If

            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            If disposing Then
                _parent = Nothing
                _parentTabEnabledContainerControl = Nothing
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

End Class
