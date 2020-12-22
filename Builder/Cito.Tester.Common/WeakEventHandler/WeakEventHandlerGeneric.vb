
Namespace WeakEventHandler

    Public Class WeakEventHandlerGeneric(Of T As Class, E As EventArgs, H As Class)

        Private Delegate Sub OpenEventHandler(this As T, sender As Object, e As E)
        Private Delegate Sub LocalHandler(sender As Object, e As E)
        Private m_TargetRef As WeakReference
        Private m_OpenHandler As OpenEventHandler
        Private m_Handler As H
        Private m_Unregister As UnregisterDelegate(Of H)

        Public Sub New(eventHandler As H, unregister As UnregisterDelegate(Of H))
            m_TargetRef = New WeakReference(TryCast(eventHandler, [Delegate]).Target)
            m_OpenHandler = DirectCast([Delegate].CreateDelegate(GetType(OpenEventHandler), Nothing, TryCast(eventHandler, [Delegate]).Method), OpenEventHandler)
            m_Handler = CastDelegate(New LocalHandler(AddressOf Invoke))
            m_Unregister = unregister
        End Sub

        Private Sub Invoke(sender As Object, e As E)
            Dim target As T = DirectCast(m_TargetRef.Target, T)

            If target IsNot Nothing Then
                m_OpenHandler.Invoke(target, sender, e)
            ElseIf m_Unregister IsNot Nothing Then
                m_Unregister(m_Handler)
                m_Unregister = Nothing
            End If
        End Sub

        Public Overridable ReadOnly Property Handler() As H
            Get
                Return m_Handler
            End Get
        End Property

        Public Shared Widening Operator CType(weh As WeakEventHandlerGeneric(Of T, E, H)) As H
            Return weh.Handler
        End Operator


        Public Shared Function CastDelegate(source As [Delegate]) As H
            If source Is Nothing Then
                Return Nothing
            End If

            Dim delegates As [Delegate]() = source.GetInvocationList()
            If delegates.Length = 1 Then
                Return TryCast([Delegate].CreateDelegate(GetType(H), delegates(0).Target, delegates(0).Method), H)
            End If

            For i As Integer = 0 To delegates.Length - 1
                delegates(i) = [Delegate].CreateDelegate(GetType(H), delegates(i).Target, delegates(i).Method)
            Next

            Return TryCast([Delegate].Combine(delegates), H)
        End Function

        Public Overridable ReadOnly Property IsAlive As Boolean
            Get
                Return m_TargetRef.IsAlive
            End Get
        End Property


        Public Overrides Function Equals(obj As Object) As Boolean
            If (obj IsNot Nothing AndAlso obj.GetType() Is Me.GetType()) Then
                Dim that As WeakEventHandlerGeneric(Of T, E, H) = DirectCast(obj, WeakEventHandlerGeneric(Of T, E, H))

                If (Me.m_TargetRef.IsAlive AndAlso that.m_TargetRef.IsAlive) Then
                    Dim t1 As Object = Me.m_TargetRef.Target
                    Dim t2 As Object = that.m_TargetRef.Target
                    If (t1 IsNot Nothing) AndAlso (t2 IsNot Nothing) Then
                        If (t1.Equals(t2)) Then
                            Return m_OpenHandler.Equals(that.m_OpenHandler)
                        End If
                    End If
                End If
            End If
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return m_OpenHandler.GetHashCode()
        End Function

    End Class
End Namespace