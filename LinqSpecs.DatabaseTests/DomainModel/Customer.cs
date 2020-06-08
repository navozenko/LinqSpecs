using System.Collections.Generic;

namespace LinqSpecs.DatabaseTests
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}
