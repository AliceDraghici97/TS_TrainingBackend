using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Training.IRepository.Entity;
using Training.Model.Entity;

namespace Training.API.Controllers.Entity
{
    public class RolesController : GenericController<Role>
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }
    }
}