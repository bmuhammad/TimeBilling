using FluentValidation;
using FreeBilling.Data.Entities;
using Mapster;
using System.ComponentModel.DataAnnotations;
using TimeBilling.Data;
using TimeBilling.Models;
using TimeBilling.Validators;

namespace TimeBilling.Apis;

public class TimeBillsApi
{
    public static void Register(WebApplication app)
    {
        var group = app.MapGroup("/api/timebills");

        group.MapGet("{id:int}", GetTimeBill)
            .WithName("GetTimeBill");

        group.MapPost("", PostTimeBill)
            .AddEndpointFilter<ValidateEndpointFilter<TimeBillModel>>();
    }

    public static async Task<IResult> GetTimeBill(IBillingRepository repository, int id)
    {     
            var bill = await repository.GetTimeBill(id);

            if (bill is null) Results.NotFound();

            return Results.Ok(bill);  
    }

    public static async Task<IResult> PostTimeBill(IBillingRepository repository,
        TimeBillModel model)
    {


        //var newEntity = new TimeBill()
        //{
        //    CustomerId = model.CustomerId,
        //    EmployeeId = model.EmployeeId,
        //    Hours = model.HoursWorked,
        //    BillingRate = model.Rate,
        //    Date = model.Date,
        //    WorkPerformed = model.Work
        //};

        var newEntity = model.Adapt<TimeBill>();

        repository.AddEntity(newEntity);
            if (await repository.SaveChanges())
            {
                var newBill = await repository.GetTimeBill(newEntity.Id);
                return Results.CreatedAtRoute("GetTimeBill", new { id = newEntity.Id }, newBill);
            }
            else
            {
                return Results.BadRequest();
            }
        
    }
}
