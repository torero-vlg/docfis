using Db.Entity.Directory;

namespace Db.Entity
{
    public class Node : Entity
    {
        /// <summary>
        /// Путь к файлу, url и тд
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// Описание, название для пользователя
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public virtual NodeType NodeType { get; set; }

        public virtual string Folder {
            get
            {
                return Path.Substring(0, Path.LastIndexOf("/", System.StringComparison.Ordinal) + 1);
            }
        }
    }
}
