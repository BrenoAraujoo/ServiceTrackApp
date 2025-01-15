namespace ServiceTrackApp.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime UpdateDate{ get; private set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        public void Update()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
