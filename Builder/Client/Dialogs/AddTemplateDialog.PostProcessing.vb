Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources

Partial Public Class AddTemplateDialog

    Interface IProcessor
        ReadOnly Property Type As Type
        Sub DoPostProcessing(ByVal dialog As AddTemplateDialog)
    End Interface
    MustInherit Class Processor(Of T)
        Implements IProcessor


        Public ReadOnly Property Type As Type Implements IProcessor.Type
            Get
                Return GetType(T)
            End Get
        End Property


        MustOverride Sub DoPostProcessing(ByVal dialog As AddTemplateDialog) Implements IProcessor.DoPostProcessing


    End Class


    Class DataSourceProcessor
        Inherits Processor(Of DataSourceResourceEntity)

        Public Overrides Sub DoPostProcessing(ByVal d As AddTemplateDialog)

            Select Case DirectCast(d.ItemTypeComboBox.SelectedValue, DataSourceBehaviourEnum)
                Case DataSourceBehaviourEnum.Exclusion
                    d.ResourceType = "exclusion"
                Case DataSourceBehaviourEnum.Inclusion
                    d.ResourceType = "inclusion"
                Case DataSourceBehaviourEnum.Normal
                    d.ResourceType = "normal"
                Case DataSourceBehaviourEnum.Seeding
                    d.ResourceType = "seeding"
                Case Else
                    Debug.Assert(False, "DataSourceBehaviourEnum value not handled!")
            End Select
        End Sub

    End Class
End Class