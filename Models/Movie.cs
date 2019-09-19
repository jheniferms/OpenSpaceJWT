
using System;
using System.Collections.Generic;

namespace OpenSpace.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
