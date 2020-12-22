Namespace PluginExtensibility.Html.Handlers.Logic
    Public Class CssBorderCollapsed

        Private Shared _collapsedStyleValues() As String = New String() {"collapse", "separate"}
        Private Shared _validBorderCollapseStyles As HashSet(Of String)

        Private _Style As String
        Private _interperterRan As Boolean

        Private _IsCollapsed As Boolean?

        Shared Sub New()
            _validBorderCollapseStyles = New HashSet(Of String)(_collapsedStyleValues,
                    StringComparer.CurrentCultureIgnoreCase)
        End Sub

        Public Sub New()
            _interperterRan = True
        End Sub

        Public Sub New(style As String)
            _interperterRan = False
            _Style = style
        End Sub

        Public Property isCollapsed As Boolean?
            Get
                If (Not _interperterRan) Then Interpert()
                Return _IsCollapsed
            End Get
            Set(value As Boolean?)
                _IsCollapsed = value
            End Set
        End Property

        Private Sub Interpert()
            For Each s In _Style.Split(" "c)
                If (Not String.IsNullOrEmpty(s)) Then
                    If (Not InterpetCollapsedStyle(s)) Then
                        Debug.Assert(False, String.Format("Unable to determine what [{0}] is", s))
                    End If
                End If
            Next
            _interperterRan = True
        End Sub

        Private Function InterpetCollapsedStyle(s As String) As Boolean
            If (_validBorderCollapseStyles.Contains(s)) Then
                _IsCollapsed = String.Equals(s, _collapsedStyleValues(0), StringComparison.CurrentCultureIgnoreCase)
                Return True
            End If
            Return False
        End Function


    End Class

End Namespace