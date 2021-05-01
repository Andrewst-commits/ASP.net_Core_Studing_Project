using Microsoft.Extensions.Logging;
using MySwaggerUIServer.Exceptions;
using MySwaggerUIServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Services
{
    public class UsersInMemoryRepository : IUsers
    {

        private readonly List<User> _users;
        private readonly ILogger<UsersInMemoryRepository> _logger;
        private const int MAX_NAME_LENGTH = 30;
        private const int MIN_AGE = 1;
        private const int MAX_AGE = 150;

        public UsersInMemoryRepository(ILogger<UsersInMemoryRepository> logger)
        {
            _users = new List<User>();
            _logger = logger;

        }

        /// <summary>
        /// Извлекаем пользователся из базы данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            return _users.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Помещаем валидного юзера в базу данных
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public User AddUser(string name, int age)
        {
            _logger.LogDebug("<add user> Request to add user. </add user>");

            Validate(name, age);

            var user = new User() { Id = _users.Count + 1, Name = name, Age = age };
            _users.Add(user);

            _logger.LogInformation($"<add user> User №{user.Id} has been added succesfully. </add user>");
            return user;
        }

        /// <summary>
        /// Обновляем информацию о пользователе, заранее провалидировав ее
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newName"></param>
        /// <param name="newAge"></param>
        /// <returns></returns>
        public bool PutUser(int id, string newName, int newAge)
        {
            _logger.LogDebug("<update user> Request to update user. </update user>");
            var user = GetUser(id);
            if (user == null)
            {
                _logger.LogInformation("User is not found");
                return false;
            }

            Validate(newName, newAge);

            user.Name = newName;
            user.Age = newAge;

            _logger.LogInformation($"<update user> User №{id} has been updated succesfully. </update user>");
            return true;
        }

        /// <summary>
        /// Удаляем юзера из базы данных по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            _logger.LogDebug("<delete user> Request to delete user </delete user>");
            var user = GetUser(id);
            if (user == null)
            {
                _logger.LogInformation("User is not found");
                return false;
            }

            _users.Remove(user);
            _logger.LogInformation($"<delete user> User №{id} has been deleted succesfully. </delete user>");
            return true;
        }

        /// <summary>
        /// Реализация фильтра по возрасту
        /// </summary>
        /// <param name="minAge"> Минимальный возраст </param>
        /// <param name="maxAge"> Максимальный возраст </param>
        /// <returns></returns>
        public IOrderedEnumerable<User> GetUsersByAge(int minAge, int maxAge)
        {
            var filteredUsers = _users
                .Where(c => c.Age >= minAge && c.Age <= maxAge)
                .OrderBy(c => c.Id);

            return filteredUsers;
        }

        /// <summary>
        /// Получить имена пользователей, состоящие более чем из четырех букв
        /// </summary>
        /// <returns></returns>
        public IOrderedEnumerable<User> GetMoreFourNameUsers()
        {
            var filteredUsers = _users
                .Where(c => c.Name.Length > 4)
                .OrderBy(c => c.Id);

            return filteredUsers;
        }





        /// <summary>
        /// Функция валидации на стороне БД
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        private void Validate(string name, int age)
        {
            if (name == null)
            {
                throw new UserNameException("Name enter is required", name);
            }
            else if (name.Length > MAX_NAME_LENGTH)
            {
                throw new UserNameException("Max length of name has been exceeded", name);
            }


            for (int i = 0; i < name.Length; i++)
            {
                if (!(name[i] >= 'a' && name[i] <= 'z' || name[i] >= 'A' && name[i] <= 'Z'))
                {
                    throw new UserNameException("Invalid symbols have been entered", name);
                }
            }

            if (age < MIN_AGE || age > MAX_AGE)
            {
                throw new UserAgeException("Range of age has been come over", age);
            }

        }



    }
}



// у UserInMemoryRepo есть и независимо от контроллера они будут выбрасываться
// для чего эти исключения 

// 1) name == null, человек не заполнил поле имени: 
//проблемы с дальнейшей обработкой пользователя по имени
//затруднение идентификации человека
//засорение базы данных неполными и некорректными данными

// 2)  age > MAX || age < MIN:
//некорректная информация, вводящая в заблуждение о представлении пользователя
//временные затраты на получение корректной информации о пользователе 
// 3)  name > MAX:
/*ухудшение читабельности текста информации о пользователе 
 * и вследствие временные затраты на выводы о пользователе*/

// 4)  проверка name на латиницу и цифры:
/*
 * база данных ограничивает регистратора в языковом отношении 
 * и просит вводить имя на интернациональном языке для того, 
 * чтобы база данных была понятна большинству людей
 * также запрещен ввод цифр в поле name для большей читабельности
 */
/*Ограничения заставят регистратора обратить внимание на ввод некорректных данных 
 * и предотвратить проблемы связанные с ними */