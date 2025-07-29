namespace BoostEvents.Web.Application.Interfaces;

public interface IUserInfo
{
    string Auth0UserId { get; }
    Guid TenantId { get; }
    Guid BusinessId { get; }
    Guid BusinessInstanceId { get; }
}