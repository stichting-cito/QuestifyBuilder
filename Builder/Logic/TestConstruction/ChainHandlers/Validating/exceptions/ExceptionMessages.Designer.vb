
Option Strict On
Option Explicit On


Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()> _
    Friend Class ExceptionMessages

        Private Shared resourceMan As Global.System.Resources.ResourceManager

        Private Shared resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
        Friend Sub New()
            MyBase.New
        End Sub

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Logic.ExceptionMessages", GetType(ExceptionMessages).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend Shared ReadOnly Property DataSourceUsedSomewhereElseMsg() As String
            Get
                Return ResourceManager.GetString("DataSourceUsedSomewhereElseMsg", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property SuggesDataSourceBindingMsg() As String
            Get
                Return ResourceManager.GetString("SuggesDataSourceBindingMsg", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property SuggesDataSourceBindingNewSectionMsg() As String
            Get
                Return ResourceManager.GetString("SuggesDataSourceBindingNewSectionMsg", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
