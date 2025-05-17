using API.Entities;
using System.Text.Json.Serialization;

namespace Med_Safe_Net_API.Entities
{
    public class UserLink
    {
        public int UserLinkId { get; set; }
        public int CareGiverId { get; set; }
        [JsonIgnore]
        public AppUser CareGiver { get; set; }
        public int PatientId { get; set; }
        [JsonIgnore]
        public AppUser Patient { get; set; }

    }
}
