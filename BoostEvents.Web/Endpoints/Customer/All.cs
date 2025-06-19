using BoostEvents.Web.Data;
using Dapper;
using FastEndpoints;

namespace BoostEvents.Web.Endpoints.Customer;

public class All : EndpointWithoutRequest<List<Data.Models.Customer>>
{
    public override void Configure()
    {
        Get("/customers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var customers = new List<Data.Models.Customer>();

        var customer = new Data.Models.Customer(1, "Brandon Jones", "brandon@boostmyevents.com");
        var customer2 = new Data.Models.Customer(2, "Ron San Martino", "ron@boostmyevents.com");
        customers.Add(customer);
        customers.Add(customer2);
        
        await SendOkAsync(customers.ToList(), ct);
    }
}