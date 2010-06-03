using System;

namespace Chinook.Domain
{
    public class MediaType
    {
        public virtual int MediaTypeId { get; private set; }
        public virtual string Name { get; set; }
    }
}