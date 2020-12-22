Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel

Namespace ContentModel

    Friend MustInherit Class ParameterCollectionClonerBase(Of TModelParameter)

        Private ReadOnly _parameter As TModelParameter
        Private Shared ReadOnly _xmlSerializer As XmlSerializer

        Shared Sub New()
            Dim xmlOverrides As New XmlAttributeOverrides
            Dim xmlDsAttrbts As New XmlAttributes()
            Dim xmlArAttrbts As New XmlAttributes()
            xmlDsAttrbts.XmlIgnore = False
            xmlArAttrbts.XmlIgnore = False

            xmlDsAttrbts.XmlElements.Add(New XmlElementAttribute("designersetting"))
            xmlArAttrbts.XmlElements.Add(New XmlElementAttribute("attributereference"))

            xmlOverrides.Add(GetType(ParameterBase), "DesignerSettings", xmlDsAttrbts)
            xmlOverrides.Add(GetType(ParameterBase), "AttributeReferences", xmlArAttrbts)
            _xmlSerializer = New XmlSerializer(GetType(TModelParameter), xmlOverrides)
        End Sub


        Public Sub New(parameter As TModelParameter)

            Dim t As Type = GetType(TModelParameter)

            If (Not t.FullName.Contains("Cito.Tester.ContentModel")) Then
                Throw New ArgumentException
            End If

            _parameter = parameter
        End Sub

        Function MakeClone() As TModelParameter
            Dim ret As TModelParameter = Nothing

            Dim sb As StringBuilder = SerializeToString(_parameter)

            ret = DeSerializerFromString(sb.ToString())

            DoPostCloneAction(ret)
            Return ret
        End Function

        Friend Shared Function SerializeToString(parameter As TModelParameter) As StringBuilder

            Dim sb As New StringBuilder
            Using sw As New StringWriter(sb)
                _xmlSerializer.Serialize(sw, parameter)
            End Using
            Return sb
        End Function

        Friend Shared Function DeSerializerFromString(xmlString As String) As TModelParameter
            Dim ret As TModelParameter = Nothing
            Using sr As New StringReader(xmlString)
                ret = DirectCast(_xmlSerializer.Deserialize(sr), TModelParameter)
            End Using

            Debug.Assert(ret IsNot Nothing)
            Return ret
        End Function


        Protected MustOverride Sub DoPostCloneAction(modelParameter As TModelParameter)

        Protected ReadOnly Property Parameter As TModelParameter
            Get
                Return _parameter
            End Get
        End Property

    End Class
End Namespace