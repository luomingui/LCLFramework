
using LCL.Domain.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;

namespace LCL.Repositories.MongoDB.Conventions
{
    public class GuidIDGeneratorConvention : IPostProcessingConvention
    {
        #region IPostProcessingConvention Members
        public void PostProcess(BsonClassMap classMap)
        {
            if (typeof(IEntity).IsAssignableFrom(classMap.ClassType) && classMap.IdMemberMap != null)
                classMap.IdMemberMap.SetIdGenerator(new GuidGenerator());
        }

        #endregion

        #region IConvention Members
        public string Name
        {
            get { return this.GetType().Name; }
        }

        #endregion
    }
}
