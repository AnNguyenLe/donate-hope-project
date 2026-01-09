namespace DonateHope.Domain.Traceables;

public interface ITraceable : ICreatedAt, IUpdatedAt, ICreatedBy, IUpdatedBy, ISoftDeletable { }
