Imports System.Diagnostics.CodeAnalysis
Imports System.Reflection
Imports System.Text


Public NotInheritable Class ReflectionHelper

    <SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")> _
    Public Shared Sub CheckExpectedType(obj As Object, expectedType As Type, userReadableObjectName As String)
        If expectedType IsNot Nothing Then
            If obj IsNot Nothing Then
                If Not expectedType.IsAssignableFrom(obj.GetType) Then
                    Dim errMessage As String = String.Format(My.Resources.Error_PluginHelper_CheckExpectedType_NotExpectedType, userReadableObjectName, expectedType.Name)
                    Throw New TesterException(errMessage)
                End If
            Else
                Throw New TesterException(My.Resources.Error_PluginHelper_CheckExpectedType_ObjectParameterNotSet)
            End If
        Else
            Throw New TesterException(My.Resources.Error_PluginHelper_CheckExpectedType_TypeParameterNotSet)
        End If
    End Sub

    Public Shared Sub CheckIsNotNothing(obj As Object, userReadableObjectName As String)
        If (TypeOf obj Is String AndAlso String.IsNullOrEmpty(DirectCast(obj, String))) OrElse (obj Is Nothing) Then
            Throw New TesterException(String.Format(My.Resources.Error_PluginHelper_CheckIsNotNothing_ObjectIsNothing, userReadableObjectName))
        End If
    End Sub

    <SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")> _
    Public Shared Function GetInstanceByInterface(Of TInterface)(assembly As Assembly, ByVal ParamArray params() As Object) As TInterface
        Try
            For Each assemblyType As Type In assembly.GetTypes
                If assemblyType.GetInterface(GetType(TInterface).ToString()) IsNot Nothing Then
                    Return DirectCast(Activator.CreateInstance(assemblyType, params), TInterface)
                End If
            Next
        Catch ex As ReflectionTypeLoadException
            Dim loadExceptionMessages As New StringBuilder

            For Each le As Exception In ex.LoaderExceptions
                loadExceptionMessages.AppendLine(le.Message)
            Next

            Dim loaderException As New LoaderExceptionsException(
                $"GetInstanceByInterface for {assembly.FullName} ({GetType(TInterface).Name}) failed with following loader exception : {loadExceptionMessages}", ex)
            loaderException.Data.Add("assembly", assembly.FullName)

            Throw loaderException
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetInstanceByInterface(interfaceTypeName As String, assembly As Assembly, ByVal ParamArray params() As Object) As Object
        Try
            For Each assemblyType As Type In assembly.GetTypes
                If assemblyType.GetInterface(interfaceTypeName) IsNot Nothing Then
                    Return Activator.CreateInstance(assemblyType, params)
                End If
            Next

            Return Nothing
        Catch ex As ReflectionTypeLoadException
            Dim loadExceptionMessages As New StringBuilder

            For Each le As Exception In ex.LoaderExceptions
                loadExceptionMessages.AppendLine(le.Message)
            Next

            Dim loaderException As New LoaderExceptionsException(
                $"GetInstanceByInterface for {assembly.FullName} ({interfaceTypeName}) failed with following loader exception : {loadExceptionMessages}", ex)
            Throw loaderException
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function TryGetInstanceByInterface(Of TInterface)(ByRef out As TInterface, assembly As Assembly, ByVal ParamArray params() As Object) As Boolean
        Dim returnValue As Boolean = False
        Try
            For Each assemblyType As Type In assembly.GetTypes
                If assemblyType.GetInterface(GetType(TInterface).ToString()) IsNot Nothing Then
                    out = DirectCast(Activator.CreateInstance(assemblyType, params), TInterface)

                    Return True
                End If
            Next
        Catch tie As TargetInvocationException
            Throw tie
        Catch
        End Try

        Return returnValue
    End Function



    <SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")>
    Public Shared Function GetTypeByBaseClass(t As Type, assembly As Assembly) As Type
        Try
            For Each assemblyType As Type In assembly.GetTypes
                If assemblyType.BaseType IsNot Nothing AndAlso assemblyType.BaseType Is t Then
                    Return assemblyType
                End If
            Next

            Return Nothing
        Catch ex As ReflectionTypeLoadException
            Dim loadExceptionMessages As New StringBuilder

            For Each le As Exception In ex.LoaderExceptions
                loadExceptionMessages.AppendLine(le.Message)
            Next

            Dim loaderException As New LoaderExceptionsException(
                $"GetInstanceByBaseClass for {assembly.FullName} ({t.Name}) failed with following loader exception : {loadExceptionMessages}", ex)
            Throw loaderException
        Catch ex As Exception
            Throw
        End Try
    End Function


    Private Sub New()

    End Sub


End Class
