﻿using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Interfaces;

namespace ServiceTrackApp.Domain.Entities
{
    public class TaskType : BaseEntity, IEntityActivable
    {

        private const int MaxTaskTypeNameLenght = 50;
        private const int MinTaskTypeNameLenght = 3;
        private const int MaxDescriptionLength = 100;
        public Guid CreatorId { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool Active { get; private set; }
        public User User{ get; private set; } //EF nav property
        public TaskType(){}

        public TaskType(Guid creatorId,string name, string? description)
        {
            SetName(name);
            SetDescription(description);
            CreatorId = creatorId;
            Activate();
        }

        public void Activate()
        {
            if (Active)
                throw new InvalidOperationException(ErrorMessage.TaskTypeAlreadyActivated);
            Active = true;
            base.Update();
        }

        public void Deactivate()
        {
            if (!Active)
                throw new InvalidOperationException(ErrorMessage.TaskTypeAlreadyInactivated);
            Active = false;
            base.Update();
        }

        public void Update(string? name, string? description, bool active)
        {
            SetName(name);
            SetDescription(description);
            Active = active;
            base.Update();
        }

        private void SetName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ErrorMessage.TaskTypeInvalidName);
            switch (name.Length)
            {
                case > MaxTaskTypeNameLenght:
                    throw new ArgumentException(ErrorMessage.TaskTypeMaxName);
                case < MinTaskTypeNameLenght:
                    throw new ArgumentException(ErrorMessage.TaskTypeMinName);
            }

            Name = name ?? Name;
        }
        
        private void SetDescription(string? description)
        {
            if (!string.IsNullOrWhiteSpace(description) && description.Length > MaxDescriptionLength)
                throw new ArgumentException(ErrorMessage.TaskMaxDescription);
            Description = description ?? Description;
        }
    }
}
