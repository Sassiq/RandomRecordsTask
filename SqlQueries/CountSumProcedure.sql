CREATE PROCEDURE CountSum
AS
	BEGIN
		SELECT SUM(CAST(Records.IntegerNumber as BIGINT)) as sum FROM Records;
	END;