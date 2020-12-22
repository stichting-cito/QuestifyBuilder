-- If the database already exists, drop it  
IF EXISTS(SELECT * FROM sysdatabases WHERE name='$(testDBName)')  
	DROP DATABASE $(testDBName)
GO 