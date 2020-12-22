Imports System.ComponentModel

Public Class ConceptScoringOptions
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        InitializeComponent()
    End Sub
End Class
