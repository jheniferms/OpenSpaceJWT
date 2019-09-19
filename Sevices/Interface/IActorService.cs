using OpenSpace.Models;
using OpenSpace.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpace.Sevices.Interface
{
    public interface IActorService
    {
        List<Actor> GetAll();
        Actor Get(int id);
        int Post(ActorRequest request);
        void Put(int MovideId, ActorRequest request);
        void Delete(int id);
    }
}
