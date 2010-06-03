using System;
using System.Collections.Generic;

namespace Chinook.Domain
{
    public class Album
    {
        public virtual Artist Artist { get;  set; }
        public virtual int AlbumId { get; private set; }
        public virtual string Title { get; set; }

        public virtual IList<Track> Tracks { get; private set; }

        public virtual void AddTrack(Track track)
        {
            track.Album = this;
            Tracks.Add(track);
        }

        public Album()
        {
            Tracks = new List<Track>();
        }
    }
}