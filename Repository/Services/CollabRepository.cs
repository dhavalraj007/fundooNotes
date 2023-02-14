using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Services
{
    public class CollabRepository:ICollabRepository
    {
        private readonly FundooContext context;
        private readonly IConfiguration configuration;

        public CollabRepository(FundooContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public CollabEntity AddCollab(CollabModel model, long UserId)
        {
            var EmailDoesNotExists = (from user in context.UserTable
                              where user.Email == model.EmailId
                              select user).IsNullOrEmpty();
            if (EmailDoesNotExists)
                return null;

            CollabEntity label = new CollabEntity();
            label.UserId = UserId;
            label.EmailId = model.EmailId;
            label.NoteId = model.NoteId;
            context.CollabTable.Add(label);
            int res = context.SaveChanges();
            if (res > 0)
                return label;
            return null;
        }

        public bool DeleteCollab(long CollabId, long NoteId, long UserId)
        {
            var clb = (from collab in context.CollabTable
                       where collab.UserId == UserId && collab.NoteId == NoteId && collab.CollabId == CollabId
                       select collab).FirstOrDefault();
            context.CollabTable.Remove(clb);
            return context.SaveChanges() > 0;
        }
        public CollabEntity GetCollab(long CollabId, long NoteId, long UserId)
        {
            var clb = (from collab in context.CollabTable
                       where collab.UserId == UserId && collab.NoteId == NoteId && collab.CollabId == CollabId
                       select collab).FirstOrDefault();
            return clb;
        }


        public List<CollabEntity> GetAllCollabs(long NoteId, long UserId)
        {
            var res = (from collab in context.CollabTable.Cast<CollabEntity>()
                       where collab.UserId == UserId && collab.NoteId == NoteId
                       select collab).ToList();
            return res;
        }
    }
}
