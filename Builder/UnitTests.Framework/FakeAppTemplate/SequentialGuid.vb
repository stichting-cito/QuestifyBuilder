Namespace FakeAppTemplate
    Friend Class SequentialGuid

        Private _CurrentGuid As Guid

        Public Sub New()
            MyBase.New()
            _CurrentGuid = Guid.Empty
        End Sub

        Public Sub New(ByVal previousGuid As Guid)
            MyBase.New()
            _CurrentGuid = previousGuid
        End Sub

        Public ReadOnly Property CurrentGuid As Guid
            Get
                Return _CurrentGuid
            End Get
        End Property

        Public Sub Inc()
            Dim bytes() As Byte = _CurrentGuid.ToByteArray
            Dim mapIndex As Integer = 0
            Do While (mapIndex < 16)
                Dim bytesIndex As Integer = SqlOrderMap(mapIndex)
                bytes(bytesIndex) = CType((bytes(bytesIndex) + 1) Mod 256, Byte)
                If (bytes(bytesIndex) <> 0) Then
                    Exit Do
                End If
                mapIndex = (mapIndex + 1)
            Loop
            _CurrentGuid = New Guid(bytes)
        End Sub

        Private Shared _SqlOrderMap() As Integer = Nothing

        Private Shared ReadOnly Property SqlOrderMap As Integer()
            Get
                If (_SqlOrderMap Is Nothing) Then
                    _SqlOrderMap = New Integer() {3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10}
                End If
                Return _SqlOrderMap
            End Get
        End Property
    End Class
End NameSpace