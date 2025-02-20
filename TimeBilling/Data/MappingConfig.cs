using FreeBilling.Data.Entities;
using Mapster;
using TimeBilling.Models;

namespace TimeBilling.Data;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TimeBillModel, TimeBill>()
             .TwoWays()
             .Map(d => d.BillingRate, s => s.Rate)
             .Map(d => d.WorkPerformed, s => s.Work)
             .Map(d => d.Hours, s => s.HoursWorked);
    }
}
