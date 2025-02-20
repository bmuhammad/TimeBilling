using FreeBilling.Data.Entities;

namespace TimeBilling.Data;

public interface IBillingRepository
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<IEnumerable<Customer>> GetCustomersWithAddresses();
    Task<Customer?> GetCustomers(int id);
    Task<IEnumerable<Employee>> GetEmpployees();

    Task<bool> SaveChanges();
}