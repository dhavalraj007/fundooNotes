using BuisnessLogic.Interfaces;
using Model;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Services
{
    public class NoteBuisness:INoteBuisness
    {
        private readonly INoteRepository noteRepository;

        public NoteBuisness(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public NoteEntity AddNote(NoteModel model, long UserId)
        {
            return noteRepository.AddNote(model, UserId);
        }

        public List<NoteEntity> GetAllNotes(long UserId)
        {
            return noteRepository.GetAllNotes(UserId);
        }

    }
}
