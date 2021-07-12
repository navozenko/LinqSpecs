using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace LinqSpecs.DatabaseTests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var customer0 = CreateCustomer("Orderless Customer");
            var customer1 = CreateCustomer("Single-order Customer", orderPrices: new[] { 100 });
            var customer2 = CreateCustomer("Double-order Customer", orderPrices: new[] { 100, 200 });
            var customer3 = CreateCustomer("Triple-order Customer", orderPrices: new[] { 100, 200, 300 });

            using var db = new SampleDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Customers.AddRange(customer0, customer1, customer2, customer3);
            db.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using var db = new SampleDbContext();
            db.Database.EnsureDeleted();
        }

        [Test]
        public void NoSpecification()
        {
            using var db = new SampleDbContext();
            var customers = db.Customers.ToList();
            Assert.That(customers, Has.Count.EqualTo(4));
        }

        [Test]
        public void SimplePropertySpecification()
        {
            using var db = new SampleDbContext();
            var nameSpec = new AdHocSpecification<Customer>(x => x.Name.StartsWith("Single"));

            var customers = db.Customers.Where(nameSpec).Select(x => x.Name).ToList();

            Assert.That(customers, Is.EquivalentTo(new[] { "Single-order Customer" }));
        }

        [Test]
        public void NavigationPropertySpecification()
        {
            using var db = new SampleDbContext();
            var hasOrdersSpec = new AdHocSpecification<Customer>(x => x.Orders.Any());

            var customers = db.Customers.Where(hasOrdersSpec).Select(x => x.Name).ToList();

            Assert.That(
                customers,
                Is.EquivalentTo(new[]
                {
                    "Single-order Customer",
                    "Double-order Customer",
                    "Triple-order Customer"
                })
            );
        }

        [Test]
        public void СombinedSpecification()
        {
            using var db = new SampleDbContext();
            var hasMultipleOrdersSpec = new AdHocSpecification<Customer>(x => x.Orders.Count() > 1);
            var highTotalPriceSpec = new AdHocSpecification<Customer>(x => x.Orders.Sum(y => y.Price) > 500);
            var vipSpec = hasMultipleOrdersSpec && highTotalPriceSpec;

            var customers = db.Customers.Where(vipSpec).Select(x => x.Name).ToList();

            Assert.That(customers, Is.EquivalentTo(new[] { "Triple-order Customer" }));
        }

        [Test]
        public void СombinedSpecificationWithPredicate()
        {
            using var db = new SampleDbContext();
            var hasMultipleOrdersSpec = new AdHocSpecification<Customer>(x => x.Orders.Count() > 1);
            Expression<Func<Customer,bool>> highTotalPriceSpec = x => x.Orders.Sum(y => y.Price) > 500;
            var vipSpec = hasMultipleOrdersSpec & highTotalPriceSpec;

            var customers = db.Customers.Where(vipSpec).Select(x => x.Name).ToList();

            Assert.That(customers, Is.EquivalentTo(new[] { "Triple-order Customer" }));
        }

        private static Customer CreateCustomer(string name, params int[] orderPrices)
        {
            var customer = new Customer(name);

            foreach (var orderPrice in orderPrices)
                customer.Orders.Add(new Order("Some Product", orderPrice));

            return customer;
        }
    }
}
