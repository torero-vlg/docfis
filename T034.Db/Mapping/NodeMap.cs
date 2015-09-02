using Db.Entity;
using FluentNHibernate.Mapping;

namespace Db.Mapping
{
    public class NodeMap : ClassMap<Node>
    {
        public NodeMap()
        {
            Id(x => x.Id).Column("NodeId").GeneratedBy.Increment();

            Map(p => p.Path);
            Map(p => p.Description);
            Map(p => p.NodeType).Column("NodeTypeId").CustomType<int>();
        }
    }
}
