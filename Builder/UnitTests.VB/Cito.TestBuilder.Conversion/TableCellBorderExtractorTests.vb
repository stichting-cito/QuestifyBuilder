
Imports DocumentFormat.OpenXml.Wordprocessing
Imports FluentAssertions
Imports NotesFor.HtmlToOpenXml
Imports Questify.Builder.Plugins.PaperBased

<TestClass>
Public Class TableCellBorderExtractorTests

    <TestMethod>
    Public Sub BorderColorParsingBlack()
        TableCellBorderExtractor.TryParseBorderColor("black").Should().Be("000000")
    End Sub

    <TestMethod>
    Public Sub BorderColorParsingNothing()
        TableCellBorderExtractor.TryParseBorderColor(Nothing).Should().BeNull()
    End Sub

    <TestMethod>
    Public Sub BorderColorParsingRed()
        TableCellBorderExtractor.TryParseBorderColor("Red").Should().BeNull()
    End Sub

    <TestMethod>
    Public Sub BorderColorParsingEmptyString()
        TableCellBorderExtractor.TryParseBorderColor("").Should().BeNull()
    End Sub

    <TestMethod>
    Public Sub BorderColorParsingHexValue()
        TableCellBorderExtractor.TryParseBorderColor("#FF0000").Should().Be("FF0000")
    End Sub

    <TestMethod>
    Public Sub BorderColorParsingInvalidHex()
        TableCellBorderExtractor.TryParseBorderColor("#FF000").Should().BeNull()
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingSolid()
        TableCellBorderExtractor.TryParseBorderValue("Solid").Should.Be(BorderValues.Single)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingSolidCasingIgnored()
        TableCellBorderExtractor.TryParseBorderValue("SoLiD").Should.Be(BorderValues.Single)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingDouble()
        TableCellBorderExtractor.TryParseBorderValue("Double").Should.Be(BorderValues.Double)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingDoubleCasingIgnored()
        TableCellBorderExtractor.TryParseBorderValue("DOUble").Should.Be(BorderValues.Double)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingNothing()
        TableCellBorderExtractor.TryParseBorderValue(Nothing).Should.Be(BorderValues.Nil)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingEmptyString()
        TableCellBorderExtractor.TryParseBorderValue(String.Empty).Should.Be(BorderValues.Nil)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingInvalidString()
        TableCellBorderExtractor.TryParseBorderValue("String.Empty").Should.Be(BorderValues.Nil)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingHidden()
        TableCellBorderExtractor.TryParseBorderValue("hidden").Should.Be(BorderValues.None)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingRidge()
        TableCellBorderExtractor.TryParseBorderValue("ridge").Should.Be(BorderValues.Nil)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingDotted()
        TableCellBorderExtractor.TryParseBorderValue("dotted").Should.Be(BorderValues.Dotted)
    End Sub

    <TestMethod>
    Public Sub BorderValuesParsingDashed()
        TableCellBorderExtractor.TryParseBorderValue("Dashed").Should.Be(BorderValues.Dashed)
    End Sub

    <TestMethod>
    Public Sub GetWPBorderTypeNothing()
        TableCellBorderExtractor.GetWordProcessingBorderType(Nothing).Should.BeNull()
    End Sub

    <TestMethod>
    Public Sub GetWPBorderTypeEmptyString()
        TableCellBorderExtractor.GetWordProcessingBorderType(String.Empty).Should.BeNull()
    End Sub

    <TestMethod>
    Public Sub GetWPBorderTypeBorder()
        TableCellBorderExtractor.GetWordProcessingBorderType("border").Should.BeNull()
    End Sub

    <TestMethod>
    Public Sub GetWPBorderTypeBorderLeft()
        TableCellBorderExtractor.GetWordProcessingBorderType("border-left").Should.BeOfType(GetType(LeftBorder))
    End Sub

    <TestMethod>
    Public Sub GetWPBorderTypeBorderTop()
        TableCellBorderExtractor.GetWordProcessingBorderType("border-Top").Should.BeOfType(GetType(TopBorder))
    End Sub

    <TestMethod>
    Public Sub GetWPBorderTypeBorderBottom()
        TableCellBorderExtractor.GetWordProcessingBorderType("border-Bottom").Should.BeOfType(GetType(BottomBorder))
    End Sub

    <TestMethod>
    Public Sub GetWPBorderTypeBorderRight()
        TableCellBorderExtractor.GetWordProcessingBorderType("border-right").Should.BeOfType(GetType(RightBorder))
    End Sub

    <TestMethod>
    Public Sub ApplyBorderStyleBorderTypeNothing()
        TableCellBorderExtractor.ApplyBorderStyle(Nothing, New BorderStyle())
    End Sub

    <TestMethod>
    Public Sub ApplyBorderStyleBorderStyleNothing()
        TableCellBorderExtractor.ApplyBorderStyle(New LeftBorder(), Nothing)
    End Sub

    <TestMethod>
    Public Sub ApplyBorderStyleBorderStyleAndBorderTypeNothing()
        TableCellBorderExtractor.ApplyBorderStyle(Nothing, Nothing)
    End Sub

    <TestMethod>
    Public Sub ApplyBorderStyle()
        Dim borderType As New LeftBorder()
        Dim borderStyle As New BorderStyle With
        {
            .BorderValue = BorderValues.Hearts,
            .BorderColor = "000000",
            .BorderWidth = Unit.Empty
        }

        TableCellBorderExtractor.ApplyBorderStyle(borderType, borderStyle)

        borderType.Val.ToString.ToLower.Should.Be("hearts")
        borderType.Size.Should.BeNull()
        borderType.Color.Value.Should.Be("000000")
    End Sub

    <TestMethod>
    Public Sub ApplyBorderStyleValidWidthBorderValueNil()
        Dim borderType As New LeftBorder()
        Dim borderStyle As New BorderStyle With
                {
                .BorderValue = BorderValues.Nil,
                .BorderColor = "000000",
                .BorderWidth = Unit.Parse("1px")
                }

        TableCellBorderExtractor.ApplyBorderStyle(borderType, borderStyle)
        borderType.Val.ToString.ToLower.Should.Be("single")
        borderType.Size.Value.Should.Be(4ui)
        borderType.Color.Value.Should.Be("000000")
    End Sub

    <TestMethod>
    Public Sub ApplyBorderStyleColorNothing()
        Dim borderType As New LeftBorder()
        Dim borderStyle As New BorderStyle With
                {
                .BorderValue = BorderValues.Nil,
                .BorderColor = Nothing,
                .BorderWidth = Unit.Parse("1px")
                }

        TableCellBorderExtractor.ApplyBorderStyle(borderType, borderStyle)
        borderType.Val.ToString.ToLower.Should.Be("single")
        borderType.Size.Value.Should.Be(4ui)
        borderType.Color.Value.Should.Be("000000")
    End Sub

    <TestMethod>
    Public Sub GetBorderSettingsNothing()
        TableCellBorderExtractor.GetBorderStyle(Nothing).Should.BeEquivalentTo(New BorderStyle())
    End Sub

    <TestMethod>
    Public Sub GetBorderSettings1pxSolidBlack()
        Dim expectation As BorderStyle = New BorderStyle() With {
                .BorderValue = BorderValues.Single,
                .BorderColor = "000000",
                .BorderWidth = Unit.Parse("1px")
        }

        TableCellBorderExtractor.GetBorderStyle("1px solid black").Should.BeEquivalentTo(expectation)
    End Sub

    <TestMethod>
    Public Sub GetBorderSettingsRidgeRed()
        Dim expectation As BorderStyle = New BorderStyle() With {
                .BorderValue = BorderValues.Nil,
                .BorderColor = String.Empty,
                .BorderWidth = Unit.Empty
                }

        TableCellBorderExtractor.GetBorderStyle("ridge red").Should.BeEquivalentTo(expectation)
    End Sub

End Class