using System.Linq;
using Chinook.Domain;
using LinqSpecs.IntegrationalTests.SampleQueries;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.IntegrationalTests
{
	[TestFixture]
	public class NotQueriesFixture
	{
		private ISessionFactory sf;
		[SetUp]
		public void SetUp()
		{
			var configuration = new Configuration().Configure();
			sf = configuration.BuildSessionFactory();
		}

		[Test]
		public void can_query_with_or_spec()
		{

			Specification<Album> or = !(new ByAlbumTitle {AlbumTitle = "Big Ones"});

			using (var s = sf.OpenSession())
			using (var tx = s.BeginTransaction())
			{

				s.Query<Album>()
					.Count(or.IsSatisfiedBy())
					.Should().Be.GreaterThan(0);
			}

		}
	}
}