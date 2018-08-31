using System.ComponentModel.DataAnnotations.Schema;
using Incoding.Data;

namespace Operations.Entities
{
    public class Phone : EntityBase
    {
        public string PHone { get; set; }

        public int Flags { get; set; }

        [ForeignKey("UserCreate")]
        public User UserCreated { get; set; }

        [ForeignKey("UserModify")]
        public User UserModified { get; set; }

        public abstract class Where
        {
        }
    }
}
