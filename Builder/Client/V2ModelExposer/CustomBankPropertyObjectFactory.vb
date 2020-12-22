Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel.Composition
Imports Questify.Builder.Logic.Service.Factories

<Export(GetType(ICustomBankPropertyEditorObjectFactory))>
Public Class CustomBankPropertyObjectFactory
    Implements ICustomBankPropertyEditorObjectFactory

    Public Function UpdateCustomBankProperty(customBankProperty As CustomBankPropertyEntity) As String Implements ICustomBankPropertyEditorObjectFactory.UpdateCustomBankProperty
        Return BankFactory.Instance.UpdateCustomProperty(customBankProperty)
    End Function

End Class
