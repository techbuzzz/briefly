namespace Briefly.Core.Domain;

public interface ISoftDeletable
{
    DateTimeOffset? Deleted { get; set; }
    Guid? DeletedBy { get; set; }
}