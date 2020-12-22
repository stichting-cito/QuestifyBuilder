Public Class Xsi
    Public Shared xsiNamespace As XNamespace = CType("http://www.w3.org/2001/XMLSchema-instance", XNamespace)
    Public Shared schemaLocation As XName = xsiNamespace + NameOf(schemaLocation)
    Public Shared noNamespaceSchemaLocation As XName = xsiNamespace + NameOf(noNamespaceSchemaLocation)
End Class