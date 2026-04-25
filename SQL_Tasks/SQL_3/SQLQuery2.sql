CREATE DATABASE AcademyDB;
GO

USE AcademyDB;
GO

CREATE TABLE Course
(
    CID INT IDENTITY(1,1) PRIMARY KEY,
    CName NVARCHAR(50) NOT NULL,
    Duration INT UNIQUE
);

CREATE TABLE Instructor
(
    ID INT IDENTITY(1,1) PRIMARY KEY,

    Fname NVARCHAR(50) NOT NULL,
    Lname NVARCHAR(50) NOT NULL,

    BD DATE NOT NULL,

    Age AS (YEAR(GETDATE()) - YEAR(BD)),  

    Address NVARCHAR(50)
        CHECK (Address IN ('Cairo','Alex')),

    Hiredate DATE DEFAULT GETDATE(),

    Salary INT 
        DEFAULT 3000
        CHECK (Salary BETWEEN 1000 AND 5000),

    OverTime INT UNIQUE,

    NetSalary AS (Salary + OverTime)  
);


CREATE TABLE Lab
(
    LID INT,
    CID INT,

    Location NVARCHAR(50),

    Capacity INT CHECK (Capacity < 20),

    PRIMARY KEY (CID, LID),

    FOREIGN KEY (CID)
        REFERENCES Course(CID)
);

CREATE TABLE Teach
(
    InstructorID INT,
    CourseID INT,

    PRIMARY KEY (InstructorID, CourseID),

    FOREIGN KEY (InstructorID)
        REFERENCES Instructor(ID),

    FOREIGN KEY (CourseID)
        REFERENCES Course(CID)     
);

INSERT INTO Course (CName, Duration)
VALUES 
('SQL', 30),
('C#', 45);

INSERT INTO Instructor (Fname, Lname, BD, Address, Salary, OverTime)
VALUES
('Omar', 'Ali', '1995-05-10', 'Cairo', 4000, 500),
('Sara', 'Hassan', '1998-03-15', 'Alex', 3500, 300);

INSERT INTO Teach (InstructorID, CourseID)
VALUES
(1,1),
(2,1),
(2,2);

INSERT INTO Lab (CID, LID, Location, Capacity)
VALUES
(1,1,'Lab A',15),
(1,2,'Lab B',18),
(2,1,'Lab C',12);

SELECT * FROM Course;
SELECT * FROM Instructor;
SELECT * FROM Teach;
SELECT * FROM Lab;

EXEC sp_changedbowner 'sa';


