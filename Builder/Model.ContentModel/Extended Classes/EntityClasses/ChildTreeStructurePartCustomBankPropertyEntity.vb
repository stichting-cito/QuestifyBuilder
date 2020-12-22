Imports System.Linq

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Class ChildTreeStructurePartCustomBankPropertyEntity

        Public ReadOnly Property ChildName As String
            Get
                If Me.TreeStructurePartCustomBankProperty IsNot Nothing AndAlso Me.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty IsNot Nothing AndAlso Me.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection IsNot Nothing Then
                    Return Me.TreeStructurePartCustomBankProperty.TreeStructureCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.First(Function(i) i.TreeStructurePartCustomBankPropertyId = Me.ChildTreeStructurePartCustomBankPropertyId).Name
                Else
                    Return "Unable to determine Name. Probably because the TreeStructurePartCustomBankProperty was deleted."
                End If
            End Get
        End Property

    End Class

End Namespace