
--1.Create a stored procedure without parameters to show the number of students per department name.[use ITI DB]
USE ITI;
GO

CREATE PROCEDURE StudentNumbers
AS
BEGIN
    SELECT 
        D.Dept_Name AS DepartmentName,
        COUNT(S.St_Id) AS StudentNumber
    FROM Department D
    LEFT JOIN Student S 
        ON S.Dept_Id = D.Dept_Id
    GROUP BY D.Dept_Name;
END;
GO

EXEC StudentNumbers;



--2.Create a stored procedure that will check for the # of employees in the project p1 
--if they are more than 3 print message to the user 'The number of employees in the project p1 is 3 or more'
--if they are less display a message to the user 'The following employees work for the project p1'
--in addition to the first name and last name of each one. [Company DB] 

USE Company_SD;
GO

CREATE PROCEDURE EmpNumbers 
    @pname VARCHAR(20)
AS
BEGIN
    DECLARE @num INT;

    SELECT @num = COUNT(ESSn)
    FROM Company.Project 
    INNER JOIN Works_for 
        ON Project.Pnumber = Works_for.Pno
    INNER JOIN [Human Resource].Employee 
        ON Employee.SSN = Works_for.ESSn
    WHERE Project.Pname = @pname;

    IF @num >= 3
    BEGIN
        SELECT 
            'The number of employees in the project ' + @pname + ' is 3 or more' AS Message,
            'More than 3' AS EmployeeName;
    END
    ELSE
    BEGIN
        SELECT 
            'The following employees work for the project ' + @pname AS Message,
            CONCAT(Fname, ' ', Lname) AS EmployeeName
        FROM Company.Project 
        INNER JOIN Works_for 
            ON Project.Pnumber = Works_for.Pno
        INNER JOIN  [Human Resource].Employee
            ON Employee.SSN = Works_for.ESSn
        WHERE Project.Pname = @pname;
    END
END;
GO

EXEC EmpNumbers 'Al Rowad';
EXEC EmpNumbers 'Al Solimaniah';
EXEC EmpNumbers 'Al Rabwah';


--3.Create a stored procedure that will be used in case there is an old employee has left the project 
--and a new one become instead of him. The procedure should take 3 parameters 
--(old Emp.number, new Emp.number and the project number) and it will be used to update works_on table. [Company DB]

USE Company_SD;
GO

CREATE PROCEDURE update_WorksFor  
    @old INT,
    @new INT,
    @pnum INT
AS
BEGIN
    BEGIN TRY
        
        UPDATE Works_for 
        SET ESSn = @new
        WHERE ESSn = @old 
          AND Pno = @pnum;

        SELECT 'Employee updated successfully' AS Message;

    END TRY

    BEGIN CATCH
        
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;

    END CATCH
END;
GO

Select * from Works_for
Select * from [Human Resource].Employee

INSERT INTO [Human Resource].Employee (SSN, Fname, Lname)
VALUES 
(1, 'Test1', 'Employee1'),
(2, 'Test2', 'Employee2');

INSERT INTO Works_for (ESSn, Pno, Hours)
VALUES (1, 500, 0),(2, 600, 0); 


EXEC update_WorksFor 102672, 1, 200;
EXEC update_WorksFor 521634, 2, 400;


SELECT *FROM Works_for;




--4.add column budget in project table and insert any draft values in it then 
--then Create an Audit table with the following structure 
--ProjectNo 	UserName 	ModifiedDate 	Budget_Old 	Budget_New 
--p2 	Dbo 	2008-01-31	95000 	200000


USE Company_SD;
GO

ALTER TABLE Company.Project
ADD Budget MONEY;
GO

UPDATE Company.Project
SET Budget = 100000; 
GO

select * from Company.Project


CREATE TABLE Audit
(
    ProjectNo INT NOT NULL,
    UserName NVARCHAR(50) NOT NULL,
    ModifiedDate DATETIME NOT NULL,
    Budget_Old MONEY NOT NULL,
    Budget_New MONEY NOT NULL
);
GO

--This table will be used to audit the update trials on the Budget column (Project table, Company DB)
--Example:
--If a user updated the budget column then the project number, user name that made that update, the date of the modification and the value of the old and the new budget will be inserted into the Audit table
--Note: This process will take place only if the user updated the budget column

CREATE OR ALTER TRIGGER Update_Audit
ON Company.Project
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Budget)
    BEGIN
        INSERT INTO Audit (ProjectNo, UserName, ModifiedDate, Budget_Old, Budget_New)
        SELECT 
            i.Pnumber,         
            SUSER_NAME(),      
            GETDATE(),         
            d.Budget,           
            i.Budget           
        FROM inserted i
        INNER JOIN deleted d
            ON i.Pnumber = d.Pnumber;
    END
END;
GO


UPDATE Company.Project
SET Budget = 200000
WHERE Pnumber = 100;
GO

UPDATE Company.Project
SET Budget = 300000
WHERE Pnumber = 200;
GO

SELECT * FROM Audit;
GO



--5.Create a trigger to prevent anyone from inserting a new record in the Department table [ITI DB]
--Print a message for user to tell him that he can not insert a new record in that table

USE ITI;
GO

CREATE TRIGGER Prevent_insert
ON Department
INSTEAD OF INSERT
AS
BEGIN
    PRINT 'You can’t insert a new record in that table';
END;
GO

INSERT INTO Department (Dept_Name, Dept_Location)
VALUES ('NewDept', 'Cairo');



--6.Create a trigger that prevents the insertion Process for Employee table in March [Company DB].
USE Company_SD;
GO

CREATE TRIGGER Prevent_InsertEmp
ON [Human Resource].Employee
AFTER INSERT
AS
BEGIN
    IF FORMAT(GETDATE(),'MM') = '03'
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT 'Insertion is not allowed in March';
    END
END;
GO

INSERT INTO [Human Resource].Employee (SSN, Fname, Lname, Bdate)
VALUES (999999, 'Test', 'Employee', '1990-01-01');


--7.Create a trigger on student table after insert to add Row in Student Audit table (Server User Name , Date, Note)
--where note will be [username] Insert New Row with Key=[Key Value] in table [table name]
--Server User Name		Date 	Note 

USE ITI;
GO

CREATE TABLE Student_Audit
(
    UserName NVARCHAR(50) NOT NULL,  
    ModifiedDate DATETIME,           
    Note NVARCHAR(100)                
);
GO

CREATE OR ALTER TRIGGER Student_Insert_Audit
ON Student
AFTER INSERT
AS
BEGIN
    INSERT INTO Student_Audit (UserName, ModifiedDate, Note)
    SELECT 
        SUSER_NAME(),  
        GETDATE(),     
        SUSER_NAME() + ' Insert New Row with Key = [' + CONVERT(NVARCHAR(20), St_Id) + '] in table Student'
    FROM inserted;  
END;
GO
INSERT INTO Student (St_Id, St_Fname, St_Lname, St_Address, St_Age, Dept_Id)
VALUES (1015, 'Omar', 'Khaled', 'Cairo', 23, 10);

SELECT * FROM Student;

SELECT * FROM Student_Audit;



--8.Create a trigger on student table instead of delete to add Row in Student Audit table 
--(Server User Name, Date, Note) where note will be� try to delete Row with Key=[Key Value]�

CREATE OR ALTER TRIGGER Student_Delete_Audit
ON Student
INSTEAD OF DELETE
AS
BEGIN
    
    INSERT INTO Student_Audit (UserName, ModifiedDate, Note)
    SELECT 
        SUSER_NAME(),
        GETDATE(),
        SUSER_NAME() + ' Try to delete Row with Key = [' + CONVERT(NVARCHAR(20), St_Id) + '] in table Student'
    FROM deleted;
END;
GO


DELETE FROM Student
WHERE St_Id = 1010;

DELETE FROM Student
WHERE Dept_Id = 10;

SELECT * FROM Student_Audit;

SELECT * FROM Student;
