Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Helpers.QTI22

    Public Class QTI22DocumentHelper
        Inherits DocumentHelper

        Public Overrides Function GetNamespaceHelper() As NamespaceHelper
            Return New QTI22NamespaceHelper
        End Function

    End Class

End Namespace