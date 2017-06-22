namespace LCL.Domain.Entities
{
    public static class EntityExtensions
    {
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }
        public static void UnDelete(this ISoftDelete entity)
        {
            entity.IsDeleted = false;
        }
    }
}