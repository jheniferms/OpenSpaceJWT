using OpenSpace.Models;
using OpenSpace.Request;
using System.Collections.Generic;

namespace OpenSpace.Sevices.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAll();
        Movie Get(int id);
        int Post(MovieRequest request);
        void Put(int MovideId, MovieRequest request);
        void Delete(int id);
    }
}
