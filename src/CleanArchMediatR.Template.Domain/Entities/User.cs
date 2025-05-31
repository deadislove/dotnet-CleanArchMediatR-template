using CleanArchMediatR.Template.Domain.Common;

namespace CleanArchMediatR.Template.Domain.Entities
{
    public class User: AggregateRoot<Guid>
    {
        public User(): base(Guid.NewGuid()) {}

        public Guid id { get; set; } = Guid.NewGuid();
        public string userName { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;

    }
}
