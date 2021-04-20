using System;
using System.ComponentModel.DataAnnotations;

namespace CustomersWebApi.Data
{
    /// <summary>
    /// Класс клиента
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Уникальный идентификатор клиента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName{ get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName{ get; set; }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
