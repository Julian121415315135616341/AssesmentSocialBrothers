using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;

namespace controller
{
    [ApiController]
    [Route("[controller]")]
    public class AddresController : ControllerBase
    {
        private readonly AddresService addressService;

        public AddresController(AddresService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAddresses([FromQuery] string search = null, [FromQuery] string sortField = null, [FromQuery] string sortOrder = null)
        {
            var addresses = addressService.getAddresses();

            if (!string.IsNullOrEmpty(search))
            {
                addresses = addresses.Where(a =>
                    a.Street.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.City.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.Code.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.Country.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                var prop = typeof(AddresDTO).GetProperty(sortField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop != null)
                {
                    addresses = sortOrder.ToLower() == "asc"
                        ? addresses.OrderBy(a => prop.GetValue(a, null)).ToList()
                        : addresses.OrderByDescending(a => prop.GetValue(a, null)).ToList();
                }
            }

            return new JsonResult(addresses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAddressById(int id)
        {
            var address = addressService.getAddres(id);
            if (address != null)
            {
                return new JsonResult(address);
            }
            else
            {
                return NotFound($"Address with ID {id} not found.");
            }
        }
        

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(AddresDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ChangeAddress(int id, [FromBody] NewAddressDTO newAddress)
        {
            if (newAddress == null || newAddress.City == null || newAddress.Code == null || newAddress.Country == null || newAddress.Street == null || newAddress.Number == null || newAddress.Number == 0)
            {
                return BadRequest("Invalid data provided");
            }

            AddresDTO addressDTO = addressService.changeAddres(newAddress, id);
            if (addressDTO != null)
            {
                return new JsonResult(addressDTO);
            }
            else
            {
                return NotFound($"Address with ID {id} not found.");
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AddresDTO))]
        [ProducesResponseType(400)]        
        public IActionResult CreateAddress([FromBody] NewAddressDTO newAddress)
        {
            if (newAddress == null || newAddress.City == null || newAddress.Code == null || newAddress.Country == null || newAddress.Street == null || newAddress.Number == null || newAddress.Number == 0)
            {
                return BadRequest("Invalid data provided");
            }

            var createdAddress = addressService.CreateAddres(newAddress.Street, newAddress.Number, newAddress.Code, newAddress.City, newAddress.Country);
            return new JsonResult(createdAddress);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAddress(int id)
        {
            if (addressService.deleteAddres(id))
            {
                return new JsonResult("Address successfully deleted");
            }
            else
            {
                return NotFound($"Address with ID {id} not found.");
            }
        }
        /// <summary>
        /// Determine distance between two different addresses using their IDs.
        /// </summary>
        [HttpGet("{idA}/{idB}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDistanceBetweenAddresses(int idA, int idB)
        {
            var distance =  addressService.calculateDistanceBetweenAddresses(idA, idB);
            if (distance != null)
            {
                return new JsonResult($"Distance: {distance} km");
            }
            else
            {
                return BadRequest("Invalid Addresses");
            }
        }
    }
}