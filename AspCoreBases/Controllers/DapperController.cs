using App.Core.Dto.Tests;
using App.Core.Infra.Repositories.Databases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspCoreTuto.Controllers
{
    [ApiController]
    [Route("test/dapper")]
    [AllowAnonymous]
    public class DapperController
    {
        private readonly IDapperTestRepository _employeeRepo;

        public DapperController(IDapperTestRepository dapperRepository)
        {
            _employeeRepo = dapperRepository ?? throw new ArgumentNullException(nameof(dapperRepository));
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IEnumerable<Person>> GetPersons()
        {
            List<Person> list = (await _employeeRepo.GetPersonsV2()).ToList();
            return list;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Person>> GetByID(int id)
        {
            return (await _employeeRepo.GetPersons()).FirstOrDefault(x => x.Id == id);
        }
    }
}
