
using System.ComponentModel.DataAnnotations.Schema;
using Incoding.Data;

namespace Operations.Entities
{
    [Table("Email")]
    public class Email : EntityBase
    {
        public string EMail { get; set; }

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
