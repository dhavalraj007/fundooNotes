using Model;
using Repository.Entity;
using System.Collections.Generic;

namespace BuisnessLogic.Interfaces
{
    public interface ILabelBuisness
    {
        public LabelEntity AddLabel(LabelModel model, long UserId);
        public List<LabelEntity> GetAllLabels(long NoteId, long UserId);
        
    }
}