using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Domain.Entities
{
    public class User
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string userName { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;
    }
}
