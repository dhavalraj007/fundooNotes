using Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Interfaces
{
    public interface INoteBuisness
    {
        public NoteEntity AddNote(NoteModel model, long UserId);
        public List<NoteEntity> GetAllNotes(long UserId);

    }
}
