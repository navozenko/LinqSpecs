using System;
using System.Linq.Expressions;
using Chinook.Domain;

namespace LinqSpecs.IntegrationalTests.SampleQueries
{
	public class ByAlbumTitle : Specification<Album>
	{
		public string AlbumTitle { get; set; }
		public override Expression<Func<Album, bool>> IsSatisfiedBy()
		{

			return a => a.Title == AlbumTitle;
		}
	}
}