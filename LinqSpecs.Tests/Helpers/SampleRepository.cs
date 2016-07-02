using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LinqSpecs.Tests
{
	public class SampleRepository : ReadOnlyCollection<string>
	{
		public SampleRepository()
			: base(new[] { "Jose", "Manuel", "Julian" })
		{ }

		public IEnumerable<string> Find(Specification<string> specfication)
		{
			return this.AsQueryable().Where(specfication.ToExpression());
		}
	}
}