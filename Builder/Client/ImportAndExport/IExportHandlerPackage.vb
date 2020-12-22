Imports System.ComponentModel
Imports Cito.Tester.Common

Public Interface IExportHandlerPackage
    Inherits IExportHandlerBase

    Property ResourceIds As List(Of Guid)


    Function Export(ByVal bgWorker As BackgroundWorker, sourceResourceManager As ResourceManagerBase, ByVal resources As ResourceEntryCollection) As Boolean


    Function Export(ByVal bgWorker As BackgroundWorker, ByVal sourceResourceManager As ResourceManagerBase, ByVal bankId As Integer) As Boolean

End Interface
