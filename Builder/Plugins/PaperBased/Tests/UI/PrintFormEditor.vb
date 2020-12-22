Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Factories
Imports System.Windows.Forms

<DefaultBindingProperty("DataSource")>
Public Class PrintFormEditor

    Private _datasource As PrintFormCollection
    Private _privatePrintFormCollection As New PrintFormCollection
    Private _testEntity As AssessmentTestResourceEntity
    Private _rows As Integer = 0


    <Bindable(BindableSupport.Yes)>
    Public Property DataSource() As PrintFormCollection
        Get
            Return _datasource
        End Get
        Set(ByVal value As PrintFormCollection)
            _datasource = value
        End Set
    End Property

    Public Property TestEntity() As AssessmentTestResourceEntity
        Get
            Return _testEntity
        End Get
        Set(ByVal value As AssessmentTestResourceEntity)
            _testEntity = value
            InitializeControl()
        End Set

    End Property


    Private Sub InitializeControl()
        If _testEntity IsNot Nothing AndAlso _datasource IsNot Nothing Then
            _datasource.RaiseListChangedEvents = True

            ResetLayoutPanel()

            MainTableLayoutPanel.SuspendLayout()

            _privatePrintFormCollection.Clear()

            For Each printFormType As PrintFormType In [Enum].GetValues(GetType(PrintFormType))
                If printFormType <> PrintFormType.UserDefinedBooklet Then
                    _privatePrintFormCollection.Add(New PrintForm() With {.Type = printFormType})
                End If
            Next

            For Each pf As PrintForm In Me.DataSource
                If pf.Type <> PrintFormType.UserDefinedBooklet Then
                    Dim currentPrintForm = _privatePrintFormCollection.GetPrintFormByType(pf.Type)
                    If currentPrintForm IsNot Nothing Then
                        Dim indexOfCurrentPrintForm = _privatePrintFormCollection.IndexOf(currentPrintForm)
                        _privatePrintFormCollection.Item(indexOfCurrentPrintForm) = pf
                    End If
                Else
                    _privatePrintFormCollection.Add(pf)
                End If
            Next

            For Each printForm As PrintForm In _privatePrintFormCollection
                AddPrintFormRowToMainTableLayout(printForm)
            Next

            Me.FindForm()?.Refresh()

            MainTableLayoutPanel.ResumeLayout()
        End If
    End Sub

    Private Sub AddPrintFormRowToMainTableLayout(printForm As PrintForm)
        Dim printviewUIControl As New ChoosePrintFormControl()

        printviewUIControl.InitializeEditorAndSetDataSource(Me.TestEntity, printForm)
        AddHandler printviewUIControl.ResourceNeeded, AddressOf SelectMediaResourceDialog_ResourceNeeded
        AddHandler printviewUIControl.PrintFormAdded, AddressOf ChoosePrintFormControl_PrintFormAdded
        AddHandler printviewUIControl.PrintFormRemoved, AddressOf ChoosePrintFormControl_PrintFormRemoved

        printviewUIControl.Name = "PrintFormUIControl"

        printviewUIControl.Anchor = AnchorStyles.Left And AnchorStyles.Right

        printviewUIControl.Margin = New Padding(0, 0, 0, 2)

        printviewUIControl.BringToFront()

        MainTableLayoutPanel.Controls.Add(printviewUIControl, 0, MainTableLayoutPanel.RowCount - 2)

        If printForm.Type = PrintFormType.UserDefinedBooklet Then
            Dim removeBookletButton = New Button() With {.Name = "RemoveButton", .Image = My.Resources.remove16, .Height = 30, .Width = 30, .Tag = printForm.Id}

            AddHandler removeBookletButton.Click, AddressOf RemoveBookletButton_Click
            MainTableLayoutPanel.Controls.Add(removeBookletButton, 1, MainTableLayoutPanel.RowCount - 2)
        End If

        MainTableLayoutPanel.RowCount += 1

        MainTableLayoutPanel.SetRow(AddButton, MainTableLayoutPanel.RowCount)
    End Sub

    Private Sub ChoosePrintFormControl_PrintFormAdded(ByVal sender As Object, ByVal e As PrintFormEventArgs)
        OnMSWordTemplateAdded(e)
    End Sub

    Private Sub ChoosePrintFormControl_PrintFormRemoved(ByVal sender As Object, ByVal e As PrintFormEventArgs)
        OnMSWordTemplateRemoved(e)
    End Sub

    Private Sub SelectMediaResourceDialog_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        OnResourceNeeded(e)
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        Dim newUserDefinedBooklet As New PrintForm() With {.Type = PrintFormType.UserDefinedBooklet}

        _privatePrintFormCollection.Add(newUserDefinedBooklet)
        Me.DataSource.Add(newUserDefinedBooklet)

        MainTableLayoutPanel.SuspendLayout()
        AddPrintFormRowToMainTableLayout(newUserDefinedBooklet)
        MainTableLayoutPanel.ResumeLayout()

    End Sub

    Private Sub RemoveBookletButton_Click(sender As Object, e As EventArgs)
        Dim removeButtonControl As Control = DirectCast(sender, Control)

        Dim rowToDelete = MainTableLayoutPanel.GetRow(removeButtonControl)

        MainTableLayoutPanel.SuspendLayout()

        MainTableLayoutPanel.Controls.Remove(MainTableLayoutPanel.GetControlFromPosition(0, rowToDelete))
        MainTableLayoutPanel.Controls.Remove(MainTableLayoutPanel.GetControlFromPosition(1, rowToDelete))

        For rowToMove = rowToDelete + 1 To MainTableLayoutPanel.RowCount - 1
            For columnToMove As Integer = 0 To MainTableLayoutPanel.ColumnCount - 1
                Dim controlToMove = MainTableLayoutPanel.GetControlFromPosition(columnToMove, rowToMove)
                If controlToMove IsNot Nothing Then
                    MainTableLayoutPanel.SetRow(controlToMove, rowToMove - 1)
                End If
            Next
        Next

        MainTableLayoutPanel.RowCount -= 1

        MainTableLayoutPanel.ResumeLayout()

        Dim printFormTypeId = DirectCast(removeButtonControl.Tag, Guid)
        Me.DataSource.Remove(printFormTypeId)
        _privatePrintFormCollection.Remove(printFormTypeId)
    End Sub


    Public Sub ResetLayoutPanel()

        Dim controlsToRemove As New List(Of Control)

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is ChoosePrintFormControl Then
                With CType(ctrl, ChoosePrintFormControl)
                    RemoveHandler .ResourceNeeded, AddressOf SelectMediaResourceDialog_ResourceNeeded
                    RemoveHandler .PrintFormAdded, AddressOf ChoosePrintFormControl_PrintFormAdded
                    RemoveHandler .PrintFormRemoved, AddressOf ChoosePrintFormControl_PrintFormRemoved
                End With
                controlsToRemove.Add(ctrl)
            End If
        Next

        controlsToRemove.ForEach(Sub(x) Me.Controls.Remove(x))
        controlsToRemove.Clear()
    End Sub




    Public Sub New()

        InitializeComponent()

    End Sub


    Protected Sub OnMSWordTemplateAdded(ByVal e As PrintFormEventArgs)
        Dim resourceName As String = e.PrintForm.ResourceName

        If Not String.IsNullOrEmpty(resourceName) Then
            If e.PrintForm.Type <> PrintFormType.UserDefinedBooklet Then
                Me.DataSource.Add(e.PrintForm)
            End If

            Dim referencedResource = DtoFactory.Generic.Get(Me.TestEntity.BankId, resourceName)
            If referencedResource IsNot Nothing Then
                If Not Me.TestEntity.ContainsDependentResource(referencedResource.resourceId) Then
                    Dim eAdded As New ResourceEventArgs(referencedResource)
                    RaiseEvent DependentResourceAdded(Me, eAdded)
                End If
            End If
        End If
    End Sub

    Protected Sub OnMSWordTemplateRemoved(ByVal e As PrintFormEventArgs)
        Dim resourceName As String = e.PrintForm.ResourceName

        If Not String.IsNullOrEmpty(resourceName) Then
            If e.PrintForm.Type <> PrintFormType.UserDefinedBooklet Then
                Me.DataSource.Remove(e.PrintForm.Id)
            End If

            If Not IsResourceUsed(resourceName, e.PrintForm.Type) Then
                Dim eRemoved As New ResourceNameEventArgs(resourceName)
                RaiseEvent DependentResourceRemoved(Nothing, eRemoved)
            End If
        End If
    End Sub


    Private Function IsResourceUsed(ByVal resource As String, ByVal type As PrintFormType) As Boolean
        For Each printFormType As PrintForm In Me.DataSource
            If (Not printFormType.Type = type) AndAlso printFormType.ResourceName = resource Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub OnResourceNeeded(ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(Me, e)
    End Sub


    Public Event DependentResourceAdded As EventHandler(Of ResourceEventArgs)
    Public Event DependentResourceRemoved As EventHandler(Of ResourceNameEventArgs)
    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
End Class
