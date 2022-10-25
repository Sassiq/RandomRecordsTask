CREATE PROCEDURE CountMedian
AS
	BEGIN
		SELECT TOP (1) Percentile_Disc (0.5)
           WITHIN GROUP (ORDER BY FloatingNumber)
           OVER() AS median
		FROM Records;
	END;