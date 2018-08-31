using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Incoding;
using Incoding.Data;

namespace Operations.Entities
{
    public class Client : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public DateTime? BirthDate { get; set; }

        [ForeignKey("EmailId")]
        public Email Email { get; set; }

        [ForeignKey("PhoneId")]
        public Phone Phone { get; set; }

        public bool FrameworkAgreementAccept { get; set; }

        public bool PersonalDataAccept { get; set; }

        public bool IsActiveForMarketing { get; set; }

        public bool IsActiveForSMS { get; set; }

        public bool IsActiveForCall { get; set; }

        public string IP { get; set; }

        [ForeignKey("UserModify")]
        public User UserModified { get; set; }

        public abstract class Where
        {
            public class ById : Specification<Client>
            {
                public int Id;
                public override Expression<Func<Client, bool>> IsSatisfiedBy() => r => r.Id == Id;
            }

            public class ByEmail : Specification<Client>
            {
                public string Email;
                public override Expression<Func<Client, bool>> IsSatisfiedBy() => r => r.Email.EMail == Email;
            }
        }
    }
}
