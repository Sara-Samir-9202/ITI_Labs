USE ITI;

SELECT * FROM Student;
SELECT * FROM Instructor;
SELECT * FROM Department;
SELECT * FROM Stud_Course;
--1
SELECT COUNT(*) 
FROM Student
WHERE St_Age IS NOT NULL;

--2
SELECT DISTINCT Ins_Name
FROM Instructor;


--3
SELECT 
    S.St_Id,
    ISNULL(S.St_Fname, 'No_Fname') + ' ' + ISNULL(S.St_Lname, 'No_Lname') AS FullName,
    ISNULL(D.Dept_Name, 'No Dept') AS DeptName
FROM Student S
LEFT JOIN Department D
ON S.Dept_Id = D.Dept_Id;

--4
SELECT 
    I.Ins_Name,
    D.Dept_Name
FROM Instructor I
LEFT JOIN Department D
ON I.Dept_Id = D.Dept_Id;

--5
SELECT 
    S.St_Fname + ' ' + S.St_Lname AS FullName,
    C.Crs_Name
FROM Student S
JOIN Stud_Course SC ON S.St_Id = SC.St_Id
JOIN Course C ON SC.Crs_Id = C.Crs_Id
WHERE SC.Grade IS NOT NULL;

--6
SELECT Top_Name, COUNT(Crs_Id) AS [COUNT]
FROM Course C INNER JOIN Topic T
ON T.Top_Id = C.Top_Id
GROUP BY Top_Name
ORDER BY [COUNT];



--7
SELECT 
    MAX(Salary) AS MaxSalary,
    MIN(Salary) AS MinSalary
FROM Instructor;

--8
SELECT * 
FROM Instructor
WHERE Salary < (SELECT AVG(Salary) FROM Instructor);


--9
SELECT D.Dept_Name
FROM Department D
JOIN Instructor I ON D.Dept_Id = I.Dept_Id
WHERE I.Salary = (SELECT MIN(Salary) FROM Instructor);


--10
SELECT TOP(2) Salary
FROM Instructor
ORDER BY Salary DESC;

--11
SELECT 
    Ins_Name,
    COALESCE(CAST(Salary AS VARCHAR(20)), 'Bonus') AS SalaryOrBonus
FROM Instructor;

--12
SELECT AVG(Salary)
FROM Instructor;

--13
select s.St_Fname as StudentFirstName , super.*  from student as s inner join student super
on s.St_super = super.St_Id;

--14
WITH RankedSalaries AS (
    SELECT Dept_Id,Ins_Name,Salary,
    DENSE_RANK() OVER (PARTITION BY Dept_Id ORDER BY Salary DESC) AS SalaryRank
    FROM Instructor
    WHERE Salary IS NOT NULL
)
SELECT Dept_Id, Ins_Name, Salary
FROM RankedSalaries
WHERE SalaryRank <= 2
ORDER BY Dept_Id, Salary DESC;

--15
SELECT *
FROM (SELECT *, ROW_NUMBER() OVER(PARTITION BY Dept_Id ORDER BY NEWID()) AS RN
		FROM Student
		WHERE Dept_Id IS NOT NULL) AS S
WHERE RN=1;


