
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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Security.Resources", GetType(Resources).Assembly)
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

        Friend ReadOnly Property CustomPrincipal_Exception_UnableClearRights() As String
            Get
                Return ResourceManager.GetString("CustomPrincipal_Exception_UnableClearRights", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightPermission_Exception_DemandRefused() As String
            Get
                Return ResourceManager.GetString("PrincipalRightPermission_Exception_DemandRefused", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightPermission_Exception_InvalidPermissionState() As String
            Get
                Return ResourceManager.GetString("PrincipalRightPermission_Exception_InvalidPermissionState", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightPermission_Exception_InvalidTag() As String
            Get
                Return ResourceManager.GetString("PrincipalRightPermission_Exception_InvalidTag", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightPermission_Exception_NoIdentityTag() As String
            Get
                Return ResourceManager.GetString("PrincipalRightPermission_Exception_NoIdentityTag", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightPermission_Exception_NoPrinipalFound() As String
            Get
                Return ResourceManager.GetString("PrincipalRightPermission_Exception_NoPrinipalFound", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightPermission_Exception_ParseVersion() As String
            Get
                Return ResourceManager.GetString("PrincipalRightPermission_Exception_ParseVersion", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightPermission_Exception_UnknownVersion() As String
            Get
                Return ResourceManager.GetString("PrincipalRightPermission_Exception_UnknownVersion", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightSecurityAttribute_Exception_EmptyRight() As String
            Get
                Return ResourceManager.GetString("PrincipalRightSecurityAttribute_Exception_EmptyRight", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrincipalRightSecurityAttribute_Exception_UnrestrictedPermNotAllowed() As String
            Get
                Return ResourceManager.GetString("PrincipalRightSecurityAttribute_Exception_UnrestrictedPermNotAllowed", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SecurityFactory_Exception_AuthProviderNotConfigured() As String
            Get
                Return ResourceManager.GetString("SecurityFactory_Exception_AuthProviderNotConfigured", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SecurityFactory_Exception_CantCreateProvider() As String
            Get
                Return ResourceManager.GetString("SecurityFactory_Exception_CantCreateProvider", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SecurityFactory_Exception_ServiceAlreadyinstantiated() As String
            Get
                Return ResourceManager.GetString("SecurityFactory_Exception_ServiceAlreadyinstantiated", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestBuilderDefaultAuthProvider_Exception_GetLoginCredentialsIsNotHandled() As String
            Get
                Return ResourceManager.GetString("TestBuilderDefaultAuthProvider_Exception_GetLoginCredentialsIsNotHandled", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestBuilderDefAuthProvider_Authenticate_Exception_ClientIsEmpty() As String
            Get
                Return ResourceManager.GetString("TestBuilderDefAuthProvider_Authenticate_Exception_ClientIsEmpty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestBuilderPermission_Exception_InsufficientPermissions() As String
            Get
                Return ResourceManager.GetString("TestBuilderPermission_Exception_InsufficientPermissions", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestBuilderWindowsAuthenticationProvider_OnGetCreditials_Exception_CurrentWindowsUserNotAuthenticated() As String
            Get
                Return ResourceManager.GetString("TestBuilderWindowsAuthenticationProvider_OnGetCreditials_Exception_CurrentWindows" & _
                        "UserNotAuthenticated", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
