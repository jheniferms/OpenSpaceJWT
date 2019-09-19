using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSpace.Sevices.Implementation
{
    public class ActorService : IActorService
    {
        private static List<Actor> Actors;

        public ActorService()
        {
            Actors = new List<Actor>()
            {
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
                new Actor()
                {
                    Id = 3,
                    Name = "Chris Evans"
                },
                new Actor()
                {
                    Id = 4,
                    Name = "Robert Downey Jr."
                },new Actor()
                {
                    Id = 5,
                    Name = "Will Smith"
                },
                new Actor()
                {
                    Id = 6,
                    Name = "Mena Massound"
                },
            };
        }

        public List<Actor> GetAll()
        {
            return Actors;
        }

        public Actor Get(int id)
        {
            Actor actor = Actors.FirstOrDefault(b => b.Id == id);
            if (actor == null)
            {
                throw new ArgumentException("Ator não existe");
            }
            return actor;
        }

        public int Post(ActorRequest request)
        {
            int newId = Actors.Count + 1;
            Actors.Add(new Actor()
            {
                Id = newId,
                Name = request.Name
            });

            return newId;
        }

        public void Put(int actorId, ActorRequest request)
        {
            Actor actorEdit = Actors.FirstOrDefault(b => b.Id == actorId);
            if (actorEdit == null)
            {
                throw new ArgumentException("Ator não existe");
            }
            actorEdit.Name = request.Name;

        }

        public void Delete(int id)
        {
            Actor actorEdit = Actors.FirstOrDefault(b => b.Id == id);
            if (actorEdit == null)
            {
                throw new ArgumentException("Ator não existe");
            }
            Actors.Remove(actorEdit);
        }
    }
}
