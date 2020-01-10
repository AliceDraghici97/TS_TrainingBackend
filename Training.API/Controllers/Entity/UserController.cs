using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Training.IRepository.Entity;
using Training.Model;

namespace Training.API.Controllers.Entity
{
    public class UserController : GenericController<User>
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        
    }
}

