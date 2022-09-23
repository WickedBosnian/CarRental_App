using CarRental_Application.Repositories;
using CarRental_Domain.Entities;
using CarRental_DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarRental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientRepository _clientRepository;
        public ClientController(IClientRepository clientService)
        {
            _clientRepository = clientService;
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public ActionResult<ClientDTO> Get(int id)
        {
            try
            {
                return Ok(_clientRepository.GetClientById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        [HttpGet("SearchClients")]
        public ActionResult<List<ClientDTO>> SearchClients(DateTime? birthdate, string? firstname, string? lastname, string? driverLicenceNumber, string? personalIdCardNumber, string? gender)
        {
            try
            {
                ClientDTO client = new ClientDTO()
                {
                    Birthdate = birthdate,
                    Firstname = firstname,
                    Lastname = lastname,
                    DriverLicenceNumber = driverLicenceNumber,
                    PersonalIdcardNumber = personalIdCardNumber,
                    Gender = gender
                };

                return Ok(_clientRepository.SearchClients(client, 1, 10));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<ClientDTO>> GetAll()
        {
            try
            {
                return Ok(_clientRepository.GetAllClients());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }

        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<int> PostClient(ClientDTO client)
        {
            try
            {
                int clientId = _clientRepository.CreateClient(client);
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
        public ActionResult Put(ClientDTO client)
        {
            try
            {
                _clientRepository.UpdateClient(client);
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
                return _clientRepository.DeleteClient(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ";" + ex.InnerException?.Message);
            }
        }
    }
}
