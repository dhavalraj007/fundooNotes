using Microsoft.Extensions.Configuration;
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
    public class LabelRepository:ILabelRepository
    {
        private readonly FundooContext context;
        private readonly IConfiguration configuration;

        public LabelRepository(FundooContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public LabelEntity AddLabel(LabelModel model,long UserId)
        {
            LabelEntity label = new LabelEntity();
            //note.NoteId = model.NoteId;
            label.UserId = UserId;
            label.LabelName = model.LabelName;
            label.NoteId = model.NoteId;
            context.LabelTable.Add(label);
            int res = context.SaveChanges();
            if (res > 0)
                return label;
            return null;
        }

        public LabelEntity UpdateLabelName(string newLabelName,long LabelId,long NoteId,long UserId)
        {
            var lab = (from label in context.LabelTable
                      where label.UserId == UserId && label.NoteId==NoteId && label.LabelId== LabelId
                      select label).FirstOrDefault();
            lab.LabelName = newLabelName;
            int res = context.SaveChanges();
            if (res > 0)
                return lab;
            return null;
        } 

        public bool DeleteLabel(long LabelId,long NoteId,long UserId)
        {
            var lab = (from label in context.LabelTable
                       where label.UserId == UserId && label.NoteId == NoteId && label.LabelId == LabelId
                       select label).FirstOrDefault();
            context.LabelTable.Remove(lab);
            return context.SaveChanges() > 0;
        }
        public LabelEntity GetLabel(long LabelId,long NoteId,long UserId)
        {
            var lab = (from label in context.LabelTable
                       where label.UserId == UserId && label.NoteId == NoteId && label.LabelId == LabelId
                       select label).FirstOrDefault();

            return lab;
        }

        
        public List<LabelEntity> GetAllLabels(long NoteId, long UserId)
        {
            var res = (from label in context.LabelTable.Cast<LabelEntity>()
                       where label.UserId == UserId && label.NoteId == NoteId
                       select label).ToList();
            return res;
        }

    }
}
