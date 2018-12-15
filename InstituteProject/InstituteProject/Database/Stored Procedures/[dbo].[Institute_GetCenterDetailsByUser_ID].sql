USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_GetCenterDetailsByUser_ID]    Script Date: 12/08/2018 19:36:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Institute_GetCenterDetailsByUser_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Institute_GetCenterDetailsByUser_ID]
GO

USE [Institute]
GO

/****** Object:  StoredProcedure [dbo].[Institute_GetCenterDetailsByUser_ID]    Script Date: 12/08/2018 19:36:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Institute_GetCenterDetailsByUser_ID] 
(
	@User_ID			INT
 )
as 
BEGIN
	SELECT c.*,ut.Description as UserTypeDesc FROM Users u
	INNER JOIN UsersTypes ut on ut.ID = u.UserType 
	INNER JOIN Center c on c.User_ID = u.ID
	WHERE u.ID = @User_ID;
			
END

GO


