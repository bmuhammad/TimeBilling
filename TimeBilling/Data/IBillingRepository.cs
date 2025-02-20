using FreeBilling.Data.Entities;

namespace TimeBilling.Data;

public interface IBillingRepository
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<IEnumerable<Customer>> GetCustomersWithAddresses();
    Task<Customer?> GetCustomers(int id);
    Task<IEnumerable<Employee>> GetEmpployees();
    Task<TimeBill?> GetTimeBill(int id);
    Task <IEnumerable<TimeBill>> GetTimeBillsForCustomer(int id);
    Task <TimeBill?>GetTimeBillForCustomer(int id, int billId);
    void AddEntity<T>(T entity) where T : notnull;
    Task<bool> SaveChanges();
}