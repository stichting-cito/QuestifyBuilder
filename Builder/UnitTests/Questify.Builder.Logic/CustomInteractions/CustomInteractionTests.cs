
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.CustomInteractions;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UnitTests.Framework.Faketory;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.CustomInteractions
{

    [TestClass]
    public class CustomInteractionTests
    {
        private byte[] zipData;

        [TestInitialize]
        public void Init()
        {
            FakeFactory.FakeServices.SetupFakeServices();
            var service = FakeFactory.FakeServices.FakeResourceService;
            A.CallTo(() => service.GetResourceByNameWithOption(A<int>.Ignored, A<string>.Ignored, A<ResourceRequestDTO>.Ignored)).ReturnsLazily(x => ReturnData());
        }

        [TestCleanup()]
        public void DeInit()
        {
            FakeFactory.FakeServices.CleanFakeServices();
            zipData = new byte[] { 0 };
        }

        [TestMethod, TestCategory("customInteractionProcessing")]
        public void ParameterCollection_HasContractName()
        {
            var assessment = new AssessmentItem();
            var prmColl = new ParameterCollection();
            prmColl.InnerParameters.Add(new PlainTextParameter() { Name = "controllerId", Value = Guid.NewGuid().ToString() });
            prmColl.InnerParameters.Add(new CustomInteractionResourceParameter() { Name = "sourceCI", Scorable = true });
            assessment.Parameters.Add(prmColl);
            SetUpZipPackage(two_Coordinates);
            CustomInteraction.AddParameters("", 1, assessment.Parameters, assessment.Solution);
            Assert.AreEqual(2, assessment.Parameters.Count);
            Assert.AreEqual(IdentifierHelper.CI_ParameterCollectionName, assessment.Parameters[1].Id);
        }

        [TestMethod, TestCategory("customInteractionProcessing")]
        public void ParameterCollection_2DecimalScoringParameters()
        {
            var assessment = new AssessmentItem();
            var prmColl = new ParameterCollection();
            prmColl.InnerParameters.Add(new PlainTextParameter() { Name = "controllerId", Value = Guid.NewGuid().ToString() });
            prmColl.InnerParameters.Add(new CustomInteractionResourceParameter() { Name = "sourceCI", Scorable = true });
            assessment.Parameters.Add(prmColl);
            SetUpZipPackage(two_Coordinates);
            CustomInteraction.AddParameters("", 1, assessment.Parameters, assessment.Solution);
            var innerParams = assessment.Parameters[1].InnerParameters;
            Assert.AreEqual(2, innerParams.Count);
            Assert.IsTrue(innerParams.All(param => param is DecimalScoringParameter));
        }

        [TestMethod, TestCategory("customInteractionProcessing")]
        public void ParameterCollection_DefinitionShouldBeInitialized()
        {
            var assessment = new AssessmentItem();
            var prmColl = new ParameterCollection();
            prmColl.InnerParameters.Add(new PlainTextParameter() { Name = "controllerId", Value = Guid.NewGuid().ToString() });
            prmColl.InnerParameters.Add(new CustomInteractionResourceParameter() { Name = "sourceCI", Scorable = true });
            assessment.Parameters.Add(prmColl);
            SetUpZipPackage(two_Coordinates);
            CustomInteraction.AddParameters("", 1, assessment.Parameters, assessment.Solution);
            var scoringParameter = assessment.Parameters[1].InnerParameters.First(parameter => parameter is ScoringParameter);
            Assert.IsNotNull(scoringParameter);
            Assert.IsNotNull(((ScoringParameter)scoringParameter).BluePrint);
        }

        [TestMethod, TestCategory("customInteractionProcessing")]
        public void ParameterCollection_2GeogebraScoringParameters()
        {
            var assessment = new AssessmentItem();
            var prmColl = new ParameterCollection();
            prmColl.InnerParameters.Add(new PlainTextParameter() { Name = "controllerId", Value = Guid.NewGuid().ToString() });
            prmColl.InnerParameters.Add(new CustomInteractionResourceParameter() { Name = "sourceCI", Scorable = true });
            prmColl.InnerParameters.Add(new ListedParameter() { Name = "ciType", Value = "ggb" });
            assessment.Parameters.Add(prmColl);
            SetUpZipPackage(GeogebraXml.ToString(), true);
            CustomInteraction.AddParameters("", 1, assessment.Parameters, assessment.Solution);
            var innerParams = assessment.Parameters[1].InnerParameters;
            Assert.AreEqual(2, innerParams.Count);
            Assert.IsTrue(innerParams.All(param => param is GeogebraScoringParameter));
        }

        private void SetUpZipPackage(string metadata, bool geogebraPackage = false)
        {
            using (var compressStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(compressStream, ZipArchiveMode.Create))
                {
                    var zipEntry = zipArchive.CreateEntry("metadata.json", CompressionLevel.NoCompression);
                    if (geogebraPackage) zipEntry = zipArchive.CreateEntry("geogebra.xml", CompressionLevel.NoCompression);

                    using (StreamWriter writer = new StreamWriter(zipEntry.Open()))
                    {
                        writer.Write(metadata);
                    }
                }
                zipData = compressStream.ToArray();
            }
        }

        private ResourceEntity ReturnData()
        {
            var ret = new ResourceEntity { };
            ret.ResourceData = new ResourceDataEntity();
            ret.ResourceData.BinData = zipData;
            return ret;
        }


        private readonly string two_Coordinates = @"{  
                                                        ""title"":""customInteraction coordinates"",
                                                        ""width"":400,
                                                        ""height"":200,
                                                        ""scalable"":false,
                                                        ""scoring"":[  
                                                            {  
                                                                ""coordinate"":{  
                                                                ""label"":""coordinate 1"",
                                                                ""correctResponse"":""(x:5.2)(y:5.2)""
                                                                }
                                                            },
                                                            {  
                                                                ""coordinate"":{  
                                                                ""label"":""coordinate 2"",
                                                                ""correctResponse"":""(x:10.1)(y:10.1)""
                                                                }
                                                            }
                                                        ]
                                                    }";

        private XElement GeogebraXml = XElement.Parse(@"<geogebra format=""4.2"" version=""4.2.51.0"" id=""664cc608-d709-4f58-8112-9384766fd6e3"" xsi:noNamespaceSchemaLocation=""http://www.geogebra.org/ggb.xsd"" xmlns="""" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
                                          <gui>
                                              <window width=""1296"" height=""786""/>
                                              <perspectives>
                                                  <perspective id=""tmp"">
                                                      <panes>
                                                          <pane location="""" divider=""0.10185185185185185"" orientation=""1""/>
                                                      </panes>
                                                      <views>
                                                          <view id=""4097"" visible=""false"" inframe=""true"" stylebar=""true"" location=""1,1,1"" size=""400"" window=""264,65,735,640""/>
                                                          <view id=""4"" toolbar=""0 59 || 2020 , 2021 , 2022 , 66 || 2001 , 2003 , 2002 , 2004 , 2005 || 2040 , 2041 , 2042 , 2044 , 2043"" visible=""false"" inframe=""false"" stylebar=""false"" location=""1,1"" size=""300"" window=""100,100,600,400""/>
                                                          <view id=""8"" toolbar=""1001 | 1002 | 1003  || 1005 | 1004 || 1006 | 1007 | 1010 | 1011 || 1008 1009 || 6"" visible=""false"" inframe=""false"" stylebar=""false"" location=""1,3"" size=""300"" window=""100,100,600,400""/>
                                                          <view id=""1"" visible=""true"" inframe=""false"" stylebar=""true"" location=""1"" size=""1269"" window=""100,100,600,400""/>
                                                          <view id=""2"" visible=""false"" inframe=""false"" stylebar=""false"" location=""3"" size=""132"" window=""100,100,250,400""/>
                                                          <view id=""16"" visible=""false"" inframe=""false"" stylebar=""false"" location=""1"" size=""150"" window=""50,50,500,500""/>
                                                          <view id=""32"" visible=""false"" inframe=""false"" stylebar=""true"" location=""1"" size=""150"" window=""50,50,500,500""/>
                                                          <view id=""64"" visible=""false"" inframe=""true"" stylebar=""true"" location=""1"" size=""150"" window=""50,50,500,500""/>
                                                          <view id=""70"" toolbar=""0 || 2020 || 2021 || 2022"" visible=""false"" inframe=""true"" stylebar=""true"" location=""1"" size=""150"" window=""50,50,500,500""/>
                                                      </views>
                                                      <toolbar show=""true"" items=""0 39 59 | 1 501 67 , 5 19 , 72 | 2 15 45 , 18 65 , 7 37 | 4 3 8 9 , 13 44 , 58 , 47 | 16 51 64 , 70 | 10 34 53 11 , 24  20 22 , 21 23 | 55 56 57 , 12 | 36 46 , 38 49 50 , 71 | 30 29 54 32 31 33 | 17 26 62 73 , 14 66 68 | 25 52 60 61 | 40 41 42 , 27 28 35 , 6"" position=""1"" help=""false""/>
                                                      <input show=""false"" cmd=""true"" top=""false""/>
                                                      <dockBar show=""true"" east=""true""/>
                                                  </perspective>
                                              </perspectives>
                                              <labelingStyle val=""3""/>
                                              <font size=""20""/>
                                              <graphicsSettings javaLatexFonts=""false""/>
                                          </gui>
                                          <euclidianView>
                                              <size width=""1269"" height=""644""/>
                                              <coordSystem xZero=""85.99999999999999"" yZero=""505.9999999999994"" scale=""49.999999999999595"" yscale=""49.99999999999995""/>
                                              <evSettings axes=""true"" grid=""true"" gridIsBold=""false"" pointCapturing=""3"" rightAngleStyle=""2"" checkboxSize=""13"" gridType=""0""/>
                                              <bgColor r=""255"" g=""255"" b=""255""/>
                                              <axesColor r=""0"" g=""0"" b=""0""/>
                                              <gridColor r=""192"" g=""192"" b=""192""/>
                                              <lineStyle axes=""3"" grid=""0""/>
                                              <axis id=""0"" show=""true"" label="""" unitLabel="""" tickStyle=""1"" showNumbers=""true""/>
                                              <axis id=""1"" show=""true"" label="""" unitLabel="""" tickStyle=""1"" showNumbers=""true""/>
                                          </euclidianView>
                                          <kernel>
                                              <continuous val=""false""/>
                                              <usePathAndRegionParameters val=""true""/>
                                              <decimals val=""2""/>
                                              <angleUnit val=""degree""/>
                                              <algebraStyle val=""0""/>
                                              <coordStyle val=""0""/>
                                              <angleFromInvTrig val=""false""/>
                                          </kernel>
                                          <scripting blocked=""false"" disabled=""false""/>
                                          <construction title=""M1 WI H03 Schatzoeken"" author="""" date=""26 August 2013"">
                                              <command name=""Segment"">
                                                  <input a0=""A"" a1=""B""/>
                                                  <output a0=""b""/>
                                              </command>
                                              <element type=""segment"" label=""b"">
                                                  <show object=""true"" label=""false""/>
                                                  <objColor r=""153"" g=""51"" b=""0"" alpha=""0.0""/>
                                                  <layer val=""0""/>
                                                  <labelMode val=""0""/>
                                                  <coords x=""-0.52"" y=""-0.4800000000000004"" z=""7.16""/>
                                                  <lineStyle thickness=""13"" type=""0"" typeHidden=""1""/>
                                                  <outlyingIntersections val=""false""/>
                                                  <keepTypeOnTransform val=""true""/>
                                              </element>
                                              <expression label=""geoA"" exp=""Sleutel 1""/>
                                              <element type=""point"" label=""P"">
                                                  <show object=""true"" label=""false""/>
                                                  <condition showObject=""Distance[P, M] &lt; 0.2""/>
                                                  <objColor r=""0"" g=""0"" b=""255"" alpha=""0.0""/>
                                                  <layer val=""0""/>
                                                  <labelMode val=""0""/>
                                                  <coords x=""10.0"" y=""4.0"" z=""1.0""/>
                                                  <pointSize val=""7""/>
                                                  <pointStyle val=""1""/>
                                              </element>
                                              <command name=""Point"">
                                                  <input a0=""a""/>
                                                  <output a0=""M""/>
                                              </command>
                                              <expression label=""geoB"" exp=""Sleutel 2""/>
                                              <element type=""text"" label=""Text1_1"">
                                                  <show object=""true"" label=""false""/>
                                                  <objColor r=""255"" g=""0"" b=""0"" alpha=""0.0""/>
                                                  <layer val=""0""/>
                                                  <labelMode val=""0""/>
                                                  <fixed val=""true""/>
                                                  <font serif=""false"" sizeM=""1.0"" size=""0"" style=""1""/>
                                                  <startPoint x=""1.0800000000000143"" y=""-1.0600000000000191"" z=""1.0""/>
                                              </element>
                                              <element type=""image"" label=""afbeelding1"">
                                                  <file name=""3e82102a20163b92fc66fdbcd871e3cc\wit papier.jpg""/>
                                                  <inBackground val=""false""/>
                                                  <startPoint number=""0"" x=""-2.1600000000000135"" y=""-4.120000000000017"" z=""1.0""/>
                                                  <show object=""true"" label=""false""/>
                                                  <objColor r=""0"" g=""0"" b=""0"" alpha=""1.0""/>
                                                  <layer val=""3""/>
                                                  <labelMode val=""0""/>
                                                  <fixed val=""true""/>
                                              </element>
                                          </construction>
                                      </geogebra>");

    }
}
