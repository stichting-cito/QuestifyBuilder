-- If the database already exists, drop it  
IF EXISTS(SELECT * FROM sysdatabases WHERE name='$(testDBName)')  
	DROP DATABASE $(testDBName)
GO

-- Create the test database
CREATE DATABASE $(testDBName)
GO