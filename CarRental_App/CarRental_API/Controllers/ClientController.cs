using CarRental_Application.Interfaces;
using CarRental_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientServices _clientService;
        public ClientController(IClientServices clientService)
        {
            _clientService = clientService;
        }
        // GET: api/<ClientController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id)
        {
            return Ok(_clientService.GetClientById(id));
        }

        [HttpGet]
        public ActionResult<List<Client>> GetAll()
        {
            return Ok(_clientService.GetAllClients());
        }

        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<int> PostClient(Client client)
        {
            return Ok(_clientService.CreateClient(client));
        }

        //// PUT api/<ClientController>/5
        [HttpPut]
        public ActionResult<Client> Put(Client client)
        {
            _clientService.UpdateClient(client);
            return Ok();
        }

        //// DELETE api/<ClientController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
