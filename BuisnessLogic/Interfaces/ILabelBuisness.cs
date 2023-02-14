using Model;
using Repository.Entity;
using System.Collections.Generic;

namespace BuisnessLogic.Interfaces
{
    public interface ILabelBuisness
    {
        public LabelEntity AddLabel(LabelModel model, long UserId);
        public LabelEntity UpdateLabelName(string newLabelName, long LabelId, long NoteId, long UserId);
        public bool DeleteLabel(long LabelId, long NoteId, long UserId);
        public LabelEntity GetLabel(long LabelId, long NoteId, long UserId);
        public List<LabelEntity> GetAllLabels(long NoteId, long UserId);
        
    }
}