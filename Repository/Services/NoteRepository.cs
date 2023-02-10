using Microsoft.Extensions.Configuration;
using Model;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;

namespace Repository.Services
{
    public class NoteRepository:INoteRepository
    {
        private readonly FundooContext context;
        private readonly IConfiguration configuration;

        public NoteRepository(FundooContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public NoteEntity AddNote(NoteModel model,long UserId)
        {
            NoteEntity note = new NoteEntity();
            //note.NoteId = model.NoteId;
            note.Title = model.Title;
            note.Description = model.Description;
            note.Color = model.Color;
            note.Image = model.Image;
            note.Reminder = model.Reminder;
            note.IsArchived = model.IsArchived;
            note.IsPinned = model.IsPinned;
            note.IsTrash = model.IsTrash;
            note.CreatedAt = DateTime.Now;
            note.ModifiedAt = DateTime.Now;
            note.UserId = UserId;

            context.NoteTable.Add(note);
            int res = context.SaveChanges();
            if(res>0)
                return note;
            return null;
        }

        public List<NoteEntity> GetAllNotes(long UserId) 
        {
            var res = (from note in context.NoteTable.Cast<NoteEntity>()
                      where note.UserId == UserId
                      select note).ToList();
            return res;
        }
        
    }
}
