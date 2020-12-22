Imports System.Text
Imports System.Xml
Imports Cito.Tester.Common

Public Class ControlTemplateFactory




    Public Shared Function Create(serializedObject As String, allowTransformationOfOldModel As Boolean) As ControlTemplate
        Dim template As ControlTemplate = Nothing

        If Not TryDeserialize(serializedObject, template) Then
            If allowTransformationOfOldModel Then
                template = ConvertFromVersion1(serializedObject)
                template.IsTransformed = True
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



    Public Shared Function ConvertFromVersion1(serializedObject As String) As ControlTemplate
        Dim sb As New StringBuilder

        Dim xmlTemplate As New XmlDocument
        xmlTemplate.LoadXml(serializedObject)

        Dim xmlControlTemplate As XmlNode = xmlTemplate.SelectSingleNode("//controlTemplate")
        Dim xmlParameterSet As XmlNode = xmlTemplate.SelectSingleNode("//parameterSet")
        Dim xmlFunctions As XmlNode = xmlTemplate.SelectSingleNode("//functions")

        sb.AppendLine("<?xml version=""1.0"" encoding=""utf-16""?>")
        sb.AppendLine("<Template xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" definitionVersion=""1"">")
        sb.AppendLine(" <Description>Converted Control Template</Description>")
        sb.AppendLine(" <Targets>")
        sb.AppendLine("     <Target xsi:type=""ControlTemplateTarget"" enabled=""true"" name=""testPlayer"">")
        sb.AppendLine("          <Description>Converted Target</Description>")
        sb.AppendLine("          <Template><![CDATA[")
        If xmlControlTemplate IsNot Nothing Then
            sb.AppendLine(xmlControlTemplate.InnerText)
        End If
        sb.AppendLine("          ]]></Template>")
        sb.AppendLine("          <ParameterSet>")
        If xmlParameterSet IsNot Nothing Then
            sb.AppendLine(xmlParameterSet.InnerXml)
        End If
        sb.AppendLine("          </ParameterSet>")
        sb.AppendLine("     </Target>")
        sb.AppendLine(" </Targets>")
        sb.AppendLine(" <SharedFunctions><![CDATA[")
        If xmlFunctions IsNot Nothing Then
            sb.AppendLine(xmlFunctions.InnerText)
        End If
        sb.AppendLine(" ]]></SharedFunctions>")
        sb.AppendLine("  <SharedParameterSet />")
        sb.AppendLine("</Template>")

        Return Create(sb.ToString, False)
    End Function



End Class