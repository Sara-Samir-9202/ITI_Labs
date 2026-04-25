USE Company_SD;
--1
SELECT D.Dependent_name, D.Sex
FROM Dependent D 
JOIN Employee E ON E.SSN = D.ESSN
WHERE D.Sex = 'F' 
  AND E.Sex = 'F'

UNION

SELECT D.Dependent_name, D.Sex
FROM Dependent D 
JOIN Employee E ON E.SSN = D.ESSN
WHERE D.Sex = 'M' 
  AND E.Sex = 'M';



--2
SELECT P.Pname,SUM(WF.Hours) AS [Total Hours Per Week]
FROM Project P
JOIN Works_for WF ON P.Pnumber = WF.Pno
GROUP BY P.Pname
ORDER BY [Total Hours Per Week];

--3

SELECT D.*
FROM Departments D
WHERE D.Dnum = (
    SELECT Dno
    FROM Employee
    WHERE SSN = (SELECT MIN(SSN) FROM Employee)
);

SELECT Departments.*, Employee.SSN
FROM Departments inner join Employee
on Departments.Dnum =  Employee.Dno
where Employee.SSN in (select MIN (Employee.SSN) from Employee)


--4
SELECT D.Dname,
       MIN(E.Salary) AS [Min Salary],
       AVG(E.Salary) AS [Avg Salary],
       MAX(E.Salary) AS [Max Salary]
FROM Departments D
JOIN Employee E ON D.Dnum = E.Dno
GROUP BY D.Dname;

--5
SELECT E.Fname + ' ' + E.Lname AS Full_Name
FROM Employee E
WHERE E.SSN IN (SELECT MGRSSN FROM Departments)
AND NOT EXISTS (
    SELECT *
    FROM Dependent D
    WHERE D.ESSN = E.SSN
);

SELECT E.Fname + ' ' + E.Lname AS Full_Name
FROM Employee E inner join Departments D
ON E.SSN = D.MGRSSN
WHERE E.SSN NOT IN (SELECT Dependent.ESSN FROM Dependent)



--6
SELECT D.Dnum, D.Dname, COUNT(E.SSN) AS Num_Of_Employees
FROM Departments D
JOIN Employee E ON D.Dnum = E.Dno
GROUP BY D.Dnum, D.Dname
HAVING AVG(E.Salary) < (SELECT AVG(Salary) FROM Employee);


--7
SELECT E.Fname, E.Lname, P.Pname
FROM Employee E
JOIN Works_for W ON E.SSN = W.ESSn
JOIN Project P ON P.Pnumber = W.Pno
ORDER BY E.Dno, E.Lname, E.Fname;

SELECT E.Fname, E.Lname, P.Pname
FROM Employee E
INNER JOIN Works_for W ON E.SSN = W.ESSn
INNER JOIN Project P ON P.Pnumber = W.Pno
ORDER BY E.Dno, E.Lname, E.Fname;

--8
	--First way
SELECT Salary 
FROM Employee
WHERE Salary >=	(SELECT Salary 
	FROM Employee 
	ORDER BY Salary DESC
	OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY);

--9
SELECT DISTINCT E.Fname + ' ' + E.Lname AS Full_Name
FROM Employee E
JOIN Dependent D
    ON D.Dependent_name LIKE E.Fname + '%';


--10
SELECT E.SSN, E.Fname + ' ' + E.Lname As [Emloyee Name]
FROM Employee E
WHERE EXISTS (
    SELECT *
    FROM Dependent D
    WHERE D.ESSN = E.SSN
);


--11
INSERT INTO Departments 
VALUES ('DEPT IT', 100, 112233, '2006-11-01');


--12
--a
UPDATE Departments
SET MGRSSN = 968574
WHERE Dnum = 100;

--b
UPDATE Departments 
SET MGRSSN = 102672, [MGRStart Date] = GETDATE()
WHERE Dnum = 20;

--c
UPDATE Employee
SET Superssn = 102672
WHERE SSN = 102660;



--13
--a
UPDATE Departments
SET MGRSSN = 102672
WHERE MGRSSN = 223344;

--b
UPDATE Employee
SET Superssn = 102672
WHERE Superssn = 223344;

--c 
UPDATE Works_for
SET ESSN = 102672
WHERE ESSN = 223344;


--d
UPDATE Dependent
SET ESSN = 102672
WHERE ESSN = 223344;

--e
DELETE FROM Employee
WHERE SSN = 223344;




--14
UPDATE Employee
SET Salary *= 1.3
FROM Employee E INNER JOIN Works_for WF
ON E.SSN = WF.ESSn
				INNER JOIN  Project P
ON P.Pnumber = WF.Pno
WHERE Pname = 'Al Rawdah';


