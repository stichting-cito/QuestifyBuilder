Imports System.ComponentModel

Namespace QTI.Helpers.QTI_Base

    Public Class EnumFunctions
        Public Shared Function EnumDescription(ByVal EnumConstant As [Enum]) As String
            Dim fi As Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
            Dim aattr() As DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
            If aattr.Length > 0 Then
                Return aattr(0).Description
            Else
                Return EnumConstant.ToString()
            End If
        End Function
    End Class
End NameSpace