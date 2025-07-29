using BoostEvents.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BoostEvents.Web.Infrastructure.Persistence;

public class BoostEventsDbContext(DbContextOptions<BoostEventsDbContext> options) : DbContext(options)
{
    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;
     
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
     
            if (char.IsUpper(c))
            {
                bool addUnderscore =
                    i > 0 &&
                    (char.IsLower(input[i - 1]) || (i + 1 < input.Length && char.IsLower(input[i + 1])));
     
                if (addUnderscore)
                    sb.Append('_');
     
                sb.Append(char.ToLowerInvariant(c));
            }
            else if (char.IsDigit(c) && i > 0 && char.IsLetter(input[i - 1]))
            {
                sb.Append('_');
                sb.Append(c);
            }
            else
            {
                sb.Append(c);
            }
        }
     
        return sb.ToString();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Set table name to lowercase
            /*entity.SetTableName(entity.GetTableName()?.ToLower());*/
            var tableName = entity.GetTableName();
            if (!string.IsNullOrWhiteSpace(tableName))
                entity.SetTableName(ToSnakeCase(tableName));

   
            // Optional: Set column names to lowercase snake case
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            // Optional: Set key/foreign key constraint names to lowercase
            foreach (var key in entity.GetKeys())
            {
                if (!string.IsNullOrWhiteSpace(key.GetName()))
                    key.SetName(ToSnakeCase(key.GetName()!));
            }
    
            // Set foreign key constraint names to snake_case
            foreach (var fk in entity.GetForeignKeys())
            {
                if (!string.IsNullOrWhiteSpace(fk.GetConstraintName()))
                    fk.SetConstraintName(ToSnakeCase(fk.GetConstraintName()!));
            }
            
            // Set index names to snake_case
            foreach (var index in entity.GetIndexes())
            {
                if (!string.IsNullOrWhiteSpace(index.GetDatabaseName()))
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }
        
        modelBuilder.Entity<Business>().HasQueryFilter(b => !b.IsDeleted);
    }
    
    // Tenant + Business
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<TenantPlan> TenantPlans => Set<TenantPlan>();
    public DbSet<TenantAiUsage> TenantAiUsages => Set<TenantAiUsage>();
    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();

    public DbSet<Business> Businesses => Set<Business>();
    public DbSet<BusinessInstance> BusinessInstances => Set<BusinessInstance>();

    // Clients & Tickets
    public DbSet<TenantClient> Clients => Set<TenantClient>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<SaleItemPriceTier> SaleItemPriceTiers => Set<SaleItemPriceTier>();
    public DbSet<Discount> Discounts => Set<Discount>();

    // Workflows
    public DbSet<Workflow> Workflows => Set<Workflow>();
    public DbSet<WorkflowStep> WorkflowSteps => Set<WorkflowStep>();
    public DbSet<WorkflowSubmission> WorkflowSubmissions => Set<WorkflowSubmission>();
    public DbSet<WorkflowStepAnswer> WorkflowStepAnswers => Set<WorkflowStepAnswer>();
    public DbSet<WorkflowStepRule> WorkflowStepRules => Set<WorkflowStepRule>();

    // Form Fields
    public DbSet<FormField> FormFields => Set<FormField>();
    public DbSet<FormFieldTemplate> FormFieldTemplates => Set<FormFieldTemplate>();

    // Entry + Devices
    public DbSet<DeviceToken> DeviceTokens => Set<DeviceToken>();

    // Logs
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<AiLog> AiLogs => Set<AiLog>();
    
    
    
} 