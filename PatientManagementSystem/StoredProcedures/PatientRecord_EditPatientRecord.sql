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