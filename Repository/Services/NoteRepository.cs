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
using System.Runtime.InteropServices.WindowsRuntime;
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
        public NoteEntity GetNote(long NoteId, long UserId)
        {
            NoteEntity note = context.NoteTable.Where(rec => rec.NoteId == NoteId && rec.UserId == UserId).FirstOrDefault();
            return note;
        }

        public bool DeleteNote(long NoteId, long UserId)
        {
            NoteEntity note = context.NoteTable.Where(rec => rec.NoteId == NoteId && rec.UserId == UserId).FirstOrDefault();
            if(note == null) return false;
            context.NoteTable.Remove(note);
            context.SaveChanges();
            return true;
        }

        public bool TooglePin(long NoteId, long UserId)
        {
            NoteEntity note = context.NoteTable.Where(rec => rec.NoteId==NoteId && rec.UserId==UserId).FirstOrDefault();
            
            if (note.IsPinned)
            {
                note.IsPinned = false;
                context.SaveChanges();
                return false;
            }
            else
            {
                note.IsPinned = true;
                context.SaveChanges();
                return true;
            }
        }

        public bool ToogleArchive(long NoteId, long UserId)
        {
            NoteEntity note = context.NoteTable.Where(rec => rec.NoteId == NoteId && rec.UserId == UserId).FirstOrDefault();

            if (note.IsArchived)
            {
                note.IsArchived = false;
                context.SaveChanges();
                return false;
            }
            else
            {
                note.IsArchived = true;
                context.SaveChanges();
                return true;
            }
        }

        public bool ToogleTrash(long NoteId, long UserId)
        {
            NoteEntity note = context.NoteTable.Where(rec => rec.NoteId == NoteId && rec.UserId == UserId).FirstOrDefault();

            if (note.IsTrash)
            {
                note.IsTrash = false;
                context.SaveChanges();
                return false;
            }
            else
            {
                note.IsTrash = true;
                context.SaveChanges();
                return true;
            }
        }


        public List<NoteEntity> GetAllNotes(long UserId) 
        {
            var res = (from note in context.NoteTable.Cast<NoteEntity>()
                      where note.UserId == UserId
                      select note).ToList();
            return res;
        }
        public List<NoteEntity> GetAllOfNotes()
        {
            var res = context.NoteTable.ToList();
            return res;
        }

    }
}
