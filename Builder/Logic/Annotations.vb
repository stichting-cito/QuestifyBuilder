Namespace Annotations
    <AttributeUsage(AttributeTargets.Method Or AttributeTargets.Parameter Or AttributeTargets.[Property] Or AttributeTargets.[Delegate] Or AttributeTargets.Field Or AttributeTargets.[Event])>
    Public NotInheritable Class CanBeNullAttribute
        Inherits Attribute
    End Class

    <AttributeUsage(AttributeTargets.Method Or AttributeTargets.Parameter Or AttributeTargets.[Property] Or AttributeTargets.[Delegate] Or AttributeTargets.Field Or AttributeTargets.[Event])>
    Public NotInheritable Class NotNullAttribute
        Inherits Attribute
    End Class

    <AttributeUsage(AttributeTargets.Method)>
    Public NotInheritable Class NotifyPropertyChangedInvocatorAttribute
        Inherits Attribute

        Public Sub New()
        End Sub

        Public Sub New(paramName As String)
            ParameterName = paramName
        End Sub

        Public Property ParameterName As String
    End Class


    <AttributeUsage(AttributeTargets.All)>
    Public NotInheritable Class UsedImplicitlyAttribute
        Inherits Attribute

        Public Sub New()
            Me.New(ImplicitUseKindFlags.[Default], ImplicitUseTargetFlags.[Default])
        End Sub

        Public Sub New(useKindFlags As ImplicitUseKindFlags)
            Me.New(useKindFlags, ImplicitUseTargetFlags.[Default])
        End Sub

        Public Sub New(targetFlags As ImplicitUseTargetFlags)
            Me.New(ImplicitUseKindFlags.[Default], targetFlags)
        End Sub

        Public Sub New(useKindFlagsParam As ImplicitUseKindFlags, targetFlagsParam As ImplicitUseTargetFlags)
            UseKindFlags = useKindFlagsParam
            TargetFlags = targetFlagsParam
        End Sub

        Public Property UseKindFlags As ImplicitUseKindFlags

        Public Property TargetFlags As ImplicitUseTargetFlags
    End Class

    <AttributeUsage(AttributeTargets.[Class] Or AttributeTargets.GenericParameter)>
    Public NotInheritable Class MeansImplicitUseAttribute
        Inherits Attribute

        Public Sub New()
            Me.New(ImplicitUseKindFlags.[Default], ImplicitUseTargetFlags.[Default])
        End Sub

        Public Sub New(useKindFlags As ImplicitUseKindFlags)
            Me.New(useKindFlags, ImplicitUseTargetFlags.[Default])
        End Sub

        Public Sub New(targetFlags As ImplicitUseTargetFlags)
            Me.New(ImplicitUseKindFlags.[Default], targetFlags)
        End Sub

        Public Sub New(useKindFlagsParam As ImplicitUseKindFlags, targetFlagsParam As ImplicitUseTargetFlags)
            UseKindFlags = useKindFlagsParam
            TargetFlags = targetFlagsParam
        End Sub

        <UsedImplicitly>
        Public Property UseKindFlags As ImplicitUseKindFlags

        <UsedImplicitly>
        Public Property TargetFlags As ImplicitUseTargetFlags
    End Class

    <Flags>
    Public Enum ImplicitUseKindFlags
        [Default] = Access Or Assign Or InstantiatedWithFixedConstructorSignature
        Access = 1
        Assign = 2
        InstantiatedWithFixedConstructorSignature = 4
        InstantiatedNoFixedConstructorSignature = 8
    End Enum

    <Flags>
    Public Enum ImplicitUseTargetFlags
        [Default] = Itself
        Itself = 1
        Members = 2
        WithMembers = Itself Or Members
    End Enum


End Namespace
