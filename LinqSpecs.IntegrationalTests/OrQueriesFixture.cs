using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
	public class OrQueriesFixture
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

			Specification<Album> or = new ByAlbumTitle { AlbumTitle = "Big Ones" }
					| new ByArtistName { ArtistName = "Aerosmith" };

			using (var s = sf.OpenSession())
			using (var tx = s.BeginTransaction())
			{

				var result = s.Query<Album>().Where(or.IsSatisfiedBy()).ToList();
				result.Should().Not.Be.Empty();
			}

		}
	}
	
}
