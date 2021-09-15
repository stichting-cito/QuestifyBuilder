
Imports System.Xml.Linq
Imports Cito.Tester.Common

<TestClass()>
Public Class ParseHtmlReferenceTest

    <TestMethod()>
    <TestCategory("XHtmlReferences")>
    Public Sub ParseHighlightReferentie()
        'Arrange
        Dim result As XhtmlReferenceList
        Dim parse As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                    <head>
                                        <title>Document Title 2</title>
                                        <link href="resource://package:1/SomeStylesheet.css" rel="StyleSheet" type="text/css" media="screen"/>
                                        <style type="text/css"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                    </head>
                                    <body style="padding: 2px; margin : 0px; width: 100%;">
                                        <p class="UserSRTitelGroot">De buitengesloten piloot </p>
                                        <p>Kies de 
                                <span id="refbbc82192-bc3f-4f1e-96d1-e76702ba73e5"
                                    contenteditable="false"
                                    cito:type="reference"
                                    cito:reftype="Highlight"
                                    cito:description="juiste"
                                    cito:value="1"
                                    xmlns:cito="http://www.cito.nl/citotester">juiste</span> koers. </p>
                                        <p></p>
                                    </body>
                                </html>
       
        'Act
        result = XhtmlReferenceFactory.ParseXhtmlReference(parse.ToString(), XhtmlReferenceType.Highlight)
        
        'Assert
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(XhtmlReferenceType.Highlight, result(0).Type)
    End Sub

    <TestMethod()>
    <TestCategory("XHtmlReferences")>
    Public Sub ParsePartialData()
        'Arrange
        Dim result As XhtmlReferenceList
        Dim parse As XElement = <p>Kies de 
                                <span id="refbbc82192-bc3f-4f1e-96d1-e76702ba73e5"
                                    contenteditable="false"
                                    cito:type="reference"
                                    cito:reftype="Highlight"
                                    cito:description="juiste"
                                    cito:value="1"
                                    xmlns:cito="http://www.cito.nl/citotester"
                                    xmlns="http://www.w3.org/1999/xhtml">juiste</span> koers. </p>
     
        'Act
        result = XhtmlReferenceFactory.ParseXhtmlReference(parse.ToString(), XhtmlReferenceType.Highlight)
     
        'Assert
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(XhtmlReferenceType.Highlight, result(0).Type)
    End Sub


    <TestMethod()>
    <TestCategory("XHtmlReferences")>
    Public Sub ParsePartialData_WithoutNamespace_ExpectsNoResults()
        'Arrange
        'The [XhtmlReferenceList] assumes the Xhtml Namespace (http://www.w3.org/1999/xhtml) for [span] objects.
        'As it is no reference should be found
        Dim result As XhtmlReferenceList
        Dim parse As XElement = <p>Kies de 
                                <span id="refbbc82192-bc3f-4f1e-96d1-e76702ba73e5"
                                    contenteditable="false"
                                    cito:type="reference"
                                    cito:reftype="Highlight"
                                    cito:description="juiste"
                                    cito:value="1"
                                    xmlns:cito="http://www.cito.nl/citotester">juiste</span> koers. </p>
        
        'Act
        result = XhtmlReferenceFactory.ParseXhtmlReference(parse.ToString(), XhtmlReferenceType.Highlight)
        
        'Assert
        Assert.AreEqual(0, result.Count) 'No result have been found
    End Sub

    <TestMethod()>
    <TestCategory("XHtmlReferences")>
    Public Sub ParseElementReferentie()
        'Arrange
        Dim result As XhtmlReferenceList
        Dim parse As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                    <head>
                                        <title>Document Title 5</title>
                                        <link href="resource://package:1/SomeStylesheet.css" rel="StyleSheet" type="text/css" media="screen"/>
                                        <style type="text/css"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                    </head>
                                    <body style="padding: 2px; margin : 0px; width: 100%;">
                                        <p class="UserSRTitelGroot">De buitengesloten piloot </p>
                                        <p>Zodra kruishoogte en 
                                            <span id="ref26ba4d25-0484-41c9-9de6-c7f46e711b36"
                                                contenteditable="false"
                                                cito:type="reference"
                                                cito:reftype="Element"
                                                cito:description="Element 1"
                                                cito:value="1"
                                                xmlns:cito="http://www.cito.nl/citotester"><u>    1    </u></span>hij even de benen in de passagiersruimte.</p>
                                        <p></p>
                                    </body>
                                </html>
        
        'Act
        result = XhtmlReferenceFactory.ParseXhtmlReference(parse.ToString(), XhtmlReferenceType.Element)
        
        'Assert
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(XhtmlReferenceType.Element, result(0).Type)
    End Sub


    <TestMethod()>
    <TestCategory("XHtmlReferences")>
    Public Sub ParseSymbolReferentie()
        'Arrange
        Dim result As XhtmlReferenceList
        Dim parse As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                    <head>
                                        <title>Document Title 5</title>
                                        <link href="resource://package:1/SomeStylesheet.css" rel="StyleSheet" type="text/css" media="screen"/>
                                        <style type="text/css"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                    </head>
                                    <body style="padding: 2px; margin : 0px; width: 100%;">
                                        <p class="UserSRTitelGroot">De buitengesloten piloot </p>
                                        <p>Zodra kruishoogte en -snelheid bereikt zijn, strekt hij even de benen in de 
                                            <span id="refeb4b39bd-d767-461c-bf9f-70c780198ed4"
                                                contenteditable="false"
                                                cito:type="reference"
                                                cito:reftype="Symbol"
                                                cito:description="passagiersruimte"
                                                cito:value="resource://package/referencesymbol1"
                                                xmlns:cito="http://www.cito.nl/citotester">passagiersruimte</span></p>
                                    </body>
                                </html>
       
        'Act
        result = XhtmlReferenceFactory.ParseXhtmlReference(parse.ToString(), XhtmlReferenceType.Symbol)
       
        'Assert
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(XhtmlReferenceType.Symbol, result(0).Type)
    End Sub

    <TestMethod()>
    <TestCategory("XHtmlReferences")>
    Public Sub ParseAllReferentie()
        'Arrange
        Dim result As XhtmlReferenceList
        Dim parse As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                    <head>
                                        <title>Document Title 5</title>
                                        <link href="resource://package:1/SomeStylesheet.css" rel="StyleSheet" type="text/css" media="screen"/>
                                        <style type="text/css"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                    </head>
                                    <body style="padding: 2px; margin : 0px; width: 100%;">
                                        <p class="UserSRTitelGroot">De buitengesloten piloot </p>
                                        <p>Zodra kruishoogte en -snelheid bereikt zijn, strekt hij even de benen in de 
                                            <span id="refeb4b39bd-d767-461c-bf9f-70c780198ed4"
                                                contenteditable="false"
                                                cito:type="reference"
                                                cito:reftype="Symbol"
                                                cito:description="passagiersruimte"
                                                cito:value="resource://package/referencesymbol1"
                                                xmlns:cito="http://www.cito.nl/citotester">passagiersruimte</span></p>
                                        <p>Zodra kruishoogte en 
                                            <span id="ref26ba4d25-0484-41c9-9de6-c7f46e711b36"
                                                contenteditable="false"
                                                cito:type="reference"
                                                cito:reftype="Element"
                                                cito:description="Element 1"
                                                cito:value="1"
                                                xmlns:cito="http://www.cito.nl/citotester"><u>    1    </u></span>hij even de benen in de passagiersruimte.</p>
                                        <p>Kies de 
                                <span id="refbbc82192-bc3f-4f1e-96d1-e76702ba73e5"
                                    contenteditable="false"
                                    cito:type="reference"
                                    cito:reftype="Highlight"
                                    cito:description="juiste"
                                    cito:value="1"
                                    xmlns:cito="http://www.cito.nl/citotester">juiste</span> koers. </p>

                                    </body>
                                </html>
       
        'Act
        result = XhtmlReferenceFactory.ParseXhtmlReference(parse.ToString())
        
        'Assert
        Assert.AreEqual(3, result.Count)

        Assert.AreEqual(XhtmlReferenceType.Symbol, result(0).Type)
        Assert.AreEqual(XhtmlReferenceType.Element, result(1).Type)
        Assert.AreEqual(XhtmlReferenceType.Highlight, result(2).Type)
    End Sub

End Class
