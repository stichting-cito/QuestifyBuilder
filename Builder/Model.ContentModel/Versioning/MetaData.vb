Imports System.Xml
Imports System.Collections.Generic
Namespace Versioning
    <Serializable()> _
    <Serialization.XmlRoot(ElementName:="MetaData")> _
    Public Class MetaData

        Private _propertyEntityMetaData As List(Of PropertyEntityMetaData)
        Private _customPropertiesMetaData As List(Of CustomBankPropertyMetaData)
        Private _conceptStructuresMetaData As List(Of ConceptStructureMetaData)
        Private _dependentResourcesMetaData As List(Of DependentResourceMetaData)
        Private _treeStructuresMetaData As List(Of TreeStructureMetaData)

        Public Sub New()
        End Sub

        Public Sub New(ByVal customPropertiesMetaData As List(Of CustomBankPropertyMetaData), ByVal conceptStructureMetaData As List(Of ConceptStructureMetaData), ByVal dependentResourcesMetaData As List(Of DependentResourceMetaData), ByVal propertyEntityMetaData As List(Of PropertyEntityMetaData), ByVal treeStructureMetaData As List(Of TreeStructureMetaData))
            _customPropertiesMetaData = customPropertiesMetaData
            _conceptStructuresMetaData = conceptStructureMetaData
            _dependentResourcesMetaData = dependentResourcesMetaData
            _propertyEntityMetaData = propertyEntityMetaData
            _treeStructuresMetaData = treeStructureMetaData
        End Sub

        <Serialization.XmlArray("CustomBankProperties"), _
Serialization.XmlArrayItem("CustomBankPropertyMetaData", GetType(CustomBankPropertyMetaData))> _
        Public Property CustomPropertiesMetaData As List(Of CustomBankPropertyMetaData)
            Get
                Return _customPropertiesMetaData
            End Get
            Set(value As List(Of CustomBankPropertyMetaData))
                _customPropertiesMetaData = value
            End Set
        End Property

        <Serialization.XmlArray("ConceptStructures"), _
Serialization.XmlArrayItem("ConceptStructureMetaData", GetType(ConceptStructureMetaData))> _
        Public Property ConceptStructureMetaData As List(Of ConceptStructureMetaData)
            Get
                Return _conceptStructuresMetaData
            End Get
            Set(value As List(Of ConceptStructureMetaData))
                _conceptStructuresMetaData = value
            End Set
        End Property

        <Serialization.XmlArray("DependentResources"), _
Serialization.XmlArrayItem("DependentResourceMetaData", GetType(DependentResourceMetaData))> _
        Public Property DependentResourcesMetaData As List(Of DependentResourceMetaData)
            Get
                Return _dependentResourcesMetaData
            End Get
            Set(value As List(Of DependentResourceMetaData))
                _dependentResourcesMetaData = value
            End Set
        End Property

        <Serialization.XmlArray("PropertyEntities"), _
Serialization.XmlArrayItem("PropertyEntityMetaData", GetType(PropertyEntityMetaData))> _
        Public Property PropertyEntityMetaData As List(Of PropertyEntityMetaData)
            Get
                Return _propertyEntityMetaData
            End Get
            Set(value As List(Of PropertyEntityMetaData))
                _propertyEntityMetaData = value
            End Set
        End Property

        <Serialization.XmlArray("TreeStructures"), _
Serialization.XmlArrayItem("TreeStructureMetaData", GetType(TreeStructureMetaData))> _
        Public Property TreeStructureMetaData As List(Of TreeStructureMetaData)
            Get
                Return _treeStructuresMetaData
            End Get
            Set(value As List(Of TreeStructureMetaData))
                _treeStructuresMetaData = value
            End Set
        End Property

    End Class
End Namespace