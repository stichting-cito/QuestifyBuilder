
Imports Cito.Tester.ContentModel

Public Class KeyFindingEditorControlBase


    Private _keyfinding As KeyFinding



    Public Property Caption() As String
        Get
            Return CaptionLabel.Text
        End Get
        Set(value As String)
            CaptionLabel.Text = value
        End Set
    End Property

    Public Property Keyfinding() As KeyFinding
        Get
            Return _keyfinding
        End Get
        Set(value As KeyFinding)
            _keyfinding = value
            KeyFindingBindingSource.DataSource = _keyfinding

            If CanInitEditor() Then
                Me.InitEditor()
            End If
        End Set
    End Property



    Protected Overrides Function CanInitEditor() As Boolean
        Return MyBase.CanInitEditor AndAlso Me.Keyfinding IsNot Nothing
    End Function

    Public Overridable Function GetKeyValuesAsString() As String
        Return Me.Keyfinding.ToString
    End Function

    Public Overridable Function GetAlternativesCount() As Nullable(Of Integer)
        Throw New NotImplementedException("This function is not implemented!")
    End Function

    Public Overridable Function GetResponseCount() As Integer
        Throw New NotImplementedException("This function is not implemented!")
    End Function

    Public Overrides Function GetRawScore() As Integer
        Return Me.Keyfinding.MaxFindingScore
    End Function



End Class
