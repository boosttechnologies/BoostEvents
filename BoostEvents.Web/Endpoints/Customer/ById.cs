using BoostEvents.Web.Data;
using Dapper;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BoostEvents.Web.Endpoints.Customer;

public class ById : EndpointWithoutRequest<Data.Models.Customer>
{
    public override void Configure()
    {
        Get("/customers/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // Pull route param manually
        var id = Route<int>("id");
        var customer = new Data.Models.Customer(id, "Brandon Jones", "brandon@boostmyevents.com");
        await SendOkAsync(customer, ct);
    }
}
