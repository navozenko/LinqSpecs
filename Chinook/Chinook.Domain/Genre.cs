using System;

namespace Chinook.Domain
{
    public class Genre
    {
        public virtual int GenreId { get; private set; }
        public virtual string Name { get; set; }
    }
}