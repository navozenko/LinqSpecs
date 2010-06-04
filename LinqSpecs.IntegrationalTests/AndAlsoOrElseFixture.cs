using System;
using System.Linq.Expressions;
using Chinook.Domain;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.IntegrationalTests
{


	public class ByArtistSpecification : Specification<Album>
	{
		private readonly Artist artist;

		public ByArtistSpecification(Artist artist)
		{
			this.artist = artist;
		}

		public Artist Artist { get; set; }

		public override Expression<Func<Album, bool>> IsSatisfiedBy()
		{
			return a => a.Artist == Artist;
		}
	}


	[TestFixture]
	public class AndAlsoOrElseFixture
	{
		private readonly AdHocSpecification<string> startWithJ 
			= new AdHocSpecification<string>(n => n.StartsWith("J"));

		private readonly AdHocSpecification<string> endsWithE 
			= new AdHocSpecification<string>(n => n.EndsWith("e"));

		[Test]
		public void and_also_is_equal_to_and()
		{
			(startWithJ & endsWithE).Should().Be.EqualTo(startWithJ && endsWithE);
		}
		[Test]
		public void or_else_is_equal_to_or()
		{
			(startWithJ | endsWithE).Should().Be.EqualTo(startWithJ || endsWithE);
		}
	}
}