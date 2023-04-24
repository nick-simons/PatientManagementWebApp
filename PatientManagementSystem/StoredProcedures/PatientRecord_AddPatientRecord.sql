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