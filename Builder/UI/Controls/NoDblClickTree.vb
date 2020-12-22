Namespace Controls

    Public Class NoDblClickTree
        Inherits TreeView

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = &H203 Then
                m.Result = IntPtr.Zero
            Else
                MyBase.WndProc(m)
            End If
        End Sub
    End Class

End Namespace
