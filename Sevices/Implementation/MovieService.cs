using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSpace.Sevices.Implementation
{
    public class MovieService : IMovieService
    {
        private static List<Movie> Movies;

        public MovieService()
        {
            Movies = new List<Movie>
            {
                new Movie()
                {
                    Id = 1,
                    Name = "Coringa",
                    ReleaseDate = new DateTime(2019,10,03),
                    Actors = new List<Actor>(){
                         new Actor()
                         {
                             Id = 1,
                             Name = "Joaquin Phoenix"
                         },
                         new Actor()
                         {
                             Id = 2,
                             Name = "Robert De Niro"
                         },
                    },
                },
                new Movie()
                {
                    Id = 2,
                    Name = "Vingadores: Ultimato",
                    ReleaseDate = new DateTime(2019,07,11),
                    Actors = new List<Actor>(){
                         new Actor()
                         {
                             Id = 3,
                             Name = "Chris Evans"
                         },
                         new Actor()
                         {
                             Id = 4,
                             Name = "Robert Downey Jr."
                         },
                    },
                },
                new Movie()
                {
                    Id = 3,
                    Name = "Aladdin",
                    ReleaseDate = new DateTime(2019,05,23),
                    Actors = new List<Actor>(){
                         new Actor()
                         {
                             Id = 5,
                             Name = "Will Smith"
                         },
                         new Actor()
                         {
                             Id = 6,
                             Name = "Mena Massound"
                         },
                    },
                },

            };
        }

        public List<Movie> GetAll()
        {
            return Movies;
        }

        public Movie Get(int id)
        {
            Movie movie = Movies.FirstOrDefault(b => b.Id == id);
            if (movie == null)
            {
                throw new ArgumentException("Filme não existe");
            }
            return movie;
        }

        public int Post(MovieRequest request)
        {
            int newId = Movies.Count + 1;
            Movies.Add(new Movie()
            {
                Id = newId,
                Name = request.Name,
                ReleaseDate = request.ReleaseDate
            });

            return newId;
        }

        public void Put(int movieId, MovieRequest request)
        {
            Movie movieEdit = Movies.FirstOrDefault(b => b.Id == movieId);
            if (movieEdit == null)
            {
                throw new ArgumentException("Filme não existe");
            }
            movieEdit.Name = request.Name;
            movieEdit.ReleaseDate = request.ReleaseDate;

        }

        public void Delete(int id)
        {
            Movie movieEdit = Movies.FirstOrDefault(b => b.Id == id);
            if (movieEdit == null)
            {
                throw new ArgumentException("Filme não existe");
            }
            Movies.Remove(movieEdit);
        }
    }
}
