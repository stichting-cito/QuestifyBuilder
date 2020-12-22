Imports System.ComponentModel
Imports System.Text.RegularExpressions

Imports Cito.TestBuilder
Imports Cito.TestBuilder.ContentModel
Imports Cito.TestBuilder.Security
Imports Cito.TestBuilder.Service

Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class CustomPropertiesControl

#Region "Fields"

    Private _contextEntity As EntityClasses.BankEntity
    Private _removedEntities As HelperClasses.EntityCollection

#End Region 'Fields

#Region "Events"

    ''' <summary>
    ''' Occurs when [data changed].
    ''' </summary>
    ''' <history>
    '''    [michelka] 31-1-2008 Created
    ''' </history>
    Public Event DataChanged As EventHandler(Of EventArgs)

#End Region 'Events

#Region "Properties"

    ''' <summary>
    ''' Gets or sets the bank context.
    ''' </summary>
    ''' <value>The bank context.</value>
    ''' <history>
    '''    [michelka] 14-1-2008 Created
    ''' </history>
    Public Property BankContext() As EntityClasses.BankEntity
        Get
            Return _contextEntity
        End Get
        Set(ByVal value As EntityClasses.BankEntity)
            _contextEntity = value
            If value IsNot Nothing Then
                Me.CustomPropertyCollectionBindingSource.DataSource = _contextEntity.CustomBankPropertyCollection

                ' add Handler to detect and administer removal of objects
                AddHandler _contextEntity.CustomBankPropertyCollection.EntityRemoved, AddressOf EntityRemovedFromCollection
            Else
                Me.CustomPropertyCollectionBindingSource.DataSource = Nothing
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets a value indicating whether this instance is dirty.
    ''' </summary>
    ''' <value><c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
    ''' <history>
    '''    [michelka] 21-1-2008 Created
    ''' </history>
    Public ReadOnly Property IsDirty() As Boolean
        Get
            Return _contextEntity.HasChangesInTopology Or (_removedEntities.Count > 0)
        End Get
    End Property

    ''' <summary>
    ''' Gets the removed entities, this collection holds all 
    ''' entities which are remove/deleted during editing
    ''' </summary>
    ''' <value>The removed entities.</value>
    ''' <history>
    '''    [michelka] 21-1-2008 Created
    ''' </history>
    Public ReadOnly Property RemovedEntities() As HelperClasses.EntityCollection
        Get
            If _removedEntities Is Nothing Then
                _removedEntities = New HelperClasses.EntityCollection(New FactoryClasses.UserBankRoleEntityFactory)
            End If
            Return _removedEntities
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Adds the removed entities.
    ''' </summary>
    ''' <history>
    ''' [ruben] 25-1-2012 Created
    ''' </history>
    Public Sub UndoRemoveEntities()

        BankContext = BankFactory.Instance.GetBank(_contextEntity.Id)
        Me.CustomPropertyCollectionBindingSource.DataSource = _contextEntity.CustomBankPropertyCollection

        _removedEntities.Clear()

    End Sub

    ''' <summary>
    ''' Raises the <see cref="E:DataChanged" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 31-1-2008 Created
    ''' </history>
    Protected Sub OnDataChanged(ByVal e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub

    ''' <summary>
    ''' Handles the Click event of the AddButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 15-1-2008 Created
    ''' </history>
    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click
        Dim Control As Control = DirectCast(sender, Control)
        Dim Position As New System.Drawing.Point(0, Control.Height)
        CustomPropertyContextMenu.Show(Control, Position)
    End Sub

    ''' <summary>
    ''' Handles the Click event of the AddValueButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 15-1-2008 Created
    ''' </history>
    Private Sub AddValueButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddValueButton.Click
        ' Get currently selected property
        Dim activeProperty As EntityClasses.ListCustomBankPropertyEntity = DirectCast(CustomPropertyCollectionBindingSource.Current, EntityClasses.ListCustomBankPropertyEntity)
        Dim NewValue As New EntityClasses.ListValueCustomBankPropertyEntity
        With NewValue
            .ListValueBankCustomPropertyId = Guid.NewGuid
            .Title = My.Resources.CustomPropertiesNewFree
            .Name = My.Resources.CustomPropertiesNewFree
        End With
        activeProperty.ListValueCustomBankPropertyCollection.Add(NewValue)
        OnDataChanged(New EventArgs)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the CustomPropertiesControl control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 24-1-2008 Created
    ''' </history>
    Private Sub CustomPropertiesControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ApplicableToMaskControl1.Labels.Add("Items", ResourceTypeEnum.ItemResource)
        ApplicableToMaskControl1.Labels.Add(My.Resources.Tests, ResourceTypeEnum.AssessmentTestResource)
        ApplicableToMaskControl1.Labels.Add("Media", ResourceTypeEnum.GenericResource)
    End Sub

    ''' <summary>
    ''' Handles the CurrentChanged event of the CustomPropertyCollectionBindingSource control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    ''' [michelka] 16-1-2008 Created
    ''' </history>
    Private Sub CustomPropertyCollectionBindingSource_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomPropertyCollectionBindingSource.CurrentChanged
        If CustomPropertyCollectionBindingSource.Current IsNot Nothing Then
            Select Case CustomPropertyCollectionBindingSource.Current.GetType.ToString
                Case GetType(EntityClasses.FreeValueCustomBankPropertyEntity).ToString

                    NameTextBox.Enabled = True
                    TitleTextBox.Enabled = True

                    ApplicableToMaskControl1.Enabled = True

                    ListValuesGridEX1.DataSource = Nothing
                    ListValuesGridEX1.Visible = False

                    AddValueButton.Visible = False
                    DeleteValueButton.Visible = False
                    ListValuesLabel.Visible = False

                Case GetType(EntityClasses.ListCustomBankPropertyEntity).ToString
                    Dim ListCustomProperty As EntityClasses.ListCustomBankPropertyEntity = DirectCast(CustomPropertyCollectionBindingSource.Current, EntityClasses.ListCustomBankPropertyEntity)

                    NameTextBox.Enabled = True
                    TitleTextBox.Enabled = True

                    ApplicableToMaskControl1.Enabled = True

                    ListValuesGridEX1.Visible = True
                    ListValuesErrorProvider.DataSource = ListCustomProperty.ListValueCustomBankPropertyCollection
                    ListValuesGridEX1.DataSource = ListCustomProperty.ListValueCustomBankPropertyCollection

                    AddValueButton.Visible = True
                    DeleteValueButton.Visible = True
                    ListValuesLabel.Visible = True

                    ' add Handler to detect and administer removal of objects
                    AddHandler ListCustomProperty.ListValueCustomBankPropertyCollection.EntityRemoved, AddressOf EntityRemovedFromCollection

                    AddValueButton.Enabled = True
                    DeleteValueButton.Enabled = True

                Case Else
                    ApplicableToMaskControl1.Enabled = False
                    ListValuesGridEX1.Visible = False
                    ListValuesGridEX1.DataSource = Nothing
                    AddValueButton.Visible = False
                    DeleteValueButton.Visible = False
                    ListValuesLabel.Visible = False

            End Select

            ' Check if there are any changes, if so raise event
            If _contextEntity.HasChangesInTopology Then
                OnDataChanged(New EventArgs)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the DeleteButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 21-1-2008 Created
    ''' </history>
    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        If CustomPropertyCollectionBindingSource.Current IsNot Nothing Then
            CustomPropertyCollectionBindingSource.RemoveCurrent()
            OnDataChanged(New EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the DeleteValueButton control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 21-1-2008 Created
    ''' </history>
    Private Sub DeleteValueButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteValueButton.Click
        If CustomPropertyCollectionBindingSource.Current IsNot Nothing Then
            Dim ListCustomProperty As EntityClasses.ListCustomBankPropertyEntity = DirectCast(CustomPropertyCollectionBindingSource.Current, EntityClasses.ListCustomBankPropertyEntity)
            Dim value As EntityClasses.ListValueCustomBankPropertyEntity
            If ListValuesGridEX1.CurrentRow IsNot Nothing Then
                value = DirectCast(ListValuesGridEX1.CurrentRow.DataRow, EntityClasses.ListValueCustomBankPropertyEntity)
                ListCustomProperty.ListValueCustomBankPropertyCollection.Remove(value)
                OnDataChanged(New EventArgs)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Entities the removed from collection.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="SD.LLBLGen.Pro.ORMSupportClasses.CollectionChangedEventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 21-1-2008 Created
    ''' </history>
    Private Sub EntityRemovedFromCollection(ByVal sender As Object, ByVal e As CollectionChangedEventArgs)
        Me.RemovedEntities.Add(DirectCast(e.InvolvedEntity, EntityBase2))
        OnDataChanged(New EventArgs)
    End Sub

    ''' <summary>
    ''' Handles the Click event of the FreeValueToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 15-1-2008 Created
    ''' </history>
    Private Sub FreeValueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FreeValueToolStripMenuItem.Click
        ' Create new property
        Dim NewFreeValue As New EntityClasses.FreeValueCustomBankPropertyEntity
        With NewFreeValue
            .CustomBankPropertyId = Guid.NewGuid
            .Name = My.Resources.CustomPropertiesNewFree
            .Title = My.Resources.CustomPropertiesNewFree
            .ApplicableToMask = 0
        End With

        ' Add to context
        Me.BankContext.CustomBankPropertyCollection.Add(NewFreeValue)
        OnDataChanged(New EventArgs)
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ListValueMultiToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 15-1-2008 Created
    ''' </history>
    Private Sub ListValueMultiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListValueMultiToolStripMenuItem.Click
        ' Create new property
        Dim NewListValue As New EntityClasses.ListCustomBankPropertyEntity
        With NewListValue
            .CustomBankPropertyId = Guid.NewGuid()
            .Name = My.Resources.CustomPropertiesNewMulti
            .Title = My.Resources.CustomPropertiesNewMulti
            .ApplicableToMask = 0
            .MultipleSelect = True
        End With

        ' Add to context
        Me.BankContext.CustomBankPropertyCollection.Add(NewListValue)
        OnDataChanged(New EventArgs)
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ListValueSingleToolStripMenuItem control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    ''' <history>
    '''    [michelka] 15-1-2008 Created
    ''' </history>
    Private Sub ListValueSingleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListValueSingleToolStripMenuItem.Click
        ' Create new property
        Dim NewListValue As New EntityClasses.ListCustomBankPropertyEntity
        With NewListValue
            .CustomBankPropertyId = Guid.NewGuid()
            .Name = My.Resources.CustomPropertiesNewSingle
            .Title = My.Resources.CustomPropertiesNewSingle
            .ApplicableToMask = 0
            .MultipleSelect = False
        End With

        ' Add to context
        Me.BankContext.CustomBankPropertyCollection.Add(NewListValue)
        OnDataChanged(New EventArgs)
    End Sub

#End Region 'Methods

End Class