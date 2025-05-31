namespace CleanArchMediatR.Template.Domain.Common
{
    public abstract class AggregateRoot<TId>
    {
        public TId Id { get; protected set; }

        protected AggregateRoot() { }

        protected AggregateRoot(TId id) { 
            Id = id;
        }
    }
}
