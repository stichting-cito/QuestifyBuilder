
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Client.Forms.Services

Public NotInheritable Class MefConfiguration

    <Export(GetType(ILegacyInputBox))>
    Public ReadOnly Property InputBox As ILegacyInputBox
        Get
            Return New InputBox
        End Get
    End Property

    <Export(GetType(IResourceEditorService))>
    Public ReadOnly Property ResourceManagerFacade As IResourceEditorService
        Get
            Return New ResourceEditorService
        End Get
    End Property

End Class
