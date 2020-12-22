
Option Infer On

Imports System.Text
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports FakeItEasy

<TestClass()>
Public Class RefreshResource

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub RefreshResourceParameter_HandlerShouldBeCalledOnce()
        Dim sourcetxt As New XhtmlResourceParameter() With {.Name = "sourcetxt", .Value = "Test"}
        Dim data As String = "The story"
        Dim handler = A.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).Invokes(Sub(f)
                                                                                                                Dim e = f.GetArgument(Of ResourceNeededEventArgs)(1)
                                                                                                                e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, Encoding.UTF8.GetBytes(data), Nothing)
                                                                                                            End Sub)
        AddHandler sourcetxt.ResourceNeeded, handler

        sourcetxt.RefreshResource()

        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).MustHaveHappened(Repeated.Exactly.Once)

        Assert.AreEqual(data, Encoding.UTF8.GetString(sourcetxt.Resource))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub RefreshResourceParameter_ReferencesShouldBeSet()
        Dim sourcetxt As New XhtmlResourceParameter() With {.Name = "sourcetxt", .Value = "Test"}
        Dim data As String = "The story"
        Dim handler = A.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).Invokes(Sub(f)
                                                                                                                Dim e = f.GetArgument(Of ResourceNeededEventArgs)(1)
                                                                                                                e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, Encoding.UTF8.GetBytes(data), Nothing)
                                                                                                            End Sub)
        AddHandler sourcetxt.ResourceNeeded, handler

        sourcetxt.RefreshResource()

        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).MustHaveHappened(Repeated.Exactly.Once)

        Assert.AreEqual(data, Encoding.UTF8.GetString(sourcetxt.Resource))
    End Sub

End Class
