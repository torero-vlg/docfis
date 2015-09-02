using System.Collections.Generic;

namespace Db.Entity.Vgiik
{
    public class Department : Entity
    {
        public virtual string Name { get; set; }

        public virtual IList<Person> Staff { get; set; }

        public virtual string Text { get; set; }

        public virtual string MainPhoto { get; set; }

        public virtual string MainPhotoDescription { get; set; }

        /// <summary>
        /// Список альбомов
        /// </summary>
        public virtual IList<Album> Albums { get; set; }

        /// <summary>
        /// Список публикаций
        /// </summary>
        public virtual ICollection<Node> Nodes { get; set; }
    }
}
