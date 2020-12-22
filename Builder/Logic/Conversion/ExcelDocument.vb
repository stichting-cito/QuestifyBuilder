Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.ExtendedProperties
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.VariantTypes

Namespace Conversion

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("OpenXML SDK Tools package", "v2.0")>
    Friend Class ExcelDocument
        Friend Sub CreatePackage(filePath As String)
            Using package As SpreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook)
                CreateParts(package)
            End Using
        End Sub

        Private Sub CreateParts(document As SpreadsheetDocument)
            Dim extendedFilePropertiesPart1 As ExtendedFilePropertiesPart = document.AddNewPart(Of ExtendedFilePropertiesPart)("rId3")
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1)

            Dim workbookPart1 As WorkbookPart = document.AddWorkbookPart()
            GenerateWorkbookPart1Content(workbookPart1)

            Dim worksheetPart1 As WorksheetPart = workbookPart1.AddNewPart(Of WorksheetPart)("rId3")
            GenerateWorksheetPart1Content(worksheetPart1)

            Dim worksheetPart2 As WorksheetPart = workbookPart1.AddNewPart(Of WorksheetPart)("rId2")
            GenerateWorksheetPart2Content(worksheetPart2)

            Dim worksheetPart3 As WorksheetPart = workbookPart1.AddNewPart(Of WorksheetPart)("rId1")
            GenerateWorksheetPart3Content(worksheetPart3)

            Dim workbookStylesPart1 As WorkbookStylesPart = workbookPart1.AddNewPart(Of WorkbookStylesPart)("rId5")
            GenerateWorkbookStylesPart1Content(workbookStylesPart1)

            Dim themePart1 As ThemePart = workbookPart1.AddNewPart(Of ThemePart)("rId4")
            GenerateThemePart1Content(themePart1)

            SetPackageProperties(document)
        End Sub

        Private Sub GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1 As ExtendedFilePropertiesPart)
            Dim properties1 As New Properties()
            Dim application1 As New Application()
            application1.Text = "Microsoft Excel"
            Dim documentSecurity1 As New DocumentSecurity()
            documentSecurity1.Text = "0"
            Dim scaleCrop1 As New ScaleCrop()
            scaleCrop1.Text = "false"

            Dim headingPairs1 As New HeadingPairs()

            Dim vTVector1 As New VTVector() With { _
                    .BaseType = VectorBaseValues.[Variant], _
                    .Size = UInt32Value.FromUInt32(2UI) _
                    }

            Dim variant1 As New [Variant]()
            Dim vTLPSTR1 As New VTLPSTR()
            vTLPSTR1.Text = "Worksheets"

            variant1.Append(vTLPSTR1)

            Dim variant2 As New [Variant]()
            Dim vTInt321 As New VTInt32()
            vTInt321.Text = "3"

            variant2.Append(vTInt321)

            vTVector1.Append(variant1)
            vTVector1.Append(variant2)

            headingPairs1.Append(vTVector1)

            Dim titlesOfParts1 As New TitlesOfParts()

            Dim vTVector2 As New VTVector() With { _
                    .BaseType = VectorBaseValues.Lpstr, _
                    .Size = UInt32Value.FromUInt32(3UI) _
                    }
            Dim vTLPSTR2 As New VTLPSTR()
            vTLPSTR2.Text = "Sheet1"
            Dim vTLPSTR3 As New VTLPSTR()
            vTLPSTR3.Text = "Sheet2"
            Dim vTLPSTR4 As New VTLPSTR()
            vTLPSTR4.Text = "Sheet3"

            vTVector2.Append(vTLPSTR2)
            vTVector2.Append(vTLPSTR3)
            vTVector2.Append(vTLPSTR4)

            titlesOfParts1.Append(vTVector2)
            Dim company1 As New Company()
            company1.Text = "None"
            Dim linksUpToDate1 As New LinksUpToDate()
            linksUpToDate1.Text = "false"
            Dim sharedDocument1 As New SharedDocument()
            sharedDocument1.Text = "false"
            Dim hyperlinksChanged1 As New HyperlinksChanged()
            hyperlinksChanged1.Text = "false"
            Dim applicationVersion1 As New ApplicationVersion()
            applicationVersion1.Text = "12.0000"

            properties1.Append(application1)
            properties1.Append(documentSecurity1)
            properties1.Append(scaleCrop1)
            properties1.Append(headingPairs1)
            properties1.Append(titlesOfParts1)
            properties1.Append(company1)
            properties1.Append(linksUpToDate1)
            properties1.Append(sharedDocument1)
            properties1.Append(hyperlinksChanged1)
            properties1.Append(applicationVersion1)

            extendedFilePropertiesPart1.Properties = properties1
        End Sub

        Private Sub GenerateWorkbookPart1Content(workbookPart1 As WorkbookPart)
            Dim workbook1 As New Workbook()
            Dim fileVersion1 As New FileVersion() With { _
                    .ApplicationName = "xl", _
                    .LastEdited = "4", _
                    .LowestEdited = "4", _
                    .BuildVersion = "4506" _
                    }
            Dim workbookProperties1 As New WorkbookProperties() With { _
                    .DefaultThemeVersion = UInt32Value.FromUInt32(124226UI) _
                    }

            Dim bookViews1 As New BookViews()
            Dim workbookView1 As New WorkbookView() With { _
                    .XWindow = 0, _
                    .YWindow = 45, _
                    .WindowHeight = UInt32Value.FromUInt32(11820UI), _
                    .WindowWidth = UInt32Value.FromUInt32(19155UI) _
                    }

            bookViews1.Append(workbookView1)

            Dim sheets1 As New Sheets()
            Dim sheet1 As New Sheet() With { _
                    .Name = "Sheet1", _
                    .SheetId = UInt32Value.FromUInt32(1UI), _
                    .Id = "rId1" _
                    }
            Dim sheet2 As New Sheet() With { _
                    .Name = "Sheet2", _
                    .SheetId = UInt32Value.FromUInt32(2UI), _
                    .Id = "rId2" _
                    }
            Dim sheet3 As New Sheet() With { _
                    .Name = "Sheet3", _
                    .SheetId = UInt32Value.FromUInt32(3UI), _
                    .Id = "rId3" _
                    }

            sheets1.Append(sheet1)
            sheets1.Append(sheet2)
            sheets1.Append(sheet3)
            Dim calculationProperties1 As New CalculationProperties() With { _
                    .CalculationId = UInt32Value.FromUInt32(125725UI) _
                    }

            workbook1.Append(fileVersion1)
            workbook1.Append(workbookProperties1)
            workbook1.Append(bookViews1)
            workbook1.Append(sheets1)
            workbook1.Append(calculationProperties1)

            workbookPart1.Workbook = workbook1
        End Sub

        Private Sub GenerateWorksheetPart1Content(worksheetPart1 As WorksheetPart)
            Dim worksheet1 As New Worksheet()
            Dim sheetDimension1 As New SheetDimension() With { _
                    .Reference = "A1" _
                    }

            Dim sheetViews1 As New SheetViews()
            Dim sheetView1 As New SheetView() With { _
                    .WorkbookViewId = UInt32Value.FromUInt32(0UI) _
                    }

            sheetViews1.Append(sheetView1)
            Dim sheetFormatProperties1 As New SheetFormatProperties() With { _
                    .DefaultRowHeight = 15.0 _
                    }
            Dim sheetData1 As New SheetData()
            Dim pageMargins1 As New PageMargins() With { _
                    .Left = 0.7, _
                    .Right = 0.7, _
                    .Top = 0.75, _
                    .Bottom = 0.75, _
                    .Header = 0.3, _
                    .Footer = 0.3 _
                    }

            worksheet1.Append(sheetDimension1)
            worksheet1.Append(sheetViews1)
            worksheet1.Append(sheetFormatProperties1)
            worksheet1.Append(sheetData1)
            worksheet1.Append(pageMargins1)

            worksheetPart1.Worksheet = worksheet1
        End Sub

        Private Sub GenerateWorksheetPart2Content(worksheetPart2 As WorksheetPart)
            Dim worksheet2 As New Worksheet()
            Dim sheetDimension2 As New SheetDimension() With { _
                    .Reference = "A1" _
                    }

            Dim sheetViews2 As New SheetViews()
            Dim sheetView2 As New SheetView() With { _
                    .WorkbookViewId = UInt32Value.FromUInt32(0UI) _
                    }

            sheetViews2.Append(sheetView2)
            Dim sheetFormatProperties2 As New SheetFormatProperties() With { _
                    .DefaultRowHeight = 15.0 _
                    }
            Dim sheetData2 As New SheetData()
            Dim pageMargins2 As New PageMargins() With { _
                    .Left = 0.7, _
                    .Right = 0.7, _
                    .Top = 0.75, _
                    .Bottom = 0.75, _
                    .Header = 0.3, _
                    .Footer = 0.3 _
                    }

            worksheet2.Append(sheetDimension2)
            worksheet2.Append(sheetViews2)
            worksheet2.Append(sheetFormatProperties2)
            worksheet2.Append(sheetData2)
            worksheet2.Append(pageMargins2)

            worksheetPart2.Worksheet = worksheet2
        End Sub

        Private Sub GenerateWorksheetPart3Content(worksheetPart3 As WorksheetPart)
            Dim worksheet3 As New Worksheet()
            Dim sheetDimension3 As New SheetDimension() With { _
                    .Reference = "A1" _
                    }

            Dim sheetViews3 As New SheetViews()
            Dim sheetView3 As New SheetView() With { _
                    .TabSelected = True, _
                    .WorkbookViewId = UInt32Value.FromUInt32(0UI) _
                    }

            sheetViews3.Append(sheetView3)
            Dim sheetFormatProperties3 As New SheetFormatProperties() With { _
                    .DefaultRowHeight = 15.0 _
                    }
            Dim sheetData3 As New SheetData()
            Dim pageMargins3 As New PageMargins() With { _
                    .Left = 0.7, _
                    .Right = 0.7, _
                    .Top = 0.75, _
                    .Bottom = 0.75, _
                    .Header = 0.3, _
                    .Footer = 0.3 _
                    }

            worksheet3.Append(sheetDimension3)
            worksheet3.Append(sheetViews3)
            worksheet3.Append(sheetFormatProperties3)
            worksheet3.Append(sheetData3)
            worksheet3.Append(pageMargins3)

            worksheetPart3.Worksheet = worksheet3
        End Sub

        Private Sub GenerateWorkbookStylesPart1Content(workbookStylesPart1 As WorkbookStylesPart)
            Dim stylesheet1 As New Stylesheet()
            Dim fonts1 As Spreadsheet.Fonts = CreateFonts()
            Dim fills1 As Fills = CreateFills()
            Dim borders1 As Borders = CreateBorders()

            Dim cellFormats1 As CellFormats

            Dim cellStyleFormats1 As CellStyleFormats = CreateCellStyleFormats(cellFormats1)

            Dim cellStyles1 As New CellStyles() With { _
                    .Count = UInt32Value.FromUInt32(0UI) _
                    }
            Dim cellStyle1 As New CellStyle() With { _
                    .Name = "Normal", _
                    .FormatId = UInt32Value.FromUInt32(0UI), _
                    .BuiltinId = UInt32Value.FromUInt32(0UI) _
                    }

            cellStyles1.Append(cellStyle1)
            Dim differentialFormats1 As New DifferentialFormats() With { _
                    .Count = UInt32Value.FromUInt32(0UI) _
                    }
            Dim tableStyles1 As New TableStyles() With { _
                    .Count = UInt32Value.FromUInt32(0UI), _
                    .DefaultTableStyle = "TableStyleMedium9", _
                    .DefaultPivotStyle = "PivotStyleLight16" _
                    }

            stylesheet1.Append(fonts1)
            stylesheet1.Append(fills1)
            stylesheet1.Append(borders1)
            stylesheet1.Append(cellStyleFormats1)
            stylesheet1.Append(cellFormats1)
            stylesheet1.Append(cellStyles1)
            stylesheet1.Append(differentialFormats1)
            stylesheet1.Append(tableStyles1)

            workbookStylesPart1.Stylesheet = stylesheet1
        End Sub

        Private Function CreateCellStyleFormats(ByRef cellFormats1 As CellFormats) As CellStyleFormats

            Dim cellStyleFormats1 As New CellStyleFormats() With { _
                    .Count = UInt32Value.FromUInt32(1UI) _
                    }
            Dim cellFormat1 As New CellFormat() With { _
                    .NumberFormatId = UInt32Value.FromUInt32(0UI), _
                    .FontId = UInt32Value.FromUInt32(0UI), _
                    .FillId = UInt32Value.FromUInt32(0UI), _
                    .BorderId = UInt32Value.FromUInt32(0UI) _
                    }

            cellStyleFormats1.Append(cellFormat1)

            cellFormats1 = New CellFormats() With { _
                .Count = UInt32Value.FromUInt32(1UI) _
                }
            Dim cellFormat2 As New CellFormat() With { _
                    .NumberFormatId = UInt32Value.FromUInt32(0UI), _
                    .FontId = UInt32Value.FromUInt32(0UI), _
                    .FillId = UInt32Value.FromUInt32(0UI), _
                    .BorderId = UInt32Value.FromUInt32(0UI), _
                    .FormatId = UInt32Value.FromUInt32(0UI) _
                    }

            cellFormats1.Append(cellFormat2)
            Return cellStyleFormats1
        End Function

        Private Function CreateBorders() As Borders

            Dim borders1 As New Borders() With { _
                    .Count = UInt32Value.FromUInt32(1UI) _
                    }

            Dim border1 As New Border()
            Dim leftBorder1 As New Spreadsheet.LeftBorder()
            Dim rightBorder1 As New Spreadsheet.RightBorder()
            Dim topBorder1 As New Spreadsheet.TopBorder()
            Dim bottomBorder1 As New Spreadsheet.BottomBorder()
            Dim diagonalBorder1 As New DiagonalBorder()

            border1.Append(leftBorder1)
            border1.Append(rightBorder1)
            border1.Append(topBorder1)
            border1.Append(bottomBorder1)
            border1.Append(diagonalBorder1)

            borders1.Append(border1)
            Return borders1
        End Function

        Private Function CreateFills() As Fills

            Dim fills1 As New Fills() With { _
                    .Count = UInt32Value.FromUInt32(2UI) _
                    }

            Dim fill1 As New Spreadsheet.Fill()
            Dim patternFill1 As New Spreadsheet.PatternFill() With { _
                    .PatternType = PatternValues.None _
                    }

            fill1.Append(patternFill1)

            Dim fill2 As New Spreadsheet.Fill()
            Dim patternFill2 As New Spreadsheet.PatternFill() With { _
                    .PatternType = PatternValues.Gray125 _
                    }

            fill2.Append(patternFill2)

            fills1.Append(fill1)
            fills1.Append(fill2)
            Return fills1
        End Function

        Private Function CreateFonts() As Spreadsheet.Fonts

            Dim fonts1 As New Spreadsheet.Fonts() With { _
                    .Count = UInt32Value.FromUInt32(1UI) _
                    }

            Dim font1 As New Font()
            Dim fontSize1 As New FontSize() With { _
                    .Val = 11.0 _
                    }
            Dim color1 As New Color() With { _
                    .Theme = UInt32Value.FromUInt32(1UI) _
                    }
            Dim fontName1 As New FontName() With { _
                    .Val = "Calibri" _
                    }
            Dim fontFamilyNumbering1 As New FontFamilyNumbering() With { _
                    .Val = 2 _
                    }
            Dim fontScheme1 As New Spreadsheet.FontScheme() With { _
                    .Val = FontSchemeValues.Minor _
                    }

            font1.Append(fontSize1)
            font1.Append(color1)
            font1.Append(fontName1)
            font1.Append(fontFamilyNumbering1)
            font1.Append(fontScheme1)

            fonts1.Append(font1)
            Return fonts1
        End Function

        Private Sub GenerateThemePart1Content(themePart1 As ThemePart)
            Dim theme1 As New Theme() With { _
                    .Name = "Office Theme" _
                    }

            Dim themeElements1 As New ThemeElements()


            Dim fontScheme2 As New DocumentFormat.OpenXml.Drawing.FontScheme() With { _
                    .Name = "Office" _
                    }

            fontScheme2.Append(CreateMajorFont())
            fontScheme2.Append(CreateMinorFont())

            Dim formatScheme1 As New FormatScheme() With { _
                    .Name = "Office" _
                    }

            formatScheme1.Append(CreateFillStyleList())
            formatScheme1.Append(CreateLineStyleList())
            formatScheme1.Append(CreateEffectStyleList())
            formatScheme1.Append(CreateBackgroundFillStyleList())

            themeElements1.Append(CreateColorScheme())
            themeElements1.Append(fontScheme2)
            themeElements1.Append(formatScheme1)
            Dim objectDefaults1 As New ObjectDefaults()
            Dim extraColorSchemeList1 As New ExtraColorSchemeList()

            theme1.Append(themeElements1)
            theme1.Append(objectDefaults1)
            theme1.Append(extraColorSchemeList1)

            themePart1.Theme = theme1
        End Sub

        Private Function CreateBackgroundFillStyleList() As BackgroundFillStyleList

            Dim backgroundFillStyleList1 As New BackgroundFillStyleList()

            Dim solidFill5 As New SolidFill()
            Dim schemeColor11 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }

            solidFill5.Append(schemeColor11)

            Dim gradientFill3 As New DocumentFormat.OpenXml.Drawing.GradientFill() With { _
                    .RotateWithShape = True _
                    }

            Dim gradientStopList3 As New GradientStopList()

            Dim gradientStop7 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 0 _
                    }

            Dim schemeColor12 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim tint4 As New Tint() With { _
                    .Val = 40000 _
                    }
            Dim saturationModulation8 As New SaturationModulation() With { _
                    .Val = 350000 _
                    }

            schemeColor12.Append(tint4)
            schemeColor12.Append(saturationModulation8)

            gradientStop7.Append(schemeColor12)

            Dim gradientStop8 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 40000 _
                    }

            Dim schemeColor13 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim tint5 As New Tint() With { _
                    .Val = 45000 _
                    }
            Dim shade5 As New Shade() With { _
                    .Val = 99000 _
                    }
            Dim saturationModulation9 As New SaturationModulation() With { _
                    .Val = 350000 _
                    }

            schemeColor13.Append(tint5)
            schemeColor13.Append(shade5)
            schemeColor13.Append(saturationModulation9)

            gradientStop8.Append(schemeColor13)

            Dim gradientStop9 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 100000 _
                    }

            Dim schemeColor14 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim shade6 As New Shade() With { _
                    .Val = 20000 _
                    }
            Dim saturationModulation10 As New SaturationModulation() With { _
                    .Val = 255000 _
                    }

            schemeColor14.Append(shade6)
            schemeColor14.Append(saturationModulation10)

            gradientStop9.Append(schemeColor14)

            gradientStopList3.Append(gradientStop7)
            gradientStopList3.Append(gradientStop8)
            gradientStopList3.Append(gradientStop9)

            Dim pathGradientFill1 As New PathGradientFill() With { _
                    .Path = PathShadeValues.Circle _
                    }
            Dim fillToRectangle1 As New FillToRectangle() With { _
                    .Left = 50000, _
                    .Top = -80000, _
                    .Right = 50000, _
                    .Bottom = 180000 _
                    }

            pathGradientFill1.Append(fillToRectangle1)

            gradientFill3.Append(gradientStopList3)
            gradientFill3.Append(pathGradientFill1)

            Dim gradientFill4 As New DocumentFormat.OpenXml.Drawing.GradientFill() With { _
                    .RotateWithShape = True _
                    }

            Dim gradientStopList4 As New GradientStopList()

            Dim gradientStop10 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 0 _
                    }

            Dim schemeColor15 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim tint6 As New Tint() With { _
                    .Val = 80000 _
                    }
            Dim saturationModulation11 As New SaturationModulation() With { _
                    .Val = 300000 _
                    }

            schemeColor15.Append(tint6)
            schemeColor15.Append(saturationModulation11)

            gradientStop10.Append(schemeColor15)

            Dim gradientStop11 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 100000 _
                    }

            Dim schemeColor16 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim shade7 As New Shade() With { _
                    .Val = 30000 _
                    }
            Dim saturationModulation12 As New SaturationModulation() With { _
                    .Val = 200000 _
                    }

            schemeColor16.Append(shade7)
            schemeColor16.Append(saturationModulation12)

            gradientStop11.Append(schemeColor16)

            gradientStopList4.Append(gradientStop10)
            gradientStopList4.Append(gradientStop11)

            Dim pathGradientFill2 As New PathGradientFill() With { _
                    .Path = PathShadeValues.Circle _
                    }
            Dim fillToRectangle2 As New FillToRectangle() With { _
                    .Left = 50000, _
                    .Top = 50000, _
                    .Right = 50000, _
                    .Bottom = 50000 _
                    }

            pathGradientFill2.Append(fillToRectangle2)

            gradientFill4.Append(gradientStopList4)
            gradientFill4.Append(pathGradientFill2)

            backgroundFillStyleList1.Append(solidFill5)
            backgroundFillStyleList1.Append(gradientFill3)
            backgroundFillStyleList1.Append(gradientFill4)
            Return backgroundFillStyleList1
        End Function

        Private Function CreateEffectStyleList() As EffectStyleList

            Dim effectStyleList1 As New EffectStyleList()

            Dim effectStyle1 As New EffectStyle()

            Dim effectList1 As New EffectList()

            Dim outerShadow1 As New OuterShadow() With { _
                    .BlurRadius = 40000L, _
                    .Distance = 20000L, _
                    .Direction = 5400000, _
                    .RotateWithShape = False _
                    }

            Dim rgbColorModelHex11 As New RgbColorModelHex() With { _
                    .Val = "000000" _
                    }
            Dim alpha1 As New Alpha() With { _
                    .Val = 38000 _
                    }

            rgbColorModelHex11.Append(alpha1)

            outerShadow1.Append(rgbColorModelHex11)

            effectList1.Append(outerShadow1)

            effectStyle1.Append(effectList1)

            Dim effectStyle2 As New EffectStyle()

            Dim effectList2 As New EffectList()

            Dim outerShadow2 As New OuterShadow() With { _
                    .BlurRadius = 40000L, _
                    .Distance = 23000L, _
                    .Direction = 5400000, _
                    .RotateWithShape = False _
                    }

            Dim rgbColorModelHex12 As New RgbColorModelHex() With { _
                    .Val = "000000" _
                    }
            Dim alpha2 As New Alpha() With { _
                    .Val = 35000 _
                    }

            rgbColorModelHex12.Append(alpha2)

            outerShadow2.Append(rgbColorModelHex12)

            effectList2.Append(outerShadow2)

            effectStyle2.Append(effectList2)

            Dim effectStyle3 As New EffectStyle()

            Dim effectList3 As New EffectList()

            Dim outerShadow3 As New OuterShadow() With { _
                    .BlurRadius = 40000L, _
                    .Distance = 23000L, _
                    .Direction = 5400000, _
                    .RotateWithShape = False _
                    }

            Dim rgbColorModelHex13 As New RgbColorModelHex() With { _
                    .Val = "000000" _
                    }
            Dim alpha3 As New Alpha() With { _
                    .Val = 35000 _
                    }

            rgbColorModelHex13.Append(alpha3)

            outerShadow3.Append(rgbColorModelHex13)

            effectList3.Append(outerShadow3)

            Dim scene3DType1 As New Scene3DType()

            Dim camera1 As New Camera() With { _
                    .Preset = PresetCameraValues.OrthographicFront _
                    }
            Dim rotation1 As New Rotation() With { _
                    .Latitude = 0, _
                    .Longitude = 0, _
                    .Revolution = 0 _
                    }

            camera1.Append(rotation1)

            Dim lightRig1 As New LightRig() With { _
                    .Rig = LightRigValues.ThreePoints, _
                    .Direction = LightRigDirectionValues.Top _
                    }
            Dim rotation2 As New Rotation() With { _
                    .Latitude = 0, _
                    .Longitude = 0, _
                    .Revolution = 1200000 _
                    }

            lightRig1.Append(rotation2)

            scene3DType1.Append(camera1)
            scene3DType1.Append(lightRig1)

            Dim shape3DType1 As New Shape3DType()
            Dim bevelTop1 As New BevelTop() With { _
                    .Width = 63500L, _
                    .Height = 25400L _
                    }

            shape3DType1.Append(bevelTop1)

            effectStyle3.Append(effectList3)
            effectStyle3.Append(scene3DType1)
            effectStyle3.Append(shape3DType1)

            effectStyleList1.Append(effectStyle1)
            effectStyleList1.Append(effectStyle2)
            effectStyleList1.Append(effectStyle3)
            Return effectStyleList1
        End Function

        Private Function CreateLineStyleList() As LineStyleList

            Dim lineStyleList1 As New LineStyleList()

            Dim outline1 As New DocumentFormat.OpenXml.Drawing.Outline() With { _
                    .Width = 9525, _
                    .CapType = LineCapValues.Flat, _
                    .CompoundLineType = CompoundLineValues.[Single], _
                    .Alignment = PenAlignmentValues.Center _
                    }

            Dim solidFill2 As New SolidFill()

            Dim schemeColor8 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim shade4 As New Shade() With { _
                    .Val = 95000 _
                    }
            Dim saturationModulation7 As New SaturationModulation() With { _
                    .Val = 105000 _
                    }

            schemeColor8.Append(shade4)
            schemeColor8.Append(saturationModulation7)

            solidFill2.Append(schemeColor8)
            Dim presetDash1 As New PresetDash() With { _
                    .Val = PresetLineDashValues.Solid _
                    }

            outline1.Append(solidFill2)
            outline1.Append(presetDash1)

            Dim outline2 As New DocumentFormat.OpenXml.Drawing.Outline() With { _
                    .Width = 25400, _
                    .CapType = LineCapValues.Flat, _
                    .CompoundLineType = CompoundLineValues.[Single], _
                    .Alignment = PenAlignmentValues.Center _
                    }

            Dim solidFill3 As New SolidFill()
            Dim schemeColor9 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }

            solidFill3.Append(schemeColor9)
            Dim presetDash2 As New PresetDash() With { _
                    .Val = PresetLineDashValues.Solid _
                    }

            outline2.Append(solidFill3)
            outline2.Append(presetDash2)

            Dim outline3 As New DocumentFormat.OpenXml.Drawing.Outline() With { _
                    .Width = 38100, _
                    .CapType = LineCapValues.Flat, _
                    .CompoundLineType = CompoundLineValues.[Single], _
                    .Alignment = PenAlignmentValues.Center _
                    }

            Dim solidFill4 As New SolidFill()
            Dim schemeColor10 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }

            solidFill4.Append(schemeColor10)
            Dim presetDash3 As New PresetDash() With { _
                    .Val = PresetLineDashValues.Solid _
                    }

            outline3.Append(solidFill4)
            outline3.Append(presetDash3)

            lineStyleList1.Append(outline1)
            lineStyleList1.Append(outline2)
            lineStyleList1.Append(outline3)
            Return lineStyleList1
        End Function

        Private Function CreateFillStyleList() As FillStyleList

            Dim fillStyleList1 As New FillStyleList()

            Dim solidFill1 As New SolidFill()
            Dim schemeColor1 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }

            solidFill1.Append(schemeColor1)

            Dim gradientFill1 As New DocumentFormat.OpenXml.Drawing.GradientFill() With { _
                    .RotateWithShape = True _
                    }

            Dim gradientStopList1 As New GradientStopList()

            Dim gradientStop1 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 0 _
                    }

            Dim schemeColor2 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim tint1 As New Tint() With { _
                    .Val = 50000 _
                    }
            Dim saturationModulation1 As New SaturationModulation() With { _
                    .Val = 300000 _
                    }

            schemeColor2.Append(tint1)
            schemeColor2.Append(saturationModulation1)

            gradientStop1.Append(schemeColor2)

            Dim gradientStop2 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 35000 _
                    }

            Dim schemeColor3 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim tint2 As New Tint() With { _
                    .Val = 37000 _
                    }
            Dim saturationModulation2 As New SaturationModulation() With { _
                    .Val = 300000 _
                    }

            schemeColor3.Append(tint2)
            schemeColor3.Append(saturationModulation2)

            gradientStop2.Append(schemeColor3)

            Dim gradientStop3 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 100000 _
                    }

            Dim schemeColor4 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim tint3 As New Tint() With { _
                    .Val = 15000 _
                    }
            Dim saturationModulation3 As New SaturationModulation() With { _
                    .Val = 350000 _
                    }

            schemeColor4.Append(tint3)
            schemeColor4.Append(saturationModulation3)

            gradientStop3.Append(schemeColor4)

            gradientStopList1.Append(gradientStop1)
            gradientStopList1.Append(gradientStop2)
            gradientStopList1.Append(gradientStop3)
            Dim linearGradientFill1 As New LinearGradientFill() With { _
                    .Angle = 16200000, _
                    .Scaled = True _
                    }

            gradientFill1.Append(gradientStopList1)
            gradientFill1.Append(linearGradientFill1)

            Dim gradientFill2 As New DocumentFormat.OpenXml.Drawing.GradientFill() With { _
                    .RotateWithShape = True _
                    }

            Dim gradientStopList2 As New GradientStopList()

            Dim gradientStop4 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 0 _
                    }

            Dim schemeColor5 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim shade1 As New Shade() With { _
                    .Val = 51000 _
                    }
            Dim saturationModulation4 As New SaturationModulation() With { _
                    .Val = 130000 _
                    }

            schemeColor5.Append(shade1)
            schemeColor5.Append(saturationModulation4)

            gradientStop4.Append(schemeColor5)

            Dim gradientStop5 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 80000 _
                    }

            Dim schemeColor6 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim shade2 As New Shade() With { _
                    .Val = 93000 _
                    }
            Dim saturationModulation5 As New SaturationModulation() With { _
                    .Val = 130000 _
                    }

            schemeColor6.Append(shade2)
            schemeColor6.Append(saturationModulation5)

            gradientStop5.Append(schemeColor6)

            Dim gradientStop6 As New DocumentFormat.OpenXml.Drawing.GradientStop() With { _
                    .Position = 100000 _
                    }

            Dim schemeColor7 As New SchemeColor() With { _
                    .Val = SchemeColorValues.PhColor _
                    }
            Dim shade3 As New Shade() With { _
                    .Val = 94000 _
                    }
            Dim saturationModulation6 As New SaturationModulation() With { _
                    .Val = 135000 _
                    }

            schemeColor7.Append(shade3)
            schemeColor7.Append(saturationModulation6)

            gradientStop6.Append(schemeColor7)

            gradientStopList2.Append(gradientStop4)
            gradientStopList2.Append(gradientStop5)
            gradientStopList2.Append(gradientStop6)
            Dim linearGradientFill2 As New LinearGradientFill() With { _
                    .Angle = 16200000, _
                    .Scaled = False _
                    }

            gradientFill2.Append(gradientStopList2)
            gradientFill2.Append(linearGradientFill2)

            fillStyleList1.Append(solidFill1)
            fillStyleList1.Append(gradientFill1)
            fillStyleList1.Append(gradientFill2)
            Return fillStyleList1
        End Function

        Private Function CreateColorScheme() As ColorScheme


            Dim colorScheme1 As New ColorScheme() With { _
                    .Name = "Office" _
                    }

            Dim dark1Color1 As New Dark1Color()
            Dim systemColor1 As New SystemColor() With { _
                    .Val = SystemColorValues.WindowText, _
                    .LastColor = "000000" _
                    }

            dark1Color1.Append(systemColor1)

            Dim light1Color1 As New Light1Color()
            Dim systemColor2 As New SystemColor() With { _
                    .Val = SystemColorValues.Window, _
                    .LastColor = "FFFFFF" _
                    }

            light1Color1.Append(systemColor2)

            Dim dark2Color1 As New Dark2Color()
            Dim rgbColorModelHex1 As New RgbColorModelHex() With { _
                    .Val = "1F497D" _
                    }

            dark2Color1.Append(rgbColorModelHex1)

            Dim light2Color1 As New Light2Color()
            Dim rgbColorModelHex2 As New RgbColorModelHex() With { _
                    .Val = "EEECE1" _
                    }

            light2Color1.Append(rgbColorModelHex2)

            Dim accent1Color1 As New Accent1Color()
            Dim rgbColorModelHex3 As New RgbColorModelHex() With { _
                    .Val = "4F81BD" _
                    }

            accent1Color1.Append(rgbColorModelHex3)

            Dim accent2Color1 As New Accent2Color()
            Dim rgbColorModelHex4 As New RgbColorModelHex() With { _
                    .Val = "C0504D" _
                    }

            accent2Color1.Append(rgbColorModelHex4)

            Dim accent3Color1 As New Accent3Color()
            Dim rgbColorModelHex5 As New RgbColorModelHex() With { _
                    .Val = "9BBB59" _
                    }

            accent3Color1.Append(rgbColorModelHex5)

            Dim accent4Color1 As New Accent4Color()
            Dim rgbColorModelHex6 As New RgbColorModelHex() With { _
                    .Val = "8064A2" _
                    }

            accent4Color1.Append(rgbColorModelHex6)

            Dim accent5Color1 As New Accent5Color()
            Dim rgbColorModelHex7 As New RgbColorModelHex() With { _
                    .Val = "4BACC6" _
                    }

            accent5Color1.Append(rgbColorModelHex7)

            Dim accent6Color1 As New Accent6Color()
            Dim rgbColorModelHex8 As New RgbColorModelHex() With { _
                    .Val = "F79646" _
                    }

            accent6Color1.Append(rgbColorModelHex8)

            Dim hyperlink1 As New DocumentFormat.OpenXml.Drawing.Hyperlink()
            Dim rgbColorModelHex9 As New RgbColorModelHex() With { _
                    .Val = "0000FF" _
                    }

            hyperlink1.Append(rgbColorModelHex9)

            Dim followedHyperlinkColor1 As New FollowedHyperlinkColor()
            Dim rgbColorModelHex10 As New RgbColorModelHex() With { _
                    .Val = "800080" _
                    }

            followedHyperlinkColor1.Append(rgbColorModelHex10)

            colorScheme1.Append(dark1Color1)
            colorScheme1.Append(light1Color1)
            colorScheme1.Append(dark2Color1)
            colorScheme1.Append(light2Color1)
            colorScheme1.Append(accent1Color1)
            colorScheme1.Append(accent2Color1)
            colorScheme1.Append(accent3Color1)
            colorScheme1.Append(accent4Color1)
            colorScheme1.Append(accent5Color1)
            colorScheme1.Append(accent6Color1)
            colorScheme1.Append(hyperlink1)
            colorScheme1.Append(followedHyperlinkColor1)
            Return colorScheme1
        End Function

        Private Function CreateMinorFont() As MinorFont

            Dim minorFont1 As New MinorFont()
            Dim latinFont2 As New LatinFont() With { _
                    .Typeface = "Calibri" _
                    }
            Dim eastAsianFont2 As New EastAsianFont() With { _
                    .Typeface = "" _
                    }
            Dim complexScriptFont2 As New ComplexScriptFont() With { _
                    .Typeface = "" _
                    }
            Dim supplementalFont30 As New SupplementalFont() With { _
                    .Script = "Jpan", _
                    .Typeface = "ＭＳ Ｐゴシック" _
                    }
            Dim supplementalFont31 As New SupplementalFont() With { _
                    .Script = "Hang", _
                    .Typeface = "맑은 고딕" _
                    }
            Dim supplementalFont32 As New SupplementalFont() With { _
                    .Script = "Hans", _
                    .Typeface = "宋体" _
                    }
            Dim supplementalFont33 As New SupplementalFont() With { _
                    .Script = "Hant", _
                    .Typeface = "新細明體" _
                    }
            Dim supplementalFont34 As New SupplementalFont() With { _
                    .Script = "Arab", _
                    .Typeface = "Arial" _
                    }
            Dim supplementalFont35 As New SupplementalFont() With { _
                    .Script = "Hebr", _
                    .Typeface = "Arial" _
                    }
            Dim supplementalFont36 As New SupplementalFont() With { _
                    .Script = "Thai", _
                    .Typeface = "Tahoma" _
                    }
            Dim supplementalFont37 As New SupplementalFont() With { _
                    .Script = "Ethi", _
                    .Typeface = "Nyala" _
                    }
            Dim supplementalFont38 As New SupplementalFont() With { _
                    .Script = "Beng", _
                    .Typeface = "Vrinda" _
                    }
            Dim supplementalFont39 As New SupplementalFont() With { _
                    .Script = "Gujr", _
                    .Typeface = "Shruti" _
                    }
            Dim supplementalFont40 As New SupplementalFont() With { _
                    .Script = "Khmr", _
                    .Typeface = "DaunPenh" _
                    }
            Dim supplementalFont41 As New SupplementalFont() With { _
                    .Script = "Knda", _
                    .Typeface = "Tunga" _
                    }
            Dim supplementalFont42 As New SupplementalFont() With { _
                    .Script = "Guru", _
                    .Typeface = "Raavi" _
                    }
            Dim supplementalFont43 As New SupplementalFont() With { _
                    .Script = "Cans", _
                    .Typeface = "Euphemia" _
                    }
            Dim supplementalFont44 As New SupplementalFont() With { _
                    .Script = "Cher", _
                    .Typeface = "Plantagenet Cherokee" _
                    }
            Dim supplementalFont45 As New SupplementalFont() With { _
                    .Script = "Yiii", _
                    .Typeface = "Microsoft Yi Baiti" _
                    }
            Dim supplementalFont46 As New SupplementalFont() With { _
                    .Script = "Tibt", _
                    .Typeface = "Microsoft Himalaya" _
                    }
            Dim supplementalFont47 As New SupplementalFont() With { _
                    .Script = "Thaa", _
                    .Typeface = "MV Boli" _
                    }
            Dim supplementalFont48 As New SupplementalFont() With { _
                    .Script = "Deva", _
                    .Typeface = "Mangal" _
                    }
            Dim supplementalFont49 As New SupplementalFont() With { _
                    .Script = "Telu", _
                    .Typeface = "Gautami" _
                    }
            Dim supplementalFont50 As New SupplementalFont() With { _
                    .Script = "Taml", _
                    .Typeface = "Latha" _
                    }
            Dim supplementalFont51 As New SupplementalFont() With { _
                    .Script = "Syrc", _
                    .Typeface = "Estrangelo Edessa" _
                    }
            Dim supplementalFont52 As New SupplementalFont() With { _
                    .Script = "Orya", _
                    .Typeface = "Kalinga" _
                    }
            Dim supplementalFont53 As New SupplementalFont() With { _
                    .Script = "Mlym", _
                    .Typeface = "Kartika" _
                    }
            Dim supplementalFont54 As New SupplementalFont() With { _
                    .Script = "Laoo", _
                    .Typeface = "DokChampa" _
                    }
            Dim supplementalFont55 As New SupplementalFont() With { _
                    .Script = "Sinh", _
                    .Typeface = "Iskoola Pota" _
                    }
            Dim supplementalFont56 As New SupplementalFont() With { _
                    .Script = "Mong", _
                    .Typeface = "Mongolian Baiti" _
                    }
            Dim supplementalFont57 As New SupplementalFont() With { _
                    .Script = "Viet", _
                    .Typeface = "Arial" _
                    }
            Dim supplementalFont58 As New SupplementalFont() With { _
                    .Script = "Uigh", _
                    .Typeface = "Microsoft Uighur" _
                    }

            minorFont1.Append(latinFont2)
            minorFont1.Append(eastAsianFont2)
            minorFont1.Append(complexScriptFont2)
            minorFont1.Append(supplementalFont30)
            minorFont1.Append(supplementalFont31)
            minorFont1.Append(supplementalFont32)
            minorFont1.Append(supplementalFont33)
            minorFont1.Append(supplementalFont34)
            minorFont1.Append(supplementalFont35)
            minorFont1.Append(supplementalFont36)
            minorFont1.Append(supplementalFont37)
            minorFont1.Append(supplementalFont38)
            minorFont1.Append(supplementalFont39)
            minorFont1.Append(supplementalFont40)
            minorFont1.Append(supplementalFont41)
            minorFont1.Append(supplementalFont42)
            minorFont1.Append(supplementalFont43)
            minorFont1.Append(supplementalFont44)
            minorFont1.Append(supplementalFont45)
            minorFont1.Append(supplementalFont46)
            minorFont1.Append(supplementalFont47)
            minorFont1.Append(supplementalFont48)
            minorFont1.Append(supplementalFont49)
            minorFont1.Append(supplementalFont50)
            minorFont1.Append(supplementalFont51)
            minorFont1.Append(supplementalFont52)
            minorFont1.Append(supplementalFont53)
            minorFont1.Append(supplementalFont54)
            minorFont1.Append(supplementalFont55)
            minorFont1.Append(supplementalFont56)
            minorFont1.Append(supplementalFont57)
            minorFont1.Append(supplementalFont58)
            Return minorFont1
        End Function

        Private Function CreateMajorFont() As MajorFont

            Dim majorFont1 As New MajorFont()
            Dim latinFont1 As New LatinFont() With { _
                    .Typeface = "Cambria" _
                    }
            Dim eastAsianFont1 As New EastAsianFont() With { _
                    .Typeface = "" _
                    }
            Dim complexScriptFont1 As New ComplexScriptFont() With { _
                    .Typeface = "" _
                    }
            Dim supplementalFont1 As New SupplementalFont() With { _
                    .Script = "Jpan", _
                    .Typeface = "ＭＳ Ｐゴシック" _
                    }
            Dim supplementalFont2 As New SupplementalFont() With { _
                    .Script = "Hang", _
                    .Typeface = "맑은 고딕" _
                    }
            Dim supplementalFont3 As New SupplementalFont() With { _
                    .Script = "Hans", _
                    .Typeface = "宋体" _
                    }
            Dim supplementalFont4 As New SupplementalFont() With { _
                    .Script = "Hant", _
                    .Typeface = "新細明體" _
                    }
            Dim supplementalFont5 As New SupplementalFont() With { _
                    .Script = "Arab", _
                    .Typeface = "Times New Roman" _
                    }
            Dim supplementalFont6 As New SupplementalFont() With { _
                    .Script = "Hebr", _
                    .Typeface = "Times New Roman" _
                    }
            Dim supplementalFont7 As New SupplementalFont() With { _
                    .Script = "Thai", _
                    .Typeface = "Tahoma" _
                    }
            Dim supplementalFont8 As New SupplementalFont() With { _
                    .Script = "Ethi", _
                    .Typeface = "Nyala" _
                    }
            Dim supplementalFont9 As New SupplementalFont() With { _
                    .Script = "Beng", _
                    .Typeface = "Vrinda" _
                    }
            Dim supplementalFont10 As New SupplementalFont() With { _
                    .Script = "Gujr", _
                    .Typeface = "Shruti" _
                    }
            Dim supplementalFont11 As New SupplementalFont() With { _
                    .Script = "Khmr", _
                    .Typeface = "MoolBoran" _
                    }
            Dim supplementalFont12 As New SupplementalFont() With { _
                    .Script = "Knda", _
                    .Typeface = "Tunga" _
                    }
            Dim supplementalFont13 As New SupplementalFont() With { _
                    .Script = "Guru", _
                    .Typeface = "Raavi" _
                    }
            Dim supplementalFont14 As New SupplementalFont() With { _
                    .Script = "Cans", _
                    .Typeface = "Euphemia" _
                    }
            Dim supplementalFont15 As New SupplementalFont() With { _
                    .Script = "Cher", _
                    .Typeface = "Plantagenet Cherokee" _
                    }
            Dim supplementalFont16 As New SupplementalFont() With { _
                    .Script = "Yiii", _
                    .Typeface = "Microsoft Yi Baiti" _
                    }
            Dim supplementalFont17 As New SupplementalFont() With { _
                    .Script = "Tibt", _
                    .Typeface = "Microsoft Himalaya" _
                    }
            Dim supplementalFont18 As New SupplementalFont() With { _
                    .Script = "Thaa", _
                    .Typeface = "MV Boli" _
                    }
            Dim supplementalFont19 As New SupplementalFont() With { _
                    .Script = "Deva", _
                    .Typeface = "Mangal" _
                    }
            Dim supplementalFont20 As New SupplementalFont() With { _
                    .Script = "Telu", _
                    .Typeface = "Gautami" _
                    }
            Dim supplementalFont21 As New SupplementalFont() With { _
                    .Script = "Taml", _
                    .Typeface = "Latha" _
                    }
            Dim supplementalFont22 As New SupplementalFont() With { _
                    .Script = "Syrc", _
                    .Typeface = "Estrangelo Edessa" _
                    }
            Dim supplementalFont23 As New SupplementalFont() With { _
                    .Script = "Orya", _
                    .Typeface = "Kalinga" _
                    }
            Dim supplementalFont24 As New SupplementalFont() With { _
                    .Script = "Mlym", _
                    .Typeface = "Kartika" _
                    }
            Dim supplementalFont25 As New SupplementalFont() With { _
                    .Script = "Laoo", _
                    .Typeface = "DokChampa" _
                    }
            Dim supplementalFont26 As New SupplementalFont() With { _
                    .Script = "Sinh", _
                    .Typeface = "Iskoola Pota" _
                    }
            Dim supplementalFont27 As New SupplementalFont() With { _
                    .Script = "Mong", _
                    .Typeface = "Mongolian Baiti" _
                    }
            Dim supplementalFont28 As New SupplementalFont() With { _
                    .Script = "Viet", _
                    .Typeface = "Times New Roman" _
                    }
            Dim supplementalFont29 As New SupplementalFont() With { _
                    .Script = "Uigh", _
                    .Typeface = "Microsoft Uighur" _
                    }

            majorFont1.Append(latinFont1)
            majorFont1.Append(eastAsianFont1)
            majorFont1.Append(complexScriptFont1)
            majorFont1.Append(supplementalFont1)
            majorFont1.Append(supplementalFont2)
            majorFont1.Append(supplementalFont3)
            majorFont1.Append(supplementalFont4)
            majorFont1.Append(supplementalFont5)
            majorFont1.Append(supplementalFont6)
            majorFont1.Append(supplementalFont7)
            majorFont1.Append(supplementalFont8)
            majorFont1.Append(supplementalFont9)
            majorFont1.Append(supplementalFont10)
            majorFont1.Append(supplementalFont11)
            majorFont1.Append(supplementalFont12)
            majorFont1.Append(supplementalFont13)
            majorFont1.Append(supplementalFont14)
            majorFont1.Append(supplementalFont15)
            majorFont1.Append(supplementalFont16)
            majorFont1.Append(supplementalFont17)
            majorFont1.Append(supplementalFont18)
            majorFont1.Append(supplementalFont19)
            majorFont1.Append(supplementalFont20)
            majorFont1.Append(supplementalFont21)
            majorFont1.Append(supplementalFont22)
            majorFont1.Append(supplementalFont23)
            majorFont1.Append(supplementalFont24)
            majorFont1.Append(supplementalFont25)
            majorFont1.Append(supplementalFont26)
            majorFont1.Append(supplementalFont27)
            majorFont1.Append(supplementalFont28)
            majorFont1.Append(supplementalFont29)
            Return majorFont1
        End Function

        Private Sub SetPackageProperties(document As OpenXmlPackage)
            document.PackageProperties.Creator = "Author Name"
            document.PackageProperties.Created = DateTime.Now
            document.PackageProperties.Modified = DateTime.Now
            document.PackageProperties.LastModifiedBy = "Author Name"
        End Sub


    End Class
End NameSpace