using Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface ILabelRepository
    {
        public LabelEntity AddLabel(LabelModel model, long UserId);
        public List<LabelEntity> GetAllLabels(long NoteId, long UserId);

    }
}
