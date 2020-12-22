Namespace QTI.PackageCreators.QTI_Base

    Public Class PackageCreatorConstants

        Public Const LANGUAGE As String = "nl"
        Public Const SCORE As String = "SCORE"
        Public Const WEIGHT As String = "WEIGHT"
        Public Const RESPONSE As String = "RESPONSE"
        Public Const TRANSLATED_SCORE As String = "TRANSLATED_SCORE"
        Public Const RAW_SCORE As String = "RAW_SCORE"
        Public Const SCORE_RESPONSE As String = "SCORERESPONSE"
        Public Const CONCEPT_RESPONSE As String = "CONCEPTRESPONSE"
        Public Const DECIMAL_RESPONSE As String = "DECIMALRESPONSE"
        Public Const SCORE_FINDING As String = "SCOREFINDING"
        Public Const MEDIACUSTOMRESPONSE As String = "MEDIACUSTOMRESPONSE"
        Public Const GENERATED_CSS As String = "cito_generated.css"
        Public Const PASSING_THRESHOLD As String = "PASSING_THRESHOLD"
        Public Const MAXSCORE As String = "MAXSCORE"
        Public Const PASSED As String = "PASSED"
        Public Const WORKFLOW As String = "WORKFLOW"
        Public Const IMSMANIFEST As String = "imsmanifest"


        Public Enum FileDirectoryType
            items = 0
            img = 1
            video = 2
            controlxsds = 3
            css = 4
            ref = 6
            audio = 7
            adaptive = 8
            attachments = 9
            template = 10
            pci = 11
            other = 99
        End Enum

        Public Enum TypeOfResource
            item = 0
            test = 1
            resource = 2
            manifest = 3
        End Enum

        Public Enum PackageType
            ItemPreview
            TestPreview
            TestPublication
            TestPackagePublication
            ItemScreenshot
        End Enum

    End Class

End Namespace