using Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Interfaces
{
    public interface ICollabBuisness
    {
        public CollabEntity AddCollab(CollabModel model, long UserId);
        public bool DeleteCollab(long CollabId, long NoteId, long UserId);
        public CollabEntity GetCollab(long CollabId, long NoteId, long UserId);
        public List<CollabEntity> GetAllCollabs(long NoteId, long UserId);

    }
}
