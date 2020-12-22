Imports System.IO
Imports Questify.Builder.Logic.Chain

Namespace QTI.Requests.BaseClasses

    Public MustInherit Class PublicationRequestBase
        Inherits ChainHandlerRequest

        Private _sourcePackage As FileInfo
        Private _targetPackageFileSystemInfo As FileInfo

        Public Sub New(ByVal sourcePackage As FileInfo, ByVal targetPackageFileSystemInfo As FileInfo)
            Me.New()
            _sourcePackage = sourcePackage
            _targetPackageFileSystemInfo = targetPackageFileSystemInfo
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        Public ReadOnly Property TargetPackageFileSystemInfo() As FileInfo
            Get
                Return _targetPackageFileSystemInfo
            End Get
        End Property


    End Class
End NameSpace