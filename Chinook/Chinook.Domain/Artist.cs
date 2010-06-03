using System;

namespace Chinook.Domain
{
    public class Artist
    {
        public virtual int ArtistId { get; private set; }
        public virtual string Name { get; set; }
    }
}