using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLab02_Part01
{
    public static class Repository
    {
        public static List<Employee> Employees = new List<Employee>()
        {
            new Employee{Id=1, Name="Ali", Salary=3000, DeptId=1},
            new Employee{Id=2, Name="Mona", Salary=6000, DeptId=1},
            new Employee{Id=3, Name="Omar", Salary=7000, DeptId=2},
            new Employee{Id=4, Name="Sara", Salary=2000, DeptId=2},
            new Employee{Id=5, Name="Nada", Salary=8000, DeptId=3},
            new Employee{Id=6, Name="Hany", Salary=4000, DeptId=3}
        };

        public static List<Department> Departments = new List<Department>()
        {
            new Department{DeptId=1, DeptName="HR"},
            new Department{DeptId=2, DeptName="IT"},
            new Department{DeptId=3, DeptName="Sales"}
        };
    }
}
