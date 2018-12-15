USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_UserLogin]    Script Date: 12/08/2018 19:36:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Institute_UserLogin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Institute_UserLogin]
GO

USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_UserLogin]    Script Date: 12/08/2018 19:36:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Institute_UserLogin] 
(
	@UserName	VARCHAR(100),
	@Password	VARCHAR(100)
)
as 
BEGIN
	SELECT u.*,ut.Description as UserTypeDesc FROM Users u
	INNER JOIN UsersTypes ut on ut.ID = u.UserType 
	WHERE UserName = @UserName and Password = @Password;


END

GO


