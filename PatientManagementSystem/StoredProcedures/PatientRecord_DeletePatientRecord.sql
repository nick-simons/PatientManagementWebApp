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