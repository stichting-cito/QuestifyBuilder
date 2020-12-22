
Option Strict On
Option Explicit On


Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()> _
    Friend Class ItemProcessing

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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Logic.ItemProcessing", GetType(ItemProcessing).Assembly)
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

        Friend Shared ReadOnly Property ErrorsFoundInCollectionparameter() As String
            Get
                Return ResourceManager.GetString("ErrorsFoundInCollectionparameter", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property InvisibleParameterHasAChangedValue() As String
            Get
                Return ResourceManager.GetString("InvisibleParameterHasAChangedValue", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property ValidateParametersCollectionParameterWillShrink() As String
            Get
                Return ResourceManager.GetString("ValidateParametersCollectionParameterWillShrink", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property ValidateParametersCollectionWillBeDeleted() As String
            Get
                Return ResourceManager.GetString("ValidateParametersCollectionWillBeDeleted", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property ValidateParametersParameterHasADifferentType() As String
            Get
                Return ResourceManager.GetString("ValidateParametersParameterHasADifferentType", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property ValidateParametersParameterWillBeDeleted() As String
            Get
                Return ResourceManager.GetString("ValidateParametersParameterWillBeDeleted", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property WarningsFoundInCollectionparameter() As String
            Get
                Return ResourceManager.GetString("WarningsFoundInCollectionparameter", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
