using LCL.Domain.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LCL;
using System.Linq;
using System.Linq.Expressions;
using LCL.Tests.ACore;

namespace LCL.Tests.Domain.Specifications
{
    [TestClass]
    public class Specification_Tests
    {
        private readonly IQueryable<Customer> _customers;

        public Specification_Tests()
        {
            _customers = new List<Customer>
            {
                new Customer("John", 17, 47000, "England"),
                new Customer("Tuana", 2, 500, "Turkey"),
                new Customer("Martin", 43, 16000, "USA"),
                new Customer("Lee", 32, 24502, "China"),
                new Customer("Douglas", 42, 42000, "England"),
                new Customer("Abelard", 14, 2332, "German"),
                new Customer("Neo", 16, 120000, "USA"),
                new Customer("Daan", 39, 6000, "Netherlands"),
                new Customer("Alessandro", 22, 8271, "Italy"),
                new Customer("Noah", 33, 82192, "Belgium")
            }.AsQueryable();
        }

        [TestMethod]
        public void Any_Should_Return_All()
        {
            _customers.Where<Customer>(new AnySpecification<Customer>().GetExpression())
                .Count()
                .ShouldBe(0);
        }

        [TestMethod]
        public void None_Should_Return_Empty()
        {
            _customers
                .Where(new NoneSpecification<Customer>().GetExpression())
                .Count().ShouldBe(0);
        }

        [TestMethod]
        public void Not_Should_Return_Reverse_Result()
        {
            _customers
                .Where(new EuropeanCustomerSpecification().Not().GetExpression())
                .Count()
                .ShouldBe(3);
        }

        [TestMethod]
        public void Should_Support_Native_Expressions_And_Combinations()
        {
            _customers
                .Where<Customer>(new ExpressionSpecification<Customer>(c => c.Age >= 18).GetExpression())
                .Count()
                .ShouldBe(6);

            _customers
                .Where<Customer>(new EuropeanCustomerSpecification().And(new ExpressionSpecification<Customer>(c => c.Age >= 18)).GetExpression())
                .Count()
                .ShouldBe(4);
        }

        [TestMethod]
        public void CustomSpecification_Test()
        {
            _customers
                .Where(new EuropeanCustomerSpecification().GetExpression())
                .Count()
                .ShouldBe(7);

            _customers
                .Where(new Age18PlusCustomerSpecification().GetExpression())
                .Count()
                .ShouldBe(6);

            _customers
                .Where(new BalanceCustomerSpecification(10000, 30000).GetExpression())
                .Count()
                .ShouldBe(2);

            _customers
                .Where(new PremiumCustomerSpecification().GetExpression())
                .Count()
                .ShouldBe(3);
        }

        [TestMethod]
        public void IsSatisfiedBy_Tests()
        {
            new PremiumCustomerSpecification().IsSatisfiedBy(new Customer("David", 49, 55000, "USA")).ShouldBeTrue();

            new PremiumCustomerSpecification().IsSatisfiedBy(new Customer("David", 49, 200, "USA")).ShouldBeFalse();
            new PremiumCustomerSpecification().IsSatisfiedBy(new Customer("David", 12, 55000, "USA")).ShouldBeFalse();
        }

        [TestMethod]
        public void CustomSpecification_Composite_Tests()
        {
            _customers
                .Where(new EuropeanCustomerSpecification().And(new Age18PlusCustomerSpecification()).GetExpression())
                .Count()
                .ShouldBe(4);

            _customers
               .Where(new EuropeanCustomerSpecification().Not().And(new Age18PlusCustomerSpecification()).GetExpression())
               .Count()
               .ShouldBe(2);

            _customers
                .Where(new Age18PlusCustomerSpecification().AndNot(new EuropeanCustomerSpecification()).GetExpression())
                .Count()
                .ShouldBe(2);
        }

        private class Customer
        {
            public string Name { get; private set; }

            public byte Age { get; private set; }

            public long Balance { get; private set; }

            public string Location { get; private set; }

            public Customer(string name, byte age, long balance, string location)
            {
                Name = name;
                Age = age;
                Balance = balance;
                Location = location;
            }
        }

        private class EuropeanCustomerSpecification : Specification<Customer>
        {
            public override Expression<Func<Customer, bool>> GetExpression()
            {
                return c => c.Location == "England" ||
                            c.Location == "Turkey" ||
                            c.Location == "German" ||
                            c.Location == "Netherlands" ||
                            c.Location == "Italy" ||
                            c.Location == "Belgium";
            }
        }

        private class Age18PlusCustomerSpecification : Specification<Customer>
        {
            public override Expression<Func<Customer, bool>> GetExpression()
            {
                return c => c.Age >= 18;
            }
        }

        private class BalanceCustomerSpecification : Specification<Customer>
        {
            public int MinBalance { get; set; }

            public int MaxBalance { get; set; }

            public BalanceCustomerSpecification(int minBalance, int maxBalance)
            {
                MinBalance = minBalance;
                MaxBalance = maxBalance;
            }

            public override Expression<Func<Customer, bool>> GetExpression()
            {
                return c => c.Balance >= MinBalance && c.Balance <= MaxBalance;
            }
        }

        private class PremiumCustomerSpecification : AndSpecification<Customer>
        {
            public PremiumCustomerSpecification()
                : base(new Age18PlusCustomerSpecification(), new BalanceCustomerSpecification(20000, int.MaxValue))
            {
            }
        }
    }
}
