using System.Collections.Generic;

namespace Db.Entity.Vgiik
{
    public class Person : Entity
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Звание, титул
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Список альбомов
        /// </summary>
        public virtual IList<Album> Albums { get; set; }
    }
}
