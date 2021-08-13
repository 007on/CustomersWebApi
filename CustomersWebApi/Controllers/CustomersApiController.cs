using CustomersWebApi.Data;
using CustomersWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CustomersWebApi.Controllers
{
    /// <summary>
    /// Класс контроллера
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CustomersApiController : Controller
    {
        /// <summary>
        /// Конструктор объектов класса
        /// </summary>
        /// <param name="model">Модель взаимодействия с данными</param>
        public CustomersApiController(IModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        /// <summary>
        /// Модель взаимодействия с данными
        /// </summary>
        public IModel Model { get; private set; }

        /// <summary>
        /// Найти клиента по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>
        /// 404 - пользователь с таким идентификатором не найден
        /// Иначе - пользователь с заданным идентификатором
        /// </returns>
        [HttpGet("{id}", Name = "GetCustomerById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var dto = await Model.FindCustomerByIdAsync(id);
            if (dto.GetCustomer() == null)
            {
                return NotFound();
            }

            return new ObjectResult(
                new
                {
                    id = dto.Id,
                    firstnname = dto.FirstName,
                    lastname = dto.LastName,
                    age = dto.Age
                });
        }

        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        /// <param name="dto">Добавляемый пользователь</param>
        /// <returns>
        /// 201 - Клиент успешно добавлен
        /// 400 - одно или несколько полей пусты
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.BirthDate))
            {
                var errorMessage = dto.CreateErrorMessage();
                return BadRequest(errorMessage);
            }
            if (!DateTime.TryParse(dto.BirthDate, out var dt))
            {
                return BadRequest("invalid birth date");
            }

            await Model.AddCustomerAsync(dto);

            return StatusCode(201);
        }
    }
}