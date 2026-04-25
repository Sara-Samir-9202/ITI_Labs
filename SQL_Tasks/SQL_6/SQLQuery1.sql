SELECT SUSER_NAME() AS CurrentLogin;


SELECT USER_NAME() AS CurrentUser;

USE ITI;
GO

SELECT name, type_desc
FROM sys.database_principals
WHERE type IN ('S','U');  -- S=SQL Login, U=Windows User

USE ITI;
GO

EXEC sp_addrolemember 'db_owner', 'SARA-ELGHAZALLY\dell';
GO


use iti

--1.	 Create a scalar function that takes date and returns Month name of that date.

IF OBJECT_ID('dbo.getmonth', 'FN') IS NOT NULL
    DROP FUNCTION dbo.getmonth;
GO  
CREATE FUNCTION dbo.getmonth(@date DATE)
RETURNS NVARCHAR(20)
AS
BEGIN
    RETURN DATENAME(MONTH, @date)
END
GO 
SELECT dbo.getmonth('2026-03-25') AS MonthName;


--2.	 Create a multi-statements table-valued function that takes 2 integers and returns the values between them.

IF OBJECT_ID('dbo.getvalues', 'FN') IS NOT NULL
    DROP FUNCTION dbo.getvalues;
GO
CREATE FUNCTION dbo.getvalues(@fvalue INT, @svalue INT)
RETURNS @t TABLE
(
    betweenvalue INT
)
AS
BEGIN
  
    WHILE @fvalue + 1 < @svalue
    BEGIN
        SET @fvalue += 1
        INSERT INTO @t
        SELECT @fvalue
    END

    RETURN
END
GO

SELECT * FROM dbo.getvalues(10,20);

--3.	 Create inline function that takes Student No and returns Department Name with Student full name.

IF OBJECT_ID('dbo.getdeptname', 'IF') IS NOT NULL
    DROP FUNCTION dbo.getdeptname;
GO

CREATE FUNCTION dbo.getdeptname(@st_no INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        Student.St_FName + ' ' + Student.St_LName AS FullName,
        Department.Dept_Name
    FROM Student
    INNER JOIN Department
        ON Student.Dept_Id = Department.Dept_Id
    WHERE Student.St_Id = @st_no
);
GO

SELECT * FROM dbo.getdeptname(3);

--4.	Create a scalar function that takes Student ID and returns a message to user 
--a.	If first name and Last name are null then display 'First name & last name are null'
--b.	If First name is null then display 'first name is null'
--c.	If Last name is null then display 'last name is null'
--d.	Else display 'First name & last name are not null'

select * from Student;

UPDATE Student
SET St_Fname = NULL,
    St_Lname = NULL
WHERE St_Id = 5;

IF OBJECT_ID('dbo.getmsg', 'FN') IS NOT NULL
    DROP FUNCTION dbo.getmsg;
GO

CREATE FUNCTION dbo.getmsg(@st_id INT)
RETURNS NVARCHAR(50)
AS
BEGIN
    DECLARE @msg NVARCHAR(50)
    DECLARE @fname NVARCHAR(20)
    DECLARE @lname NVARCHAR(20)

    
    SELECT @fname = St_Fname, @lname = St_Lname
    FROM Student
    WHERE St_Id = @st_id

    SET @msg = CASE
        WHEN @fname IS NULL AND @lname IS NULL THEN 'First name & last name are null'
        WHEN @fname IS NULL AND @lname IS NOT NULL THEN 'first name is null'
        WHEN @fname IS NOT NULL AND @lname IS NULL THEN 'last name is null'
        ELSE 'First name & last name are not null'
    END

    RETURN @msg
END
GO
-- 1️ Both is null
SELECT dbo.getmsg(5) AS msg; 

-- 2️ Fname is NULL 
SELECT dbo.getmsg(14) AS msg;

-- 3️ Lname is NULL
SELECT dbo.getmsg(13) AS msg;

-- 4️ Both is not null
SELECT dbo.getmsg(10) AS msg; 


--5    Create inline function that takes integer which represents manager ID and displays department name, Manager Name and hiring date 


select * from Department;
select * from Instructor;

UPDATE Department
SET Dept_Manager = 1
WHERE Dept_Id = 10;

IF OBJECT_ID('dbo.getdetails', 'IF') IS NOT NULL
    DROP FUNCTION dbo.getdetails;
GO

CREATE FUNCTION dbo.getdetails(@mgr_id INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        Department.Dept_Name,
        Instructor.Ins_Name AS Manager_Name,
        Department.Manager_hiredate
    FROM Department
    INNER JOIN Instructor
        ON Department.Dept_Manager = Instructor.Ins_Id
    WHERE Department.Dept_Manager = @mgr_id
);
GO

SELECT * FROM dbo.getdetails(1);




--6.	Create multi-statements table-valued function that takes a string
--If string='first name' returns student first name
--If string='last name' returns student last name 
--If string='full name' returns Full Name from student table 
--Note: Use “ISNULL” function

select * from Student;

IF OBJECT_ID('dbo.getstname', 'TF') IS NOT NULL
    DROP FUNCTION dbo.getstname;
GO

CREATE FUNCTION dbo.getstname(@string NVARCHAR(50))
RETURNS @t TABLE 
(
    studentName NVARCHAR(50)
)
AS
BEGIN

    IF @string = 'first name'
        INSERT INTO @t
        SELECT ISNULL(St_Fname, 'Fname')
        FROM Student

    ELSE IF @string = 'last name'
        INSERT INTO @t
        SELECT ISNULL(St_Lname, 'Lname')
        FROM Student

    ELSE IF @string = 'full name'
        INSERT INTO @t
        SELECT ISNULL(St_Fname, 'Fname') + ' ' + ISNULL(St_Lname, 'Lname')
        FROM Student

    RETURN
END
GO

SELECT * FROM dbo.getstname('first name');
SELECT * FROM dbo.getstname('last name');
SELECT * FROM dbo.getstname('full name');



--7.	Write a query that returns the Student No and Student first name without the last char

SELECT 
    St_Id AS 'Student No', 
    SUBSTRING(St_Fname, 1, LEN(St_Fname) - 1) AS 'Student first name'
FROM Student


--8.	Wirte query to delete all grades for the students Located in SD Department

select * from Stud_Course
select * from Department

DELETE sc
FROM Stud_Course sc
INNER JOIN Student s ON sc.St_Id = s.St_Id
INNER JOIN Department d ON s.Dept_Id = d.Dept_Id
WHERE d.Dept_Name = 'SD';


--9.	Using Merge statement between the following two tables [User ID, Transaction Amount]

CREATE TABLE dbo.[last transactions](
    UserID INT PRIMARY KEY,
    Amount MONEY
);

INSERT INTO dbo.[last transactions] (UserID, Amount)
VALUES 
(1, 4000),
(4, 2000),
(2, 10000);

CREATE TABLE dbo.[daily transactions](
    UserID INT PRIMARY KEY,
    Amount MONEY
);

INSERT INTO dbo.[daily transactions] (UserID, Amount)
VALUES
(1, 1000), 
(2, 2000),  
(3, 1000);


MERGE INTO dbo.[last transactions] AS l
USING dbo.[daily transactions] AS d
ON l.UserID = d.UserID

WHEN MATCHED THEN
    UPDATE SET l.Amount = d.Amount 

WHEN NOT MATCHED BY TARGET THEN
    INSERT(UserID, Amount)
    VALUES(d.UserID, d.Amount)

WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
GO

SELECT * FROM dbo.[last transactions];




--10.	Try to Create Login Named(ITIStud) who can access Only student and Course tables from ITI DB 
--then allow him to select and insert data into tables and deny Delete and update   

USE ITI;
GO

IF EXISTS (SELECT * FROM sys.database_principals WHERE name = 'ITIStud')
    DROP USER ITIStud;
GO

IF EXISTS (SELECT * FROM sys.server_principals WHERE name = 'ITIStud')
    DROP LOGIN ITIStud;
GO

CREATE LOGIN ITIStud WITH PASSWORD = 'StrongPass123!';
GO

CREATE USER ITIStud FOR LOGIN ITIStud;
GO

GRANT SELECT, INSERT ON dbo.Student TO ITIStud;
GRANT SELECT, INSERT ON dbo.Stud_Course TO ITIStud;

DENY UPDATE, DELETE ON dbo.Student TO ITIStud;
DENY UPDATE, DELETE ON dbo.Stud_Course TO ITIStud;
GO

SELECT * FROM dbo.Student;
INSERT INTO dbo.Student (St_Id, St_Fname, St_Lname, Dept_Id) 
VALUES (200, 'Ali', 'Ahmed', 10);


UPDATE dbo.Student SET St_Fname='Mohamed' WHERE St_Id=100;
DELETE FROM dbo.Student WHERE St_Id=100;

SELECT dp.name AS UserName, dp.type_desc AS UserType, 
       r.name AS RoleName
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.name = 'ITIStud';