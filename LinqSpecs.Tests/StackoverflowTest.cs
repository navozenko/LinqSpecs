using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
    public class StackoverflowTest
    {
        [Test]
        public void RecreateStackOverFlow()
        {
            var customSpec = new CustomSpec("Jose");
            Assert.True(true); //!!!!Put a break point here and let it sit for a second or two, you'll stack overflow
        }
    }


    public class CustomSpec : Specification<string>
    {
        public string Name { get; set; }

        public CustomSpec(string name)
        {
            Name = name;
        }

        public override Expression<Func<string, bool>> ToExpression()
        {
            return s => s == Name;
        }
    }
}