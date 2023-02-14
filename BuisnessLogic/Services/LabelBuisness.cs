using BuisnessLogic.Interfaces;
using Model;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Services
{
    public class LabelBuisness : ILabelBuisness
    {
        private readonly ILabelRepository labelRepository;

        public LabelBuisness(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        public LabelEntity AddLabel(LabelModel model, long UserId)
        {
            return labelRepository.AddLabel(model, UserId);
        }

        public LabelEntity UpdateLabelName(string newLabelName, long LabelId, long NoteId, long UserId)
        {
            return labelRepository.UpdateLabelName(newLabelName, LabelId, NoteId, UserId);
        }

        public LabelEntity GetLabel(long LabelId, long NoteId, long UserId)
        {
            return labelRepository.GetLabel(LabelId, NoteId, UserId);
        }

        public List<LabelEntity> GetAllLabels(long NoteId, long UserId)
        {
            return labelRepository.GetAllLabels(NoteId, UserId);
        }

        public bool DeleteLabel(long LabelId, long NoteId, long UserId)
        {
            return labelRepository.DeleteLabel(LabelId, NoteId, UserId);
        }
    }
}
