Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common

Public Class TestBuilderAssemblyResolveManager


    Private Shared _cachedResManager As DataBaseResourceManager
    Private Shared WithEvents _managedAppDomain As AppDomain
    Private Shared ReadOnly PluginExtentions As String() = {".dll", ".exe"}
    Private Shared ReadOnly PluginRoot As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Plugins")
    Private Shared ReadOnly KnownDllsToSkip As New HashSet(Of String)(New String() {
                        "Cito.Tester.ContentModel.XmlSerializers",
                        "Questify.Builder.UI.resources",
                        "Cito.Tester.ContentModel.resources",
                        "Cito.Tester.Common.resources",
                        "Questify.Builder.Client.resources",
                        "Questify.Builder.UI.Wpf.resources",
                        "MakarovDev.ExpandCollapsePanel.resources",
                        "Microsoft.AI.PerfCounterCollector.resources"})



    Public Shared Sub Initialize()
        _managedAppDomain = AppDomain.CurrentDomain
    End Sub


    Private Shared Function GetActiveOwnedForm(ByVal parentForm As Form) As Form
        Dim activeForm As Form = Nothing

        For Each frm As Form In parentForm.OwnedForms
            activeForm = frm
            Exit For
        Next

        Return activeForm
    End Function


    Private Shared Function GetBankContextId() As Integer?
        Dim mainFormInstance As Form = GetMainForm()
        Dim activeEditor As Form
        Dim bankContext As New Integer?
        If mainFormInstance IsNot Nothing Then
            activeEditor = GetActiveOwnedForm(mainFormInstance)
            If activeEditor IsNot Nothing Then
                If TypeOf activeEditor Is DataSourceEditor Then
                    Dim dse As DataSourceEditor = CType(activeEditor, DataSourceEditor)

                    If dse IsNot Nothing AndAlso dse.DataSourceResourceEntity IsNot Nothing AndAlso dse.DataSourceResourceEntity.Bank IsNot Nothing Then
                        bankContext = dse.DataSourceResourceEntity.BankId
                    End If
                ElseIf TypeOf activeEditor Is ReportFormWizard Then
                    bankContext = CType(activeEditor, ReportFormWizard).BankId
                ElseIf TypeOf activeEditor Is TestEditor_v2 Then
                    Dim testResource = CType(activeEditor, TestEditor_v2).TestResourceEntity
                    If testResource IsNot Nothing Then
                        bankContext = testResource.BankId
                    End If
                End If
            End If

            If Not bankContext.HasValue Then
                Dim frmMain As MainForm = DirectCast(mainFormInstance, MainForm)
                If frmMain.MainBankBrowser.SelectedBank IsNot Nothing Then
                    bankContext = frmMain.MainBankBrowser.SelectedBank.id
                End If
            End If

        End If
        Return bankContext
    End Function


    Private Shared Function GetMainForm() As Form
        Dim mainFormInstance As Form = Nothing

        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is MainForm Then
                mainFormInstance = frm
                Exit For
            End If
        Next

        Return mainFormInstance
    End Function


    Private Shared Function ResolveInPluginsFolder(ByVal name As String) As Assembly
        Dim dynamicAssemblyToReturn As Assembly = Nothing
        Dim pluginAssemblyPath As String

        Debug.WriteLine(
            $"[ASSEMBLYLOAD] Trying to resolve plugin assembly with name [{name}] in folder [{PluginRoot _
                           }] with one of the following extentions: {String.Join(", ", PluginExtentions)}.")

        For Each extention As String In PluginExtentions
            pluginAssemblyPath = Path.Combine(PluginRoot, $"{name}{extention}")

            If File.Exists(pluginAssemblyPath) Then
                Debug.WriteLine($"[ASSEMBLYLOAD] Assembly with name [{name}] and extention [{extention}] exist.")

                dynamicAssemblyToReturn = Assembly.LoadFrom(pluginAssemblyPath)
                Debug.WriteLine($"[ASSEMBLYLOAD] Loaded assembly (name [{name}{extention}].")
                Exit For
            Else
                Debug.WriteLine(
                    $"[ASSEMBLYLOAD] Assembly with name [{name}] and extention [{extention}] does not exist.")
            End If
        Next

        Return dynamicAssemblyToReturn
    End Function

    Private Shared Function ResolveInAppFolder(ByVal name As String) As Assembly
        Dim dynamicAssemblyToReturn As Assembly = Nothing
        For Each extension As String In PluginExtentions
            Dim assemblyName As String = GetAssemblyName(name, True, extension)
            Dim fullPath As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assemblyName)
            If File.Exists(fullPath) Then
                dynamicAssemblyToReturn = Assembly.LoadFile(fullPath)
                Exit For
            Else
                Debug.WriteLine($"[ASSEMBLYLOADFROMFILE] Assembly with name [{assemblyName}]] does not exist.")
            End If
        Next

        Return dynamicAssemblyToReturn

    End Function

    Private Shared Function GetAssemblyName(asssemblyName As String) As String
        Return GetAssemblyName(asssemblyName, True, "dll")
    End Function

    Private Shared Function GetAssemblyName(asssemblyName As String, withextension As Boolean) As String
        Return GetAssemblyName(asssemblyName, False, String.Empty)
    End Function

    Private Shared Function GetAssemblyName(asssemblyName As String, withextension As Boolean, extension As String) As String
        Dim name As String
        If asssemblyName.IndexOf(",") > -1 Then
            name = asssemblyName.Substring(0, asssemblyName.IndexOf(","))
        Else
            name = asssemblyName
        End If
        If withextension Then
            extension = extension.Trim("."c)
            name = $"{name}.{extension}"
        End If
        Return name.Trim
    End Function


    Private Shared Function ManagedAppDomainAssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly Handles _managedAppDomain.AssemblyResolve
        Dim dynamicAssemblyToReturn As Assembly
        If Not KnownDllsToSkip.Contains(GetAssemblyName(args.Name, False)) Then
            dynamicAssemblyToReturn = ResolveInAppFolder(args.Name)
            If dynamicAssemblyToReturn Is Nothing Then
                dynamicAssemblyToReturn = ResolveInPluginsFolder(args.Name)
            End If
            If dynamicAssemblyToReturn Is Nothing Then Debug.WriteLine($"Dll {args.Name} could not be found")
            Return dynamicAssemblyToReturn
        End If
        Return Nothing
    End Function


End Class
