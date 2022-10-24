CREATE PROCEDURE PostRecords
	@Date Date,
	@EnglishString nvarchar(10),
	@RussianString nvarchar(10),
	@IntegerNumber int,
	@FloatingNumber float
AS
	BEGIN
		INSERT INTO Records(Date, EnglishString, RussianString, IntegerNumber, FloatingNumber)
		VALUES (@Date, @EnglishString, @RussianString, @IntegerNumber, @FloatingNumber)
	END