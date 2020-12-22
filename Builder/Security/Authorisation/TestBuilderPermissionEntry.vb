Imports System.Xml.Serialization

<XmlRoot("TestBuilderPermissionEntry", [Namespace]:="http://Cito.TestBuilder.Security/xml/serialization", IsNullable:=True), _
 Serializable()> _
Public Class TestBuilderPermissionEntry

    Private _PermissionTarget As TestBuilderPermissionTarget
    Private _TargettedNamedTask As TestBuilderPermissionNamedTask
    Private _PermissionAccess As TestBuilderPermissionAccess
    Private _WhenOwnerCondition As Boolean


    Public Sub New()
    End Sub

    Public Sub New(ByVal permissionTarget As TestBuilderPermissionTarget, ByVal targettedNamedTask As TestBuilderPermissionNamedTask, ByVal permissionAccess As TestBuilderPermissionAccess, ByVal whenOwnerCondition As Boolean)
        Me._PermissionTarget = permissionTarget
        Me._TargettedNamedTask = targettedNamedTask
        Me._PermissionAccess = permissionAccess
        Me._WhenOwnerCondition = whenOwnerCondition
    End Sub

    Public Sub New(ByVal permissionTarget As TestBuilderPermissionTarget, ByVal targettedNamedTask As TestBuilderPermissionNamedTask, ByVal permissionAccess As TestBuilderPermissionAccess)
        Me._PermissionTarget = permissionTarget
        Me._TargettedNamedTask = targettedNamedTask
        Me._PermissionAccess = permissionAccess
    End Sub



    Public ReadOnly Property Key() As String
        Get
            Return String.Format("{0};{1}", _PermissionTarget, _TargettedNamedTask)
        End Get
    End Property

    Public Property PermissionTarget() As TestBuilderPermissionTarget
        Get
            Return Me._PermissionTarget
        End Get
        Set(ByVal value As TestBuilderPermissionTarget)
            _PermissionTarget = value
        End Set
    End Property

    Public Property TargettedNamedTask() As TestBuilderPermissionNamedTask
        Get
            Return Me._TargettedNamedTask
        End Get
        Set(ByVal value As TestBuilderPermissionNamedTask)
            _TargettedNamedTask = value
        End Set
    End Property

    Public Property PermissionAccess() As TestBuilderPermissionAccess
        Get
            Return Me._PermissionAccess
        End Get
        Set(ByVal value As TestBuilderPermissionAccess)
            _PermissionAccess = value
        End Set
    End Property

    Public Property WhenOwnerCondition() As Boolean
        Get
            Return _WhenOwnerCondition
        End Get
        Set(ByVal value As Boolean)
            _WhenOwnerCondition = value
        End Set
    End Property


End Class
