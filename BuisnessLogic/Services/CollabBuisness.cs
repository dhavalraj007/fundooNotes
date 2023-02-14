using BuisnessLogic.Interfaces;
using Model;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Services
{
    public class CollabBuisness : ICollabBuisness
    {
        private readonly ICollabRepository collabRepository;

        public CollabBuisness(ICollabRepository collabRepository)
        {
            this.collabRepository = collabRepository;
        }
        public CollabEntity AddCollab(CollabModel model, long UserId)
        {
            return collabRepository.AddCollab(model, UserId); 
        }

        public bool DeleteCollab(long CollabId, long NoteId, long UserId)
        {
            return collabRepository.DeleteCollab(CollabId, NoteId, UserId);
        }

        public List<CollabEntity> GetAllCollabs(long NoteId, long UserId)
        {
            return collabRepository.GetAllCollabs(NoteId, UserId);
        }

        public CollabEntity GetCollab(long CollabId, long NoteId, long UserId)
        {
            return collabRepository.GetCollab(CollabId,NoteId, UserId);
        }
    }
}
