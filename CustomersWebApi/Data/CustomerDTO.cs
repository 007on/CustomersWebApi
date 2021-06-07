using System;
using System.Text;

namespace CustomersWebApi.Data
{
    public class CustomerDTO
    {
        private Customer customer;

        /// <summary>
        /// Конструктор объектов класса по умолчанию
        /// </summary>
        public CustomerDTO() { }

        /// <summary>
        /// Конструктор объектов класса по объекту класса Customer
        /// </summary>
        /// <param name="customer"></param>
        public CustomerDTO(Customer customer)
        {
            if (customer != null)
            {
                this.customer = customer;
                Id = customer.Id;
                FirstName = customer.FirstName;
                LastName = customer.LastName;
                BirthDate = customer.BirthDate.ToString();
            }
        }

        /// <summary>
        /// Уникальный идентификатор клиента
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        public string BirthDate
        {
            get;
            set;
        }

        /// <summary>
        /// Возраст клиента
        /// </summary>
        public int Age { get => GetAge(customer.BirthDate); }

        private int GetAge(DateTime birthDate)
        {
            var now = DateTime.Today;
            var age = now.Year - birthDate.Year;
            if (birthDate > now.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        /// <summary>
        /// Сформировать сообщение об ошибке
        /// </summary>
        /// <returns>Сообщение об ошибке</returns>
        public string CreateErrorMessage()
        {
            var message = new StringBuilder();
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                message.Append(nameof(FirstName));
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(nameof(LastName));
            }
            if (!DateTime.TryParse(BirthDate, out var d))
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(nameof(BirthDate));
            }

            return message.ToString() + " can't be empty";
        }

        /// <summary>
        /// Создать клиента, с заданными свойствами
        /// </summary>
        /// <returns></returns>
        public Customer GetCustomer()
        {
            if (customer == null)
            {
                try
                {
                    customer = new Customer()
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        BirthDate = DateTime.Parse(BirthDate)
                    };
                }
                catch
                {
                    customer = null;
                }
            }

            return customer;
        }
    }
}