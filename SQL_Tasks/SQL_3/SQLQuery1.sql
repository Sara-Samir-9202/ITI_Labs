USE Company_SD;
--1
SELECT Dnum, Dname, SSN, Fname + ' ' + Lname AS [Manager Name]
FROM Departments D, Employee E
WHERE E.SSN = D.MGRSSN;

--2
SELECT Dname, Pname
FROM Departments D, Project P
WHERE D.Dnum = P.Dnum
ORDER BY Dname;

--3
SELECT Fname + ' ' + Lname AS [Employee Name], D.*
FROM Employee E, Dependent D
WHERE E.SSN = D.ESSN;

--4
SELECT Pnumber, Pname, Plocation
FROM Project
WHERE City in('Cairo', 'Alex'); 

--5
SELECT *
FROM Project
WHERE Pname LIKE 'a%';

--6
SELECT *
FROM Employee
WHERE Dno = 30 AND Salary BETWEEN 1000 AND 2000; 

--7
		--First way
SELECT Fname + ' ' + Lname AS [Employee Name]
FROM Employee E, Works_for WF, Project P
WHERE E.SSN = WF.ESSn AND P.Pnumber = WF.Pno 
AND Pname = 'Al Rabwah' AND HOURS >= 10;

		--Second way
SELECT Fname + ' ' + Lname AS [Employee Name]
FROM Employee E INNER JOIN Works_for WF
ON E.SSN = WF.ESSn AND Hours >= 10
				INNER JOIN Project P
ON P.Pnumber = WF.Pno AND Pname = 'Al Rabwah';

SELECT Fname + ' ' + Lname AS [Employee Name]
FROM Employee E, Works_for WF, Project P
WHERE E.SSN = WF.ESSn
  AND P.Pnumber = WF.Pno
  AND P.Pname = 'Al Rabwah'
  AND WF.Hours >= 10
  AND E.Dno = 10;


--8
SELECT * FROM Employee;
SELECT E.Fname + ' ' + E.Lname AS [Employee Name]
FROM Employee E, Employee S
WHERE S.SSN = E.Superssn 
AND S.Fname + ' ' + S.Lname = 'Kamel Mohamed';

--9
SELECT Fname + ' ' + Lname AS [Employee Name], Pname
FROM Employee E INNER JOIN Works_for WF
ON E.SSN = WF.ESSn
				INNER JOIN Project P
ON P.Pnumber = WF.Pno
ORDER BY Pname;

--10
SELECT Pnumber, Dname, Lname, Address, Bdate
FROM Project P INNER JOIN Departments D
ON D.Dnum = P.Dnum AND City = 'Cairo'
				INNER JOIN Employee E
ON E.SSN = D.MGRSSN;

--11
SELECT DISTINCT S.*
FROM Employee E, Employee S
WHERE S.SSN = E.Superssn;

SELECT E.*
FROM Employee E
join Departments d
on e.SSN = d.MGRSSN;


--12
SELECT *
FROM Employee E LEFT OUTER JOIN Dependent D
ON E.SSN = D.ESSN;

--13
INSERT INTO Employee
VALUES('Sara', 'Samir', 102672, GETDATE(), '44 Hilopolis.Cairo', 'F', 3000, 112233, 30);

select * from Employee;

--14
INSERT INTO Employee
VALUES('Omar', 'Samir', 102660, GETDATE(), '269 El-Haram st. Giza', 'M', NULL, NULL, 30);

--15
UPDATE Employee
SET Salary = Salary * 1.2
WHERE SSN = 102672;
