using System;
using System.Collections.Generic;
namespace Chinook.Domain
{
    public class Playlist
    {
        public virtual int PlaylistId { get; private set; }
        public virtual string Name { get; set; }

        public virtual IList<Track> Tracks { get; private set; } 

        public virtual void AddTrack(Track track)
        {   
            Tracks.Add(track);
        }

        public Playlist()
        {
            Tracks = new List<Track>();
        }
    }

   
}