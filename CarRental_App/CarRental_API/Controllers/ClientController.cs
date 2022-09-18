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

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id)
        {
            try
            {
                return Ok(_clientService.GetClientById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Client>> GetAll()
        {
            try
            {
                return Ok(_clientService.GetAllClients());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<int> PostClient(Client client)
        {
            try
            {
                int clientId = _clientService.CreateClient(client);
                if(clientId == -1)
                {
                    throw new Exception("There was an error. Client was not created.");
                }

                return Ok(clientId);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        //// PUT api/<ClientController>/5
        [HttpPut]
        public ActionResult Put(Client client)
        {
            try
            {
                _clientService.UpdateClient(client);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        //// DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                return _clientService.DeleteClient(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
