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