Public Interface IPermissionService
    Function TryUserIsPermittedToNamedTask(ByVal access As TestBuilderPermissionAccess, ByVal permissionTarget As TestBuilderPermissionTarget, ByVal targettedNamedTask As TestBuilderPermissionNamedTask, ByVal bankId As Integer, ByVal entityInstanceId As Integer) As Boolean

    Function TryUserIsPermittedTo(ByVal access As TestBuilderPermissionAccess, ByVal permissionTarget As TestBuilderPermissionTarget, ByVal bankId As Integer) As Boolean

    Sub UserIsPermittedToNamedTask(ByVal access As TestBuilderPermissionAccess, ByVal permissionTarget As TestBuilderPermissionTarget, ByVal targettedNamedTask As TestBuilderPermissionNamedTask, ByVal bankId As Integer, ByVal entityInstanceId As Integer)

    Function UserIsPermittedTo(ByVal access As TestBuilderPermissionAccess, ByVal permissionTarget As TestBuilderPermissionTarget, ByVal bankId As Integer) As Boolean

End Interface
