Imports System.Drawing
Imports Cito.Tester.ContentModel
Imports System.Runtime.CompilerServices
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel

    Public Module ParametersExtension

        <Extension()>
        Public Function ValidateHierarchical(parameter As ParameterSetCollection) As Boolean
            Dim worker As New HierarchicalParameterSetCollectionValidator(parameter)
            Return worker.Validate()
        End Function

        <Extension()>
        Public Function GetValidateHierarchicalErrors(parameter As ParameterSetCollection) As String
            Dim worker As New HierarchicalParameterSetCollectionValidator(parameter)
            Return worker.GetErrors()
        End Function

        <Extension()>
        Public Function XmlString(parameter As ParameterSetCollection) As String
            Dim ret As String = ParameterSetCollectionCloner.SerializeToString(parameter).ToString()
            Return ret
        End Function

        <Extension()>
        Public Function DeepCloneWithDesignerSettingsAndAttributeReferences(parameter As ParameterSetCollection) As ParameterSetCollection
            Dim worker As New ParameterSetCollectionCloner(parameter)
            Return worker.MakeClone()
        End Function

        <Extension()>
        Public Function DeepCloneWithDesignerSettingsAndAttributeReferences(parameter As ParameterCollection) As ParameterCollection
            Dim worker As New ParameterCollectionCloner(parameter)
            Return worker.MakeClone()
        End Function


        <Extension>
        Public Function IsValid(prm As ParameterBase) As Boolean
            Dim w = ParamValidator.baseParamValidator.GetValidator(prm)
            Return w.isValid()
        End Function

        <Extension>
        Friend Function IsValid(prm As ParameterBase, bag As IDictionary(Of String, String)) As Boolean
            Dim w = ParamValidator.baseParamValidator.GetValidator(prm)
            w.ValueBag = bag
            Return w.isValid()
        End Function

        <Extension>
        Friend Function GetError(prm As ParameterBase, bag As IDictionary(Of String, String)) As String
            Dim w = ParamValidator.baseParamValidator.GetValidator(prm)
            w.ValueBag = bag
            Return String.Join(vbNewLine, (From errorMessage In w.GetError() Where Not String.IsNullOrEmpty(errorMessage)).ToArray())
        End Function

        <Extension>
        Public Function GetError(prm As ParameterBase) As String
            Return prm.GetError(Nothing)
        End Function

        <Extension>
        Public Function GetDesignerValue(prm As ParameterBase, key As String, defaultVal As String) As String
            Dim s = prm.DesignerSettings.FirstOrDefault(Function(p) p.Key = key)
            Return If(s Is Nothing, defaultVal, s.Value)
        End Function

        <Extension>
        Public Function GetDesignerValue(prm As ParameterBase, key As String, defaultVal As Boolean) As Boolean
            Dim s = prm.DesignerSettings.FirstOrDefault(Function(p) p.Key = key)
            Return If(s Is Nothing, defaultVal, Convert.ToBoolean(s.Value))
        End Function

        <Extension>
        Public Function GetDesignerValue(prm As ParameterBase, key As String, defaultVal As Integer) As Integer
            Dim s = prm.DesignerSettings.FirstOrDefault(Function(p) p.Key = key)
            Return If(s Is Nothing, defaultVal, Convert.ToInt32(s.Value))
        End Function

        <Extension>
        Public Function GetDesignerIntValue(prm As ParameterBase, key As String) As Integer?
            Dim s = prm.DesignerSettings.FirstOrDefault(Function(p) p.Key = key)
            If s Is Nothing Then
                Return Nothing
            Else
                Return Convert.ToInt32(s.Value)
            End If
        End Function

        <Extension>
        Public Function IsVisible(prm As ParameterBase) As Boolean
            Return prm.GetDesignerValue("visible", True)
        End Function

        <Extension>
        Public Function ConditionalEnabledSwitchParameter(prm As ParameterBase) As String
            Return prm.GetDesignerValue("conditionalEnabledSwitchParameter", "")
        End Function

        <Extension>
        Public Function IsRedirected(prm As ParameterBase) As Boolean
            Return prm.GetDesignerValue("redirectEnabled", False)
        End Function

        <Extension>
        Public Function Group(prm As ParameterBase) As String
            Return prm.GetDesignerValue("group", String.Empty)
        End Function

        <Extension>
        Public Function IsGroupConditionalEnabled(prm As ParameterBase) As Boolean
            Return prm.GetDesignerValue("groupConditionalEnabled", False)
        End Function

        <Extension>
        Public Function GroupConditionalEnabledSwitch(prm As ParameterBase) As String
            Return prm.GetDesignerValue("groupConditionalEnabledSwitch", String.Empty)
        End Function

        <Extension>
        Public Function GroupConditionalEnabledWhenValue(prm As ParameterBase) As String
            Return prm.GetDesignerValue("groupConditionalEnabledWhenValue", String.Empty)
        End Function
        <Extension>
        Public Function IsRequired(prm As ParameterBase) As Boolean
            Return prm.GetDesignerValue("required", False)
        End Function

        <Extension>
        Public Function Label(prm As ParameterBase) As String
            Return prm.GetDesignerValue("label", String.Empty)
        End Function

        <Extension>
        Public Function Description(prm As ParameterBase) As String
            Return prm.GetDesignerValue("description", String.Empty)
        End Function

        <Extension>
        Public Function ValidationRex(prm As ParameterBase) As String
            Return prm.GetDesignerValue("validationRegEx", String.Empty)
        End Function

        <Extension>
        Public Function ValidationRegExMessage(prm As ParameterBase) As String
            Return prm.GetDesignerValue("validationRegExMessage", String.Empty)
        End Function

        <Extension>
        Public Function DefaultValue(prm As ParameterBase) As String
            Return prm.GetDesignerValue("defaultvalue", String.Empty)
        End Function


        <Extension>
        Public Function SubSetIdentifierStrategy(prm As CollectionParameter) As String
            Return prm.GetDesignerValue("subsetidentifiers", "Numeric")
        End Function

        <Extension>
        Public Function RangeFrom(prm As IntegerParameter) As Integer?
            Dim value = prm.GetDesignerValue("rangeFrom", Integer.MinValue)
            If value = Integer.MinValue Then
                Return Nothing
            Else
                Return value
            End If
        End Function


        <Extension>
        Public Function RangeTo(prm As IntegerParameter) As Integer?
            Dim value = prm.GetDesignerValue("rangeTo", Integer.MinValue)
            If value = Integer.MinValue Then
                Return Nothing
            Else
                Return value
            End If
        End Function
        <Extension>
        Public Function GetSize(resourceParameter As ResourceParameter, bankId As Integer) As Size?
            Dim returnValue As Size? = Nothing
            If resourceParameter IsNot Nothing Then
                Dim resource = ResourceFactory.Instance.GetResourceByNameWithOption(bankId, Uri.UnescapeDataString(resourceParameter.Value), new ResourceRequestDTO())
                Dim genericResource = DirectCast(resource, GenericResourceEntity)
                If genericResource.Height.HasValue AndAlso genericResource.Width.HasValue Then
                    returnValue = New Size(genericResource.Width.Value, genericResource.Height.Value)
                End If
            End If
            Return returnValue
        End Function

    End Module

End Namespace
