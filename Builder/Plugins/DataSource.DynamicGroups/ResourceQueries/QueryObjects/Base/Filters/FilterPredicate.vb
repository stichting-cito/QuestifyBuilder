Imports System.Xml.Serialization

<XmlRoot("Filter")> _
<XmlInclude(GetType(AndFilterPredicate))> _
<XmlInclude(GetType(OrFilterPredicate))> _
<XmlInclude(GetType(NotFilterPredicate))> _
<XmlInclude(GetType(NOOPFilterPredicate))> _
<XmlInclude(GetType(ItemInTestFilterPredicate))> _
<XmlInclude(GetType(ResourcePropertyFilterPredicate))> _
Public MustInherit Class FilterPredicate


    Private _isContainer As Boolean = False
    Private _name As String = Me.GetType.ToString
    Private _nameLocalized As String = Me.GetType.ToString



    Public Sub New(isContainer As Boolean)
        Me._isContainer = isContainer
    End Sub

    Public Sub New()
    End Sub



    Public ReadOnly Property IsContainer() As Boolean
        Get
            Return _isContainer
        End Get
    End Property

    Public Overridable ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Public Overridable ReadOnly Property NameLocalized() As String
        Get
            Return _nameLocalized
        End Get
    End Property



    Public Shared Operator And(ByVal One As FilterPredicate, ByVal Other As FilterPredicate) As FilterPredicate
        Return New AndFilterPredicate(One, Other)
    End Operator

    Public Shared Operator Not(ByVal One As FilterPredicate) As FilterPredicate
        Return New NotFilterPredicate(One)
    End Operator

    Public Shared Operator Or(ByVal One As FilterPredicate, ByVal Other As FilterPredicate) As FilterPredicate
        Return New OrFilterPredicate(One, Other)
    End Operator

    Public Overridable Function AddFilter(filter As FilterPredicate) As Boolean
        Return False
    End Function

    Public Overridable Function FindContainerForFilter(filter As FilterPredicate) As FilterPredicate
        Return Nothing
    End Function

    Public Overridable Function RemoveFilter(filter As FilterPredicate) As Boolean
        Return False
    End Function

    Public Function [And](ByVal other As FilterPredicate) As FilterPredicate
        Return New AndFilterPredicate(Me, other)
    End Function

    Public Function [Not]() As FilterPredicate
        Return New NotFilterPredicate(Me)
    End Function

    Public Function [Or](ByVal other As FilterPredicate) As FilterPredicate
        Return New OrFilterPredicate(Me, other)
    End Function

    Public Overrides Function ToString() As String
        Return Me.NameLocalized
    End Function


End Class