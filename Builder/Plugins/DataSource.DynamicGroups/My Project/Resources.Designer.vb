
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Plugins.DataSource.DynamicGroups.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend ReadOnly Property AndFilterPredicateName() As String
            Get
                Return ResourceManager.GetString("AndFilterPredicateName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AndFilterPredicateNameLocalized() As String
            Get
                Return ResourceManager.GetString("AndFilterPredicateNameLocalized", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemInTestFilterPredicateName() As String
            Get
                Return ResourceManager.GetString("ItemInTestFilterPredicateName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemInTestFilterPredicateNameLocalized() As String
            Get
                Return ResourceManager.GetString("ItemInTestFilterPredicateNameLocalized", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NOOPFilterPredicateName() As String
            Get
                Return ResourceManager.GetString("NOOPFilterPredicateName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NOOPFilterPredicateNameLocalized() As String
            Get
                Return ResourceManager.GetString("NOOPFilterPredicateNameLocalized", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NotFilterPredicateName() As String
            Get
                Return ResourceManager.GetString("NotFilterPredicateName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NotFilterPredicateNameLocalized() As String
            Get
                Return ResourceManager.GetString("NotFilterPredicateNameLocalized", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OrFilterPredicateName() As String
            Get
                Return ResourceManager.GetString("OrFilterPredicateName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OrFilterPredicateNameLocalized() As String
            Get
                Return ResourceManager.GetString("OrFilterPredicateNameLocalized", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveFilterCommandName() As String
            Get
                Return ResourceManager.GetString("RemoveFilterCommandName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveFilterCommandNameLocalized() As String
            Get
                Return ResourceManager.GetString("RemoveFilterCommandNameLocalized", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourcePropertyFilterPredicateName() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyFilterPredicateName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourcePropertyFilterPredicateNameLocalized() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyFilterPredicateNameLocalized", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SetFilterCommandName() As String
            Get
                Return ResourceManager.GetString("SetFilterCommandName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SetFilterCommandNameLocalized() As String
            Get
                Return ResourceManager.GetString("SetFilterCommandNameLocalized", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
