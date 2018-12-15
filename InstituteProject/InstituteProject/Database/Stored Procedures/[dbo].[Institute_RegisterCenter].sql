USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_RegisterCenter]    Script Date: 12/08/2018 19:36:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Institute_RegisterCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Institute_RegisterCenter]
GO

USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_RegisterCenter]    Script Date: 12/08/2018 19:36:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Institute_RegisterCenter] 
(
	@UserName			VARCHAR(100),
	@Email				VARCHAR(100),
	@Password			VARCHAR(100),
	@InstituteName		VARCHAR(1000),
	@ContactOfPerson	VARCHAR(1000),
	@ContactNo			VARCHAR(100),
	@OfficeNo			VARCHAR(100),
	@Address			VARCHAR(1000),
	@InstituteOrSchool	VARCHAR(1000),
	@NoOfSeats			INT
	
)
as 
BEGIN

	DECLARE @CenterDetails AS TABLE
	(
		ID					INT,
		UserType			INT,
		UserName			VARCHAR(100),
		Password			VARCHAR(100),
		EmailAddress		VARCHAR(100),
		InstituteName		VARCHAR(100),
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
		VALUES(2,@UserName,@Password,@Email,CURRENT_TIMESTAMP,0)
	
		DECLARE @User_ID INT
		SELECT @User_ID = ID FROM Users WHERE UserName =@UserName; 
		
		INSERT INTO Center(Name,User_ID,ContactOfPerson,ContactNo,OfficeNo,Address,InstituteOrSchool,NoOfSeats,IsApproved,IsActive,Created_Date,Created_By)
		VALUES(@InstituteName,@User_ID,@ContactOfPerson,@ContactNo,@OfficeNo,@Address,@InstituteOrSchool,@NoOfSeats,0,1,CURRENT_TIMESTAMP,0)
				
		INSERT INTO @CenterDetails(ID,UserType,UserName,Password,EmailAddress,InstituteName,UserTypeDesc)
		SELECT u.ID,UserType,UserName,Password,EmailAddress,c.Name as InstituteName,ut.Description as UserTypeDesc FROM Users u
		LEFT JOIN UsersTypes ut on ut.ID = u.UserType 
		LEFT JOIN Center c on c.User_ID = u.ID
		WHERE u.ID = @User_ID;
			
	END
	ELSE
	BEGIN
		INSERT INTO @ErrorDetails VALUES('UserName or Email is already exist.')

	END
	
	SELECT * FROM @CenterDetails;	
	SELECT * FROM @ErrorDetails;
	
END

GO


