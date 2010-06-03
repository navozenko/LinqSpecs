using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Chinook.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.IntegrationalTests
{

	public class ByArtistName : Specification<Album>
	{
		public string ArtistName { get; set; }

		public override Expression<Func<Album, bool>> IsSatisfiedBy()
		{
			return a => a.Artist.Name == ArtistName;
		}
	}

	public class ByAlbumTitle : Specification<Album>
	{
		public string AlbumTitle { get; set; }
		public override Expression<Func<Album, bool>> IsSatisfiedBy()
		{

			return a => a.Title == AlbumTitle;
		}
	}


	[TestFixture]
	public class AndQueries
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
				
				var result = s.Query<Album>().Where(and.IsSatisfiedBy()).ToList();
				result.Count.Should().Be.EqualTo(1);
			}

		}


	}
}
