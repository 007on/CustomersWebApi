using CustomersWebApi.Data;
using System.Threading.Tasks;

namespace CustomersWebApi.Models
{
    /// <summary>
    /// Интерфейс модели
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="customer">Добавляемый клиент</param>
        Task AddCustomerAsync(CustomerDTO customer);

        /// <summary>
        /// Найти клиента по ID
        /// </summary>
        /// <param name="id">ID искомого клиента</param>
        /// <returns>Найденный клиент</returns>
        Task<CustomerDTO> FindCustomerByIdAsync(int id);
    }
}