using System;

namespace Chinook.Domain
{
    public class Employee : Person
    {
        public virtual int EmployeeId { get; private set; }

        public virtual string Title { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual DateTime HireDate { get; set; }
        public virtual Employee ReportsTo { get; set; }
        
    }
}