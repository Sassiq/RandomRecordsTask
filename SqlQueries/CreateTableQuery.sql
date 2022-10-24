use RecordsDB;

create table Records
(
	Date Date NOT NULL,
	EnglishString nvarchar(10) NOT NULL,
	RussianString nvarchar(10) NOT NULL,
	IntegerNumber int NOT NULL,
	FloatingNumber float NOT NULL,
);