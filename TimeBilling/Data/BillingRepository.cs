using FreeBilling.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TimeBilling.Data;

public class BillingRepository : IBillingRepository
{
    private readonly BillingContext _context;

    private readonly ILogger<BillingRepository> _logger;

    public BillingRepository(BillingContext context, ILogger<BillingRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Employee>> GetEmpployees()
    {
        try
        {
            return await _context.Employees
              .OrderBy(e => e.Name)
              .ToListAsync();
        }
        catch (Exception ex)
        {

            _logger.LogError($"Could not get Employees: {ex.Message}");
            throw;
        }
    }
    public async Task<IEnumerable<Customer>> GetCustomersWithAddresses()
    {
        try
        {
            return await _context.Customers
                .Include(c => c.Address)
                .OrderBy(e => e.CompanyName)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not get Customers: {ex.Message}");
            throw;
        }
    }
    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        try
        {
            return await _context.Customers
                .OrderBy(e => e.CompanyName)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not get Customers: {ex.Message}");
            throw;
        }
    }

    public async Task<Customer?> GetCustomers(int id)
    {
        try
        {
            return await _context.Customers
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not get Customers: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> SaveChanges()
    {
        try
        {
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Could not Save to the Database: {ex.Message}");
            throw;
        }
    }

    public async Task<TimeBill?> GetTimeBill(int id)
    {
        var bill = await _context.TimeBills
            .Include(b => b.Employee)
            .Include(b => b.Customer)
            .ThenInclude(c => c!.Address)
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();

        return bill;
    }

    public void AddEntity<T>(T entity) where T : notnull
    {

        _context.Add(entity);
    }

    public async Task<IEnumerable<TimeBill>> GetTimeBillsForCustomer(int id)
    {
        return await  _context.TimeBills
            .Where(b => b.CustomerId != null && b.CustomerId == id)
            .Include(b => b.Customer)
            .Include(b => b.Employee)
            .ToListAsync();
    }

    public async Task<TimeBill?> GetTimeBillForCustomer(int id, int billId)
    {
        return await _context.TimeBills
             .Where(b => b.CustomerId != null && b.CustomerId == id && b.Id == billId)
             .Include(b => b.Customer)
             .Include(b => b.Employee)
             .FirstOrDefaultAsync();
    }
}
