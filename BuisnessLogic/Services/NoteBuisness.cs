using BuisnessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public NoteEntity GetNote(long NoteId, long UserId)
        {
            return noteRepository.GetNote(NoteId, UserId);
        }

        public bool DeleteNote(long NoteId, long UserId)
        {
            return noteRepository.DeleteNote(NoteId, UserId);
        }

        public string UploadImage(long NoteId, long UserId, IFormFile img)
        {
            return noteRepository.UploadImage(NoteId, UserId, img);
        }
        public List<NoteEntity> GetAllNotes(long UserId)
        {
            return noteRepository.GetAllNotes(UserId);
        }   

        public List<NoteEntity> GetAllOfNotes()
        {
            return noteRepository.GetAllOfNotes();
        }

        public bool TooglePin(long NoteId, long UserId)
        {
            return noteRepository.TooglePin(NoteId, UserId);
        }
        public bool ToogleArchive(long NoteId, long UserId)
        {
            return noteRepository.ToogleArchive(NoteId, UserId);
        } 
        public bool ToogleTrash(long NoteId, long UserId)
        {
            return noteRepository.ToogleTrash(NoteId, UserId);
        }
        public bool DeleteForever(long NoteId, long UserId)
        {
            return noteRepository.DeleteForever(NoteId, UserId);
        }
    }
}
