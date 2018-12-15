IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name ='Institute')
BEGIN
	CREATE DATABASE Institute
END
ELSE
BEGIN
	PRINT 'Database is already present.'
END

USE Institute
--------------------------------------------------------------------
CREATE TABLE UsersTypes 
(
	ID					INT IDENTITY(1,1) PRIMARY KEY,
	Description			VARCHAR(100)
)
IF NOT EXISTS(SELECT 1 FROM UsersTypes WHERE Description='Admin')
BEGIN
	INSERT INTO UsersTypes VALUES('Admin')
END
IF NOT EXISTS(SELECT 1 FROM UsersTypes WHERE Description='Center')
BEGIN
	INSERT INTO UsersTypes VALUES('Center')
END

IF NOT EXISTS(SELECT 1 FROM UsersTypes WHERE Description='Student')
BEGIN
	INSERT INTO UsersTypes VALUES('Student')
END
--------------------------------------------------------------------
CREATE TABLE Users 
(
	ID					INT IDENTITY(1,1) PRIMARY KEY,
	UserType			INT,
	UserName			VARCHAR(100),
	Password			VARCHAR(100),
	EmailAddress		VARCHAR(100),
	IsActive			BIT,
	Created_Date		DATETIME,
	Created_By			INT,
	Edited_Date			DATETIME,
	Edited_By			INT	
)
IF NOT EXISTS (SELECT * FROM Users WHERE UserName ='admin')
BEGIN
	INSERT INTO Users(UserType,UserName,Password,EmailAddress,Created_Date,Created_By)
	VALUES(1,'admin','institute','vivekjadhav8390@gmail.com',CURRENT_TIMESTAMP,0)
END

--------------------------------------------------------------------
CREATE TABLE Student 
(
	ID					INT IDENTITY(1,1) PRIMARY KEY,
	Name				VARCHAR(1000),
	User_ID				INT,
	College				VARCHAR(1000),
	ContactNo			VARCHAR(100),
	PermenentContact	VARCHAR(100),
	Address				VARCHAR(1000),
	DOB					DateTime,
	IsApproved			BIT,
	IsActive			BIT,
	Created_Date		DATETIME,
	Created_By			INT,
	Edited_Date			DATETIME,
	Edited_By			INT
)
--------------------------------------------------------------------
CREATE TABLE Center 
(
	ID					INT IDENTITY(1,1) PRIMARY KEY,
	Name				VARCHAR(1000),
	User_ID				INT,
	ContactOfPerson		VARCHAR(1000),
	ContactNo			VARCHAR(100),
	OfficeNo			VARCHAR(100),
	Address				VARCHAR(1000),
	InstituteOrSchool	VARCHAR(1000),
	NoOfSeats			INT,
	IsApproved			BIT,
	IsActive			BIT,
	Created_Date		DATETIME,
	Created_By			INT,
	Edited_Date			DATETIME,
	Edited_By			INT
)

--------------------------------------------------------------------
CREATE TABLE Exams 
(
	ID					INT IDENTITY(1,1) PRIMARY KEY,
	Name				VARCHAR(1000),
	ExamDate			DATETIME,
	Duration			INT,
	AnswerKeyFileName	VARCHAR(1000),
	IsActive			BIT,
	Created_Date		DATETIME,
	Created_By			INT,
	Edited_Date			DATETIME,
	Edited_By			INT
)
--------------------------------------------------------------------
CREATE TABLE Student_Center_Mapping
(
	ID					INT IDENTITY(1,1) PRIMARY KEY,
	Exam_ID				INT,
	Center_ID			INT,
	Student_ID			INT,
	Marks				INT,
	IsExamAttended		BIT,
	IsActive			BIT,
	Created_Date		DATETIME,
	Created_By			INT,
	Edited_Date			DATETIME,
	Edited_By			INT
)

--------------------------------------------------------------------
CREATE TABLE Student_Center_Mapping
(
	ID					INT IDENTITY(1,1) PRIMARY KEY,
	Exam_ID				INT,
	Center_ID			INT,
	Student_ID			INT,
	Marks				INT,
	IsExamAttended		BIT,
	IsActive			BIT,
	Created_Date		DATETIME,
	Created_By			INT,
	Edited_Date			DATETIME,
	Edited_By			INT
)


--------------------------------------------------------------------
--------------------------------------------------------------------





