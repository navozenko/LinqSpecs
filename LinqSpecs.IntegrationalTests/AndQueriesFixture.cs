using System.Linq;
using Chinook.Domain;
using LinqSpecs.IntegrationalTests.SampleQueries;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;
using SharpTestsEx;
using Enumerable = System.Linq.Enumerable;

namespace LinqSpecs.IntegrationalTests
{
	[TestFixture]
	public class AndQueriesFixture
	{
		private ISessionFactory sf;
		[SetUp]
		public void SetUp()
		{
			var configuration = new Configuration().Configure();
			sf = configuration.BuildSessionFactory();
		}

		[Test]
		public void can_query_with_and_spec()
		{

			Specification<Album> and = new ByAlbumTitle { AlbumTitle = "Big Ones" }
			                           & new ByArtistName { ArtistName = "Aerosmith" };

			using(var s = sf.OpenSession())
			using(var tx = s.BeginTransaction())
			{
				
				var result = Enumerable.ToList<Album>(s.Query<Album>().Where(and.IsSatisfiedBy()));
				result.Count.Should().Be.EqualTo(1);
			}

		}
	}
}