CREATE PROCEDURE [dbo].[GetMaintenanceWindow]
AS
BEGIN
	SELECT TOP 1 maintenancePlannedTimestamp AS PlannedTimestamp
	FROM dbo.MaintenanceWindow
END