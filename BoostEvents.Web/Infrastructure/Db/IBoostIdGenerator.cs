namespace BoostEvents.Web.Infrastructure.Db;

public interface IBoostIdGenerator
{
    Guid New(); // For COMB GUID generation
}