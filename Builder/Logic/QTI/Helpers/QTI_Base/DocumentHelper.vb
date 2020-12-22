Imports System.Linq
Imports System.Reflection
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base

    Public Class DocumentHelper

        Public Overridable Function GetNamespaceHelper() As NamespaceHelper
            Return Nothing
        End Function

        Public Sub ModifyXmlDocument(Of T As IModifyDocument)(ByRef xmlDoc As XmlDocument)
            Dim assemblies = Assembly.GetExecutingAssembly()
            Dim assemblyTypes() As Type = Nothing
            Try
                assemblyTypes = assemblies.GetTypes()
            Catch reflectionTypeLoadException As ReflectionTypeLoadException
                assemblyTypes = reflectionTypeLoadException.Types
            End Try
            If assemblyTypes IsNot Nothing Then
                Dim instances = From ins In assemblyTypes
                                Where ins IsNot Nothing AndAlso ins.GetInterfaces().Contains(GetType(T)) AndAlso ins.GetConstructor(Type.EmptyTypes) IsNot Nothing
                                Select DirectCast(Activator.CreateInstance(ins), T)
                For Each instance In instances
                    instance.Modify(xmlDoc, Me)
                Next
            End If
        End Sub

        Friend Sub ReplaceElement(org As XmlNode, newElements As XmlNodeList)
            If Not newElements.Count = 0 Then
                newElements.Cast(Of XmlNode)().ToList.ForEach(Sub(newElement As XmlNode)
                                                                  org.ParentNode.InsertBefore(org.OwnerDocument.ImportNode(newElement, True), org)
                                                              End Sub)
                org.ParentNode.RemoveChild(org)
            End If
        End Sub

    End Class
End Namespace