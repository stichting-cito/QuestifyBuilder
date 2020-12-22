﻿Imports System.Xml.Linq
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Helpers.QTI22

    Public Class QTI22NamespaceHelper
        Inherits NamespaceHelper

        Public Overrides Function GetImsQtiNamespace() As XNamespace
            Dim imsmetadata As XNamespace = "http://www.imsglobal.org/xsd/imsqti_v2p2"
            Return imsmetadata
        End Function

        Public Overrides Function GetImsMetadataNamespace() As XNamespace
            Dim imsmetadata As XNamespace = "http://www.imsglobal.org/xsd/imsqti_metadata_v2p2"
            Return imsmetadata
        End Function

        Public Overrides Function GetSSMLNamespace() As XNamespace
            Dim ssmlNamespace As XNamespace = "http://www.w3.org/2010/10/synthesis"
            Return ssmlNamespace
        End Function

    End Class

End Namespace