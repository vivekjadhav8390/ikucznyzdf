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
	@UserID			INT,
	@NewPassword	VARCHAR(100)
)
as 
BEGIN
	
	IF EXISTS(SELECT 1 FROM Users u WHERE u.ID = @UserID)
	BEGIN
	
		UPDATE u SET u.Password = @NewPassword,u.Edited_Date = CURRENT_TIMESTAMP,u.Edited_By = @UserID
		FROM Users u
		WHERE u.ID = @UserID;
		
		SELECT u.*,ut.Description as UserTypeDesc FROM Users u
		INNER JOIN UsersTypes ut on ut.ID = u.UserType 
		WHERE u.ID = @UserID;
	END
END

GO


