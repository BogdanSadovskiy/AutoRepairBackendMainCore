namespace AutoRepairMainCore.DTO.Models
{
    public class CarResults<T>
    {
        public T Entity {  get; set; }
        public bool isNew { get; set; }

        public CarResults(T entity, bool isNew)
        {
            Entity = entity;
            this.isNew = isNew;
        }
    }
}
