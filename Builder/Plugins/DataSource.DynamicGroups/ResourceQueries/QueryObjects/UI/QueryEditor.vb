Imports System.Drawing
Imports System.Windows.Forms
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.UI.Commanding
Imports Questify.Builder.UI.Commanding

Public Class QueryEditor


    Private WithEvents _context As New QueryEditorContext
    Private _currentFilterEditor As FilterEditorBase
    Private _resourceManager As DataBaseResourceManager



    Public Sub New()
        InitializeComponent()

    End Sub



    Public Property Query() As Query
        Get
            Return _context.CurrentQuery
        End Get
        Set(ByVal value As Query)
            _context.SetCurrentQuery(value)
            CreateEditorTree(FilterContainerTreeView, _context.CurrentQuery)
        End Set
    End Property

    Public Property ResourceManager() As DataBaseResourceManager
        Get
            Return _resourceManager
        End Get
        Set(ByVal value As DataBaseResourceManager)
            _resourceManager = value
        End Set
    End Property



    Private Sub BindCommands()
        QueryEditorCommandManager.Bind(New SetFilterCommand(GetType(AndFilterPredicate), My.Resources.AndFilterPredicateName, My.Resources.AndFilterPredicateNameLocalized), New ContextParameterBinding(Me._context), AndToolStripMenuItem1)
        QueryEditorCommandManager.Bind(New SetFilterCommand(GetType(OrFilterPredicate), My.Resources.OrFilterPredicateName, My.Resources.OrFilterPredicateNameLocalized), New ContextParameterBinding(Me._context), OrToolStripMenuItem1)
        QueryEditorCommandManager.Bind(New SetFilterCommand(GetType(NotFilterPredicate), My.Resources.NotFilterPredicateName, My.Resources.NotFilterPredicateNameLocalized), New ContextParameterBinding(Me._context), NotToolStripMenuItem1)

        QueryEditorCommandManager.Bind(New SetFilterCommand(GetType(ResourcePropertyFilterPredicate), My.Resources.ResourcePropertyFilterPredicateName, My.Resources.ResourcePropertyFilterPredicateNameLocalized), New ContextParameterBinding(Me._context), PropertyToolStripMenuItem)
        QueryEditorCommandManager.Bind(New SetFilterCommand(GetType(ItemInTestFilterPredicate), My.Resources.ItemInTestFilterPredicateName, My.Resources.ItemInTestFilterPredicateNameLocalized), New ContextParameterBinding(Me._context), ItemInTestToolStripMenuItem)

        QueryEditorCommandManager.Bind(New RemoveFilterCommand(Me.FilterContainerTreeView, My.Resources.RemoveFilterCommandName, My.Resources.RemoveFilterCommandNameLocalized), New ContextParameterBinding(Me._context), RemoveToolStripMenuItem)
    End Sub

    Private Sub CreateEditorTree(tree As TreeView, Query As ItemQuery)
        With tree
            .Nodes.Clear()
            .Nodes.Add(New TreeNode("Filter"))

            If Query IsNot Nothing Then
                CreateEditorTree(.Nodes(0), Query.Filter, 0)
            End If
        End With
    End Sub

    Private Sub CreateEditorTree(parentNode As TreeNode, Filter As FilterPredicate, level As Integer)
        If level = 0 Then
            parentNode.Nodes.Clear()
        End If

        If Filter IsNot Nothing AndAlso Filter.IsContainer Then
            Dim filterNode As TreeNode = CreateFilterTreeNode(Filter)
            parentNode.Nodes.Add(filterNode)

            If TypeOf Filter Is DyadicFilterPredicate Then
                With CType(Filter, DyadicFilterPredicate)
                    CreateEditorTree(filterNode, .One, level + 1)
                    CreateEditorTree(filterNode, .Other, level + 1)
                End With

            ElseIf TypeOf Filter Is MonadicFilterPredicate Then
                With CType(Filter, MonadicFilterPredicate)
                    CreateEditorTree(filterNode, .Wrapped, level + 1)
                End With
            End If
        Else
            If Filter Is Nothing Then
                parentNode.Nodes.Add(CreateFilterPlaceHolderTreeNode)
            Else
                parentNode.Nodes.Add(CreateFilterTreeNode(Filter))
            End If
        End If

        parentNode.ExpandAll()
    End Sub

    Private Sub updateTree(tree As TreeView)
        If tree.Nodes.Count > 0 Then
            UpdateTreeNodeLabel(tree.Nodes(0).Nodes)
        End If
    End Sub

    Private Sub UpdateTreeNodeLabel(nodes As TreeNodeCollection)
        For Each node As TreeNode In nodes
            If TypeOf node.Tag Is FilterPredicate Then
                node.Text = CType(node.Tag, FilterPredicate).ToString
            End If

            UpdateTreeNodeLabel(node.Nodes)
        Next
    End Sub

    Private Function CreateFilterPlaceHolderTreeNode() As TreeNode
        Dim filterNode As New TreeNode
        Dim NOOPFilter As New NOOPFilterPredicate
        filterNode.Text = NOOPFilter.ToString
        filterNode.Tag = NOOPFilter

        Return filterNode
    End Function

    Private Function CreateFilterTreeNode(Filter As FilterPredicate) As TreeNode
        Dim filterNode As TreeNode
        If Filter Is Nothing OrElse TypeOf Filter Is NOOPFilterPredicate Then
            filterNode = CreateFilterPlaceHolderTreeNode()
        Else
            filterNode = New TreeNode
            filterNode.Text = Filter.ToString
            filterNode.Tag = Filter
        End If

        Return filterNode
    End Function

    Private Sub FilterContainerTreeView_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles FilterContainerTreeView.AfterSelect
        If e.Node IsNot Nothing AndAlso e.Node.Tag IsNot Nothing Then
            If TypeOf e.Node.Tag Is FilterPredicate Then
                Dim selectedFilter As FilterPredicate = CType(e.Node.Tag, FilterPredicate)

                If e.Node.Parent IsNot Nothing AndAlso TypeOf e.Node.Tag Is FilterPredicate Then
                    Dim ParentFilter As FilterPredicate = CType(e.Node.Parent.Tag, FilterPredicate)
                    _context.SetSelectedFilter(selectedFilter, ParentFilter)
                Else
                    _context.SetSelectedFilter(selectedFilter, Nothing)
                End If
            Else
                _context.SetSelectedFilter(Nothing, Nothing)
            End If
        Else
            _context.SetSelectedFilter(Nothing, Nothing)
        End If
    End Sub

    Private Sub FilterContainerTreeView_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles FilterContainerTreeView.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim p As Point = New Point(e.X, e.Y)

            Dim node As TreeNode = FilterContainerTreeView.GetNodeAt(p)

            If node IsNot Nothing Then
                FilterContainerTreeView.SelectedNode = node

                FilterContainerTreeContextMenuStrip.Show(FilterContainerTreeView, p)
            End If
        End If
    End Sub

    Private Function FindFilterNode(nodes As TreeNodeCollection, filter As FilterPredicate) As TreeNode
        Dim resultNode As TreeNode = Nothing
        For Each node As TreeNode In nodes
            If node.Tag IsNot Nothing AndAlso TypeOf node.Tag Is FilterPredicate AndAlso filter.Equals(CType(node.Tag, FilterPredicate)) Then
                resultNode = node
            Else
                resultNode = FindFilterNode(node.Nodes, filter)
            End If

            If resultNode IsNot Nothing Then
                Exit For
            End If
        Next

        Return resultNode
    End Function

    Private Sub QueryEditor_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        BindCommands()
    End Sub

    Private Sub _context_ContextedChanged(sender As Object, e As System.EventArgs) Handles _context.ContextChanged
        updateTree(FilterContainerTreeView)

        With CType(sender, QueryEditorContext)
            QuerySplitContainer.Panel2.Controls.Clear()

            _currentFilterEditor = Nothing

            If .SelectedFilter IsNot Nothing Then
                _currentFilterEditor = FilterEditorFactory.CreateEditorFor(.SelectedFilter, Me.ResourceManager)
                _currentFilterEditor.ResourceManager = Me.ResourceManager

                Debug.Assert(_currentFilterEditor IsNot Nothing, String.Format("Filter of type {0} is not known by FilterEditorFactory", .SelectedFilter.GetType.ToString))

                If _currentFilterEditor IsNot Nothing Then
                    QuerySplitContainer.Panel2.Controls.Add(_currentFilterEditor)
                    _currentFilterEditor.Filter = .SelectedFilter
                    _currentFilterEditor.Dock = DockStyle.Fill
                End If
            Else
                If .CurrentQuery IsNot Nothing Then
                    Dim details As QueryDetails = New QueryDetails
                    details.Dock = DockStyle.Fill
                    details.ItemQuery = .CurrentQuery
                    QuerySplitContainer.Panel2.Controls.Add(details)
                End If
            End If
        End With
    End Sub

    Private Sub _context_RequestUIUpdate(sender As Object, e As System.EventArgs) Handles _context.RequestUIUpdate
        If _context.CurrentQuery IsNot Nothing Then
            CreateEditorTree(FilterContainerTreeView.Nodes(0), _context.CurrentQuery.Filter, 0)

            If _context.SelectedFilter IsNot Nothing Then
                Dim filterNode As TreeNode = FindFilterNode(FilterContainerTreeView.Nodes, _context.SelectedFilter)

                Debug.Assert(filterNode IsNot Nothing, String.Format("Bug: Unable to find the filter of type {0} in UI Tree!", _context.SelectedFilter.GetType().ToString))
                FilterContainerTreeView.SelectedNode = filterNode
            End If
        End If
    End Sub


End Class