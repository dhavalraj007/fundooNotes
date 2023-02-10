using Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface INoteRepository
    {
        public NoteEntity AddNote(NoteModel model, long UserId);
        public List<NoteEntity> GetAllNotes(long UserId);

    }
}
