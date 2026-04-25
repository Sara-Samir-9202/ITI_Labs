using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLab02_Part01
{
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
