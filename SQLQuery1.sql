-- Creating Table for Personal_info--
CREATE TABLE [dbo].Personal_Info(
[Id] NVARCHAR(128) NOT NULL,
[Fname] NVARCHAR(MAX) NOT NULL,
[Lname] NVARCHAR(MAX),
[DOB] DATE NOT NULL,
[Gender] NVARCHAR(20) NOT NULL,
[Contact_No] NVARCHAR(15) NOT NULL,
[Address] NVARCHAR(MAX) NOT NULL,
PRIMARY KEY (Id),
FOREIGN KEY (Id) REFERENCES AspNetUsers(Id)
);
GO

-- Creating Table for health_info--
CREATE TABLE [dbo].Health_Info(
[Id] INT IDENTITY(1,1) NOT NULL,
[Alchol_Consumption] NVARCHAR(MAX) NOT NULL,
[Smoking] NVARCHAR(MAX) NOT NULL,
[Height]  NUMERIC (10,3)NOT NULL,
[Weight] NUMERIC (10,3)NOT NULL,
[Mood_Level] NVARCHAR(MAX) NOT NULL,
[Date] DATE NOT NULL,
[PId] NVARCHAR(128) NOT NULL,
PRIMARY KEY (Id),
FOREIGN KEY (PId) REFERENCES Personal_Info(Id)
);
GO

-- Creating Table for employee_info--
CREATE TABLE [dbo].Employee_Info(
[Id] NVARCHAR(128) NOT NULL,
[Fname] NVARCHAR(max) NOT NULL,
[Lname] NVARCHAR(MAX),
[Role]	NVARCHAR(MAX) NOT NULL,
[DOB] DATE NOT NULL,
[Gender] NVARCHAR(MAX) NOT NULL,
[Contact_No] NVARCHAR(15) NOT NULL,
[Address] NVARCHAR(MAX) NOT NULL,
PRIMARY KEY (Id),
FOREIGN KEY (Id) REFERENCES AspNetUsers(Id)
);
GO

-- Create Table for  Location--
CREATE TABLE [dbo].Location(
[Id] INT IDENTITY(1,1) NOT NULL,
[Loc_Name] NVARCHAR(max) NOT NULL,
[Description] NVARCHAR(MAX) NOT NULL,
[Address] NVARCHAR(MAX) NOT NULL,
[PId] NVARCHAR(128) NOT NULL,
PRIMARY KEY (Id),
FOREIGN KEY (PId) REFERENCES Personal_Info(Id)
);


-- Create Table for Reservation Appiontment--
CREATE TABLE [dbo].Reservation(
[R_Id] INT IDENTITY(1,1) NOT NULL,
[R_DateTime] DATETIME NOT NULL,
[Reason] NVARCHAR(MAX) NOT NULL,
[PId] NVARCHAR(128) ,
[R_Status] NVARCHAR(20) ,
[EId] NVARCHAR(128) ,
PRIMARY KEY (R_Id),
FOREIGN KEY (PId) REFERENCES dbo.Personal_Info(Id),
FOREIGN KEY (EId) REFERENCES dbo.Employee_Info(Id)
);
GO
