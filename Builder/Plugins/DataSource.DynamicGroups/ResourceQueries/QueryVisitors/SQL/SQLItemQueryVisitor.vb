Public Class SQLItemQueryVisitor
    Inherits ItemQueryVisitor


    Private _SqlStatementStringBuilder As New System.Text.StringBuilder



    Public Sub New(query As ItemQuery)
        MyBase.New(query)
        Me.AddVisitorForType(GetType(AndFilterPredicate), AddressOf AndVisitHandler)
        Me.AddVisitorForType(GetType(OrFilterPredicate), AddressOf OrVisitHandler)
        Me.AddVisitorForType(GetType(NotFilterPredicate), AddressOf NotVisitHandler)
    End Sub



    Public ReadOnly Property SqlStatement() As String
        Get
            Return _SqlStatementStringBuilder.ToString
        End Get
    End Property



    Private Sub AndVisitHandler(filter As AndFilterPredicate)
        Me._SqlStatementStringBuilder.Append(" (")
        Me.Visit(filter.One)
        Me._SqlStatementStringBuilder.Append(" AND ")
        Me.Visit(filter.Other)
        Me._SqlStatementStringBuilder.Append(")")
    End Sub

    Private Sub NotVisitHandler(filter As NotFilterPredicate)
        Me._SqlStatementStringBuilder.Append(" NOT (")
        Me.Visit(filter.Wrapped)
        Me._SqlStatementStringBuilder.Append(")")
    End Sub

    Sub OrVisitHandler(filter As OrFilterPredicate)
        Me._SqlStatementStringBuilder.Append(" (")
        Me.Visit(filter.One)
        Me._SqlStatementStringBuilder.Append(" OR ")
        Me.Visit(filter.Other)
        Me._SqlStatementStringBuilder.Append(")")
    End Sub


End Class