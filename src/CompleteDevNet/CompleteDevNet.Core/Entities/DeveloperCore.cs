using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteDevNet.Core.Entities
{
    public class DeveloperCore
    {
        public long Id { get; set; }
        public Guid IdentGuid { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? SkillSet { get; set; }
        public string? Hobby { get; set; }
    }
}
