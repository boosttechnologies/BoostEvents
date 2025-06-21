using BoostEvents.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BoostEvents.Web;

public class BoostEventsDbContext(DbContextOptions<BoostEventsDbContext> options) : DbContext(options)
{
    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsUpper(c))
            {
                // Append underscore if not first char and previous wasn't already an underscore
                if (i > 0 && input[i - 1] != '_')
                    sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
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
            entity.SetTableName(entity.GetTableName()!.ToLower());

            // Optional: Set column names to lowercase too
            /*foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.Name.ToLower());
            }*/
            // Optional: Set column names to lowercase snake case
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            // Optional: Set key/foreign key constraint names to lowercase
            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName()!.ToLower());
            }

            foreach (var fk in entity.GetForeignKeys())
            {
                fk.SetConstraintName(fk.GetConstraintName()!.ToLower());
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName()!.ToLower());
            }
            
            
        }
    }
    
    public DbSet<Business> Business => Set<Business>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
} 