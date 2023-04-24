--Create a new database
IF db_id('PatientRecordsDB') IS NULL
CREATE DATABASE PatientRecordsDB
GO

--Create PatientRecords table in the db
use PatientRecordsDB

CREATE TABLE PatientRecords
(
    Id INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    BirthDate DATE NOT NULL,
    Gender CHAR(1) NOT NULL
);
GO

--Initialize DB with some values
use PatientRecordsDB
IF (NOT EXISTS (SELECT 1 FROM PatientRecords))
BEGIN
INSERT INTO PatientRecords (FirstName, LastName, BirthDate, Gender)
VALUES
('John', 'Doe', '01/01/1990', 'M'),
('Jane', 'Doe', '06/15/1988', 'F'),
('Bill', 'Smith', '04/15/2002', 'M'),
('Sarah', 'Marshall', '12/12/1974', 'F');
END;
GO

--Create stored proc for getting all patient records
SET QUOTED_IDENTIFIER ON 
GO 

CREATE OR ALTER PROCEDURE [dbo].[PatientRecord_GetAllPatientRecords]
AS
BEGIN
    SELECT 
        Id,
        FirstName,
        LastName,
        BirthDate,
        Gender
    FROM dbo.PatientRecords;
END

--Create stored proc for adding patient records to db
SET QUOTED_IDENTIFIER ON 
GO 

CREATE OR ALTER PROCEDURE [dbo].[PatientRecord_AddPatientRecord]
(
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @BirthDate DATE,
    @Gender CHAR(1)
)
AS
BEGIN
    INSERT INTO dbo.PatientRecords
    (
        [FirstName],
        [LastName],
        [BirthDate],
        [Gender]
    )
    OUTPUT
        inserted.Id
    VALUES
    (
        @FirstName,
        @LastName,
        @BirthDate,
        @Gender
    );    
END

--Create stored proc for updating a patient record
SET QUOTED_IDENTIFIER ON 
GO 

CREATE OR ALTER PROCEDURE [dbo].[PatientRecord_EditPatientRecord]
(
    @RecordId INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @BirthDate DATE,
    @Gender CHAR(1)
)
AS
BEGIN
    UPDATE dbo.PatientRecords
    SET
        FirstName = @FirstName,
        LastName = @LastName,
        BirthDate = @BirthDate,
        Gender = @Gender
    WHERE
        Id = @RecordId;
END

--Create stored procedure for deleting a patient record
SET QUOTED_IDENTIFIER ON 
GO 

CREATE OR ALTER PROCEDURE [dbo].[PatientRecord_DeletePatientRecord]
(
    @RecordId INT
)
AS
BEGIN
    DELETE FROM dbo.PatientRecords
    WHERE
        Id = @RecordId;
END