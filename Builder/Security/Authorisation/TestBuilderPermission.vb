Imports System.Xml.Serialization
Imports System.Diagnostics.CodeAnalysis

<SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix"),
    XmlRoot("TestBuilderPermission", [Namespace]:="http://Cito.TestBuilder.Security/xml/serialization", IsNullable:=True),
    Serializable()>
Public Class TestBuilderPermission

    Private _requiredPermissions As New TestBuilderPermissionEntryCollection


    Public Sub New()

    End Sub

    Public Sub New(ByVal permissionTarget As TestBuilderPermissionTarget, ByVal permissionAccess As TestBuilderPermissionAccess)
        Me.New()
        AddPermissionEntry(New TestBuilderPermissionEntry(permissionTarget, TestBuilderPermissionNamedTask.None, permissionAccess))
    End Sub

    Public Sub New(ByVal permissionTarget As TestBuilderPermissionTarget, ByVal permissionNamedTask As TestBuilderPermissionNamedTask, ByVal permissionAccess As TestBuilderPermissionAccess)
        AddPermissionEntry(New TestBuilderPermissionEntry(permissionTarget, permissionNamedTask, permissionAccess))
    End Sub



    Public ReadOnly Property PermissionEntries() As TestBuilderPermissionEntryCollection
        Get
            Return _requiredPermissions
        End Get
    End Property

    <Xml.Serialization.XmlIgnore()>
    Public ReadOnly Property PermissionEntriesArray() As ArrayList
        Get
            Dim result As New ArrayList(_requiredPermissions.ToArray())
            Return result
        End Get
    End Property


    Public Sub AddPermissionEntry(ByVal entry As TestBuilderPermissionEntry)
        If entry Is Nothing Then
            Throw New ArgumentNullException("entry")
        End If

        SyncLock _requiredPermissions
            Dim currentEntry As TestBuilderPermissionEntry = _requiredPermissions.GetEntryByKey(entry.Key)
            If currentEntry IsNot Nothing Then

                Dim newEntry As New TestBuilderPermissionEntry(currentEntry.PermissionTarget, currentEntry.TargettedNamedTask, currentEntry.PermissionAccess Or entry.PermissionAccess)
                _requiredPermissions(_requiredPermissions.IndexOf(currentEntry)) = newEntry
            Else
                _requiredPermissions.Add(entry)
            End If
        End SyncLock
    End Sub

    Public Sub Demand(ByVal bankId As Integer, alternativeBankIdsWithAncestors As Integer(), alternativeBankIdsWithoutAncestors As Integer(), grantedPermissionsDictionary As Dictionary(Of Integer, TestBuilderPermission))
        If Not TryDemand(bankId, alternativeBankIdsWithAncestors, alternativeBankIdsWithoutAncestors, grantedPermissionsDictionary) Then
            Throw New SecurityException(My.Resources.TestBuilderPermission_Exception_InsufficientPermissions)
        End If
    End Sub

    Public Function TryDemand(ByVal bankId As Integer, alternativeBankIdsWithAncestors As Integer(), alternativeBankIdsWithoutAncestors As Integer(), grantedPermissionsDictionary As Dictionary(Of Integer, TestBuilderPermission)) As Boolean
        Dim requiredPermissionsRemaining As TestBuilderPermission
        Dim useBankId As Integer
        Dim alternativeBankIndex As Integer = 0
        Dim alternativeBankIDs As Integer() = Nothing
        Dim tryTheAncestorDirection As Boolean = True
        Dim tryChildDirection As Boolean = False

        useBankId = bankId

        Do
            Dim grantedPermissions As TestBuilderPermission = Nothing
            If grantedPermissionsDictionary.ContainsKey(useBankId) Then
                grantedPermissions = grantedPermissionsDictionary(useBankId)
            End If
            requiredPermissionsRemaining = CheckPermissions(Me, useBankId, grantedPermissions)
            If (requiredPermissionsRemaining Is Nothing) OrElse useBankId <= 0 Then Continue Do

            If tryTheAncestorDirection Then
                tryTheAncestorDirection = False
                tryChildDirection = True

                If alternativeBankIDs Is Nothing Then
                    alternativeBankIDs = alternativeBankIdsWithAncestors
                    alternativeBankIndex = 0
                End If
            End If

            If alternativeBankIDs Is Nothing OrElse alternativeBankIndex = alternativeBankIDs.Length Then
                If tryChildDirection Then
                    tryChildDirection = False

                    If requiredPermissionsRemaining._requiredPermissions.Count = 1 Then
                        Dim requiredAccessMask As Integer

                        For Each permissionEntry As TestBuilderPermissionEntry In requiredPermissionsRemaining._requiredPermissions
                            requiredAccessMask = permissionEntry.PermissionAccess
                            Exit For
                        Next

                        If (requiredAccessMask And TestBuilderPermissionAccess.Refer) = requiredAccessMask OrElse
                           requiredAccessMask = TestBuilderPermissionAccess.AnyTask Then
                            alternativeBankIDs = alternativeBankIdsWithoutAncestors
                            alternativeBankIndex = 0
                        End If
                    End If
                End If
            End If

            If alternativeBankIDs IsNot Nothing AndAlso alternativeBankIndex < alternativeBankIDs.Length Then
                useBankId = alternativeBankIDs(alternativeBankIndex)
                alternativeBankIndex += 1
            Else
                useBankId = 0
            End If


        Loop While requiredPermissionsRemaining IsNot Nothing AndAlso useBankId > 0

        Return (requiredPermissionsRemaining Is Nothing)
    End Function


    Private Function CheckPermissions(requiredPermission As TestBuilderPermission, bankId As Integer, grantedPermissions As TestBuilderPermission) As TestBuilderPermission
        Dim requiredPermissionsRemaining As TestBuilderPermission = Nothing

        If (requiredPermission._requiredPermissions Is Nothing) OrElse requiredPermission._requiredPermissions.Count <= 0 Then Return requiredPermissionsRemaining

        For Each requiredPermissionEntry As TestBuilderPermissionEntry In requiredPermission._requiredPermissions
            Dim remainingRequiredPermissionsMask As Integer = requiredPermissionEntry.PermissionAccess

            If grantedPermissions IsNot Nothing AndAlso grantedPermissions._requiredPermissions IsNot Nothing Then
                remainingRequiredPermissionsMask = DeterminePermissions(requiredPermissionEntry, grantedPermissions, bankId, remainingRequiredPermissionsMask)
            End If

            If remainingRequiredPermissionsMask > 0 Then
                If requiredPermissionsRemaining Is Nothing Then
                    requiredPermissionsRemaining = New TestBuilderPermission()
                End If

                requiredPermissionsRemaining.AddPermissionEntry(New TestBuilderPermissionEntry(requiredPermissionEntry.PermissionTarget, requiredPermissionEntry.TargettedNamedTask, remainingRequiredPermissionsMask, requiredPermissionEntry.WhenOwnerCondition))
            End If
        Next

        Return requiredPermissionsRemaining
    End Function

    Private Function DeterminePermissions(
            requiredPermissionEntry As TestBuilderPermissionEntry,
            grantedPermissions As TestBuilderPermission,
            bankId As Integer,
            remainingRequiredPermissionsMask As Integer) As Integer

        If requiredPermissionEntry.PermissionTarget = TestBuilderPermissionTarget.BankEntity AndAlso bankId > 0 Then
            If grantedPermissions._requiredPermissions.Count > 0 Then
                grantedPermissions.AddPermissionEntry(New TestBuilderPermissionEntry(TestBuilderPermissionTarget.BankEntity, TestBuilderPermissionNamedTask.None, TestBuilderPermissionAccess.Refer))
            End If
        End If

        For Each grantedPermissionEntry As TestBuilderPermissionEntry In grantedPermissions._requiredPermissions
            If (grantedPermissionEntry.PermissionTarget = requiredPermissionEntry.PermissionTarget AndAlso
(grantedPermissionEntry.PermissionTarget <> TestBuilderPermissionTarget.NamedTask OrElse grantedPermissionEntry.TargettedNamedTask = requiredPermissionEntry.TargettedNamedTask)
) OrElse
grantedPermissionEntry.PermissionTarget = TestBuilderPermissionTarget.AllTargets OrElse
requiredPermissionEntry.PermissionTarget = TestBuilderPermissionTarget.Any Then
                If remainingRequiredPermissionsMask = TestBuilderPermissionAccess.AnyTask Then
                    remainingRequiredPermissionsMask = 0
                    Exit For
                Else
                    If remainingRequiredPermissionsMask And grantedPermissionEntry.PermissionAccess Then
                        remainingRequiredPermissionsMask = remainingRequiredPermissionsMask Xor (remainingRequiredPermissionsMask And grantedPermissionEntry.PermissionAccess)

                        If remainingRequiredPermissionsMask = 0 Then
                            Exit For
                        End If
                    End If
                End If
            End If
        Next
        Return remainingRequiredPermissionsMask
    End Function


End Class


Public Enum TestBuilderPermissionTarget
    None = 0
    Any = 1
    AllTargets = 2
    NamedTask = 3
    BankEntity = 4
    TestEntity = 5
    TestTemplateEntity = 6
    ItemEntity = 7
    ItemLayoutTemplateEntity = 8
    MediaEntity = 9
    ControlTemplateEntity = 10
    UserEntity = 12
    UserApplicationRoleEntity = 13
    UserBankRoleEntity = 14
    RoleEntity = 15
    RolePermissionEntity = 16
    CustomBankPropertyEntity = 17
    AspectEntity = 18
    DataSourceEntity = 19
    DataSourceTemplateEntity = 20
    TestPackageEntity = 21
End Enum



Public Enum TestBuilderPermissionNamedTask
    None = 0

    EditXhtmlParameterSource = 1

    SwitchItemLayoutTemplate = 2

    ClearBank = 3

    ChangeItemCode = 4

    TestDesignMinimal = 5

    TestDesignBasic = 6

    TestDesignAdvanced = 7

    ChangeTestCode = 8

    RestrictedPackagePublication = 9

    ChangeDeliveryCode = 10

    MoveResourcesOrCustomBankProperties = 11

    ImportItemsWithWordTemplate = 12

    ImportItemsWithAccessTemplate = 13

    ImportItemsWithExcelTemplate = 14

    AllowPublicationToServer = 15

    ChangeWorkflowMetadataWhenProhibittedByState = 16
End Enum



<SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags")>
<Flags()>
Public Enum TestBuilderPermissionAccess
    None = 0

    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> DALCreate = 1
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> DALRead = 2
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> DALUpdate = 4
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> DALDelete = 8

    Execute = 16

    AddNew = 32 Or DALCreate
    Refer = 64 Or DALRead
    List = 128 Or Refer Or DALRead
    View = 256 Or DALRead
    Reserved512 = 512
    Edit = 1024 Or View Or DALUpdate
    Delete = 2048 Or DALDelete
    Publish = 4069 Or Refer
    Export = 8192 Or Refer
    Import = 16384 Or AddNew Or DALUpdate
    ViewSource = 32768 Or Refer
    ViewProperties = 65536 Or Refer
    EditProperties = 131072 Or ViewProperties Or Edit
    ExportRawData = 262144 Or Refer
    ImportRawData = 524288 Or AddNew Or DALUpdate
    AddDependency = 1048576 Or Edit
    DeleteDependency = 2097152 Or Edit


    FullAccess = Execute Or AddNew Or Refer Or List Or View Or Edit Or Delete Or Publish Or Export Or Import Or ViewSource Or ViewProperties Or
             EditProperties Or ExportRawData Or ImportRawData Or AddDependency Or DeleteDependency

    FullAccess2 = DALCreate Or DALRead Or DALUpdate Or DALDelete Or AddNew Or Refer Or List Or View Or Edit Or Delete Or ViewProperties Or EditProperties
    FullAccess23 = DALCreate Or DALRead Or DALUpdate Or DALDelete Or AddNew Or Refer Or List Or View Or Edit Or Delete

    AnyTask = 4194304
End Enum



Public Enum TestDesignPermission
    Minimal = 0
    Basic = 1
    Advanced = 2
End Enum

