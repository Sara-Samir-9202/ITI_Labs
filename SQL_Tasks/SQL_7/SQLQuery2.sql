Use ITI

--1.Create a view that displays student full name, course name if the student has a grade more than 50. 

IF OBJECT_ID('dbo.Student_Details', 'V') IS NOT NULL
    DROP VIEW dbo.Student_Details;
GO

CREATE VIEW dbo.Student_Details
AS
SELECT 
    CONCAT(S.St_Fname, ' ', S.St_Lname) AS FullName,
    C.Crs_Name AS CourseName,
    SC.Grade
FROM dbo.Student AS S
INNER JOIN dbo.Stud_Course AS SC 
    ON S.St_Id = SC.St_Id
INNER JOIN dbo.Course AS C 
    ON C.Crs_Id = SC.Crs_Id
WHERE SC.Grade > 50;
GO

SELECT * FROM dbo.Student_Details;
GO



--2.Create an Encrypted view that displays manager names and the topics they teach.

IF OBJECT_ID('dbo.View_ManagerDetails', 'V') IS NOT NULL
    DROP VIEW dbo.View_ManagerDetails;
GO

CREATE VIEW dbo.View_ManagerDetails
WITH ENCRYPTION
AS
SELECT 
    Ins.Ins_Name AS ManagerName,
    T.Top_Name AS Topic
FROM dbo.Department AS Dept
INNER JOIN dbo.Instructor AS Ins 
    ON Dept.Dept_Manager = Ins.Ins_Id
INNER JOIN dbo.Ins_Course IC 
    ON Ins.Ins_Id = IC.Ins_Id
INNER JOIN dbo.Course C 
    ON IC.Crs_Id = C.Crs_Id
INNER JOIN dbo.Topic T 
    ON C.Top_Id = T.Top_Id;
GO

SELECT * FROM dbo.View_ManagerDetails;
GO


EXEC sp_helptext 'dbo.View_ManagerDetails';

--3.Create a view that will display Instructor Name, Department Name for the ‘SD’ or ‘Java’ Department 

IF OBJECT_ID('dbo.View_InsDeptDetails', 'V') IS NOT NULL
    DROP VIEW dbo.View_InsDeptDetails;
GO

CREATE VIEW dbo.View_InsDeptDetails
AS
SELECT 
    Ins.Ins_Name AS InstructorName,
    Dept.Dept_Name AS DepartmentName
FROM dbo.Instructor AS Ins
INNER JOIN dbo.Department AS Dept
    ON Dept.Dept_Id = Ins.Dept_Id
WHERE Dept.Dept_Name IN ('SD', 'Java');
GO

SELECT * FROM dbo.View_InsDeptDetails;
GO



--4.	 Create a view “V1” that displays student data for student who lives in Alex or Cairo. 
--Note: Prevent the users to run the following query 
--Update V1 set st_address=’tanta’
--Where st_address=’alex’;

IF OBJECT_ID('dbo.V1', 'V') IS NOT NULL
    DROP VIEW dbo.V1;
GO

CREATE VIEW dbo.V1
AS
SELECT *
FROM dbo.Student
WHERE St_Address IN ('Alex', 'Cairo')
WITH CHECK OPTION;  
GO

SELECT * FROM dbo.V1;
GO

UPDATE V1
SET St_Address = 'Tanta'
WHERE St_Address = 'Alex';  


--5.Create a view that will display the project name and the number of employees work on it. “Use Company DB”

USE Company_SD;
GO

IF OBJECT_ID('dbo.View_ProjectDetails', 'V') IS NOT NULL
    DROP VIEW dbo.View_ProjectDetails;
GO

CREATE VIEW dbo.View_ProjectDetails
AS
SELECT 
    P.Pname AS ProjectName,
    COUNT(E.SSN) AS NumOfEmps
FROM Company.Project AS P
INNER JOIN dbo.Works_For WF
    ON P.Pnumber = WF.Pno
INNER JOIN [Human Resource].Employee AS E
    ON E.SSN = WF.ESSn
GROUP BY P.Pname;
GO

SELECT * 
FROM dbo.View_ProjectDetails;
GO

--6.	Create the following schema and transfer the following tables to it 
--a.	Company Schema 
--i.	Department table (Programmatically)
--ii.	Project table (by wizard)
--b.	Human Resource Schema
--i.	Employee table (Programmatically)

--a.Company Schema 
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Company')
    EXEC('CREATE SCHEMA Company');
GO

--i.Department table (Programmatically)
IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL
    ALTER SCHEMA Company TRANSFER dbo.Departments;
GO

--b.Human Resource Schema
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Human Resource')
    EXEC('CREATE SCHEMA [Human Resource]');
GO

--i.Employee table (Programmatically)
IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL
    ALTER SCHEMA [Human Resource] TRANSFER dbo.Employee;
GO




--7.Create index on column (manager_Hiredate) that allow u to cluster the data in table Department. What will happen?

Use ITI

EXEC sp_help 'Department';

-- can't use clustered cause the table have primary key and already one clustered index
--PK_Department	clustered, unique, primary key located on PRIMARY	Dept_Id

Create nonclustered index  manager_Hiredate on Department(Manager_hiredate);


--8.	Create index that allow u to enter unique ages in student table. What will happen?
select * from Student;

Create unique index Unique_Age on Student(st_age) 

--9.	Create a cursor for Employee table that increases Employee salary by 10% if Salary <3000 
--and increases it by 20% if Salary >=3000. Use company DB

USE CompanyDB;
GO
select * from DEPARTMENT;
DELETE FROM Employee;

INSERT INTO Employee (SSN, FNAME, LNAME, BDATE, ADDRESS, SEX, SALARY, SUPERSSN, DNO)
VALUES
('E001','Ahmed','Ali','1990-01-10','Cairo','M',2500,NULL,1),  
('E002','Sara','Mohamed','1985-03-20','Alex','F',3200,NULL,2), 
('E003','Omar','Hassan','1992-07-15','Giza','M',2800,NULL,1),  
('E004','Mona','Said','1988-11-05','Cairo','F',4500,NULL,2);   


USE CompanyDB;  
GO

DECLARE @SSN CHAR(10);   
DECLARE @Salary MONEY;

DECLARE Emp_Cursor CURSOR FOR
SELECT SSN, Salary
FROM Employee
FOR UPDATE; 

OPEN Emp_Cursor;

FETCH NEXT FROM Emp_Cursor INTO @SSN, @Salary;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF @Salary < 3000
        UPDATE Employee
        SET Salary = Salary + (@Salary * 0.1)  
        WHERE CURRENT OF Emp_Cursor;
    ELSE
        UPDATE Employee
        SET Salary = Salary + (@Salary * 0.2) 
        WHERE CURRENT OF Emp_Cursor;

    FETCH NEXT FROM Emp_Cursor INTO @SSN, @Salary;
END


CLOSE Emp_Cursor;
DEALLOCATE Emp_Cursor;
GO

SELECT SSN, FNAME, LNAME, SALARY
FROM Employee;




--10.	Display Department name with its manager name using cursor.
USE ITI;
GO

DECLARE DeptDetails CURSOR
FOR 
SELECT Dept_Name, Ins_Name
FROM Department
INNER JOIN Instructor 
    ON Department.Dept_Manager = Instructor.Ins_Id
FOR READ ONLY;  

DECLARE @deptName NVARCHAR(20), 
        @mangName NVARCHAR(20);

OPEN DeptDetails;

FETCH NEXT FROM DeptDetails INTO @deptName, @mangName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @deptName AS DepartmentName, @mangName AS ManagerName;
    FETCH NEXT FROM DeptDetails INTO @deptName, @mangName;
END

CLOSE DeptDetails;
DEALLOCATE DeptDetails;
GO

--11.	Try to display all instructor names in one cell separated by comma. Using Cursor

declare instNames cursor
for select distinct Ins_Name
from Instructor where Ins_Name is not null
for read only
declare @name varchar(20),@all_names varchar(300)=''
open instNames
fetch instNames into @name
while @@FETCH_STATUS=0
	begin
		set @all_names=concat(@all_names,' , ',@name)
		fetch instNames into @name 
	end
select @all_names
close instNames
deallocate instNames

--12.Try to generate script from DB ITI that describes all tables and views in this DB

-- Error because of encryption of views
