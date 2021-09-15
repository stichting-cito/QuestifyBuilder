
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel

<TestClass()>
Public MustInherit Class GenericTestBase(Of T As CommonEntityBase)

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub WhenSettingFieldToWrongValue_IDataErrorInfo_HasError()
        'Arrange
        Dim obj As T = CreateTheObject()
        Dim dataError As IDataErrorInfo = DirectCast(obj, IDataErrorInfo)
        Dim ex As Exception = Nothing

        'Act
        Try
            SetErroneousParam(obj)
        Catch e As Exception
            ex = e
        End Try

        'Assert
        'Two possibilities, exception or IDataErrorInfo functionality.
        If ex IsNot Nothing Then
            Assert.IsTrue(TypeOf ex Is SD.LLBLGen.Pro.ORMSupportClasses.ORMEntityValidationException, "Exception that can be thrown in severe cases.")
        End If
        Assert.IsFalse(String.IsNullOrEmpty(dataError.Error), String.Format("Object [{0}] is not in global error modus.", GetType(T)))
    End Sub


    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub ItemResourceIsSerialisable_UsedForMultiEditClone()
        'Arrange
        Dim result As ItemResourceEntity = BinaryCloner.DeepClone(Of ItemResourceEntity)(New ItemResourceEntity())
        Using ms As New IO.MemoryStream
            SerializeHelper.XmlSerializeToStream(ms, New ItemResourceEntity)
            ms.Seek(0, IO.SeekOrigin.Begin)
            
            'Act
            Try
                result = DirectCast(SerializeHelper.XmlDeserializeFromStream(ms, GetType(ItemResourceEntity)), ItemResourceEntity)
            Catch

            End Try
        End Using

        Assert.IsFalse(result Is Nothing)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub WhenSettingFieldToWrongValueAndThenToCorrectValue_IDataErrorInfo_HasNoError()
        'Arrange
        Dim obj As T = CreateTheObject()
        Dim dataError As IDataErrorInfo = DirectCast(obj, IDataErrorInfo)
        Dim ex As Exception = Nothing

        'Act
        Try
            SetErroneousParam(obj)
        Catch e As Exception
            ex = e
        End Try
        Try
            SetCorrectParam(obj)
        Catch e As Exception
            Assert.Fail() 'SHOULD NOT OCCUR!
        End Try

        obj.ValidateEntity()


        'Assert
        'Two possibilities, exception or IDataErrorInfo functionality.
        If ex IsNot Nothing Then
            Assert.IsTrue(TypeOf ex Is SD.LLBLGen.Pro.ORMSupportClasses.ORMEntityValidationException, "Exception that can be thrown in severe cases.")
        End If
        Assert.IsTrue(String.IsNullOrEmpty(dataError.Error), String.Format("Object [{0}] is STILL in global error modus.", GetType(T)))
    End Sub

    Protected MustOverride Function CreateTheObject() As T
    Protected MustOverride Sub SetErroneousParam(obj As T)
    Protected MustOverride Sub SetCorrectParam(obj As T)


End Class
