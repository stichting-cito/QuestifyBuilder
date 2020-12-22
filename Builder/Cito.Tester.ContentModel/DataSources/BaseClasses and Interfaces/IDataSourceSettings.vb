Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Datasources


    <TypeConverter(GetType(LocalizedEnumConverter))>
    Public Enum DataSourceBehaviourEnum
        <XmlEnum("normal")> Normal = 0

        <XmlEnum("inclusion")> Inclusion = 1

        <XmlEnum("exclusion")> Exclusion = 2

        <XmlEnum("seeding")> Seeding = 3
    End Enum


    Public Interface IDataSourceSettings


        Property Behaviour As DataSourceBehaviourEnum

        Property DataSourceConfigSrc As String

        Property DataSourceConfigType As String

        Property DataSourceConfigUIFactorySrc As String

        Property DataSourceConfigUIFactoryType As String

        Property DataSourceSrc As String

        Property DataSourceType As String

        Property Identifier As String

        ReadOnly Property SettingsCollection As SettingsCollection2

        Property Title As String


    End Interface

End Namespace

