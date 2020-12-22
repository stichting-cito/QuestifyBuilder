Imports Questify.Builder.UI.Commanding

Public Class SetFilterCommand
    Inherits CommandBase

    Private ReadOnly _filterType As Type

    Public Sub New(filterType As Type, name As String, nameLocalized As String)
        If filterType Is Nothing Then
            Throw New ArgumentNullException(nameof(filterType))
        End If

        If String.IsNullOrEmpty(name) Then
            Throw New ArgumentNullException(nameof(name))
        End If

        If String.IsNullOrEmpty(nameLocalized) Then
            Throw New ArgumentNullException(nameof(name))
        End If

        Me._filterType = filterType
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
            Dim filterToAdd As FilterPredicate = Activator.CreateInstance(Me._filterType)

            If .ParentContainerFilter IsNot Nothing Then
                .ParentContainerFilter.AddFilter(filterToAdd)
            Else
                context.CurrentQuery.Filter = filterToAdd
            End If

            .SelectedFilter = filterToAdd
            .DoRequestUIUpdate(Nothing, Nothing)
        End With
    End Sub

    Protected Overrides Function GetCanExecuteState(parameter As Object) As Boolean
        Trace.Assert(TypeOf parameter Is QueryEditorContext)
        Dim context As QueryEditorContext = TryCast(parameter, QueryEditorContext)

        If context IsNot Nothing Then
            If context.SelectedFilter Is Nothing OrElse TypeOf context.SelectedFilter Is NOOPFilterPredicate Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

End Class