Imports System.Configuration
Imports System.Diagnostics.CodeAnalysis

Public Class PluginHandlerElement
    Inherits ConfigurationElement

    <ConfigurationProperty("type", IsKey:=True, IsRequired:=True)>
    Public Property Type As String
        Get
            Return DirectCast(MyBase.Item("type"), String)
        End Get
        Set
            MyBase.Item("type") = Value
        End Set
    End Property

    <SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.String.Format(System.String,System.Object)")>
    Public Overrides Function ToString() As String
        Dim output As String = $"HandlerElement : Type = '{Type}'"
        Return output
    End Function

End Class

