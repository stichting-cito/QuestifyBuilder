Imports System.Runtime.CompilerServices

Namespace Forms

    Module ExtensionMethods

        <Extension>
        Public Sub EnsuresFormUpdateAfterEditing(rootControl As Control)

            If (rootControl Is Nothing) Then Return

            If rootControl.Controls IsNot Nothing Then
                For Each subControl As Control In rootControl.Controls
                    subControl.EnsuresFormUpdateAfterEditing()
                Next
            End If

            Dim containerControl = TryCast(rootControl, ContainerControl)
            If (containerControl IsNot Nothing) Then
                containerControl.ValidateChildren()
            End If
        End Sub

    End Module

End Namespace

