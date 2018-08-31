using CQRS.Block;
using Incoding.CQRS;
using Newtonsoft.Json;
using Operations.Entities;
namespace Operations.Queries.Client
{
    public class RegisterClientStep1Command : CommandBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>Unique</summary>
        public string Email { get; set; }

        /// <summary>Unique</summary>
        public string Phone { get; set; }

        public string IP { get; set; }

        public bool FrameworkAgreementAccept { get; set; }

        public bool PersonalDataAccept { get; set; }

        public bool MarketingAccept { get; set; }

        [JsonIgnore]
        public int UserModifyId { get; set; }

        protected override void Execute()
        {
            var currentUser = Repository.LoadById<User>(UserModifyId);
            var email = new Email()
            {
                EMail = Email,
                UserModified = currentUser,
                UserCreated = currentUser,
                Flags = 0
            };

            var phone = new Phone()
            {
                PHone = Phone,
                UserModified = currentUser,
                UserCreated = currentUser,
                Flags = 0
            };

            var user = new Entities.Client()
            {
                Email = email,
                Phone = phone,
                FrameworkAgreementAccept = FrameworkAgreementAccept,
                PersonalDataAccept = PersonalDataAccept,
                IsActiveForMarketing = MarketingAccept,
                LastName = LastName,
                FirstName = FirstName,
                UserModified = currentUser,
                IP = IP
            };
            Repository.Save(user);
            Repository.Flush();
            Result = user.Id;
        }
    }
}