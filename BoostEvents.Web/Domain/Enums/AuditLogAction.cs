namespace BoostEvents.Web.Domain;

public enum AuditLogAction
{
    Unknown = 0,
    UserLogin = 1,
    WorkflowSubmitted = 2,
    CompTicketIssued = 3,
    ClientCheckedIn = 4,
    DevicePaired = 5,
    ApiKeyCreated = 6,
    AiPromptExecuted = 7,
    SaleItemPurchased = 8
}