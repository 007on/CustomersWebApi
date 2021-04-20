using System;
using System.Text;

namespace CustomersWebApi.Data
{
    public class CustomerDTO
    {
        /// <summary>
        /// Конструктор объектов класса по умолчанию
        /// </summary>
        public CustomerDTO() { Customer = new Customer(); }

        /// <summary>
        /// Конструктор объектов класса по объекту класса Customer
        /// </summary>
        /// <param name="customer"></param>
        public CustomerDTO(Customer customer)
        {
            Customer = customer;
        }

        /// <summary>
        /// Клиент, полученный из БД
        /// </summary>
        public Customer Customer { get; }

        /// <summary>
        /// Уникальный идентификатор клиента
        /// </summary>
        public int Id
        {
            get => Customer.Id;
            set => Customer.Id = value;
        }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName
        {
            get => Customer?.FirstName;
            set => Customer.FirstName = value;
        }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName
        {
            get => Customer?.LastName;
            set => Customer.LastName = value;
        }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        public string BirthDate
        {
            get => Customer.BirthDate.ToString();
            set
            {
                DateTime date;
                DateTime.TryParse(value, out date);
                Customer.BirthDate = date;
            }
        }

        /// <summary>
        /// Возраст клиента
        /// </summary>
        public int Age { get => GetAge(Customer.BirthDate); }

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
        public Customer CreateCustomer()
        {
            return new Customer()
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = DateTime.Parse(BirthDate)
            };
        }
    }
}
