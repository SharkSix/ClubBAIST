CREATE Database ClubBAIST
USE ClubBAIST
Drop TABLE Membership
Drop TABLE Member
Drop TABLE TeeTimeMember
Drop TABLE TeeTime

CREATE TABLE TeeTime
(
	Date VARCHAR(10)NOT NULL,
	Teetime VARCHAR(5)NOT NULL PRIMARY KEY(Date,TeeTime),
	Phone VARCHAR(11),
	NumberOfCarts int,
	Time Decimal NOT NULL
)

CREATE TABLE StandingTeeTimeRequest
(
	ShareholderNumber VARCHAR(7)NOT NULL,
	MemberTwoNumber VARCHAR(7) NOT NULL,
	MemberThreeNumber VARCHAR(7) NOT NULL,
	MemberFourNumber VARCHAR(7) NOT NULL,
	TeeTime VARCHAR(5) NOT NULL,
	StartDate VARCHAR(10) NOT NULL,
	EndDate VARCHAR(10) NOT NUll

	PRIMARY KEY(ShareholderNumber,StartDate)
)

CREATE TABLE Membership
(
	MemberShipCode VARCHAR(2)
	CONSTRAINT PK_MemberShip_MemberShipCode PRIMARY KEY NOT NULL,
	Description VARCHAR(20) NOT NULL

)

CREATE TABLE Member
(
	MemberNumber VARCHAR(7) 
	CONSTRAINT PK_Members_MemberNumber PRIMARY KEY NOT NULL,
	FirstName VARCHAR(25) NOT NULL,
	LastName VARCHAR(25) NOT NULL,
	MemberShipCode VARCHAR(2) 
	CONSTRAINT FK_Member_MemberShipCode_MemberShip_MemberShipCode
	FOREIGN KEY REFERENCES 
	MemberShip(MemberShipCode) NOT NULL
)

CREATE TABLE MembershipApplication
(
	FirstName VARCHAR(25) NOT NULL, 
	LastName VARCHAR(25) NOT NULL,
	Address VARCHAR(200)NOT NULL,
	PostalCode VARCHAR(6)NOT NULL,
	Phone VARCHAR(11)NOT NULL,
	AlternatePhone VARCHAR(11),
	Email VARCHAR(50)NOT NULL,
	DateOfBirth VARCHAR(10)NOT NULL,
	Occupation VARCHAR(50)NOT NULL,
	CompanyName VARCHAR(100)NOT NULL,
	CompanyAddress VARCHAR(200)NOT NULL,
	CompanyPostalCode VARCHAR(6)NOT NULL,
	CompanyPhone VARCHAR(11)NOT NULL,
	Date VARCHAR(10)NOT NULL,
	ShareholderOneNumber VARCHAR(7)NOT NULL,
	ShareholderOneSignDate VARCHAR(10) NOT NULL,
	ShareholderTwoNumber VARCHAR(7)NOT NULL,
	ShareholderTwoSignDate VARCHAR(10)NOT NULL,
	Status VARCHAR(20)NOT NULL

	PRIMARY KEY(FirstName,LastName)
)

CREATE TABLE TeeTimeMember
(
	Date VARCHAR(10)NOT NULL,
	TeeTime VARCHAR(5)NOT NULL, 
	MemberNumber VARCHAR(7) NOT NULL
	PRIMARY KEY(Date,TeeTime,MemberNumber),
	FOREIGN KEY (Date,TeeTime) REFERENCES TeeTime(Date,TeeTime),
	FOREIGN KEY (MemberNumber) REFERENCES Member(MemberNumber)
)
drop table PlayerScore
CREATE TABLE PlayerScore
(
	Date VARCHAR(10)NOT NULL,
	Teetime VARCHAR(5) NOT NULL,
	MemberNumber VARCHAR(7) NOT NULL,
	Course VARCHAR(20) NOT NULL,
	Rating float ,
	Slope float ,
	Hole1 int,
	Hole2 int,
	Hole3 int,
	Hole4 int,
	Hole5 int,
	Hole6 int,
	Hole7 int,
	Hole8 int,
	Hole9 int,
	Hole10 int,
	Hole11 int,
	Hole12 int,
	Hole13 int,
	Hole14 int,
	Hole15 int,
	Hole16 int,
	Hole17 int,
	Hole18 int,
	HandicapDifferential float,
	PRIMARY KEY(Date,Teetime,MemberNumber),
	FOREIGN KEY (Date,Teetime) REFERENCES Teetime(Date,Teetime),
	FOREIGN KEY (MemberNumber) REFERENCES Member(MemberNumber)
)



CREATE PROCEDURE GetTeeTime(@Date VARCHAR(10) = NULL)											
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF  @Date IS NULL 
		RAISERROR('GetTeeTime - Required parameter: @Date',16,1)
	ELSE

		BEGIN
			SELECT Date,Teetime,Phone,NumberOfCarts,Time
			FROM Teetime
			WHERE Date = @Date

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetTeeTime - SELECT error: TeeTime table',16,1)
		END

	RETURN @ReturnCode



CREATE PROCEDURE GetDailyTeeSheet(@Date VARCHAR(10) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF  @Date IS NULL 
		RAISERROR('GetTeeTime - Required parameter: @Date',16,1)
	ELSE
		BEGIN
			SELECT TeeTime.Date,TeeTime.Teetime,TeeTime.Phone,TeeTime.NumberOfCarts,TeeTime.Time,FirstName + ' '+ LastName AS 'MemberName'
			FROM Teetime inner join TeeTimeMember on Teetime.Date = TeeTimeMember.Date and Teetime.Teetime = TeeTimeMember.TeeTime 
							inner join Member on TeeTimeMember.MemberNumber = Member.MemberNumber
			WHERE TeeTimeMember.Date = @Date and TeeTimeMember.Date= TeeTime.Date and TeeTime.Teetime = TeeTimeMember.TeeTime
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetDailyTeeSheet - SELECT error',16,1)
		END

	RETURN @ReturnCode


CREATE PROCEDURE IsMemberExexist(@MemberNumber VARCHAR(7) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF  @MemberNumber IS NULL 
		RAISERROR('IsMemberExexist - Required parameter: @MemberNumber',16,1)
	ELSE
		BEGIN
			SELECT FirstName + LastName as MemberName, MemberShipCode
			FROM Member
			WHERE MemberNumber = @MemberNumber
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('IsMemberExexist - SELECT error: Member table',16,1)
		END

	RETURN @ReturnCode


CREATE PROCEDURE AddTeeTime(@Date VARCHAR(10) = NULL, 
							@Teetime VARCHAR(5) = NULL,
							@Phone VARCHAR(10) = NULL,
							@NumberOfCarts VARCHAR(7) = NULL,
							@Time decimal = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @Date IS NULL OR @Teetime IS NULL OR @Phone IS NULL OR @NumberOfCarts IS NULL OR @Time IS NULL
		RAISERROR('AddTeeTime - Date,TeeTime,Phone,NumberOfCart,Time Field are required',16,1)--sends error to app.
	ELSE
		BEGIN
			INSERT INTO TeeTime
			(Date, Teetime,Phone,NumberOfCarts,Time)
			VALUES
			(@Date, @Teetime,@Phone,@NumberOfCarts,@Time)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddTeeTime - INSERT error: TeeTime Table.',16,1)
		END

	RETURN @ReturnCode

drop 	PROCEDURE AddTeeTimeMember
CREATE PROCEDURE AddTeeTimeMember(@Date VARCHAR(10) = NULL, 
							@Teetime VARCHAR(5) = NULL,
							@MemberNumber VARCHAR(7) = NULL)

AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @Date IS NULL OR @Teetime IS NULL OR @MemberNumber IS NULL 
		RAISERROR('AddTeeTimeMember - Date,TeeTime,MemberNumber Field are required',16,1)--sends error to app.
	ELSE
		BEGIN
			INSERT INTO TeeTimeMember
			(Date, Teetime,MemberNumber)
			VALUES
			(@Date, @Teetime,@MemberNumber )

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddTeeTimeMember - INSERT error: TeeTimeMember Table.',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE ISMemberNumberQualified
CREATE PROCEDURE ISMemberNumberQualified(@MemberNumber VARCHAR(7) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF  @MemberNumber IS NULL 
		RAISERROR('ISMemberNumberQualified - Required parameter: @MemberNumber',16,1)
	ELSE
		BEGIN
			SELECT MemberShipCode
			FROM Member
			WHERE MemberNumber = @MemberNumber and MemberShipCode != 'C1'
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('ISMemberNumberQualified - SELECT error: Member table',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE CheckShareholder
CREATE PROCEDURE CheckShareholder(@MemberNumber VARCHAR(7) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF  @MemberNumber IS NULL 
		RAISERROR('CheckShareholder - Required parameter: @MemberNumber',16,1)
	ELSE
		BEGIN
			SELECT MemberNumber
			FROM Member
			WHERE MemberNumber = @MemberNumber and (MemberShipCode = 'G1' or MemberShipCode = 'G2'or MemberShipCode = 'G3')
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('CheckShareholder - SELECT error: Member table',16,1)
		END

	RETURN @ReturnCode


Drop PROCEDURE IsShareholdHadRequest

CREATE PROCEDURE IsShareholdHadRequest(@MemberNumber VARCHAR(7) = NULL,
										@StartDate VARCHAR(10) = null)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF  @MemberNumber IS NULL 
		RAISERROR('IsShareholdHadRequest - Required parameter: @MemberNumber',16,1)
	ELSE
		BEGIN
			SELECT ShareholderNumber
			FROM StandingTeeTimeRequest
			WHERE ShareholderNumber = @MemberNumber and StartDate=@StartDate
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('IsShareholdHadRequest - SELECT error: StandingTeeTimeRequest table',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE AddStandingTeeTimeRequest
CREATE PROCEDURE AddStandingTeeTimeRequest(@ShareholderNumber VARCHAR(7) = NULL, 
							@MemberTwoNumber VARCHAR(7) = NULL,
							@MemberThreeNumber VARCHAR(7) = NULL,
							@MemberFourNumber VARCHAR(7) = NULL,
							@TeeTime VARCHAR(5) = NULL,
							@StartDate VARCHAR(10) = NULL,
							@EndDate VARCHAR(10) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @ShareholderNumber IS NULL OR @MemberTwoNumber IS NULL OR @MemberThreeNumber IS NULL OR @MemberFourNumber IS NULL OR @TeeTime IS NULL OR @StartDate IS NULL OR @EndDate IS NULL
		RAISERROR('AddStandingTeeTime - @ShareholderNumber,@MemberTwoNumber,Phone,@MemberThreeNumber,@MemberFourNumber,@TeeTime,@StartDate,@EndDate Field are required',16,1)--sends error to app.
	ELSE
		BEGIN
			INSERT INTO StandingTeeTimeRequest
			(ShareholderNumber,MemberTwoNumber,MemberThreeNumber,MemberFourNumber,TeeTime,StartDate,EndDate)
			VALUES
			(@ShareholderNumber,@MemberTwoNumber,@MemberThreeNumber,@MemberFourNumber,@TeeTime,@StartDate,@EndDate)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddStandingTeeTime - INSERT error: StandingTeeTimeRequest Table.',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE GetBookedTeeTime
CREATE PROCEDURE GetBookedTeeTime(@MemberNumber VARCHAR(7) = NULL,
									@Date VARCHAR(10) = NULL,
									@TeeTime VARCHAR(5) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF  @MemberNumber IS NULL OR @Date IS NULL OR @TeeTime IS NULL 
		RAISERROR('GetBookedTeeTime - Required parameter: @MemberNumber,@Date,@TeeTime',16,1)
	ELSE
		BEGIN
			SELECT Teetime.Date,TeeTime.Teetime,TeeTimeMember.MemberNumber,TeeTime.NumberOfCarts,TeeTime.Phone,TeeTime.Time
			FROM TeeTime inner join TeeTimeMember on TeeTime.Date = TeeTimeMember.Date and TeeTime.Teetime=TeeTimeMember.TeeTime
			WHERE TeeTime.Date = @Date and TeeTime.Teetime=@TeeTime and TeeTimeMember.MemberNumber = @MemberNumber
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetBookedTeeTime - SELECT error',16,1)
		END

	RETURN @ReturnCode

CREATE PROCEDURE GetTeeTimeWithMembers(@Date VARCHAR(10) = NULL,
									@TeeTime VARCHAR(5) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF   @Date IS NULL OR @TeeTime IS NULL 
		RAISERROR('GetTeeTimeWithMembers - Required parameter:@Date,@TeeTime',16,1)
	ELSE
		BEGIN
			SELECT Teetime.Date,TeeTime.Teetime,TeeTimeMember.MemberNumber,TeeTime.NumberOfCarts,TeeTime.Phone,TeeTime.Time
			FROM TeeTime inner join TeeTimeMember on TeeTime.Date = TeeTimeMember.Date and TeeTime.Teetime=TeeTimeMember.TeeTime
			WHERE TeeTime.Date = @Date and TeeTime.Teetime=@TeeTime 
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetTeeTimeWithMembers - SELECT error',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE UpdateTeeTime
CREATE PROCEDURE UpdateTeeTime(@Date VARCHAR(10) = NULL, 
								@Teetime VARCHAR(5) = NULL,
								@Phone VARCHAR(10) = NULL,
								@NumberOfCarts int = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF @Date IS NULL OR @Teetime IS NULL OR @Phone IS NULL OR @NumberOfCarts IS NULL 
		RAISERROR('AddTeeTime - Date,TeeTime,Phone,NumberOfCart,Time Field are required',16,1)--sends error to app.
	ELSE
		BEGIN
			UPDATE TeeTime
			SET Phone=@Phone,NumberOfCarts=@NumberOfCarts
			WHERE Date = @Date and Teetime=@Teetime 
			
			delete FROM TeeTimeMember WHERE Date = @Date and Teetime=@Teetime

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('UpdateTeeTime - Update error',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE DeleteTeeTime
CREATE PROCEDURE DeleteTeeTime(@Date VARCHAR(10) = NULL, 
								@Teetime VARCHAR(5) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF @Date IS NULL OR @Teetime IS NULL 
		RAISERROR('DeleteTeeTime - Date,TeeTime Fields are required',16,1)--sends error to app.
	ELSE
		BEGIN
			delete FROM TeeTimeMember WHERE Date = @Date and Teetime=@Teetime
			delete FROM TeeTime WHERE Date = @Date and Teetime=@Teetime
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('DeleteTeeTime - Delete error',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE DeleteStandingTeeTime

CREATE PROCEDURE DeleteStandingTeeTime(@StartDate VARCHAR(10) = NULL, 
								@MemberNumber VARCHAR(7) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		IF @StartDate IS NULL OR @MemberNumber IS NULL 
		RAISERROR('CancleStandingTeeTime - @StartDate,@MemberNumber Fields are required',16,1)--sends error to app.
	ELSE
		BEGIN
			delete FROM StandingTeeTimeRequest WHERE StartDate = @StartDate and ShareholderNumber=@MemberNumber
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('DeleteTeeTime - Delete error',16,1)
		END

	RETURN @ReturnCode


CREATE PROCEDURE AddMembershipApplication(@FirstName VARCHAR(25) = NULL, 
							@LastName VARCHAR(25) = NULL,
							@Address VARCHAR(200) = NULL,
							@PostalCode VARCHAR(6) = NULL,
							@Phone VARCHAR(11) = NULL,
							@AlternatePhone VARCHAR(11) = NULL,
							@Email VARCHAR(50) = NULL,
							@DateOfBirth VARCHAR(10) = NULL,
							@Occupation VARCHAR(50) = NULL,
							@CompanyName VARCHAR(100) = NULL,
							@CompanyAddress VARCHAR(200) = NULL,
							@CompanyPostalCode VARCHAR(6) = NULL,
							@CompanyPhone VARCHAR(11) = NULL,
							@Date VARCHAR(10) = NULL,
							@ShareholderOneNumber VARCHAR(7) = NULL,
							@ShareholderOneSignDate VARCHAR(10) = NULL,
							@ShareholderTwoNumber VARCHAR(7) = NULL,
							@ShareholderTwoSignDate VARCHAR(10) = NULL,
							@Status VARCHAR(20) = NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @LastName IS NULL OR @FirstName IS NULL OR @Address IS NULL OR @PostalCode IS NULL OR @Phone IS NULL OR @Email IS NULL OR 
	@DateOfBirth IS NULL OR @Occupation IS NULL OR @CompanyName IS NULL OR @CompanyAddress IS NULL OR @CompanyPostalCode IS NULL OR @CompanyPhone IS NULL OR @Date IS NULL OR
	@ShareholderOneNumber IS NULL OR @ShareholderOneSignDate IS NULL OR @ShareholderTwoNumber IS NULL OR @ShareholderTwoSignDate IS NULL OR @Status IS NULL 
		RAISERROR('AddMembershipApplication - All Fields except @AlternatePhone are all required',16,1)--sends error to app.
	ELSE
		BEGIN
			INSERT INTO MembershipApplication
			(FirstName,LastName,Address,PostalCode,Phone,AlternatePhone,Email,DateOfBirth,Occupation,
			CompanyName,CompanyAddress,CompanyPostalCode,CompanyPhone,Date,ShareholderOneNumber,ShareholderOneSignDate,
			ShareholderTwoNumber,ShareholderTwoSignDate,Status)
			VALUES
			(@FirstName,@LastName,@Address,@PostalCode,@Phone,@AlternatePhone,@Email,@DateOfBirth,@Occupation,
			@CompanyName,@CompanyAddress,@CompanyPostalCode,@CompanyPhone,@Date,@ShareholderOneNumber,@ShareholderOneSignDate,
			@ShareholderTwoNumber,@ShareholderTwoSignDate,@Status)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddMembershipApplication - INSERT error: MembershipApplication Table.',16,1)
		END

	RETURN @ReturnCode

drop PROCEDURE GetAllMembershipAppliactions
CREATE PROCEDURE GetAllMembershipAppliactions
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		BEGIN
			SELECT FirstName,LastName,Date,Status
			FROM MembershipApplication

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('MembershipApplication - SELECT error',16,1)
		END

	RETURN @ReturnCode

CREATE PROCEDURE GetMembershipAppliactionByName(@FirstName VARCHAR(25) = NULL, 
							@LastName VARCHAR(25) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		BEGIN
			SELECT *
			FROM MembershipApplication
			where FirstName=@FirstName and LastName=@LastName
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('GetMembershipAppliactionByName - SELECT error',16,1)
		END

	RETURN @ReturnCode

DROp PROCEDURE GetAllMember
CREATE PROCEDURE GetAllMember
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		BEGIN
			SELECT FirstName,LastName,MemberNumber
			FROM Member
			order by MemberNumber DESC

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('MembershipApplication - SELECT error',16,1)
		END

	RETURN @ReturnCode

CREATE PROCEDURE AddMember(@MemberNumber VARCHAR(7) = NULL, 
							@FirstName VARCHAR(25) = NULL,
							@LastName VARCHAR(25) = NULL,
							@MemberShipCode VARCHAR(2) =NULL)
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1

	IF @MemberNumber IS NULL OR @FirstName IS NULL OR @LastName IS NULL OR @MemberShipCode IS NULL
		RAISERROR('AddMember - All Fields are required',16,1)--sends error to app.
	ELSE
		BEGIN
			INSERT INTO Member
			(MemberNumber,FirstName,LastName,MemberShipCode)
			VALUES
			(@MemberNumber,@FirstName,@LastName,@MemberShipCode)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddMember - INSERT error: Member Table.',16,1)
		END

	RETURN @ReturnCode
Drop PROCEDURE GetMemberByNumber
CREATE PROCEDURE GetMemberByNumber(@MemberNumber VARCHAR(7) = NULL)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		BEGIN
			SELECT FirstName,LastName,MemberNumber,Description
			FROM Member inner join Membership on Member.MemberShipCode=Membership.MemberShipCode
			where MemberNumber=@MemberNumber
			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('Member table - SELECT error',16,1)
		END

	RETURN @ReturnCode
drop PROCEDURE AddPlayerScore
CREATE PROCEDURE AddPlayerScore(@Date VARCHAR(10) = NULL, @Teetime VARCHAR(5) = NULL,@MemberNumber VARCHAR(7) = NULL,
							@Course VARCHAR(20) = Null,@Rating float,@Slope float,@Hole1 int =0,@Hole2 int =0,@Hole3 int =0,@Hole4 int =0,@Hole5 int =0,@Hole6 int =0,
							@Hole7 int =0,@Hole8 int =0,@Hole9 int =0,@Hole10 int =0,@Hole11 int =0,@Hole12 int =0,@Hole13 int =0,@Hole14 int =0,@Hole15 int =0,
							@Hole16 int =0,@Hole17 int =0,@Hole18 int =0,@HandicapDifferential float)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		BEGIN
			INSERT INTO PlayerScore
			(Date, Teetime,MemberNumber,Course,Rating,Slope,Hole1,Hole2,Hole3,Hole4,Hole5,Hole6,Hole7,Hole8,Hole9,Hole10,
			Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HandicapDifferential)
			VALUES
			(@Date, @Teetime,@MemberNumber,@Course,@Rating,@Slope,@Hole1,@Hole2,@Hole3,@Hole4,@Hole5,@Hole6,@Hole7,@Hole8,@Hole9,@Hole10,
			@Hole11,@Hole12,@Hole13,@Hole14,@Hole15,@Hole16,@Hole17,@Hole18,@HandicapDifferential)

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddPlayerScore - INSERT error: PlayerScore Table.',16,1)
		END

	RETURN @ReturnCode

DROP PROCEDURE GetHandicapReport
CREATE PROCEDURE GetHandicapReport(@Time datetime)
												
AS
	DECLARE @ReturnCode INT
	SET @ReturnCode = 1
		BEGIN
			select Member.MemberNumber,Member.FirstName+' '+Member.LastName as 'Member Name',convert(datetime,PlayerScore.Date) as 'date',PlayerScore.HandicapDifferential 
			from PlayerScore inner join Member on PlayerScore.MemberNumber=Member.MemberNumber 
			where convert(datetime,PlayerScore.Date) <=dateadd(dd,-1,dateadd(m,1,@Time))
			order by PlayerScore.Date DESC

			IF @@ERROR = 0
				SET @ReturnCode = 0
			ELSE
				RAISERROR('AddPlayerScore - INSERT error: PlayerScore Table.',16,1)
		END

	RETURN @ReturnCode


select Member.MemberNumber,Member.FirstName+' '+Member.LastName as 'Member Name',convert(datetime,PlayerScore.Date) as 'date',PlayerScore.HandicapDifferential 
			from PlayerScore inner join Member on PlayerScore.MemberNumber=Member.MemberNumber 
			where convert(datetime,PlayerScore.Date) <=dateadd(m,1,@Time)
			order by PlayerScore.Date DESC
			
(select date =convert(datetime,PlayerScore.Date)

select ÈÕÆÚ =convert(datetime,'05/03/2020')

INSERT INTO TeeTime
(Date,Teetime,Phone,NumberOfCarts,Time)
VALUES
('05/03/2020', '07:07','7807807801','1','2'),
('05/04/2020', '07:07','7807807801','1','2'),
('05/05/2020', '07:07','7807807801','1','2'),
('05/06/2020', '07:07','7807807801','1','2'),
('05/07/2020', '07:07','7807807801','1','2'),
('05/08/2020', '07:07','7807807801','1','2'),
('05/09/2020', '07:07','7807807801','1','2'),
('05/10/2020', '07:07','7807807801','1','2'),
('05/11/2020', '07:07','7807807801','1','2'),
('05/12/2020', '07:07','7807807801','1','2'),
('05/13/2020', '07:07','7807807801','1','2'),
('05/14/2020', '07:07','7807807801','1','2'),
('05/15/2020', '07:07','7807807801','1','2'),
('05/16/2020', '07:07','7807807801','1','2'),
('05/17/2020', '07:07','7807807801','1','2'),
('05/18/2020', '07:07','7807807801','1','2'),
('05/19/2020', '07:07','7807807801','1','2'),
('05/20/2020', '07:07','7807807801','1','2'),
('05/21/2020', '07:07','7807807801','1','2'),
('05/22/2020', '07:07','7807807801','1','2'),
('05/23/2020', '07:07','7807807801','1','2'),
('05/24/2020', '07:07','7807807801','1','2'),
('05/25/2020', '07:07','7807807801','1','2'),
('05/26/2020', '07:07','7807807801','1','2'),
('05/27/2020', '07:07','7807807801','1','2'),
('05/28/2020', '07:07','7807807801','1','2'),
('05/29/2020', '07:07','7807807801','1','2'),
('05/30/2020', '07:07','7807807801','1','2'),
('05/31/2020', '07:07','7807807801','1','2')
('05/02/2020', '07:07','7807807801','1','2'),
('05/01/2020', '07:07','7807807801','1','2'),
('05/01/2020', '07:07','7807807801','1','2'),

INSERT INTO TeeTimeMember
(Date,Teetime,MemberNumber)
VALUES
--('05/02/2020', '07:07','0000003'),
--('05/01/2020', '07:07','0000003'),
--('05/01/2020', '07:07','0000002'),
('05/03/2020', '07:07','0000001'),
('05/04/2020', '07:07','0000001'),
('05/05/2020', '07:07','0000001'),
('05/06/2020', '07:07','0000001'),
('05/07/2020', '07:07','0000001'),
('05/08/2020', '07:07','0000001'),
('05/09/2020', '07:07','0000001'),
('05/10/2020', '07:07','0000001'),
('05/11/2020', '07:07','0000001'),
('05/12/2020', '07:07','0000001'),
('05/13/2020', '07:07','0000001'),
('05/14/2020', '07:07','0000001'),
('05/15/2020', '07:07','0000001'),
('05/16/2020', '07:07','0000001'),
('05/17/2020', '07:07','0000001'),
('05/18/2020', '07:07','0000001'),
('05/19/2020', '07:07','0000001'),
('05/20/2020', '07:07','0000001'),
('05/21/2020', '07:07','0000001'),
('05/22/2020', '07:07','0000001'),
('05/23/2020', '07:07','0000001'),
('05/24/2020', '07:07','0000001'),
('05/25/2020', '07:07','0000001'),
('05/26/2020', '07:07','0000001'),
('05/27/2020', '07:07','0000001'),
('05/28/2020', '07:07','0000001'),
('05/29/2020', '07:07','0000001'),
('05/30/2020', '07:07','0000001'),
('05/31/2020', '07:07','0000001'),
('05/03/2020', '07:07','0000002'),
('05/04/2020', '07:07','0000002'),
('05/05/2020', '07:07','0000002'),
('05/06/2020', '07:07','0000002'),
('05/07/2020', '07:07','0000002'),
('05/08/2020', '07:07','0000002'),
('05/09/2020', '07:07','0000002'),
('05/10/2020', '07:07','0000002'),
('05/11/2020', '07:07','0000002'),
('05/12/2020', '07:07','0000002'),
('05/13/2020', '07:07','0000002'),
('05/14/2020', '07:07','0000002'),
('05/15/2020', '07:07','0000002'),
('05/16/2020', '07:07','0000002'),
('05/17/2020', '07:07','0000002'),
('05/18/2020', '07:07','0000002'),
('05/19/2020', '07:07','0000002'),
('05/20/2020', '07:07','0000002'),
('05/21/2020', '07:07','0000002'),
('05/22/2020', '07:07','0000002'),
('05/23/2020', '07:07','0000002'),
('05/24/2020', '07:07','0000002'),
('05/25/2020', '07:07','0000002'),
('05/26/2020', '07:07','0000002'),
('05/27/2020', '07:07','0000002'),
('05/28/2020', '07:07','0000002'),
('05/29/2020', '07:07','0000002'),
('05/30/2020', '07:07','0000002'),
('05/31/2020', '07:07','0000002')




INSERT INTO Membership
(MemberShipCode,Description)
VALUES
('G1', 'Shareholder'),
('G2', 'Senior Shareholder'),
('G3', 'Associate'),
('S1', 'Shareholder Spouse'),
('S2', 'Associate Spouse'),
('B1', 'Pee Wee'),
('B2', 'Junior'),
('B3', 'Intermediate'),
('C1', 'Social')


INSERT INTO Member
(MemberNumber,FirstName,LastName,MemberShipCode)
VALUES
('0000003', 'Test2','Test2','G3'),
('0000002', 'Test1','Test1','G2')

INSERT INTO PlayerScore
			(Date, Teetime,MemberNumber,Course,Rating,Slope,Hole1,Hole2,Hole3,Hole4,Hole5,Hole6,Hole7,Hole8,Hole9,Hole10,
			Hole11,Hole12,Hole13,Hole14,Hole15,Hole16,Hole17,Hole18,HandicapDifferential)
			VALUES
			('05/01/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','2.2'),
			('05/02/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','4.8'),
			('05/03/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','7.8'),
			('05/04/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','6.4'),
			('05/05/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','7.0'),
			('05/06/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','8.0'),
			('05/07/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','11.5'),
			('05/08/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','2.8'),
			('05/09/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','7.9'),
			('05/10/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','8.7'),
			('05/11/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','9.8'),
			('05/12/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','12.2'),
			('05/13/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','3.6'),
			('05/14/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','4.1'),
			('05/15/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','5.2'),
			('05/16/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','6.8'),
			('05/17/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','8.4'),
			('05/18/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','7.7'),
			('05/19/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','3.3'),
			('05/20/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','4.2'),
			('05/21/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','3.3'),
			('05/22/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','2.2'),
			('05/23/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','3.3'),
			('05/24/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','3.3'),
			('05/25/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','2.1'),
			('05/26/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','7.4'),
			('05/27/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','6.6'),
			('05/28/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','6.5'),
			('05/29/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','1.2'),
			('05/30/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','3.3'),
			('05/31/2020', '07:07','0000002','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			'8','11','4','3','2','7','3','11','-3.3')
			--('05/01/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','1.0'),
			--('05/02/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','2.0'),
			--('05/03/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','3.0'),
			--('05/04/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','4.0'),
			--('05/05/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','5.0'),
			--('05/06/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','6.0'),
			--('05/07/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','7.0'),
			--('05/08/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','8.0'),
			--('05/09/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','9.0'),
			--('05/10/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','31.0'),
			--('05/11/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','30.0'),
			--('05/12/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','29.0'),
			--('05/13/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','28.0'),
			--('05/14/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','27.0'),
			--('05/15/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','26.0'),
			--('05/16/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','23.0'),
			--('05/17/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','22.0'),
			--('05/18/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','21.0'),
			--('05/19/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','20.0'),
			--('05/20/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','10.0'),
			--('05/21/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','11.0'),
			--('05/22/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','12.0'),
			--('05/23/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','13.0'),
			--('05/24/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','14.0'),
			--('05/25/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','15.0'),
			--('05/26/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','16.0'),
			--('05/27/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','17.0'),
			--('05/28/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','18.0'),
			--('05/29/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','19.0'),
			--('05/30/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','20.0'),
			--('05/31/2020', '07:07','0000001','ClubBaist','69.1','121','4','5','6','7','8','8','9','11','12','8',
			--'8','11','4','3','2','7','3','11','18')
