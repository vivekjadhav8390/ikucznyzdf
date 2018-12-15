USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_RegisterStudent]    Script Date: 12/08/2018 19:36:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Institute_RegisterStudent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Institute_RegisterStudent]
GO

USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_RegisterStudent]    Script Date: 12/08/2018 19:36:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Institute_RegisterStudent] 
(
	@UserName			VARCHAR(100),
	@Email				VARCHAR(100),
	@Password			VARCHAR(100),
	@FullName			VARCHAR(100),
	@CollegeName		VARCHAR(100),
	@ContactNo			VARCHAR(100),
	@PerContactNo		VARCHAR(100),
	@Address			VARCHAR(100),
	@DateOfBirth		DATETIME
 )
as 
BEGIN
	
	
	DECLARE @StudentDetails AS TABLE
	(
		ID					INT,
		UserType			INT,
		UserName			VARCHAR(100),
		Password			VARCHAR(100),
		EmailAddress		VARCHAR(100),
		FullName			VARCHAR(100),
		UserTypeDesc		VARCHAR(100)
	)
	
	DECLARE @ErrorDetails AS TABLE
	(
		Error			VARCHAR(100)
	)
	
	IF NOT EXISTS(SELECT 1 FROM Users u WHERE ISNULL(u.UserName,'') = ISNULL(@UserName,''))
	OR NOT EXISTS(SELECT 1 FROM Users u WHERE ISNULL(u.EmailAddress,'') = ISNULL(@Email,''))
	BEGIN
		INSERT INTO Users(UserType,UserName,Password,EmailAddress,Created_Date,Created_By)
		VALUES(3,@UserName,@Password,@Email,CURRENT_TIMESTAMP,0)
	
		DECLARE @User_ID INT
		SELECT @User_ID = ID FROM Users WHERE UserName =@UserName; 
	
		INSERT INTO Student(User_ID,Name,CollegeName,ContactNo,PermenentContact,Address,DateOfBirth,IsApproved,IsActive,Created_Date,Created_By)
		VALUES(@User_ID,@FullName,@CollegeName,@ContactNo,@PerContactNo,@Address,@DateOfBirth,0,1,CURRENT_TIMESTAMP,0)
				
		INSERT INTO @StudentDetails(ID,UserType,UserName,Password,EmailAddress,FullName,UserTypeDesc)
		SELECT u.ID,UserType,UserName,Password,EmailAddress,s.Name,ut.Description as UserTypeDesc FROM Users u
		LEFT JOIN UsersTypes ut on ut.ID = u.UserType 
		LEFT JOIN Student s on s.User_ID = u.ID
		WHERE u.ID = @User_ID;
			
	END
	ELSE
	BEGIN
		INSERT INTO @ErrorDetails VALUES('UserName or Email is already exist.')

	END
	
	SELECT * FROM @StudentDetails;	
	SELECT * FROM @ErrorDetails;

END

GO


