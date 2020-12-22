Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Model.Entities


Public Class EventArgs(Of T)
    Inherits EventArgs

    Public Property Value() As T

    Public Sub New(ByVal t As T)
        Value = t
    End Sub

    Public Sub New()
    End Sub

End Class




Public Class BankSelectedEventArgs
    Inherits EventArgs

    Public ReadOnly Property SelectedBank() As BankDto

    Public Sub New(ByVal selectedBank As BankDto)
        Me.SelectedBank = selectedBank
    End Sub

End Class



Public Class AddBankEventArgs
    Inherits EventArgs

    Public ReadOnly Property ParentBank() As BankDto

    Public Sub New(ByVal parentBank As BankDto)
        Me.ParentBank = parentBank
    End Sub

End Class




Public Class TestComponentSelectedEventArgs
    Inherits EventArgs

    Public ReadOnly Property SelectedComponents() As List(Of AssessmentTestNode)

    Public Sub New(ByVal selectedComponents As List(Of AssessmentTestNode))
        Me.SelectedComponents = selectedComponents
    End Sub

End Class

Public Class CurrentTestComponentSelectedChangingEventArgs
    Inherits EventArgs

    Public Property Cancel() As Boolean

    Public Sub New()
    End Sub

End Class



Public Class TestPackageComponentSelectedEventArgs
    Inherits EventArgs

    Public ReadOnly Property SelectedComponents() As List(Of TestPackageNode)

    Public Sub New(ByVal selectedComponents As List(Of TestPackageNode))
        Me.SelectedComponents = selectedComponents
    End Sub

End Class


Public Class CurrentTestPackageComponentSelectedChangingEventArgs
    Inherits EventArgs

    Public Property Cancel() As Boolean

    Public Sub New()
    End Sub

End Class


Public Class SelectedTestCollectionEventArgs
    Inherits EventArgs

    Private Sub New()
    End Sub

    Public Shared Shadows ReadOnly Empty As New SelectedTestCollectionEventArgs()

    Public Property SelectedItemCollection() As Janus.Windows.GridEX.GridEXSelectedItemCollection

    Public Property Position() As Integer

    Public Property AddToTestSet() As Cito.Tester.ContentModel.TestSet

    Public Property Cancelled() As Boolean
    Public Sub New(ByVal selectedItemCollection As Janus.Windows.GridEX.GridEXSelectedItemCollection, ByVal position As Integer, ByVal addToTestSet As Cito.Tester.ContentModel.TestSet)
        Me.SelectedItemCollection = selectedItemCollection
        Me.Position = position
        Me.AddToTestSet = addToTestSet
    End Sub

End Class
Public Class SelectedTestPackageCollectionEventArgs
    Inherits EventArgs

    Private Sub New()
    End Sub

    Public Shared Shadows ReadOnly Empty As New SelectedTestPackageCollectionEventArgs()

    Public Property SelectedItemCollection() As Janus.Windows.GridEX.GridEXSelectedItemCollection

    Public Property Position() As Integer

    Public Property Cancelled() As Boolean

    Public Sub New(ByVal selectedItemCollection As Janus.Windows.GridEX.GridEXSelectedItemCollection, ByVal position As Integer)
        Me.SelectedItemCollection = selectedItemCollection
        Me.Position = position
    End Sub

End Class



Public Class SelectedItemCollectionEventArgs
    Inherits EventArgs

    Private Sub New()
    End Sub

    Public Shared Shadows ReadOnly Empty As New SelectedItemCollectionEventArgs()

    Public Property SelectedItemCollection() As Janus.Windows.GridEX.GridEXSelectedItemCollection

    Public Property Position() As Integer

    Public Property AddToSection() As Cito.Tester.ContentModel.TestSection2

    Public Property Cancelled() As Boolean

    Public ReadOnly Property AbsolutePosition As Integer

    Public Sub New(ByVal selectedItemCollection As Janus.Windows.GridEX.GridEXSelectedItemCollection, ByVal relativePosition As Integer, absolutePosition As Integer, ByVal addToSection As Cito.Tester.ContentModel.TestSection2)
        Me.SelectedItemCollection = selectedItemCollection
        Position = relativePosition
        Me.AbsolutePosition = absolutePosition
        Me.AddToSection = addToSection
    End Sub

End Class

Public Class SelectedCustomPropertyEventArgs
    Inherits EventArgs

    Public Sub New(ByVal selectedCustomProperty As CustomBankPropertyEntity)
        Me.SelectedCustomProperty = selectedCustomProperty
    End Sub

    Public Property SelectedCustomProperty As CustomBankPropertyEntity
End Class

Public Class RefreshEventArgs
    Inherits EventArgs

    Public Property OnlySelectedEntities() As Boolean

    Public Property SelectedResource() As CommonEntityBase

    Private Sub New()
        SelectedResource = Nothing
    End Sub

    Public Shared Shadows ReadOnly Empty As RefreshEventArgs = New RefreshEventArgs()

    Public Sub New(ByVal selectedResource As CommonEntityBase, onlySelectedEntities As Boolean)
        Me.SelectedResource = selectedResource
        Me.OnlySelectedEntities = onlySelectedEntities
    End Sub

End Class

Public Class SizeEventArgs
    Inherits EventArgs

    Public Property Size() As Size

    Public Sub New(ByVal size As Size)
        Me.Size = size
    End Sub

End Class

Public Class ResourceUsedCheckEventArgs
    Inherits ResourceEventArgs

    Public Property Result() As Boolean

    Public Sub New(ByVal resource As ResourceDto)
        MyBase.New(resource)
        Result = True
    End Sub

End Class



Public Class ValidateResourceEventArgs
    Inherits ResourceEventArgs

    Public Property Valid() As Boolean = True

    Public Sub New(ByVal resource As ResourceDto)
        MyBase.New(resource)
    End Sub

End Class

Public Class ValidateNewCodeNameEventArgs
    Inherits EventArgs

    Public ReadOnly Property NewCodeName() As String

    Public Property Valid() As Boolean

    Public Sub New(ByVal newCodeName As String)
        Valid = False
        Me.NewCodeName = newCodeName
    End Sub

End Class



Public Class BankRoleViewerSelectionChangedEventArgs
    Inherits EventArgs

    Public ReadOnly Property SelectedRowType() As BankRoleGridRowEntityType

    Public Sub New(ByVal rowType As BankRoleGridRowEntityType)
        SelectedRowType = rowType
    End Sub

End Class


Public Class RowFormattingEventArgs
    Inherits EventArgs
    Public Property Disabled() As Boolean

    Public Property Resource() As ResourceDto

    Public Property Tag As Object

    Public Sub New()
    End Sub

End Class



Public Class FastSearchInitiatedEventArgs
    Inherits EventArgs

    Public ReadOnly Property SearchInBankProperties() As Boolean

    Public ReadOnly Property SearchInItemText() As Boolean

    Public ReadOnly Property SearchKeywords() As String

    Public ReadOnly Property TestContextResourceId() As Guid

    Public ReadOnly Property MaxRecordsToReturn() As Integer


    Public ReadOnly Property IncludeSubbanks() As Boolean
    Public Sub New(ByVal searchInBankProperties As Boolean, ByVal searchInItemText As Boolean, ByVal searchKeywords As String, ByVal testContextResourceId As Guid, ByVal maxRecords As Integer, includeSubbanks As Boolean)
        Me.SearchInBankProperties = searchInBankProperties
        Me.SearchInItemText = searchInItemText
        Me.SearchKeywords = searchKeywords
        Me.TestContextResourceId = testContextResourceId
        MaxRecordsToReturn = maxRecords
        Me.IncludeSubbanks = includeSubbanks
    End Sub

End Class

