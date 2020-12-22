


Imports System
Imports System.CodeDom.Compiler
Imports System.Collections
Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Reflection
Imports System.Text

Imports Cito.Tester.Common

Imports Microsoft.VisualBasic


Public Enum LanguageType
    CSharp
    VBNet
End Enum

Public Class CompileEngine


    Private _compiledAssembly As Assembly
    Private _compileErrors As List(Of String) = New List(Of String)
    Private _language As LanguageType = LanguageType.VBNet
    Private _outputText As String = String.Empty
    Private _references As ArrayList = New ArrayList()
    Private _sourceCode As String = String.Empty




    Public Sub New(code As String)
        _sourceCode = code
    End Sub


    Public Sub New(code As String, language As LanguageType)
        _sourceCode = code
        _language = language
    End Sub




    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    Public ReadOnly Property CompileErrors As List(Of String)
        Get
            Return _compileErrors
        End Get
    End Property



    Public Property Language As LanguageType
        Get
            Return _language
        End Get
        Set
            _language = value
        End Set
    End Property


    Public Property OutputText As String
        Get
            Return _outputText
        End Get
        Set
            _outputText = value
        End Set
    End Property



    Public ReadOnly Property References As ArrayList
        Get
            Return _references
        End Get
    End Property



    Public Property SourceCode As String
        Get
            Return _sourceCode
        End Get
        Set
            _sourceCode = value
        End Set
    End Property



    Public Sub CallMethod(entryPoint As String, ByVal ParamArray parameters As Object())
        Try
            Dim mods() As [Module] = _compiledAssembly.GetModules(False)
            Dim types() As Type = mods(0).GetTypes()

            For Each type As Type In types
                Dim mi As MethodInfo = type.GetMethod(entryPoint, BindingFlags.Public Or BindingFlags.Static)

                If mi IsNot Nothing Then
                    If mi.GetParameters().Length = parameters.Length Then
                        Try
                            Dim outputObj As Object = mi.Invoke(Nothing, parameters)
                            If TypeOf outputObj Is String Then
                                _outputText = DirectCast(outputObj, String)
                            Else
                                Throw New NotSupportedException(My.Resources.Error_CompileEngine_CallMethod_OutputDoesNotReturnStringObject)
                            End If
                        Catch ex As Exception
                            Throw New ControlTemplateScriptException(My.Resources.Error_CompileEngine_CallMethod_ErrorWhileExecutingControlTemplateScript, _sourceCode, ex)
                        End Try
                    Else
                        Throw New ControlTemplateException(String.Format(My.Resources.Error_CompileEngine_CallMethod_NumberOfParametersDoesNotMatch, mi.GetParameters().Length))
                    End If

                    Return
                End If
            Next

            Throw New ControlTemplateException(String.Format(My.Resources.Error_CompileEngine_CallMethod_CannotFindEntryPoint, entryPoint))

        Catch ex As Exception

            Throw New ControlTemplateException(String.Format(My.Resources.Error_CompileEngine_CallMethod_ExceptionOccurred, ex.Message), ex.InnerException)
        End Try
    End Sub



    Public Function Run() As Boolean
        _compiledAssembly = CreateAssembly(_sourceCode)
        Return (_compileErrors.Count = 0)
    End Function



    Public Function Run(entryMethod As String, ByVal ParamArray parameters As Object()) As Boolean
        Dim result As Boolean = False
        If Run() Then
            CallMethod(entryMethod, parameters)
            result = True
        End If

        Return result
    End Function



    Private Function CreateAssembly(realSourceCode As String) As Assembly
        If realSourceCode.Length = 0 Then
            Throw New ControlTemplateException(My.Resources.Error_CompileEngine_CreateAssembly_NoScriptCode)
        End If

        Dim cacheKey As String = realSourceCode.GetHashCode.ToString
        SyncLock TemplateCacheManager.CacheLock
            Dim cachedTemplateAssembly As Assembly = TemplateCacheManager.GetCachedTemplate(cacheKey)

            If cachedTemplateAssembly Is Nothing Then
                Dim codeProvider As CodeDomProvider = Nothing
                Dim provOptions As New Dictionary(Of String, String)

                If FrameworkChecker.IsVersionInstalled("v3.5") Then
                    provOptions.Add("CompilerVersion", "v3.5")
                Else
                    provOptions.Add("CompilerVersion", "v4.0")
                End If

                If _language = LanguageType.VBNet Then
                    codeProvider = New VBCodeProvider(provOptions)
                Else
                    Throw New NotSupportedException(String.Format(My.Resources.Error_CompileEngine_CreateAssembly_LanguageNotSupported, _language.ToString()))
                End If

                Dim tempFileName As String = Path.GetRandomFileName()
                Dim tempAssemblyPath As String = Path.Combine(TempStorageHelper.GetTempStoragePath, tempFileName & ".dll")
#If DEBUG Then
                Dim tempSourcePath As String = Path.Combine(TempStorageHelper.GetTempStoragePath, tempFileName & ".vb")
                Using writer As New StreamWriter(tempSourcePath)
                    writer.Write(realSourceCode)
                End Using
#End If

                Dim compilerParams As CompilerParameters = New CompilerParameters
                compilerParams.OutputAssembly = tempAssemblyPath
#If DEBUG Then
                compilerParams.CompilerOptions = "/target:library"
                compilerParams.GenerateInMemory = False
                compilerParams.IncludeDebugInformation = True
#Else
				compilerParams.CompilerOptions = "/target:library /optimize"
				compilerParams.GenerateInMemory = True
				compilerParams.IncludeDebugInformation = False
#End If
                compilerParams.GenerateExecutable = False

                compilerParams.ReferencedAssemblies.Add("mscorlib.dll")
                compilerParams.ReferencedAssemblies.Add("System.dll")
                compilerParams.ReferencedAssemblies.Add("System.Core.dll")

                For Each refAssembly As String In _references
                    Try
                        Dim trimChar As String = Environment.NewLine
                        Dim trimMedRef As String = refAssembly.Trim(trimChar.ToCharArray())
                        trimMedRef = trimMedRef.Trim()
                        If trimMedRef.Length > 0 Then
                            compilerParams.ReferencedAssemblies.Add(trimMedRef)
                        End If
                    Catch ex As Exception
                        Throw New ControlTemplateException(String.Format(My.Resources.Error_CompileEngine_CreateAssembly_CannotLoadReference, refAssembly))
                    End Try
                Next

                Environment.CurrentDirectory = Path.GetDirectoryName(New Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath)

#If DEBUG Then
                Dim results As CompilerResults = codeProvider.CompileAssemblyFromFile(compilerParams, tempSourcePath)
#Else
				Dim results As CompilerResults = codeProvider.CompileAssemblyFromSource(compilerParams, realSourceCode)
#End If

                _compileErrors.Clear()
                If results.Errors.Count > 0 Then
                    For Each err As CompilerError In results.Errors
                        _compileErrors.Add(err.ErrorText)
                    Next

                    Return Nothing
                End If

                TemplateCacheManager.CacheTemplate(cacheKey, results.CompiledAssembly)

                Return results.CompiledAssembly
            Else
                Return cachedTemplateAssembly
            End If
        End SyncLock
    End Function




    Private Function PreProcessSourceCode(sourceCode As String) As String
        Return sourceCode
    End Function

    Private Shared Function Replace(original As String, pattern As String, replacement As String, comparisonType As StringComparison) As String
        Return Replace(original, pattern, replacement, comparisonType, -1)
    End Function

    Private Shared Function Replace(original As String, pattern As String, replacement As String, comparisonType As StringComparison, stringBuilderInitialSize As Integer) As String
        If original Is Nothing Then
            Return Nothing
        End If
        If [String].IsNullOrEmpty(pattern) Then
            Return original
        End If
        Dim posCurrent As Integer = 0
        Dim lenPattern As Integer = pattern.Length
        Dim idxNext As Integer = original.IndexOf(pattern, comparisonType)
        Dim result As StringBuilder

        If stringBuilderInitialSize < 0 Then
            result = New StringBuilder(Math.Min(4096, original.Length))
        Else
            result = New StringBuilder(stringBuilderInitialSize)
        End If

        While idxNext >= 0
            result.Append(original, posCurrent, idxNext - posCurrent)
            result.Append(replacement)
            posCurrent = idxNext + lenPattern
            idxNext = original.IndexOf(pattern, posCurrent, comparisonType)
        End While

        result.Append(original, posCurrent, original.Length - posCurrent)
        Return result.ToString()
    End Function


End Class