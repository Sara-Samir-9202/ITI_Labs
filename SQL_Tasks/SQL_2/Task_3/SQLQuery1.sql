USE Company_SD;

--1
SELECT * FROM Employee;

--2
SELECT Fname, Lname, Salary, Dno
FROM Employee;

--3
SELECT Pname, Plocation, Dnum
FROM Project;

--4
SELECT Fname + ' ' + Lname AS [Name],
Salary * 0.1 * 12 AS [Annual Comm]
FROM Employee;

--5
SELECT SSN, Fname + ' ' + Lname AS [Name], Salary 
FROM Employee
WHERE Salary > 1000;

--6
SELECT SSN, Fname + ' ' + Lname AS [Name], Salary 
FROM Employee
WHERE Salary > 10000;

--7
SELECT Fname + ' ' + Lname AS [Name], Salary 
FROM Employee
WHERE Sex = 'F';

--8
SELECT Dname, Dnum 
FROM Departments
WHERE MGRSSN = 968574;

--9
Select Pname, Pnumber, Plocation 
FROM Project
WHERE Dnum = 10;