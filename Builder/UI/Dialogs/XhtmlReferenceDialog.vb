Imports Cito.Tester.Common

Public Class XhtmlReferenceDialog


    Dim _selectedReference As XhtmlReference



    Public Sub New(ByVal references As XhtmlReferenceList)
        InitializeComponent()

        XhtmlReferenceBindingSource.DataSource = references
    End Sub



    Public Property SelectedReference() As XhtmlReference
        Get
            Return _selectedReference
        End Get
        Private Set(ByVal value As XhtmlReference)
            _selectedReference = value
        End Set
    End Property

    Public Property [ReadOnly]() As Boolean
        Get
            Return Not ReferenceGrid.Enabled
        End Get
        Set(ByVal value As Boolean)
            ReferenceGrid.Enabled = Not value
        End Set
    End Property



    Protected Overrides Function OnOk() As Boolean
        If Me.ReadOnly Then
            Return True
        ElseIf ReferenceGrid.SelectedItems.Count > 0 Then
            If TypeOf ReferenceGrid.GetRow().DataRow Is XhtmlReference Then
                Me.SelectedReference = DirectCast(ReferenceGrid.GetRow().DataRow, XhtmlReference)
                Return True
            End If
        End If
        Return False
    End Function


End Class