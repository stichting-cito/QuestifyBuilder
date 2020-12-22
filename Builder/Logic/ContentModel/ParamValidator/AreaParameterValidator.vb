Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.ParamValidator
    Friend Class AreaParameterValidator : Inherits CollectionParameterValidator

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            Dim baseResult = MyBase.GetError()
            If DirectCast(Param, AreaParameter).ShapeList Is Nothing OrElse Not DirectCast(Param, AreaParameter).ShapeList.Count > 0 Then
                Dim label = Param.GetDesignerValue("label", String.Empty)
                If String.IsNullOrEmpty(label) Then label = Param.GetDesignerValue("group", String.Empty)
                If String.IsNullOrEmpty(label) Then label = Param.Name
                baseResult = baseResult.ToArray.Concat({String.Format(My.Resources.MandatoryEmptyShapelistMessage, label)}).ToList()
            End If
            Return baseResult
        End Function

        Public Overrides Function DoCheckIsValid() As Boolean
            Dim baseResult = MyBase.DoCheckIsValid()
            If Param.GetDesignerValue("required", False) Then
                baseResult = baseResult AndAlso DirectCast(Param, AreaParameter).ShapeList IsNot Nothing AndAlso DirectCast(Param, AreaParameter).ShapeList.Count > 0
            End If
            Return baseResult
        End Function

    End Class

End Namespace

