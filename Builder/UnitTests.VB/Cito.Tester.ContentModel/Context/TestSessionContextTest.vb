
Option Infer On

Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class TestSessionContextTest

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSessionContextPopinAndDisposeSessionContext_Test()
        Dim tempSessionContext As New SessionContext()

        Using p As New SessionContextProvider(tempSessionContext)
            Assert.IsTrue(ReferenceEquals(tempSessionContext, SessionContextProvider.CurrentSession), "The created temporary SessionContext is not used!")
            Assert.IsTrue(ReferenceEquals(tempSessionContext, TestSessionContext.CurrentSessionContext), "The created temporary SessionContext is not used!")
        End Using

        Assert.IsFalse(ReferenceEquals(tempSessionContext, SessionContextProvider.CurrentSession), "The tempSessionContext instance is not cleaned up!")
        Assert.IsFalse(ReferenceEquals(tempSessionContext, TestSessionContext.CurrentSessionContext), "The tempSessionContext instance is not cleaned up!")

    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <Description("The Cause of..")>
    Public Sub TestSessionContextNeedsToConvertEscapedCharsUsingCustomSessionContext_Test()
        Dim tempSessionContext As New SessionContext()

        'Arrange
        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource("") 'DUMMY
                       result = e.ResourceName
                   End Sub

        AddHandler tempSessionContext.ResourceNeeded, evnt
        tempSessionContext.GetResourceObject("00015_5-6_7-8_vsopro_bl_wonen%20in%20china_popupklein.jpg", Nothing)
        RemoveHandler tempSessionContext.ResourceNeeded, evnt

        'Assert
        Assert.AreNotEqual(tempSessionContext, TestSessionContext.CurrentSessionContext)
        Assert.IsFalse(String.IsNullOrEmpty(result))
        Assert.IsFalse(result.Contains("%20"), "Escaped chars should not be here.")
    End Sub

    <TestMethod()>
    Public Sub CreateLooseSessionContext_ShouldNotBeAccessible()
        'Arrange
        Dim a = New AssessmentItem With {.Title = "Some title..."}
        Dim newContext = New SessionContext
        
        'Note I've not used SessionContextProvider to use the sessionContext.
        'Act
        newContext.CurrentItem = a
        
        'Assert
        Assert.AreNotSame(TestSessionContext.CurrentItem, newContext.CurrentItem, "Should not be equal since sessionContext has not been used.")
    End Sub

    <TestMethod()>
    Public Sub CreateSessionContext_ShouldNotBeShared()
        'Arrange
        Dim a = New AssessmentItem With {.Title = "Some title..."}

        Dim newContext = New SessionContext
        Using p = New SessionContextProvider(newContext)
            'Act
            newContext.CurrentItem = a
            
            'Assert
            Assert.AreSame(TestSessionContext.CurrentItem, newContext.CurrentItem, "Should not be equal since sessionContext has not been used.")
        End Using
    End Sub

    <TestMethod()>
    Public Sub WhenUsingClauseIsGone_Revert()
        'Arrange
        Dim a = New AssessmentItem With {.Title = "Some title..."}

        Dim newContext = New SessionContext
        Using p = New SessionContextProvider(newContext)
            'Act
            newContext.CurrentItem = a
        End Using
       
        'Assert
        Assert.AreNotSame(TestSessionContext.CurrentItem, newContext.CurrentItem, "Should not be equal since sessionContext has not been used.")
    End Sub

    <TestMethod(), WorkItem(8970), WorkItem(8964)> <TestCategory("ContentModel")> <Description("The Cause of..")>
    Public Sub TestSessionContextNeedsToConvertEscapedChars_Test()
        'Arrange
        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource("") 'DUMMY
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        TestSessionContext.GetResourceObject("00015_5-6_7-8_vsopro_bl_wonen%20in%20china_popupklein.jpg", Nothing)
        
        'Assert
        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        Assert.IsFalse(String.IsNullOrEmpty(result))
        Assert.IsFalse(result.Contains("%20"), "Escaped chars should not be here.")
    End Sub

    <TestMethod(), WorkItem(8970), WorkItem(8964)> <TestCategory("ContentModel")> <Description("Fix also applied to GetResourceMetaData")>
    Public Sub TestSessionContextNeedsToConvertEscapedChars_GetResourceMetaData_Test()
        'Arrange
        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource("") 'DUMMY
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        'Act
        TestSessionContext.GetResourceMetaData("00015_5-6_7-8_vsopro_bl_wonen%20in%20china_popupklein.jpg")
        
        'Assert
        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        Assert.IsFalse(String.IsNullOrEmpty(result))
        Assert.IsFalse(result.Contains("%20"), "Escaped chars should not be here.")
    End Sub

    <TestMethod(), WorkItem(8970), WorkItem(8964)> <TestCategory("ContentModel")> <Description("Fix also applied to GetResourceMetaDataValue")>
    Public Sub TestSessionContextNeedsToConvertEscapedChars_GetResourceMetaDataValue_Test()
        'Arrange
        Dim result As String = String.Empty
        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       e.BinaryResource = New BinaryResource("") 'DUMMY
                       result = e.ResourceName
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt
        
        'Act
        TestSessionContext.GetResourceMetaDataValue("00015_5-6_7-8_vsopro_bl_wonen%20in%20china_popupklein.jpg", "metedatanaam")
        
        'Assert
        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        Assert.IsFalse(String.IsNullOrEmpty(result))
        Assert.IsFalse(result.Contains("%20"), "Escaped chars should not be here.")
    End Sub

End Class
