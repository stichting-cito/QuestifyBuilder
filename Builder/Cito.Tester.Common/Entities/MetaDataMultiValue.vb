Imports System.Text
Imports System.Xml.Serialization

Public Class MetaDataMultiValue
    Inherits MetaData


    Public Enum enumMetaDataSubType
        SingleSelect = 1
        MultiSelect = 2
    End Enum



    Dim _listValues As New MetaDataCollection
    Dim _metaDataSubType As enumMetaDataSubType
    Dim _code As Guid



    <XmlAttribute("value")> _
    Public Overrides Property Value() As String
        Get
            Dim sb As New StringBuilder

            For Each mdv As MetaData In Me.ListValues
                If mdv.IsSelected Then
                    If sb.Length > 0 Then
                        sb.Append(";")
                    End If
                    sb.Append($"{mdv.Name} - {mdv.Title}")
                End If
            Next
            Return sb.ToString
        End Get

        Set(value As String)
            Throw New NotSupportedException("Set the value for MetaDataMultiValue using ListValue Collection and isSelected property")
        End Set
    End Property

    Public ReadOnly Property ListValues() As MetaDataCollection
        Get
            Return _listValues
        End Get
    End Property

    <XmlAttribute("metaDataSubType")> _
    Public Property MetaDataSubType() As enumMetaDataSubType
        Get
            Return _metaDataSubType
        End Get
        Set(value As enumMetaDataSubType)
            _metaDataSubType = value
        End Set
    End Property

    Public Property Code() As Guid
        Get
            Return _code
        End Get
        Set(value As Guid)
            _code = value
        End Set
    End Property





    Private Function ContainsValue(valueName As String) As Boolean
        Dim result As Boolean = False

        For Each md As MetaData In Me.ListValues
            If md.Name.Equals(valueName, StringComparison.InvariantCultureIgnoreCase) Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function



    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(name As String, value As String, type As enumMetaDataType)
        MyBase.New(name, value, type)
    End Sub


End Class
