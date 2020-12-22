Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports System.ComponentModel
Imports Questify.Builder.Logic.Service.Exceptions

Public Module ExtensionMethods

    <Extension()>
    Public Sub DisableScrollWheel(ByVal combo As System.Windows.Forms.ComboBox)
        AddHandler combo.MouseWheel, New MouseEventHandler(Sub(sender As Object, e As MouseEventArgs)
                                                               DirectCast(e, HandledMouseEventArgs).Handled = True
                                                           End Sub)
    End Sub

    <Extension()>
    Public Function GetAllControlsOfType(Of T As Control)(parent As Control) As IEnumerable(Of T)
        Dim result = New List(Of T)()
        For Each control As Control In parent.Controls
            If TypeOf control Is T Then
                result.Add(DirectCast(control, T))
            End If
            If control.HasChildren() Then
                result.AddRange(control.GetAllControlsOfType(Of T)())
            End If
        Next
        Return result
    End Function


    <Extension()>
    Public Function AsIEnumerable(ByVal matchColl As MatchCollection) As IEnumerable(Of Match)
        Dim ret As New List(Of Match)
        For Each e As Match In matchColl
            ret.Add(e)
        Next
        Return ret
    End Function


    <Extension()>
    Public Function GetNesterAttributeValues(ByVal node As XmlNode, attributeName As String) As IEnumerable(Of String)
        Dim ret As New List(Of String)()
        If (node IsNot Nothing) Then
            gnavRecursive(ret, node, attributeName)
        End If
        Return ret
    End Function

    Private Sub gnavRecursive(ByRef ret As List(Of String), node As XmlNode, attr2Find As String)
        If node.Attributes IsNot Nothing AndAlso node.Attributes(attr2Find) IsNot Nothing Then ret.Add(node.Attributes(attr2Find).Value.ToString())
        For Each n As XmlNode In node.ChildNodes()
            gnavRecursive(ret, n, attr2Find)
        Next
    End Sub

    <Extension()>
    Public Function ToXmlElement(ByVal xml As String) As XmlElement
        Dim frag As XmlDocumentFragment = New XmlDocument().CreateDocumentFragment()
        frag.InnerXml = xml
        Return TryCast(frag.FirstChild, XmlElement)
    End Function


    <Extension()>
    Public Sub AddAttributeReferenceDrivenChangeHandlers(paramEditor As ParameterEditorControlBase, collParam As CollectionParameter, paramSet As ParameterSetCollection)
        AddAttributeReference(collParam, paramSet, paramEditor)
    End Sub

    <Extension()>
    Public Sub AddAttributeReferenceDrivenChangeHandlers(collParam As CollectionParameter, paramSet As ParameterSetCollection)
        AddAttributeReference(collParam, paramSet)
    End Sub

    Private Sub AddAttributeReference(collParam As CollectionParameter, paramSet As ParameterSetCollection, Optional paramEditor As ParameterEditorControlBase = Nothing)
        Try
            For Each attrRef In collParam.AttributeReferences
                Dim sourceParam = paramSet.GetParameters().FirstOrDefault(Function(p) p.Name.Equals(attrRef.Value, StringComparison.OrdinalIgnoreCase))
                Debug.Assert(sourceParam IsNot Nothing, "Cannot find source parameter: " + attrRef.Value)

                Dim targetProp = collParam.GetType().GetProperties().FirstOrDefault(Function(prop) prop.Name.Equals(attrRef.Name, StringComparison.OrdinalIgnoreCase))
                Debug.Assert(targetProp IsNot Nothing, "Cannot find property to reference: " + attrRef.Name)

                If attrRef.WhatToCopy = AttributeReference.WhatToCpy.Value Then
                    Dim sourceValue = sourceParam.GetType().GetProperty("Value")
                    Debug.Assert(sourceValue IsNot Nothing, "Cannot find 'Value' of source parameter: " + attrRef.Value)

                    If paramEditor IsNot Nothing Then
                        Dim subscription = Sub(sender As Object, e As PropertyChangedEventArgs)
                                               If targetProp.PropertyType.IsEnum Then
                                                   Dim newConvertedValue = [Enum].Parse(targetProp.PropertyType, sourceValue.GetValue(sourceParam).ToString)
                                                   targetProp.SetValue(collParam, newConvertedValue)
                                               Else
                                                   Dim newConvertedValue = Convert.ChangeType(sourceValue.GetValue(sourceParam), targetProp.PropertyType)
                                                   targetProp.SetValue(collParam, newConvertedValue)
                                               End If
                                           End Sub
                        subscription(paramEditor, Nothing)
                        AddHandler sourceParam.PropertyChanged, subscription
                    Else
                        If targetProp.PropertyType.IsEnum Then
                            Dim newConvertedValue = [Enum].Parse(targetProp.PropertyType, sourceValue.GetValue(sourceParam).ToString)
                            targetProp.SetValue(collParam, newConvertedValue)
                        Else
                            Dim newConvertedValue = Convert.ChangeType(sourceValue.GetValue(sourceParam), targetProp.PropertyType)
                            targetProp.SetValue(collParam, newConvertedValue)
                        End If
                    End If
                ElseIf attrRef.WhatToCopy = AttributeReference.WhatToCpy.Parameter Then
                    targetProp.SetValue(collParam, sourceParam)
                End If
            Next
        Catch ex As Exception
            Throw New AppLogicException(String.Format(My.Resources.ErrorParsingAttributeReferencesForParameter, collParam.Name), ex)
        End Try
    End Sub



End Module

