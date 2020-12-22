Imports System.ComponentModel


<Serializable> _
Public Class ParameterList
    Inherits List(Of ParameterBase)
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Public Overloads Sub Add(item As ParameterBase)
        MyBase.Add(item)
        AddHandler item.PropertyChanged, AddressOf Parameters_PropertyChanged
    End Sub

    Public Overloads Sub Clear()
        For Each parameterBase As ParameterBase In Me
            RemoveHandler parameterBase.PropertyChanged, AddressOf Parameters_PropertyChanged
        Next
        MyBase.Clear()
    End Sub


    Public Overloads Sub Insert(index As Integer, parameterBase As ParameterBase)
        AddHandler parameterBase.PropertyChanged, AddressOf Parameters_PropertyChanged
        MyBase.Insert(index, parameterBase)
    End Sub

    Public Overloads Function Remove(parameterBase As ParameterBase) As Boolean
        RemoveHandler parameterBase.PropertyChanged, AddressOf Parameters_PropertyChanged

        Return MyBase.Remove(parameterBase)
    End Function

    Public Overloads Sub RemoveAt(index As Integer)
        RemoveHandler Me(index).PropertyChanged, AddressOf Parameters_PropertyChanged
        MyBase.RemoveAt(index)
    End Sub

    Private Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Overloads Function Contains(name As String) As Boolean

        For i As Integer = 0 To Me.Count - 1
            If Me.Item(i).Name.Equals(name) Then
                return True
            End If
        Next

        Return False
    End Function

    Private Sub Parameters_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        NotifyPropertyChanged(e.PropertyName)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class