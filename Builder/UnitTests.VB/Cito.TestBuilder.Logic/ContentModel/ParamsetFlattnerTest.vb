
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common

<TestClass()> Public Class ParamsetFlattnerTest

    <TestMethod(), TestCategory("Logic")> Public Sub FlattenEmpty()
        'Arrange
        Dim prmSet = GetemptySet()
        
        'Act
        Dim result = prmSet.FlattenParameters()
        
        'Assert
        Assert.AreEqual(0, result.Count())
    End Sub

    <TestMethod(), TestCategory("Logic")> Public Sub FlattenSetWith1ParameterSet()
        'Arrange
        Dim prmSet = get1ParamSet()
       
        'Act
        Dim result = prmSet.FlattenParameters()
        
        'Assert
        Assert.AreEqual(10, result.Count())
    End Sub


    <TestMethod(), TestCategory("Logic")> Public Sub FlattenSetWith2ParameterSet()
        'Arrange
        Dim prmSet = get2ParamSet()
        
        'Act
        Dim result = prmSet.FlattenParameters()
        
        'Assert
        Assert.AreEqual(20, result.Count())
    End Sub

    <TestMethod(), TestCategory("Logic")> Public Sub FlattenSetWith1ParameterSetWith1Coll()
        'Arrange
        Dim prmSet = get1ParmsetWithColl()
        
        'Act
        Dim result = prmSet.FlattenParameters()
        
        'Assert
        Assert.AreEqual(6, result.Count())
    End Sub

    Private Function GetemptySet() As ParameterSetCollection
        Dim ret = DirectCast(SerializeHelper.XmlDeserializeFromString(emptySet.ToString(), GetType(ParameterSetCollection)), ParameterSetCollection)
        Return ret
    End Function

    Private emptySet As XElement = <ArrayOfParameterCollection></ArrayOfParameterCollection>

    Private Function get1ParamSet() As ParameterSetCollection
        Dim ret = DirectCast(SerializeHelper.XmlDeserializeFromString(prm1Parmset.ToString(), GetType(ParameterSetCollection)), ParameterSetCollection)
        Return ret
    End Function

    Private prm1Parmset As XElement = <ArrayOfParameterCollection>
                                          <ParameterCollection id="kenmerken">
                                              <booleanparameter name="1">False</booleanparameter>
                                              <integerparameter name="2"/>
                                              <booleanparameter name="3">False</booleanparameter>
                                              <plaintextparameter name="4"/>
                                              <listedparameter name="5">basic</listedparameter>
                                              <booleanparameter name="6"/>
                                              <booleanparameter name="7"/>
                                              <plaintextparameter name="8"/>
                                              <booleanparameter name="9"/>
                                              <plaintextparameter name="10"/>
                                          </ParameterCollection>
                                      </ArrayOfParameterCollection>

    Private Function get2ParamSet() As ParameterSetCollection
        Dim ret = DirectCast(SerializeHelper.XmlDeserializeFromString(prm2Parmset.ToString(), GetType(ParameterSetCollection)), ParameterSetCollection)
        Return ret
    End Function

    Private prm2Parmset As XElement = <ArrayOfParameterCollection>
                                          <ParameterCollection id="kenmerken">
                                              <booleanparameter name="1">False</booleanparameter>
                                              <integerparameter name="2"/>
                                              <booleanparameter name="3">False</booleanparameter>
                                              <plaintextparameter name="4"/>
                                              <listedparameter name="5">basic</listedparameter>
                                              <booleanparameter name="6"/>
                                              <booleanparameter name="7"/>
                                              <plaintextparameter name="8"/>
                                              <booleanparameter name="9"/>
                                              <plaintextparameter name="10"/>
                                          </ParameterCollection>
                                          <ParameterCollection id="kenmerken2">
                                              <booleanparameter name="1">False</booleanparameter>
                                              <integerparameter name="2"/>
                                              <booleanparameter name="3">False</booleanparameter>
                                              <plaintextparameter name="4"/>
                                              <listedparameter name="5">basic</listedparameter>
                                              <booleanparameter name="6"/>
                                              <booleanparameter name="7"/>
                                              <plaintextparameter name="8"/>
                                              <booleanparameter name="9"/>
                                              <plaintextparameter name="10"/>
                                          </ParameterCollection>
                                      </ArrayOfParameterCollection>

    Private Function get1ParmsetWithColl() As ParameterSetCollection
        Dim ret = DirectCast(SerializeHelper.XmlDeserializeFromString(prm1ParmsetWithColl.ToString(), GetType(ParameterSetCollection)), ParameterSetCollection)
        Return ret
    End Function

    Private prm1ParmsetWithColl As XElement = <ArrayOfParameterCollection>
                                                  <ParameterCollection id="kenmerken">
                                                      <booleanparameter name="1">False</booleanparameter>
                                                      <integerparameter name="2"/>
                                                      <integerparameter name="3"/>
                                                      <collectionparameter name="col1">
                                                          <subparameterset id="subPrm1">
                                                              <listedparameter name="gapType">Integer</listedparameter>
                                                          </subparameterset>
                                                          <subparameterset id="subPrm2">
                                                              <listedparameter name="gapType">Integer</listedparameter>
                                                          </subparameterset>
                                                          <definition id="">
                                                              <listedparameter name="gapType"/>
                                                          </definition>
                                                      </collectionparameter>
                                                  </ParameterCollection>
                                              </ArrayOfParameterCollection>

End Class