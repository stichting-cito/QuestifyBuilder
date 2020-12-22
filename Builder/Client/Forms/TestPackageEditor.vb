Imports System.Collections.ObjectModel
Imports System.Diagnostics.CodeAnalysis
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports Cinch
Imports Questify.Builder.Configuration
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Questify.Builder.Logic.TestPackageConstruction
Imports Questify.Builder.Logic.TestPackageConstruction.ChainHandlers.Processing
Imports Questify.Builder.Security
Imports Questify.Builder.UI.PublicationService
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces


Public Class TestPackageEditor

    Private _testPackage As TestPackage
    Private _originalTestPackageHash As Byte()
    Private _pasteDestinationParentForRecurseComponents As Object
    Private _resourceManager As DataBaseResourceManager
    Private _testPackageComponentsMarkedForRemoval As List(Of TestPackageComponent)
    Private _testPackageResourceEntity As TestPackageResourceEntity
    Private ReadOnly _validationControls As New List(Of TestPackageEditorContainerBase)
    Private _isAllowedToEditTestPackages As Boolean
    Private _viewSelectionDialogAtStartupCancelled As Boolean = False
    Private WithEvents ThisEditorTestSelectorDialog As SelectTestResourceDialog
    Private ReadOnly _cachedSupportedViewsOfTest As New Dictionary(Of String, List(Of String))
    Private WithEvents _constructionFacade As TestPackageConstructionFacade = New TestPackageConstructionFacade
    Private _testsetResourceReferencesBeingDropped As List(Of String)
    Private ReadOnly _windowfacade As IWindowFacade = New WindowFacade()
    Private _isAddingResource As Boolean = False

    Private Enum ShowWindowCommands As Integer
        Restore = 9
    End Enum

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As ShowWindowCommands) As Boolean
    End Function

    Private Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(ByVal testPackageResource As TestPackageResourceEntity)
        MyClass.New()

        If testPackageResource Is Nothing Then
            Throw New ArgumentNullException("testPackageResource")
        End If

        _testPackageResourceEntity = testPackageResource
        _resourceManager = New DataBaseResourceManager(_testPackageResourceEntity.BankId)

        CheckTestPackageDesignPermissions()
    End Sub


    Protected ReadOnly Property IsDirty() As Boolean
        Get
            Return (ResourceDataIsDirty OrElse MetaDataControl.IsDirty() OrElse _testPackageResourceEntity.HasChangesInTopology OrElse ResourceCustomProperties1.RemovedEntities.Count > 0)
        End Get
    End Property

    Protected ReadOnly Property ResourceDataIsDirty() As Boolean
        Get
            Dim currentEntityHash As Byte() = If(_testPackage IsNot Nothing, _testPackage.GetMD5Hash(), Nothing)
            Return Not ArrayHelper.CompareByteArray(_originalTestPackageHash, currentEntityHash)
        End Get
    End Property


    Public ReadOnly Property TestPackageResourceEntity() As TestPackageResourceEntity
        Get
            Return _testPackageResourceEntity
        End Get
    End Property



    Private Property TestComponentsMarkedForRemoval As List(Of TestPackageComponent)

    Private Sub CheckTestPackageDesignPermissions()
        _isAllowedToEditTestPackages = PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.TestPackageEntity, _testPackageResourceEntity.BankId)
    End Sub

    Private Sub HierarchyControl_DragDropTest(ByVal sender As Object, ByVal e As SelectedTestCollectionEventArgs) Handles HierarchyControl.DragDropTest
        Dim selectedItemColl = e.SelectedItemCollection
        If Not (selectedItemColl Is Nothing) Then
            Dim selectedCollection As New List(Of AssessmentTestResourceDto)

            For i As Integer = 0 To selectedItemColl.Count - 1
                Dim entity As AssessmentTestResourceDto = DirectCast(selectedItemColl.Item(i).GetRow.DataRow, AssessmentTestResourceDto)
                If Not (entity Is Nothing) Then
                    selectedCollection.Add(entity)
                End If
            Next
            AddTestsToTestPackage(selectedCollection.Select(Function(s) s.Name), e.AddToTestSet, _constructionFacade)
            ThisEditorTestSelectorDialog.RefreshDatasource()
        End If
    End Sub

    Private Sub ThisEditorItemSelectorDialog_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles ThisEditorTestSelectorDialog.FormClosing
        Me.ThisEditorTestSelectorDialog.Dispose()
        Me.ThisEditorTestSelectorDialog = Nothing
    End Sub

    Private Sub ThisEditorItemSelectorDialog_AddingResource(ByVal sender As Object, ByVal e As SelectedTestCollectionEventArgs) Handles ThisEditorTestSelectorDialog.AddingResource
        Dim testSetContext As TestSet = HierarchyControl.TestSetContext

        If testSetContext IsNot Nothing Then
            _isAddingResource = True
            If AddTestsToTestPackage(Me.ThisEditorTestSelectorDialog.SelectedEntities.Select(Function(s) s.Name), testSetContext, _constructionFacade) Then
                If ThisEditorTestSelectorDialog IsNot Nothing Then
                    ThisEditorTestSelectorDialog.RefreshDatasource()
                End If
            Else
                e.Cancelled = True
            End If
            _isAddingResource = False
        Else
            Trace.Fail("Unable to add test resource to testpackage", "There was no sectionContext for adding test. No tests were added to testPackage.")
        End If
    End Sub

    Private Function AddTestsToTestPackage(ByVal testsToAdd As IEnumerable(Of String), ByVal addToTestSet As TestSet, ByVal facade As TestPackageConstructionFacade) As Boolean
        Dim returnValue As Boolean
        Dim insertAtPosition = HierarchyControl.PositionToInsertItem
        Dim test As IList(Of ResourceRef) = TestPackageProcessingHelpers.GetTestResourceRefList(testsToAdd)
        returnValue = TestPackageConstruction.AddTestToTestPackage(_testPackage, _testPackageResourceEntity, _resourceManager, test, addToTestSet, insertAtPosition, facade)
        If returnValue Then
            With HierarchyControl
                .RefetchDataSource(True)
                .SetSelection(testsToAdd.Count)
            End With
        End If

        For Each view In _testPackage.IncludedViews
            Dim plugin = IoCHelper.GetInstances(Of ITestPackageEditorPlugin).Where(Function(p) p.IsSupportedView(view)).FirstOrDefault()

            If plugin IsNot Nothing Then
                plugin.AddTests(addToTestSet, TestPackagePropertiesControl.CurrentPropertyEditorControls)
            End If
        Next

        Return returnValue
    End Function

    Private Sub AddTestToolStripButton_Click(sender As Object, e As EventArgs) Handles AddTestToolStripButton.Click, AddNewTestToolStripMenuItem.Click
        ShowTestSelector()
    End Sub

    Private Sub AddTestsetMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddNewTestsetToolStripMenuItem.Click, AddTestsetToolStripButton.Click, HierarchyControl.AddTestsetMenuClick
        Dim createdTestSet As CreatedTestPackageNodeAndViews(Of TestSet, TestSetViewBase) = TestPackageFactory.CreateTestSetAndViews(_testPackage.IncludedViews)
        _testPackage.TestSets.Add(createdTestSet.TestNode)

        createdTestSet.TestNode.Parent = _testPackage
        createdTestSet.TestNode.Identifier = Guid.NewGuid().ToString()
        createdTestSet.TestNode.Title = My.Resources.NewTestset

        HierarchyControl.RefetchDataSource(False)
    End Sub

    Private Sub BindControlsAtLoad(ByVal resetModelHash As Boolean)
        If resetModelHash Then
            _originalTestPackageHash = _testPackage.GetMD5Hash()
        End If

        TestPackagePropertiesControl.TestPackageEntity = _testPackageResourceEntity
        TestSetControl.TestPackageEntity = _testPackageResourceEntity
        TestReferenceControl.TestPackageEntity = _testPackageResourceEntity

        HierarchyControl.TestPackage = _testPackage
        HierarchyControl.TestPackageIsNew = _testPackageResourceEntity.IsNew

        MetaDataControl.ResourceEntity = _testPackageResourceEntity
        ResourceCustomProperties1.ResourceEntity = _testPackageResourceEntity
    End Sub

    Private Sub HandleCommandExecuteRequestFromPropertyEditor(ByVal command As TestEditorCommands)
        Select Case command
            Case TestEditorCommands.ChangeTestCode
                ChangeTestPackageCode()
            Case TestEditorCommands.LockTestOrderInTestset
                LockTestOrderInTestset(True)
            Case TestEditorCommands.UnlockTestOrderInTestset
                LockTestOrderInTestset(False)
            Case Else
                Throw New NotImplementedException($"Command '{command.ToString()}' not implemented in function 'HandleCommandExecuteRequestFromPropertyEditor'")
        End Select
    End Sub

    Private Sub ChangeTestPackageCode()
        If PermissionFactory.Instance.TryUserIsPermittedToNamedTask(TestBuilderPermissionAccess.Execute, TestBuilderPermissionTarget.NamedTask, TestBuilderPermissionNamedTask.ChangeTestCode, _testPackageResourceEntity.BankId, 0) Then
            If Not Me.IsDirty Then
                Dim dlg As New ChangeTestCodeDialog(_testPackage.Identifier)
                AddHandler dlg.ValidateNewCodeName, AddressOf ChangeTestPackageCodeDialog_ValidateNewCodeName
                Dim result As DialogResult = dlg.ShowDialog()

                If result = DialogResult.OK Then
                    _testPackage.Identifier = dlg.NewCodeName

                    TrySave(dlg)
                End If
            Else
                MessageBox.Show(String.Format(My.Resources.ItemEditor_OnlyPossibleToChangeTheResourceCodeWhenNoOtherChanges, My.Resources.TestPackageNoCaps), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show(My.Resources.ItemEditor_NoSufficientRightsToPerformThisAction, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub TrySave(dlg As ChangeTestCodeDialog)
        Dim oldCode As String = _testPackage.Identifier

        Try
            If Me.SaveTestPackage() Then

                For Each view In _testPackage.IncludedViews
                    Dim plugin = IoCHelper.GetInstances(Of ITestPackageEditorPlugin).Where(Function(p) p.IsSupportedView(view)).FirstOrDefault()

                    If plugin IsNot Nothing Then
                        plugin.CodeChanged(_testPackageResourceEntity, dlg.NewCodeName, oldCode)
                    End If
                Next

                _testPackageResourceEntity = ResourceFactory.Instance.GetTestPackage(_testPackageResourceEntity)
                _testPackage = _testPackageResourceEntity.GetTestPackage

                If PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.ItemEntity, _testPackageResourceEntity.BankId) Then
                    SaveAsToolStripButton.Enabled = True
                End If

                BindControlsAtLoad(True)

                Me.DataBindings("text").ReadValue()

                MessageBox.Show(My.Resources.TestPackageEditor_TestCodeRenamingIsSucceeded, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                _testPackage.Identifier = oldCode
            End If
        Catch ex As Exception
            Dim sb As New StringBuilder()
            sb.AppendLine(ex.Message)

            Dim otherException As Exception = ex.InnerException
            Do Until otherException Is Nothing
                sb.AppendFormat("------------------------------------------------------------------------------------{0}{0}{1}", Environment.NewLine, otherException.Message)
                otherException = otherException.InnerException
            Loop
            MessageBox.Show(sb.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LockTestOrderInTestset(doLock As Boolean)
        If HierarchyControl.SelectedComponents.Count = 0 Then
            Return
        End If

        Dim firstSelectedComponent As TestPackageNode = HierarchyControl.SelectedComponents(0)
        Dim testSet As TestSet = New TestSet

        If TypeOf firstSelectedComponent Is TestSet Then
            testSet = DirectCast(firstSelectedComponent, TestSet)
        ElseIf firstSelectedComponent.GetType() = GetType(TestReference) Then
            testSet = _testPackage.TestSets.FirstOrDefault(Function(x) x.Components.Contains(DirectCast(firstSelectedComponent, TestReference)))
        End If

        If testSet Is Nothing Then
            Return
        End If

        For Each component As TestReference In testSet.Components
            component.LockedOrder = testSet.LockedOrder
        Next

        DetermineTestPackageComponentsButtonsEnableState(True)
        HierarchyControl.LockOrderOfTestsInTestSet(testSet.LockedOrder)

        For Each view In _testPackage.IncludedViews
            Dim plugin = IoCHelper.GetInstances(Of ITestPackageEditorPlugin).Where(Function(p) p.IsSupportedView(view)).FirstOrDefault()

            If plugin IsNot Nothing Then
                plugin.LockTestOrderInTestSet(testSet, TestPackagePropertiesControl.CurrentPropertyEditorControls, doLock)
            End If
        Next
    End Sub

    Private Sub DeleteTestPackageComponentToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteTestPackageComponentToolStripButton.Click, DeleteTestPackageComponentToolStripMenuItem.Click, HierarchyControl.DeleteTestPackageComponentMenuClick
        Dim selectedComponents As List(Of TestPackageNode) = HierarchyControl.SelectedComponents

        If MessageBox.Show(My.Resources.TestPackageEditor_DeleteComponentsQuestionMessage, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            Dim removedDependencies As List(Of String) = TestPackageConstruction.DeleteTestPackageComponents(selectedComponents, _testPackage, _testPackageResourceEntity, _constructionFacade)

            _testsetResourceReferencesBeingDropped.AddRange(removedDependencies)
            If ThisEditorTestSelectorDialog IsNot Nothing Then
                ThisEditorTestSelectorDialog.RefreshDatasource()
            End If

            For Each view In _testPackage.IncludedViews
                Dim plugin = IoCHelper.GetInstances(Of ITestPackageEditorPlugin).Where(Function(p) p.IsSupportedView(view)).FirstOrDefault()

                If plugin IsNot Nothing Then
                    plugin.DeleteTestPackageComponent(_testPackage, selectedComponents, _testPackageResourceEntity)
                End If
            Next

        End If

        HierarchyControl.RefetchDataSource(False)
    End Sub


    Private Sub ShowTestSelector()
        If (Me.ThisEditorTestSelectorDialog Is Nothing) Then
            Me.ThisEditorTestSelectorDialog = New SelectTestResourceDialog(_testPackageResourceEntity.BankId, _testPackageResourceEntity, _testPackage, AddressOf ContainsTestSupportedViews, True)
            If (Screen.GetWorkingArea(Me).Size.Width - (Me.Location.X + Me.Width) >= Me.ThisEditorTestSelectorDialog.Width) Then
                Me.ThisEditorTestSelectorDialog.StartPosition = FormStartPosition.Manual
                Me.ThisEditorTestSelectorDialog.Location = New Point(Me.Bounds.Right + 5, Me.Location.Y)
            Else
                Me.ThisEditorTestSelectorDialog.StartPosition = FormStartPosition.CenterParent
            End If

            Me.ThisEditorTestSelectorDialog.Show(Me)
        Else
            Me.ThisEditorTestSelectorDialog.Activate()
        End If
    End Sub

    Private Sub DetermineTestPackageComponentsButtonsEnableState(ByVal singleItemSelected As Boolean)
        Me.DisableAllTestComponentButtons()

        Dim selectedComponents As List(Of TestPackageNode) = HierarchyControl.SelectedComponents

        If HierarchyControl.SelectedComponents.Count > 0 Then
            Dim firstSelectedComponent As TestPackageNode = HierarchyControl.SelectedComponents(0)

            If TypeOf firstSelectedComponent Is TestPackage Then
                AddTestsetToolStripButton.Enabled = _isAllowedToEditTestPackages
                AddNewTestsetToolStripMenuItem.Enabled = _isAllowedToEditTestPackages
            ElseIf TypeOf firstSelectedComponent Is TestSet Then
                AddTestToolStripButton.Enabled = True
                AddNewTestToolStripMenuItem.Enabled = True

                DeleteTestPackageComponentToolStripButton.Enabled = _isAllowedToEditTestPackages
                DeleteTestPackageComponentToolStripMenuItem.Enabled = _isAllowedToEditTestPackages

                Dim bEnabled = singleItemSelected AndAlso _isAllowedToEditTestPackages

                MoveTestSetDownToolStripButton.Visible = True
                MoveTestSetDownToolStripButton.Enabled = bEnabled
                MoveTestsetDownInTestPackageToolStripMenuItem.Visible = True
                MoveTestsetDownInTestPackageToolStripMenuItem.Enabled = bEnabled
                MoveTestsetDownInTestpackageToolStripMenuItem1.Visible = True
                MoveTestsetDownInTestpackageToolStripMenuItem1.Enabled = bEnabled

                MoveTestSetUpToolStripButton.Visible = True
                MoveTestSetUpToolStripButton.Enabled = bEnabled
                MoveTestSetUpInTestPackageToolStripMenuItem.Visible = True
                MoveTestSetUpInTestPackageToolStripMenuItem.Enabled = bEnabled
                MoveTestsetUpInTestpackageToolStripMenuItem1.Visible = True
                MoveTestsetUpInTestpackageToolStripMenuItem1.Enabled = bEnabled

                MoveButtonsMenuToolStripSeparator.Visible = True
                MoveButtonsToolStripSeparator.Visible = True

            ElseIf TypeOf firstSelectedComponent Is TestReference Then
                Dim testReference = DirectCast(firstSelectedComponent, TestReference)
                AddTestToolStripButton.Enabled = True
                AddNewTestToolStripMenuItem.Enabled = True

                DeleteTestPackageComponentToolStripButton.Enabled = _isAllowedToEditTestPackages
                DeleteTestPackageComponentToolStripMenuItem.Enabled = _isAllowedToEditTestPackages

                MoveTestDownInTestsetToolStripMenuItem.Visible = Not testReference.LockedOrder
                MoveTestDownInTestsetToolStripMenuItem.Enabled = Not testReference.LockedOrder
                MoveTestDownInTestsetToolStripMenuItem1.Visible = Not testReference.LockedOrder
                MoveTestDownInTestsetToolStripMenuItem1.Enabled = Not testReference.LockedOrder

                MoveTestDownToolStripButton.Visible = Not testReference.LockedOrder
                MoveTestDownToolStripButton.Enabled = Not testReference.LockedOrder

                MoveTestUpInTestsetToolStripMenuItem.Visible = Not testReference.LockedOrder
                MoveTestUpInTestsetToolStripMenuItem.Enabled = Not testReference.LockedOrder
                MoveTestUpInTestsetToolStripMenuItem1.Visible = Not testReference.LockedOrder
                MoveTestUpInTestsetToolStripMenuItem1.Enabled = Not testReference.LockedOrder

                MoveTestUpToolStripButton.Visible = Not testReference.LockedOrder
                MoveTestUpToolStripButton.Enabled = Not testReference.LockedOrder

                MoveButtonsMenuToolStripSeparator.Visible = True
                MoveButtonsToolStripSeparator.Visible = True
            Else
                Throw New NotSupportedException(
                    $"Unexpected selected test entity type: '{firstSelectedComponent.GetType().FullName}'")
            End If
        End If

        HierarchyControl.DetermineTestComponentsContextMenuEnableState(_isAllowedToEditTestPackages, selectedComponents)
    End Sub

    Private Sub EnsuresFormUpdateAfterEditing()
        For Each c As Control In Me.Controls
            c.Focus()
        Next
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ExportToExcelToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExportToExcelToolStripButton.Click
        HierarchyControl.ExportToExcel()
    End Sub

    <SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Private Function GetFullTestPackageEntity(ByVal entity As TestPackageResourceEntity) As TestPackageResourceEntity
        Return ResourceFactory.Instance.GetTestPackage(entity)
    End Function

    Private Function GetTestEntity(ByVal ref As TestReference) As ResourceEntity
        Dim testSourceName As String = ref.SourceName
        Dim entity As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(_testPackageResourceEntity.BankId, testSourceName, New ResourceRequestDTO())

        If entity Is Nothing Then
            Throw New AppLogicException($"Could not get test with name '{ref.SourceName}' from database")
        End If

        Return entity
    End Function

    Private Sub HierarchyControl_AddTestPackage(ByVal sender As Object, ByVal e As TestPackageHierarchyControl.AddTestToTestPackageEventArgs) Handles HierarchyControl.AddTestToTestPackage
        ShowTestSelector()
    End Sub

    Private Sub HierarchyControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles HierarchyControl.Enter
        Me.DetermineTestPackageComponentsButtonsEnableState(Me.HierarchyControl.SelectedComponents.Count = 1)
    End Sub

    Private Sub HierarchyControl_TestPackageComponentSelected(ByVal sender As Object, ByVal e As TestPackageComponentSelectedEventArgs) Handles HierarchyControl.TestPackageComponentSelected
        If e.SelectedComponents.Count = 1 Then
            Me.SuspendLayout()

            For Each tabPage As TabPage In TabControl1.TabPages
                TabControl1.TabPages.Remove(tabPage)
            Next

            _validationControls.Clear()
            Dim firstSelectedComponent As TestPackageNode = e.SelectedComponents(0)

            If TypeOf firstSelectedComponent Is TestPackage Then
                Dim testPackage As TestPackage = DirectCast(firstSelectedComponent, TestPackage)

                Me.TestPackagePropertiesControl.InitializePropertyEditorsAndSetDataSource(testPackage, _testPackage.IncludedViews)
                _validationControls.Add(Me.TestPackagePropertiesControl)

                TabControl1.TabPages.Add(TabPage3)
            ElseIf TypeOf firstSelectedComponent Is TestReference Then
                Dim testRef As TestReference = DirectCast(firstSelectedComponent, TestReference)

                Me.TestReferenceControl.InitializePropertyEditorsAndSetDataSource(testRef, _testPackage.IncludedViews)
                _validationControls.Add(Me.TestReferenceControl)

                TabControl1.TabPages.Add(TabPage1)
            ElseIf TypeOf firstSelectedComponent Is TestSet Then
                Dim testSet As TestSet = DirectCast(firstSelectedComponent, TestSet)

                Me.TestSetControl.InitializePropertyEditorsAndSetDataSource(testSet, _testPackage.IncludedViews)
                _validationControls.Add(TestSetControl)

                TabControl1.TabPages.Add(TabPage2)
            Else
                Throw New NotSupportedException("A not supported test entity is selected in the hierarchy control!")
            End If

            Me.ResumeLayout()

            If Not _isAddingResource AndAlso Not (TypeOf firstSelectedComponent Is TestSet AndAlso TypeOf firstSelectedComponent Is TestReference) AndAlso
    ThisEditorTestSelectorDialog IsNot Nothing AndAlso ThisEditorTestSelectorDialog.Visible Then
                ThisEditorTestSelectorDialog.Close()
            End If
        End If


        Me.DetermineTestPackageComponentsButtonsEnableState(e.SelectedComponents.Count = 1)
    End Sub

    Private Sub HierarchyControl_TestPackageComponentDoubleClicked(sender As Object, e As TestPackageComponentSelectedEventArgs) Handles HierarchyControl.TestPackageComponentDoubleClicked
        If e.SelectedComponents.Count <> 1 Then
            Return
        End If

        Dim firstSelectedComponent As TestPackageNode = e.SelectedComponents(0)

        If TypeOf firstSelectedComponent IsNot TestReference Then
            Return
        End If

        Dim testEditorForm As Form = Nothing
        Dim testRef As TestReference = DirectCast(firstSelectedComponent, TestReference)

        If testRef Is Nothing Then
            Return
        End If

        Dim assessmentTestResourceEntity = CType(GetTestEntity(testRef), AssessmentTestResourceEntity)

        If (assessmentTestResourceEntity Is Nothing) Then
            MessageBox.Show(My.Resources.TestPackageEditor_TestNotFoundMessage, My.Resources.TestPackageEditor_TestNotFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not ResourceFactory.Instance.ResourceExists(assessmentTestResourceEntity.BankId, assessmentTestResourceEntity.ResourceId, False, New AssessmentTestResourceEntityFactory()) Then
            Return
        End If

        Dim forms As List(Of Form) = GetOwnedTestEditors().ToList()
        forms.AddRange(DirectCast(Me.Owner, MainForm).GetOwnedFormsOfType(GetType(TestEditor_v2)))
        DirectCast(Me.Owner, MainForm).GetOwnedFormsOfType(GetType(TestPackageEditor)).ToList().ForEach(Sub(tpe) forms.AddRange(DirectCast(tpe, TestPackageEditor).GetOwnedTestEditors()))

        testEditorForm = forms.OfType(Of TestEditor_v2).FirstOrDefault(Function(f) f.TestResourceEntity.ResourceId = assessmentTestResourceEntity.ResourceId)

        If testEditorForm Is Nothing Then
            testEditorForm = New TestEditor_v2(assessmentTestResourceEntity, True, True)
            AddHandler DirectCast(testEditorForm, TestEditor_v2).PreviewTest, AddressOf TestEditor_PreviewTest
            AddHandler DirectCast(testEditorForm, TestEditor_v2).MetaDataControl.OpenResourcePropertyDialogButtonClicked, Sub(sender1 As Object, e1 As EventArgs(Of Integer))
                                                                                                                              FormHelper.OpenResourcePropertyDialog(assessmentTestResourceEntity, e1.Value)
                                                                                                                          End Sub
            testEditorForm.Show(Me)
        Else
            ShowWindow(testEditorForm.Handle, ShowWindowCommands.Restore)
            testEditorForm.Focus()
        End If
    End Sub

    Private Function GetOwnedTestEditors() As IEnumerable(Of Form)
        Return From form In Me.OwnedForms Where form.GetType Is GetType(TestEditor_v2)
    End Function

    Private Sub TestEditor_PreviewTest(ByVal sender As Object, ByVal e As EntityActionEventArgs)
        Dim testResourceEntity As AssessmentTestResourceEntity = Nothing
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim test = DirectCast(e.SelectedEntity, AssessmentTestResourceDto)
        If Not FormHelper.TestsContainItems(New AssessmentTestResourceDto() {test}.ToList) Then
            Me.Cursor = Cursors.Default
        Else
            testResourceEntity = DirectCast(ResourceFactory.Instance.GetResourceByIdWithOption(test.ResourceId, New AssessmentTestResourceEntityFactory(), New ResourceRequestDTO()), AssessmentTestResourceEntity)

            If testResourceEntity.ResourceData Is Nothing Then
                Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testResourceEntity)
                testResourceEntity.ResourceData = data
            End If

            Dim factoryResult As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(testResourceEntity.ResourceData.BinData, True)
            Dim selectedTestPreviewHandler As TestPreviewHandlerIdentifier

            Using publicationClient = New PublicationServiceClient()
                Dim testPreviewers = publicationClient.GetAvailableTestPreviewHandlers(testResourceEntity.BankId, New String() {testResourceEntity.Name}, Nothing)
                If (testPreviewers.Count > 1) Then
                    Using selectTestPreviewDialog As New SelectTestPreviewDialog(factoryResult, testPreviewers)
                        If selectTestPreviewDialog.ShowDialog(Me) = DialogResult.OK Then
                            selectedTestPreviewHandler = selectTestPreviewDialog.SelectedTestPreviewHandler
                        Else
                            Me.Cursor = Cursors.Default
                            Return
                        End If
                    End Using
                ElseIf (testPreviewers.Count = 1) Then
                    selectedTestPreviewHandler = testPreviewers.First()
                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show(My.Resources.MainForm_TestCannotBePreviewed, My.Resources.MainForm_TestCannotBePreviewed_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If

            End Using
            Dim testPreviewLauncher As TestPreviewLauncher = New TestPreviewLauncher(testResourceEntity.BankId, testResourceEntity.Name, selectedTestPreviewHandler, False)
            FormHelper.ShowPreviewTestProgressDialog(testPreviewLauncher, Me.Cursor)
        End If
    End Sub

    Private Sub MoveTestDownInTestSetToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MoveTestDownInTestsetToolStripMenuItem.Click, MoveTestDownToolStripButton.Click, MoveTestDownInTestsetToolStripMenuItem1.Click
        HierarchyControl.MoveCurrentComponentDownInSection()
    End Sub

    Private Sub MoveTestUpInTestSetToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MoveTestUpToolStripButton.Click, MoveTestUpInTestsetToolStripMenuItem1.Click, MoveTestUpInTestsetToolStripMenuItem.Click
        HierarchyControl.MoveCurrentComponentUpInSection()
    End Sub

    Private Sub MoveTestSetDownToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MoveTestSetDownToolStripButton.Click, MoveTestsetDownInTestPackageToolStripMenuItem.Click, MoveTestsetUpInTestpackageToolStripMenuItem1.Click, MoveTestsetDownInTestpackageToolStripMenuItem1.Click
        HierarchyControl.MoveCurrentTestsetDownInTestPackage()
    End Sub

    Private Sub MoveSectionUpToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MoveTestSetUpToolStripButton.Click, MoveTestSetUpInTestPackageToolStripMenuItem.Click, MoveTestDownInTestsetToolStripMenuItem1.Click
        HierarchyControl.MoveCurrentTestsetUpInTestPackage()
    End Sub

    Private Sub RecurseComponentsCallBack(ByVal component As TestNodeBase)
        If TypeOf component Is TestReference Then
            RecurseComponentsCallBack_AddTestRef(DirectCast(component, TestReference))
        ElseIf TypeOf component Is TestSet Then
            RecurseComponentsCallBack_AddTestSet(DirectCast(component, TestSet))
        End If
    End Sub

    Private Sub RecurseComponentsCallBack_AddTestRef(ByVal newTestReference As TestReference)
        Dim resourceForReferencedTest As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(_testPackageResourceEntity.BankId, newTestReference.SourceName, New ResourceRequestDTO())

        If Not _testPackageResourceEntity.ContainsDependentResource(resourceForReferencedTest) Then

            If Not ContainsTestSupportedViews(newTestReference, GeneralHelper.GetViewsWithoutGeneral(_testPackage.IncludedViews.Select(Function(s) s.ToString()).ToList())) Then
                MessageBox.Show(My.Resources.TestPackageEditor_CannotAddTestIncompatibleViewTypes, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
            If newTestReference.Parent Is Nothing Then
                If TypeOf _pasteDestinationParentForRecurseComponents Is TestSet Then
                    DirectCast(_pasteDestinationParentForRecurseComponents, TestSet).Components.Add(newTestReference)
                Else
                    Throw New Exception("Unexpected parent type for component during paste. Parent for TestRef must be Testset.")
                End If
            End If

            DependencyManagement.AddDependentResourceToResource(_testPackageResourceEntity, resourceForReferencedTest)

        ElseIf newTestReference.Parent IsNot Nothing Then
            _testPackageComponentsMarkedForRemoval.Add(newTestReference)
        End If
    End Sub

    Private Sub RecurseComponentsCallBack_AddTestSet(ByVal newTestset As TestSet)
        If newTestset IsNot Nothing Then
            If TypeOf _pasteDestinationParentForRecurseComponents Is TestPackage Then
                Dim tests As List(Of TestReference) = newTestset.GetAllTestReferencesInTestSet()
                For Each testRef As TestReference In tests
                    If Not ContainsTestSupportedViews(testRef, _testPackage.IncludedViews.Select(Function(s) s.ToString()).ToList()) Then
                        MessageBox.Show(My.Resources.TestPackageEditor_CannotAddTestIncompatibleViewTypes, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Next
                Dim testPackage As TestPackage = DirectCast(_pasteDestinationParentForRecurseComponents, TestPackage)
                testPackage.TestSets.Add(newTestset)

                Dim allDependentResourcesForTestSet As New ResourceEntryCollection()
                For Each viewType In _testPackage.IncludedViews
                    Dim view As TestSetViewBase = TestPackageFactory.CreateTemporaryTestSetView(newTestset, viewType)
                    view.GetDependencyResourcesForThisTestset(allDependentResourcesForTestSet, False)
                Next
                For Each entry As ResourceEntry In allDependentResourcesForTestSet
                    Dim resourceForReference As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(_testPackageResourceEntity.BankId, entry.Name, New ResourceRequestDTO())
                    If resourceForReference IsNot Nothing Then
                        DependencyManagement.AddDependentResourceToResource(_testPackageResourceEntity, resourceForReference)
                    Else
                        Throw New Exception(
    $"Unexpected Error during paste. Unable to resolve dependent resource reference: '{ _
                       entry.Name}'.")
                    End If
                Next
            Else
                Throw New Exception("Unexpected type of paste destination during paste. Paste destination must either be a AssessmentTest.")
            End If
        Else
            Throw New Exception("Expected component of type TestPart during paste.")
        End If
    End Sub

    Private Function SaveAsTestPackage() As Boolean
        Dim resultValue As Boolean = False

        If Not _testPackageResourceEntity.IsNew Then
            Dim newCode As InputBoxResult = InputBox.Show(My.Resources.ItemEditor_PleaseEnterNewItemCode, False, My.Resources.ItemEditor_SaveAsText, String.Format(My.Resources.CopyOf0, _testPackageResourceEntity.Name), AddressOf ValidationHelper.IsValidTestCode)

            If newCode.Text.Length > 0 Then
                Dim originalTestPackageCode As String = _testPackage.Identifier
                Dim originalTestPackageResource As TestPackageResourceEntity = _testPackageResourceEntity


                Try
                    Dim newtestPackageResourceEntity = _testPackageResourceEntity.CopyToNew(newCode.Text)
                    newtestPackageResourceEntity.Version = String.Empty

                    _testPackageResourceEntity = newtestPackageResourceEntity
                    _testPackage.Identifier = newCode.Text

                    resultValue = SaveTestPackage()
                    If resultValue Then
                        _testPackageResourceEntity = ResourceFactory.Instance.GetTestPackage(_testPackageResourceEntity)
                        _testPackage = _testPackageResourceEntity.GetTestPackage
                        Mediator.Instance.NotifyColleagues(Of EventArgs(Of IPropertyEntity))("ResourceEditor_RefreshGridAndSelectResource",
    New EventArgs(Of IPropertyEntity)(_testPackageResourceEntity))
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message, Application.ProductName)
                End Try

                If Not resultValue Then
                    _testPackageResourceEntity = originalTestPackageResource
                    _testPackage.Identifier = originalTestPackageCode
                End If

                BindControlsAtLoad(resultValue)
            End If
        Else
            Throw New ArgumentNullException("test is not yet saved. Save as.. functionality is not supported.")
        End If
        Return resultValue
    End Function

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripButton.Click, SaveAsToolStripMenuItem.Click
        EnsuresFormUpdateAfterEditing()

        If Me.SaveAsTestPackage() Then

            Dim titleBarBinding As Binding = Me.DataBindings("text")
            If titleBarBinding IsNot Nothing Then
                Me.DataBindings.Remove(titleBarBinding)
            End If
            titleBarBinding = New Binding("text", _testPackage, "identifier")
            AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
            Me.DataBindings.Add(titleBarBinding)

        End If
    End Sub

    Private Sub SaveCloseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveCloseToolStripButton.Click, SaveCloseToolStripMenuItem.Click

        EnsuresFormUpdateAfterEditing()
        If Me.IsDirty Then
            If PreSaveAndSaveAndPostSaveTestPackage() Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Function SaveIfNecessary() As Boolean
        If Me.IsDirty AndAlso PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.TestEntity, _testPackageResourceEntity.BankId) Then
            Dim result As DialogResult = MessageBox.Show(My.Resources.Editor_SaveChangesQuestionMessage, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            Select Case result
                Case DialogResult.Cancel
                    Return False
                Case DialogResult.No
                    Return True
                Case DialogResult.Yes
                    Return SaveTestPackage()
            End Select
        End If

        Return True
    End Function

    Private Function SaveTestPackage() As Boolean
        Dim resultValue As Boolean = False

        For Each view In _testPackage.IncludedViews
            Dim plugin = IoCHelper.GetInstances(Of ITestPackageEditorPlugin).Where(Function(p) p.IsSupportedView(view)).FirstOrDefault()

            If Not plugin Is Nothing Then
                plugin.PreSave(_testPackage)
            End If
        Next

        If Me.IsDirty AndAlso Me.ValidateTestPackage() AndAlso Me.MetaDataControl.CanUpdateResource(ResourceDataIsDirty) Then

            Dim serializedTestPackageModel As Byte() = Nothing
            serializedTestPackageModel = SerializeHelper.XmlSerializeToByteArray(_testPackage)

            _testPackageResourceEntity.ResourceData.BinData = serializedTestPackageModel

            If Not _testPackage.Identifier.Equals(_testPackageResourceEntity.Name) Then
                _testPackageResourceEntity.Name = _testPackage.Identifier
            End If
            If Not _testPackage.Title.Equals(_testPackageResourceEntity.Title) Then
                _testPackageResourceEntity.Title = _testPackage.Title
            End If

            If ResourceCustomProperties1.RemovedEntities.Count > 0 Then
                BankFactory.Instance.DeleteCustomPropertyValues(ResourceCustomProperties1.RemovedEntities)
            End If

            If TestPackageResourceEntity.RequiresMajorVersionIncrement() Then
                If Not IncrementMajorVersion() Then
                    Return False
                End If
            End If

            Dim result As String = ResourceFactory.Instance.UpdateTestPackageResource(_testPackageResourceEntity)

            If String.IsNullOrEmpty(result) Then
                For Each ctl As TestPackageEditorContainerBase In _validationControls
                    ctl.ResetDatasource()
                Next
                StatusTextLabel.Text = My.Resources.TestPackageEditor_SaveTestPackage_TestPackageSavedStatusbarMessage

                resultValue = True
            Else
                MessageBox.Show(result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                StatusTextLabel.Text = My.Resources.TestPackageEditor_SaveTestPackage_SaveFailedStatusbarMessage
                resultValue = False
            End If
        Else
            resultValue = False
        End If

        Return resultValue
    End Function

    Private Function IncrementMajorVersion() As Boolean
        Return _windowfacade.OpenMajorVersionDialog(TestPackageResourceEntity)
    End Function

    Private Sub SaveToolStripMenuTest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click, SaveToolStripButton.Click
        PreSaveAndSaveAndPostSaveTestPackage()
    End Sub

    Private Sub PropertyControl_CommandExecuteRequest(ByVal sender As Object, ByVal e As CommandExecuteRequestEventArgs) Handles TestPackagePropertiesControl.CommandExecuteRequest, TestSetControl.CommandExecuteRequest
        Dim selectedTestComponent As TestPackageNode = HierarchyControl.SelectedComponent
        If selectedTestComponent IsNot Nothing Then
            HandleCommandExecuteRequestFromPropertyEditor(e.Command)
        End If
    End Sub

    Private Sub SubControl_DataChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TestPackagePropertiesControl.DataChanged, TestSetControl.DataChanged, TestReferenceControl.DataChanged
        HierarchyControl.RefreshDataSource()
    End Sub

    Private Sub SubControl_DependentResourceAdded(ByVal sender As Object, ByVal e As ResourceEventArgs) Handles TestPackagePropertiesControl.DependentResourceAdded, TestReferenceControl.DependentResourceAdded
        DependencyManagement.AddDependentResourceToResource(_testPackageResourceEntity, e.Resource.ResourceId)
    End Sub

    Private Sub SubControl_DependentResourceRemoved(ByVal sender As Object, ByVal e As ResourceNameEventArgs) Handles TestPackagePropertiesControl.DependentResourceRemoved, TestReferenceControl.DependentResourceRemoved
        DependencyManagement.RemoveDependentResourceFromResource(_testPackageResourceEntity, e.ResourceName)
    End Sub

    Private Sub SubControl_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs) Handles TestSetControl.ResourceNeeded, TestPackagePropertiesControl.ResourceNeeded, TestReferenceControl.ResourceNeeded
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    Private Sub TestPackageEditor_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler Me.TestPackagePropertiesControl.ResourceNeeded, AddressOf TestPackageProperties_ResourceNeeded

        TestBuilderAsyncProtocolContextManager.UnRegisterResourceManager(_resourceManager)
        TestSessionContext.CurrentItem = Nothing
        _testPackage = Nothing
        If _resourceManager IsNot Nothing Then
            _resourceManager.Dispose()
            _resourceManager = Nothing
        End If
        _testPackageResourceEntity = Nothing
    End Sub

    Private Sub TestPackageEditor_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not _viewSelectionDialogAtStartupCancelled Then
            EnsuresFormUpdateAfterEditing()

            TestBuilderClientSettings.TestPackageEditorWindowState = Me.WindowState
            If Me.WindowState = FormWindowState.Normal Then
                TestBuilderClientSettings.TestPackageEditorBounds = Me.Bounds
            End If

            e.Cancel = Not Me.SaveIfNecessary() AndAlso IsDirty

            If Not e.Cancel Then
                If _resourceManager IsNot Nothing Then
                    _resourceManager.Dispose()
                    _resourceManager = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub TestPackageEditor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Visible = False

        AddHandler Me.TestPackagePropertiesControl.ResourceNeeded, AddressOf TestPackageProperties_ResourceNeeded

        _testsetResourceReferencesBeingDropped = New List(Of String)

        If Not _testPackageResourceEntity.IsNew Then
            _testPackageResourceEntity = GetFullTestPackageEntity(_testPackageResourceEntity)
        End If

        _testPackage = _testPackageResourceEntity.GetTestPackage

        Dim includedViewsInTest As List(Of String) = TestPackageFactory.GetIncludedViews(_testPackage)
        If includedViewsInTest.Count = 0 OrElse _testPackageResourceEntity.IsNew Then
            Dim viewSelectorDialog As New SelectSupportedViewsForAssessmentTestDialog(SelectSupportedViewsForAssessmentTestDialog.DialogDisplayMode.NewTestTemplate, False)
            If viewSelectorDialog.ShowDialog() = DialogResult.OK Then
                Dim selectedViews As List(Of String) = viewSelectorDialog.SelectedViewTypes

                For Each viewToCreate In selectedViews
                    TestPackageFactory.CreateView(_testPackage, viewToCreate)
                Next

                includedViewsInTest = TestPackageFactory.GetIncludedViews(_testPackage)
            End If
        End If

        If includedViewsInTest.Count > 0 Then
            BindControlsAtLoad(True)

            Me.DataBindings.Clear()
            Dim titleBarBinding As New Binding("text", _testPackage, "identifier")
            AddHandler titleBarBinding.Format, AddressOf TitleToFormTitleFormatter
            Me.DataBindings.Add(titleBarBinding)

            If Not PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.TestEntity, _testPackageResourceEntity.BankId) Then
                SaveToolStripMenuItem.Enabled = False
                SaveToolStripButton.Enabled = False
                SaveCloseToolStripMenuItem.Enabled = False
                SaveCloseToolStripButton.Enabled = False
                SaveAsToolStripMenuItem.Enabled = False
                SaveAsToolStripButton.Enabled = False
            End If

            If SaveAsToolStripButton.Enabled Then
                SaveAsToolStripButton.Enabled = Not TestPackageResourceEntity.IsNew
                SaveAsToolStripMenuItem.Enabled = Not TestPackageResourceEntity.IsNew
            End If

            Me.Width = TestBuilderClientSettings.TestPackageEditorBounds.Width
            Me.Height = TestBuilderClientSettings.TestPackageEditorBounds.Height

            Me.WindowState = CType(TestBuilderClientSettings.TestPackageEditorWindowState, FormWindowState)
            If Me.WindowState = FormWindowState.Normal Then
                Me.Bounds = TestBuilderClientSettings.TestPackageEditorBounds
            End If

            Me.Visible = True
        Else
            _viewSelectionDialogAtStartupCancelled = True
            Me.Close()
        End If

    End Sub

    Private Sub ChangeTestPackageCodeDialog_ValidateNewCodeName(ByVal sender As Object, ByVal e As ValidateNewCodeNameEventArgs)
        e.Valid = Not ResourceFactory.Instance.ResourceExists(_testPackageResourceEntity.BankId, e.NewCodeName, True)
        If Not e.Valid Then
            MessageBox.Show(My.Resources.TestPackageEditor_TestCodeNameAlreadyExistsInBankHierarchy1, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub TestPackageProperties_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    Private Sub TitleToFormTitleFormatter(ByVal sender As Object, ByVal e As ConvertEventArgs)
        Dim testPackageResourceType As String = My.Resources.TestPackage
        e.Value = String.Format(My.Resources.EditorFor01, testPackageResourceType, e.Value)
    End Sub

    Private Function GetMergedValidationList(includedviews As List(Of String)) As ValidationValueCollection
        Dim mergedValidationList As New ValidationValueCollection
        For Each validationViewType In includedviews
            Dim view As TestPackageViewBase = TestPackageFactory.CreateView(_testPackage, validationViewType)
            Dim validationEntityOfView As ValidatingEntityBase = DirectCast(view, ValidatingEntityBase)

            For Each validationEntry As ValidationValue In validationEntityOfView.GetValidationErrors(True)
                If Not mergedValidationList.ContainsValidationValue(validationEntry.FieldName, validationEntry.ValidatingEntity) Then
                    mergedValidationList.Add(validationEntry)
                End If
            Next
        Next
        Return (mergedValidationList)
    End Function

    Private Function ValidateTestPackage() As Boolean
        Dim returnvalue As Boolean
        Dim includedviews As New List(Of String)(_testPackage.IncludedViews)
        Dim mergedValidationList As ValidationValueCollection = GetMergedValidationList(includedviews)
        If Not includedviews.Count = _testPackage.IncludedViews.Count Then
            mergedValidationList = GetMergedValidationList(_testPackage.IncludedViews)
        End If
        If mergedValidationList.Count > 0 Then
            Dim errorMessageBuilder As New StringBuilder()
            errorMessageBuilder.AppendFormat(My.Resources.TestPackageEditor_ValidateItem_ValidationErrors, Environment.NewLine)

            For Each validationEntry As ValidationValue In mergedValidationList
                errorMessageBuilder.AppendFormat(" - {0} In '{1}' ({2}){3}", validationEntry.Message, validationEntry.ValidatingEntity.ValidationEntityIdentifier, validationEntry.FriendlyEntityName, Environment.NewLine)
            Next
            MessageBox.Show(errorMessageBuilder.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            returnvalue = False
        Else
            returnvalue = True
        End If
        Return returnvalue
    End Function

    Private Function PreSaveAndSaveAndPostSaveTestPackage() As Boolean
        Dim returnValue As Boolean = False

        EnsuresFormUpdateAfterEditing()

        If SaveTestPackage() Then
            _testPackageResourceEntity = ResourceFactory.Instance.GetTestPackage(_testPackageResourceEntity)
            _testPackage = _testPackageResourceEntity.GetTestPackage
            If PermissionFactory.Instance.TryUserIsPermittedTo(TestBuilderPermissionAccess.Edit, TestBuilderPermissionTarget.ItemEntity, _testPackageResourceEntity.BankId) Then
                Me.SaveAsToolStripButton.Enabled = True
            End If

            BindControlsAtLoad(True)

            returnValue = True
        End If

        Return returnValue
    End Function

    Private Sub ChangeViewTypesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ChangeViewTypesToolStripMenuItem.Click
        Dim currentViews As New List(Of String)(_testPackage.IncludedViews)
        Using dialog As New SelectSupportedViewsForAssessmentTestDialog(SelectSupportedViewsForAssessmentTestDialog.DialogDisplayMode.AddOrDeleteViewTypesOnTest, currentViews, False)

            If dialog.ShowDialog(Me) = DialogResult.OK Then
                Dim selectedViews As List(Of String) = dialog.SelectedViewTypes

                Dim alltestsReferencesInTestCollection As ReadOnlyCollection(Of TestReference) = _testPackage.GetAllTestReferencesInTestPackage()
                For Each testRef As TestReference In alltestsReferencesInTestCollection

                    If Not ContainsTestSupportedViews(testRef, selectedViews) Then
                        MessageBox.Show(My.Resources.TestPackageEditor_ChangeViewTypes_InCompatibleTestsInTestPackageMessage, My.Resources.TestPackageEditor_ChangeViewTypes_InCompatibleTestsInTestPackageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Next

                For Each currentView In currentViews.Where(Function(v) v <> GenericTestModelPlugin.PLUGIN_NAME)
                    If Not selectedViews.Contains(currentView) AndAlso MessageBox.Show(String.Format(My.Resources.TestPackageEditor_RemoveViewTypeMessageBox, currentView), My.Resources.TestPackageEditor_RemoveViewTypeMessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        TestPackageFactory.DeleteViewFromModel(_testPackage, currentView)
                    End If
                Next

                selectedViews.Where(Function(v) Not currentViews.Contains(v)).ToList().ForEach(Sub(v)
                                                                                                   Dim addedView As TestPackageViewBase = TestPackageFactory.CreateView(_testPackage, v)
                                                                                               End Sub)

                BindControlsAtLoad(False)
            End If
        End Using
    End Sub

    Private Function ContainsTestSupportedViews(ByVal testRef As TestReference, ByVal mustSupportViewTypes As List(Of String)) As Boolean
        Dim testEntity As AssessmentTestResourceEntity = CType(GetTestEntity(testRef), AssessmentTestResourceEntity)
        Return ContainsTestSupportedViews(testEntity.GetAssessmentTest, mustSupportViewTypes)
    End Function

    Private Function ContainsTestSupportedViews(ByVal assessmentTest As AssessmentTest2, ByVal mustSupportViewTypes As List(Of String)) As Boolean
        Dim viewTypeLeftovers As New List(Of String)(mustSupportViewTypes)
        Dim templateTargets As List(Of String)

        If Not _cachedSupportedViewsOfTest.ContainsKey(assessmentTest.Identifier) Then
            templateTargets = assessmentTest.IncludedViews
            _cachedSupportedViewsOfTest.Add(assessmentTest.Identifier, templateTargets)
        Else
            templateTargets = _cachedSupportedViewsOfTest(assessmentTest.Identifier)
        End If

        For Each viewType As String In templateTargets
            If viewTypeLeftovers.Contains(viewType) Then
                viewTypeLeftovers.Remove(viewType)
            End If
        Next
        Return (viewTypeLeftovers.Count = 0)
    End Function




    Public Sub DisableAllTestComponentButtons()
        AddTestToolStripButton.Enabled = False
        AddNewTestToolStripMenuItem.Enabled = False
        AddTestsetToolStripButton.Enabled = False
        AddNewTestsetToolStripMenuItem.Enabled = False
        DeleteTestPackageComponentToolStripButton.Enabled = False
        DeleteTestPackageComponentToolStripMenuItem.Enabled = False
        MoveTestDownInTestsetToolStripMenuItem.Visible = False
        MoveTestDownInTestsetToolStripMenuItem1.Visible = False
        MoveTestDownToolStripButton.Visible = False
        MoveTestUpInTestsetToolStripMenuItem.Visible = False
        MoveTestUpToolStripButton.Visible = False
        MoveTestUpInTestsetToolStripMenuItem1.Visible = False
        MoveTestSetDownToolStripButton.Visible = False
        MoveTestsetDownInTestPackageToolStripMenuItem.Visible = False
        MoveTestsetDownInTestpackageToolStripMenuItem1.Visible = False
        MoveTestSetUpInTestPackageToolStripMenuItem.Visible = False
        MoveTestsetUpInTestpackageToolStripMenuItem1.Visible = False
        MoveTestSetUpToolStripButton.Visible = False
        MoveButtonsMenuToolStripSeparator.Visible = False
        MoveButtonsToolStripSeparator.Visible = False

        HierarchyControl.DisableAllTestComponentContextMenu()
    End Sub


    Private Sub _constructionFacade_ResolveValidationError(ByVal sender As Object, ByVal e As TestConstructionValidationEventArgs) Handles _constructionFacade.ResolveValidationError
        Dim dialog As New ResolveValidationErrorDialog(e.UnderlyingException, e.ResolutionsAvailable)
        dialog.ShowDialog(Me)
        e.Resolution = dialog.Resolution
    End Sub
End Class
