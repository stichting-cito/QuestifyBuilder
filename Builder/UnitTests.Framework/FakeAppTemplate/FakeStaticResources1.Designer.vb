
Option Strict On
Option Explicit On


Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()> _
    Friend Class FakeStaticResources

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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.UnitTests.Framework.FakeStaticResources", GetType(FakeStaticResources).Assembly)
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

        Friend Shared ReadOnly Property a_tale_of_two_cities() As String
            Get
                Return ResourceManager.GetString("a_tale_of_two_cities", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property DefaultStyleSheet() As String
            Get
                Return ResourceManager.GetString("DefaultStyleSheet", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property transparentPix() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("transparentPix", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property
    End Class
End Namespace
