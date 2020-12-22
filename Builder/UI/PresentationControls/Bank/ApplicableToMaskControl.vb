Imports System.ComponentModel

<System.ComponentModel.DefaultBindingProperty("Mask")> _
Public Class ApplicableToMaskControl
    Implements IBindableComponent


    Private _mask As Long
    Private WithEvents _labels As New ApplicableToMaskLabelCollection



    <Bindable(BindableSupport.Yes)> _
    Public Property Mask() As Long
        Get
            Return _mask
        End Get
        Set(ByVal value As Long)
            _mask = value
            CreateControls(Me.Mask)
        End Set
    End Property

    Public ReadOnly Property Labels() As ApplicableToMaskLabelCollection
        Get
            Return _labels
        End Get
    End Property



    Private Sub CheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim involvedCheckBox As CheckBox = DirectCast(sender, CheckBox)
        Dim maskBit As Integer = Integer.Parse(involvedCheckBox.Tag.ToString)

        If involvedCheckBox.Checked Then
            Me.Mask = (Me.Mask Or maskBit)
        Else
            Me.Mask = (Me.Mask And Not maskBit)
        End If

    End Sub

    Private Sub _labels_CollectionChanged() Handles _labels.CollectionChanged
        CreateControls(Me.Mask)
    End Sub




    Private Sub CreateControls(ByVal mask As Long)
        Dim i As Long = 0
        FlowLayoutPanel1.Controls.Clear()

        For Each item As ApplicableToMaskItem In Me.Labels
            Dim newCheckBox As New CheckBox
            Dim maskBit As Integer = item.Bitmask

            With newCheckBox
                .AutoSize = True
                .Name = String.Format("bitMaskCheckBox{0}", i)
                .Text = item.Caption
                .Tag = item.Bitmask
                .Enabled = item.Enabled

                If (mask And maskBit) = maskBit Then
                    .Checked = True
                Else
                    .Checked = False
                End If
            End With

            AddHandler newCheckBox.CheckedChanged, AddressOf CheckBox_CheckedChanged

            FlowLayoutPanel1.Controls.Add(newCheckBox)
            i += 1
        Next
    End Sub


    Private Sub ApplicableToMaskControl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateControls(Me.Mask)
    End Sub
End Class


Public Class ApplicableToMaskLabelCollection
    Implements IList(Of ApplicableToMaskItem)


    Private ReadOnly _internalList As New List(Of ApplicableToMaskItem)



    Public Event CollectionChanged()



    Public Sub Add(ByVal item As ApplicableToMaskItem) Implements ICollection(Of ApplicableToMaskItem).Add
        _internalList.Add(item)
        RaiseEvent CollectionChanged()
    End Sub

    Public Sub Add(ByVal caption As String, ByVal bitmask As Integer)
        Add(caption, bitmask, True)
    End Sub

    Public Sub Add(ByVal caption As String, ByVal bitmask As Integer, enabled As Boolean)
        Dim item As ApplicableToMaskItem = New ApplicableToMaskItem()
        item.Caption = caption
        item.Bitmask = bitmask
        item.Enabled = enabled

        Add(item)
    End Sub

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of ApplicableToMaskItem).Clear
        _internalList.Clear()
        RaiseEvent CollectionChanged()
    End Sub

    Public Function Contains(ByVal item As ApplicableToMaskItem) As Boolean Implements System.Collections.Generic.ICollection(Of ApplicableToMaskItem).Contains
        Return _internalList.Contains(item)
    End Function

    Public Sub CopyTo(ByVal array() As ApplicableToMaskItem, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of ApplicableToMaskItem).CopyTo
        _internalList.CopyTo(array, arrayIndex)
        RaiseEvent CollectionChanged()
    End Sub

    Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of ApplicableToMaskItem).Count
        Get
            Return _internalList.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of ApplicableToMaskItem).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Function Remove(ByVal item As ApplicableToMaskItem) As Boolean Implements System.Collections.Generic.ICollection(Of ApplicableToMaskItem).Remove
        _internalList.Remove(item)
        RaiseEvent CollectionChanged()
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of ApplicableToMaskItem) Implements System.Collections.Generic.IEnumerable(Of ApplicableToMaskItem).GetEnumerator
        Return _internalList.GetEnumerator
    End Function

    Public Function IndexOf(ByVal item As ApplicableToMaskItem) As Integer Implements System.Collections.Generic.IList(Of ApplicableToMaskItem).IndexOf
        Return _internalList.IndexOf(item)
    End Function

    Public Sub Insert(ByVal index As Integer, ByVal item As ApplicableToMaskItem) Implements System.Collections.Generic.IList(Of ApplicableToMaskItem).Insert
        _internalList.Insert(index, item)
        RaiseEvent CollectionChanged()
    End Sub

    Default Public Property Item(ByVal index As Integer) As ApplicableToMaskItem Implements System.Collections.Generic.IList(Of ApplicableToMaskItem).Item
        Get
            Return _internalList.Item(index)
        End Get
        Set(ByVal value As ApplicableToMaskItem)
            _internalList.Item(index) = value
        End Set
    End Property

    Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of ApplicableToMaskItem).RemoveAt
        _internalList.RemoveAt(index)
        RaiseEvent CollectionChanged()
    End Sub

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _internalList.GetEnumerator
    End Function


End Class


