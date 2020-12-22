Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Helpers.QTI30

    Public Class QTI30DocumentHelper
        Inherits DocumentHelper

        Public Overrides Function GetNamespaceHelper() As NamespaceHelper
            Return New QTI30NamespaceHelper
        End Function

    End Class

End Namespace