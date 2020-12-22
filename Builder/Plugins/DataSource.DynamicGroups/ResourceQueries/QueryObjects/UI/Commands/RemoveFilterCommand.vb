Imports System.Windows.Forms
Imports Questify.Builder.UI.Commanding

Public Class RemoveFilterCommand
    Inherits CommandBase

    Public Sub New(filterTree As TreeView, name As String, nameLocalized As String)
        If filterTree Is Nothing Then
            Throw New ArgumentNullException("filterTree")
        End If

        Me.Name = name
        Me.NameLocalized = nameLocalized
    End Sub

    Public Overrides ReadOnly Property Image() As System.Drawing.Image
        Get
            Return Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String

    Public Overrides ReadOnly Property NameLocalized As String

    Public Overrides Sub Execute(parameter As Object)
        Trace.Assert(TypeOf parameter Is QueryEditorContext)
        Dim context As QueryEditorContext = TryCast(parameter, QueryEditorContext)

        With context
            If .ParentContainerFilter IsNot Nothing Then
                .ParentContainerFilter.RemoveFilter(context.SelectedFilter)

                context.SelectedFilter = .ParentContainerFilter
            Else
                context.CurrentQuery.Filter = Nothing
                context.SelectedFilter = Nothing
            End If

            .DoRequestUIUpdate(Nothing, Nothing)
        End With
    End Sub

    Protected Overrides Function GetCanExecuteState(parameter As Object) As Boolean
        Trace.Assert(TypeOf parameter Is QueryEditorContext)
        Dim context As QueryEditorContext = TryCast(parameter, QueryEditorContext)

        If context IsNot Nothing Then
            If TypeOf context.SelectedFilter Is NOOPFilterPredicate Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

End Class