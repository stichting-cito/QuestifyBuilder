Imports Cito.Tester.Common

Public Class ItemLayoutTemplateFactory




    Public Shared Function Create(serializedObject As String, allowTransformationOfOldModel As Boolean) As ItemLayoutTemplate
        Dim template As ItemLayoutTemplate = Nothing

        If Not TryDeserialize(serializedObject, template) Then
            If allowTransformationOfOldModel Then
                template = ConvertFromVersion1(serializedObject)
            Else
                Throw New ContentModelException("The stream contains model, but transformation to the new model is not allowed.")
            End If
        End If

        Return template
    End Function

    Private Shared Function TryDeserialize(Of T)(serializedObject As String, ByRef DeserializedObject As T) As Boolean
        Dim returnValue As Boolean = False

        Try
            DeserializedObject = DirectCast(SerializeHelper.XmlDeserializeFromString(serializedObject, GetType(T)), T)
            returnValue = True
        Catch
        End Try

        Return returnValue
    End Function



    Public Shared Function ConvertFromVersion1(serializedObject As String) As ItemLayoutTemplate
        Dim template As New ItemLayoutTemplate
        Dim tpTarget As New ItemLayoutTemplateTarget("ces", "Converted target", serializedObject)
        template.Description = "Converted template"
        template.IsTransformed = True
        template.Targets.Add(tpTarget)
        Return template
    End Function


End Class