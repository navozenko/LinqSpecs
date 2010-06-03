using System;
using System.Linq.Expressions;
using Chinook.Domain;

namespace LinqSpecs.IntegrationalTests.SampleQueries
{
	public class ByArtistName : Specification<Album>
	{
		public string ArtistName { get; set; }

		public override Expression<Func<Album, bool>> IsSatisfiedBy()
		{
			return a => a.Artist.Name == ArtistName;
		}
	}
}