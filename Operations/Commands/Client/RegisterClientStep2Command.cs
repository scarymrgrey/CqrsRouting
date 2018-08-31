using System.ComponentModel.DataAnnotations;
using CQRS.Block;
using Incoding.CQRS;
using Newtonsoft.Json;

namespace Operations.Commands.Client
{
    public class RegisterClientStep2Command : CommandBase
    {
        [Required]
        public int ClientId { get; set; }

        [JsonIgnore]
        public int UserModifyId { get; set; }

        protected override void Execute()
        {
            var client = Repository.GetById<Entities.Client>(ClientId);
            if (client == null)
                throw new CQRSValidationException<RegisterClientStep2Command>(r=>r.ClientId,"ClientId should exist");
        }
    }
}