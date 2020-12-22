
Imports System.Xml
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Namespace Datasources

    <Serializable> _
    Public Class DataSourceSettings
        Implements IDataSourceSettings


        Private _behaviour As DataSourceBehaviourEnum
        Private _dataSourceConfig As DataSourceConfig
        Private _dataSourceConfigName As String = "dataSource"
        Private _dataSourceConfigSrc As String
        Private _dataSourceConfigType As String
        Private _dataSourceConfigUIFactorySrc As String
        Private _dataSourceConfigUIFactoryType As String
        Private _dataSourceSrc As String
        Private _dataSourceType As String
        Private _identifier As String
        Private _settingsCollection As New SettingsCollection2
        Private _title As String



        Public Sub New()
        End Sub

        Public Sub New(dataSourceType As String, dataSourceConfigType As String, dataSourceConfigUIFactoryType As String)
            With Me
                .DataSourceType = dataSourceType
                .DataSourceConfigType = dataSourceConfigType
                .DataSourceConfigUIFactoryType = dataSourceConfigUIFactoryType
            End With
        End Sub

        Public Sub New(dataSourceSrc As String, dataSourceType As String, dataSourceConfigSrc As String, dataSourceConfigType As String, dataSourceConfigUIFactorySrc As String, dataSourceConfigUIFactoryType As String)
            With Me
                .DataSourceSrc = dataSourceSrc
                .DataSourceType = dataSourceType

                .DataSourceConfigSrc = dataSourceConfigSrc
                .DataSourceConfigType = dataSourceConfigType

                .DataSourceConfigUIFactorySrc = dataSourceConfigUIFactorySrc
                .DataSourceConfigUIFactoryType = dataSourceConfigUIFactoryType
            End With
        End Sub



        <XmlAttribute("behaviour")> _
        Public Property Behaviour As DataSourceBehaviourEnum Implements IDataSourceSettings.Behaviour
            Get
                Return Me._behaviour
            End Get
            Set
                Me._behaviour = value
            End Set
        End Property

        <XmlIgnore> _
        Public Property DataSourceConfig As DataSourceConfig
            Get
                If _dataSourceConfig Is Nothing Then
                    _dataSourceConfig = Me.GetConfig
                End If

                Return _dataSourceConfig
            End Get
            Set
                _dataSourceConfig = value
            End Set
        End Property

        <XmlAttribute("dataSourceConfigName")> _
        Public Property DataSourceConfigName As String
            Get
                Return _dataSourceConfigName
            End Get
            Set
                _dataSourceConfigName = value
            End Set
        End Property

        <XmlAttribute("dataSourceConfigSrc")> _
        Public Property DataSourceConfigSrc As String Implements IDataSourceSettings.DataSourceConfigSrc
            Get
                Return _dataSourceConfigSrc
            End Get
            Set
                _dataSourceConfigSrc = value
            End Set
        End Property


        <XmlAttribute("dataSourceConfigType")> _
        Public Property DataSourceConfigType As String Implements IDataSourceSettings.DataSourceConfigType
            Get
                Return _dataSourceConfigType
            End Get
            Set
                _dataSourceConfigType = value
            End Set
        End Property

        <XmlAttribute("dataSourceConfigUIFactorySrc")> _
        Public Property DataSourceConfigUIFactorySrc As String Implements IDataSourceSettings.DataSourceConfigUIFactorySrc
            Get
                Return _dataSourceConfigUIFactorySrc
            End Get
            Set
                _dataSourceConfigUIFactorySrc = value
            End Set
        End Property

        <XmlAttribute("dataSourceConfigUIFactoryType")> _
        Public Property DataSourceConfigUIFactoryType As String Implements IDataSourceSettings.DataSourceConfigUIFactoryType
            Get
                Return _dataSourceConfigUIFactoryType
            End Get
            Set
                _dataSourceConfigUIFactoryType = value
            End Set
        End Property

        <XmlAttribute("dataSourceSrc")> _
        Public Property DataSourceSrc As String Implements IDataSourceSettings.DataSourceSrc
            Get
                Return _dataSourceSrc
            End Get
            Set
                _dataSourceSrc = value
            End Set
        End Property

        <XmlAttribute("dataSourceType")> _
        Public Property DataSourceType As String Implements IDataSourceSettings.DataSourceType
            Get
                Return _dataSourceType
            End Get
            Set
                _dataSourceType = value
            End Set
        End Property

        <XmlAttribute("identifier")> _
        Public Property Identifier As String Implements IDataSourceSettings.Identifier
            Get
                Return Me._identifier
            End Get
            Set
                _identifier = value
            End Set
        End Property

        <XmlArray("settingsCollection")> _
        <XmlArrayItem(GetType(Settings2), ElementName:="settings")> _
        Public ReadOnly Property SettingsCollection As SettingsCollection2 Implements IDataSourceSettings.SettingsCollection
            Get
                Return _settingsCollection
            End Get
        End Property

        <XmlAttribute("title")> _
        Public Property Title As String Implements IDataSourceSettings.Title
            Get
                Return _title
            End Get
            Set
                _title = value
            End Set
        End Property




        Public Function CreateGetDataSource() As IDataSource
            Dim dataSourceConfig As DataSourceConfig = Me.DataSourceConfig
            Dim dataSourceType As Type
            Dim dataSourceInstance As IDataSource = Nothing

            dataSourceType = Type.GetType(Me.DataSourceType)

            If dataSourceType IsNot Nothing Then
                dataSourceInstance = CType(Activator.CreateInstance(dataSourceType, dataSourceConfig), IDataSource)
            Else
                Trace.Fail($"Unable to locate plugin for {Me.DataSourceType}")
            End If

            Return dataSourceInstance
        End Function


        Public Function GetMD5Hash() As Byte()
            Return SerializeHelper.GetMD5Hash(Me)
        End Function


        Public Function ShouldSerializeDataSourceConfigName() As Boolean
            Return Not (String.IsNullOrEmpty(DataSourceConfigName) OrElse DataSourceConfigName = "dataSource")
        End Function



        Public Function ShouldSerializeSettingsCollection() As Boolean
            Me.PutConfig(Me.DataSourceConfig)
            Return True
        End Function


        Private Function GetConfig() As DataSourceConfig

            Dim dataSourceConfigType As Type = Type.GetType(Me.DataSourceConfigType)
            Return GetConfig(Me.DataSourceConfigName, dataSourceConfigType)
        End Function

        Private Function GetConfig(settingsName As String, dataSourceConfigType As Type) As DataSourceConfig
            If (dataSourceConfigType Is Nothing) Then
                Throw New ArgumentNullException("dataSourceConfigType", My.Resources.SelectionTemplateNotConfiguredCorrectly)
            End If

            Dim settingsContainer As Settings2 = SettingsCollection.GetSettingsByName(settingsName)
            Dim config As DataSourceConfig

            If settingsContainer IsNot Nothing Then
                config = CType(SerializeHelper.XmlDeserializeFromString(settingsContainer.Settings.OuterXml, dataSourceConfigType), DataSourceConfig)
            Else
                config = CType(Activator.CreateInstance(dataSourceConfigType), DataSourceConfig)
            End If

            Return config
        End Function

        Private Sub PutConfig(settingsName As String, dataSourceConfig As DataSourceConfig, dataSourceConfigType As Type)
            Dim settingsContainer As Settings2 = SettingsCollection.GetSettingsByName(settingsName)

            If dataSourceConfig Is Nothing Then
                Throw New ArgumentNullException("dataSourceConfig")
            End If

            If settingsContainer Is Nothing Then
                settingsContainer = New Settings2(settingsName, Nothing)
                SettingsCollection.Add(settingsContainer)
            End If

            Dim serializedSettings As String = SerializeHelper.XmlSerializeToString(dataSourceConfig, True)
            Dim settingsXmlDocument As New XmlDocument
            settingsXmlDocument.LoadXml(serializedSettings)
            settingsContainer.Settings = settingsXmlDocument.DocumentElement
        End Sub

        Private Sub PutConfig(dataSourceConfig As DataSourceConfig)
            Dim dataSourceConfigType As Type = Type.GetType(Me.DataSourceConfigType)

            PutConfig(Me.DataSourceConfigName, dataSourceConfig, dataSourceConfigType)
        End Sub


    End Class

End Namespace

