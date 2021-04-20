using CustomersWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CustomersWebApi.Models
{
    public class CustomersDbContext : DbContext, IModel
    {
        public CustomersDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        /// <inheritdoc/>
        public async Task AddCustomerAsync(CustomerDTO dto)
        {
            await Customers.AddAsync(dto.CreateCustomer());
            await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<CustomerDTO> FindCustomerByIdAsync(int id)
        {
            var result = await Customers.FindAsync(id);
            return new CustomerDTO(result);
        }
    }
}