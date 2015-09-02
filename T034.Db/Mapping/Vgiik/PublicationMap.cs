using Db.Entity.Vgiik;
using FluentNHibernate.Mapping;

namespace Db.Mapping.Vgiik
{
    public class PublicationMap : ClassMap<Publication>
    {
        public PublicationMap()
        {
            Id(x => x.Id).Column("PublicationId").GeneratedBy.Increment();
        }
    }
}
