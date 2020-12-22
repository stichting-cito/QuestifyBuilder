-- =============================================
-- Description:	<Vertaald bindata zoals '0x3C3F786D6C20..' naar Xml>
-- =============================================
Create FUNCTION [dbo].[udf_BinToXml](@bindata as image)
RETURNS xml
AS
BEGIN
	return Cast(CONVERT(varbinary(max), @bindata)  as xml)
END