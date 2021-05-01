using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySwaggerUIServer.Attributes;
using MySwaggerUIServer.Models;
using MySwaggerUIServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Controllers
{
    /// <summary>
    /// Операции с пользователями
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsers _users;
        private readonly ILogger<UserController> _logger;

        public UserController(IUsers users, ILogger<UserController> logger)
        {
            _logger = logger;
            _users = users;
        }

        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        /// <param name="id"> Уникальный номер </param>
        /// <returns></returns>
        /// <response code = "200"> Пользователь найден </response>
        /// <response code = "404"> Пользователь не найден </response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetById(int id)
        {
            await Task.CompletedTask;

            var user = _users.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;

        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <param name="name"> Имя пользователя </param>
        /// <param name="age"> Возраст пользователя </param>
        /// <returns></returns>
        /// <response code = "200"> Пользователь успешно добавлен </response>
        /// <response code = "500"> Ошибка сервера </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> AddUser

           ([OnlyLatin(ErrorMessage =  "Name must contain only English letters")]
            [Length(MinLen = 2, MaxLen = 30, ErrorMessage = "Length of name must be")]
            string name,

            [Range(MinNum = 18, MaxNum = 120, ErrorMessage = "Range of age must be")]
            int age)
        {
            await Task.CompletedTask;
            var user = _users.AddUser(name, age);
            return user;
        }


        /// <summary>
        /// Изменить имя пользователя по id
        /// </summary>
        /// <param name="id"> Уникальный номер </param>
        /// <param name="newName"> Новое имя пользователя </param>
        /// <param name="newAge"> Новый возраст пользователя </param>
        /// <returns></returns>
        /// <response code = "200"> Имя пользователя успешно изменено </response>
        /// <response code = "404"> Пользователь не найден </response>
        /// <response code = "500"> Ошибка сервера</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateNameById

           (int id,

            [OnlyLatin(ErrorMessage = "Name must contain only English letters")]
            [Length(MinLen = 2, MaxLen = 30, ErrorMessage = "Length of name must be")]
            string newName,

            [Range(MinNum = 18, MaxNum = 120, ErrorMessage = "Range of age must be")]
            int newAge)
        {
            await Task.CompletedTask;
            var isUpdated = _users.PutUser(id, newName, newAge);
            return isUpdated ? Ok() : NotFound();
        }

        /// <summary>
        /// Удалить пользователя по id
        /// </summary>
        /// <param name="id"> Уникальный номер </param>
        /// <returns></returns>
        /// <response code = "200"> Пользователь успешно удален </response>
        /// <response code = "404"> Пользователь не найден </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUserById(int id)
        {
            await Task.CompletedTask;
            var isDeleted = _users.DeleteUser(id);
            return isDeleted ? Ok() : NotFound();
        }

        /// <summary>
        /// Фильтр по возрасту
        /// </summary>
        /// <param name="min"> Минимальный возраст </param>
        /// <param name="max"> Максимальный возраст </param>
        /// <returns></returns>
        [HttpGet("/listUsers/ageFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User[]>> GetUsersByAge(int min, int max)
        {
            await Task.CompletedTask;
            IOrderedEnumerable<User> orderedUsers = _users.GetUsersByAge(min, max);
            var orderedUsersArray = orderedUsers.ToArray();

            return orderedUsersArray;
        }

        /// <summary>
        /// Получить имена пользователей, состоящие более чем из четырех букв
        /// </summary>
        /// <response code = "200"> Список успешно получен </response>
        /// <returns></returns>
        [HttpGet("/listUsers/nameFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User[]>> GetMoreFourNameUsers()
        {
            await Task.CompletedTask;
            IOrderedEnumerable<User> orderedUsers = _users.GetMoreFourNameUsers();
            var orderedUsersArray = orderedUsers.ToArray();

            return orderedUsersArray;
        }
    }
}
