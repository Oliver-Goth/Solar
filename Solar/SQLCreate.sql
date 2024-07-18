-- Self contained script to create database, with the right permissions
USE master

GO

DROP DATABASE Solar

GO

CREATE DATABASE Solar

GO

USE Solar

GO

CREATE TABLE Users (
	ID int IDENTITY(1,1) PRIMARY KEY,
	Username varchar(100),
	Password varchar(2550)
);

CREATE TABLE Installer (
	UserID INT PRIMARY KEY FOREIGN KEY REFERENCES Users(ID),
	Installer varchar(255) NULL,
	AccountNumber varchar(255) NULL,
	Department varchar(255) NULL,
	Contact varchar(255) NULL,
	PhoneNumber char(8) NULL,
	Email varchar(255) NULL,
);

CREATE TABLE Dimensioning (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Description varchar(255)
);

-- Values from schema
INSERT INTO Dimensioning VALUES ('Solcelleanlæg dimensioneres efter forbrug (forbrugsoplysninger udfyldes)'),
('Solcelleanlæg dimensioneres efter størst mulig anlæg/stor produktion'),
('Solcelleanlæg dimensioneres efter ønsket kWp (effekt)')

CREATE TABLE Status (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Status varchar(255)
);

-- Values from schema
INSERT INTO Status VALUES ('Tilbudsanmodning'),('Behandlet'),('Ordre')

CREATE TABLE Project (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	CaseName varchar(255) NULL,
	Address varchar(255) NOT NULL,
	ZIP int NOT NULL,
	Deadline DATE NULL,
	Followup DATE NULL,
	StartDate DATE NULL,
	Installer varchar(255) NULL,
	AccountNumber varchar(255) NULL,
	Department varchar(255) NULL,
	Contact varchar(255) NULL,
	PhoneNumber CHAR(8) NULL,
	Email varchar(255) NULL,
	Remarks varchar(2550) NULL,
	StatusID INT FOREIGN KEY REFERENCES Status(ID),
	UserID INT FOREIGN KEY REFERENCES Users(ID),
	DimensioningID INT FOREIGN KEY REFERENCES Dimensioning(ID)
)

CREATE TABLE RoofType (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	RoofType varchar(50) NOT NULL
)

-- Values from schema
INSERT INTO RoofType VALUES ('Hældningstag'),('Fladt tag'),('Jordstativ')

CREATE TABLE RoofMaterial (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Material varchar(50) NOT NULL,
	RoofTypeID INT REFERENCES RoofType(ID)
)

-- Get the ID from a sloped roof
DECLARE @slopedRoofID AS INT
SELECT @slopedRoofID = ID FROM RoofType WHERE RoofType LIKE 'Hældningstag'

INSERT INTO RoofMaterial VALUES ('Eternit',@slopedRoofID),('Tegl',@slopedRoofID),
('Stål',@slopedRoofID),('Tagpap',@slopedRoofID),('Skifer',@slopedRoofID)

-- Get the ID from flat roof
DECLARE @flatRoofID AS INT
SELECT @flatRoofID = ID FROM RoofType WHERE RoofType LIKE 'Fladt tag'

INSERT INTO RoofMaterial VALUES ('Ballast',@flatRoofID),('Forankret',@flatRoofID)

CREATE TABLE Assembly (
	ProjectID INT PRIMARY KEY FOREIGN KEY REFERENCES Project(ID),
	EastWestDirection BIT NOT NULL,
	Slope DECIMAL(19,2) NULL,
	BuildingHeight DECIMAL (19,2) NULL,
	RoofTypeID INT FOREIGN KEY REFERENCES RoofType(ID),
	RoofMaterialID INT FOREIGN KEY REFERENCES RoofMaterial(ID)
)

CREATE TABLE Battery (
	ProjectID INT PRIMARY KEY FOREIGN KEY REFERENCES Project(ID),
	BatteryPrepare BIT NOT NULL
)

CREATE TABLE BatteryRequest (
	ProjectID INT PRIMARY KEY FOREIGN KEY REFERENCES Project(ID),
	Capacity INT NOT NULL,
)

CREATE TABLE DimensioningkWp (
	ProjectID INT PRIMARY KEY FOREIGN KEY REFERENCES Project(ID),	
	KiloWattPeak DECIMAL(19,2) NOT NULL
)

CREATE TABLE ConsumptionCategory (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Category varchar(50) NOT NULL
)

-- Values from schema
INSERT INTO ConsumptionCategory VALUES ('Privat'),('Erherv'),('Offentlig')

CREATE TABLE DimensioningConsumption (
	ProjectID INT PRIMARY KEY FOREIGN KEY REFERENCES Project(ID),
	CategoryID INT FOREIGN KEY REFERENCES ConsumptionCategory(ID),
	CurrentConsumption INT NULL,
	HeatPump BIT NOT NULL,
	HeatPumpIncluded BIT NOT NULL,
	HouseSize INT NULL,
	ElectricVehicle BIT NULL,
	EVIncluded BIT NULL,
	EvKilometer INT NULL
)