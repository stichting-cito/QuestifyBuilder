Imports Cito.Tester.ContentModel

Public Class AspectGeneralProperties


    Private _aspect As Aspect



    Public Overridable Property Aspect() As Aspect
        Get
            Return _aspect
        End Get
        Set(ByVal value As Aspect)
            If value IsNot Nothing Then
                Dim refresh As Boolean = (_aspect IsNot Nothing)

                _aspect = value
                Me.AspectBindingSource.DataSource = _aspect

                If refresh Then
                    Me.AspectBindingSource.ResetCurrentItem()
                End If
            End If
        End Set
    End Property



    Private Sub ChangeCodeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OnChangeItemCode(New EventArgs())
    End Sub



    Protected Overridable Sub OnChangeItemCode(ByVal e As EventArgs)
        RaiseEvent ChangeItemCode(Me, e)
    End Sub



    Public Event ChangeItemCode As EventHandler(Of EventArgs)


End Class