CREATE PROCEDURE [dbo].[SetMaintenanceWindow]
(
	@plannedTimestamp DATETIME
)
AS
BEGIN
	SET NOCOUNT ON;

    DELETE FROM dbo.MaintenanceWindow

	IF NOT ISNULL(@plannedTimestamp, '') = '' AND @plannedTimestamp > GETDATE()
	BEGIN
		INSERT INTO dbo.MaintenanceWindow (maintenancePlannedTimestamp) VALUES (@plannedTimestamp)
	END

	RETURN 0;
END