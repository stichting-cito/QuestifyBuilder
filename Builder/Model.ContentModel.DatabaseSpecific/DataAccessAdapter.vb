Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.Common
Imports System.Configuration
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports SD.LLBLGen.Pro.DQE.SqlServer




Namespace Questify.Builder.Model.ContentModel.DatabaseSpecific
    Public Class DataAccessAdapter
        Inherits DataAccessAdapterBase

        Public Shared ConnectionStringKeyName As String = "Main.ConnectionString"

        Public Sub New()
            Me.New(ReadConnectionStringFromConfig(), False, CType(Nothing, CatalogNameOverwriteHashtable), Nothing)
        End Sub

        Public Sub New(keepConnectionOpen As Boolean)
            Me.New(ReadConnectionStringFromConfig(), keepConnectionOpen, CType(Nothing, CatalogNameOverwriteHashtable), Nothing)
        End Sub

        Public Sub New(connectionString As String)
            Me.New(connectionString, False, CType(Nothing, CatalogNameOverwriteHashtable), Nothing)
        End Sub

        Public Sub New(connectionString As String, keepConnectionOpen As Boolean)
            Me.New(connectionString, keepConnectionOpen, CType(Nothing, CatalogNameOverwriteHashtable), Nothing)
        End Sub

        Public Sub New(connectionString As String, keepConnectionOpen As Boolean, catalogNameUsageSetting As CatalogNameUsage, catalogNameToUse As String)
            MyBase.New(PersistenceInfoProviderSingleton.GetInstance())
            InitClassPhase2(connectionString, keepConnectionOpen, catalogNameUsageSetting, SchemaNameUsage.Default, catalogNameToUse, String.Empty, Nothing, Nothing)
        End Sub

        Public Sub New(connectionString As String, keepConnectionOpen As Boolean, schemaNameUsageSetting As SchemaNameUsage, schemaNameToUse As String)
            MyBase.New(PersistenceInfoProviderSingleton.GetInstance())
            InitClassPhase2(connectionString, keepConnectionOpen, CatalogNameUsage.Default, schemaNameUsageSetting, String.Empty, schemaNameToUse, Nothing, Nothing)
        End Sub

        Public Sub New(connectionString As String, keepConnectionOpen As Boolean, catalogNameOverwrites As CatalogNameOverwriteHashtable, schemaNameOverwrites As SchemaNameOverwriteHashtable)
            MyBase.New(PersistenceInfoProviderSingleton.GetInstance())
            InitClassPhase2(connectionString, keepConnectionOpen, CatalogNameUsage.Default, SchemaNameUsage.Default, String.Empty, String.Empty, catalogNameOverwrites, schemaNameOverwrites)
        End Sub

        Public Shared Sub SetArithAbortFlag(value As Boolean)
            DynamicQueryEngine.ArithAbortOn = value
        End Sub

        Public Shared Sub SetSqlServerCompatibilityLevel(compatibilityLevel As SqlServerCompatibilityLevel)
            DynamicQueryEngine.DefaultCompatibilityLevel = compatibilityLevel
        End Sub

        Protected Overrides Function CreateDynamicQueryEngine() As DynamicQueryEngineBase
            Return Me.PostProcessNewDynamicQueryEngine(New DynamicQueryEngine())
        End Function

        Private Shared Function ReadConnectionStringFromConfig() As String
            Return ConfigFileHelper.ReadConnectionStringFromConfig(ConnectionStringKeyName)
        End Function

        Protected Overrides Sub SetPerInstanceCompatibilityLevel(dqe As DynamicQueryEngineBase)
            If _compatibilityLevel.HasValue Then
                CType(dqe, DynamicQueryEngine).CompatibilityLevel = _compatibilityLevel.Value
            End If
        End Sub

        Private _compatibilityLevel As Nullable(Of SqlServerCompatibilityLevel) = Nothing

        Public Property CompatibilityLevel As Nullable(Of SqlServerCompatibilityLevel)
            Get
                Return _compatibilityLevel
            End Get
            Set
                _compatibilityLevel = value
            End Set
        End Property





    End Class
End Namespace
