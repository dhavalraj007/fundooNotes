using Microsoft.AspNetCore.Http;
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
        public NoteEntity GetNote(long NoteId, long UserId);
        public bool DeleteNote(long NoteId, long UserId);
        public string UploadImage(long NoteId, long UserId, IFormFile img);

        public List<NoteEntity> GetAllNotes(long UserId);
        public List<NoteEntity> GetAllOfNotes();

        public bool TooglePin(long NoteId, long UserId);
        public bool ToogleArchive(long NoteId, long UserId);
        public bool ToogleTrash(long NoteId, long UserId);
        public bool DeleteForever(long NoteId, long UserId);


    }
}
