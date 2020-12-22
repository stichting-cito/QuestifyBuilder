Imports System.Activities
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Namespace CustomInteractions.Flow

    Public NotInheritable Class GetTypeOfCustomInteraction
        Inherits CodeActivity(Of String)
        Public Property ParameterSetCollection As InArgument(Of ParameterSetCollection)

        Public Property InlineParameters As InArgument(Of ParameterSetCollection)

        Protected Overrides Function Execute(context As CodeActivityContext) As String
            Dim parameters As ParameterSetCollection = context.GetValue(ParameterSetCollection)
            Dim inlinePars As ParameterSetCollection = context.GetValue(InlineParameters)
            Dim resultValue As String = String.Empty

            If inlinePars IsNot Nothing Then
                If inlinePars.FlattenParameters().ToList().Any(Function(p) p.Name IsNot Nothing AndAlso p.Name.StartsWith("source", StringComparison.OrdinalIgnoreCase)) Then
                    Dim prm As ParameterBase = inlinePars.FlattenParameters().ToList().First(Function(p) p.Name IsNot Nothing AndAlso p.Name.StartsWith("source", StringComparison.OrdinalIgnoreCase))
                    If prm IsNot Nothing AndAlso prm.[GetType]() Is GetType(CustomInteractionResourceParameter) Then
                        Dim fileMimeType As String = FileHelper.GetMimeFromFile(DirectCast(prm, CustomInteractionResourceParameter).Value)
                        If fileMimeType = "application/vnd.geogebra.file" Then
                            Return "ggb"
                        End If
                    End If
                End If
            ElseIf parameters IsNot Nothing AndAlso parameters.FlattenParameters().ToList().Any(Function(p) p.Name.Equals("ciType", StringComparison.OrdinalIgnoreCase)) Then
                Dim ciType As ParameterBase = parameters.FlattenParameters().ToList().First(Function(p) p.Name.Equals("ciType", StringComparison.OrdinalIgnoreCase))
                If ciType IsNot Nothing Then
                    If ciType.[GetType]() Is GetType(ListedParameter) Then
                        Return DirectCast(ciType, ListedParameter).Value
                    End If
                End If
            End If
            Return resultValue
        End Function
    End Class
End NameSpace