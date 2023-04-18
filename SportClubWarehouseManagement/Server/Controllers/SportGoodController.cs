using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportClubAPI.Data.Repositories.Interfaces;
using SportClubWMS.Shared;

namespace SportClubAPI.Controllers
{
    [Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class SportGoodController : Controller
    {
        private readonly ISportGoodRepository _SportGoodRepository;


        public SportGoodController(ISportGoodRepository SportGoodRepository)
        {
            _SportGoodRepository = SportGoodRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetAllSportGoodsWithoutCustomers()
        {
            return Ok(_SportGoodRepository.GetAllSportGoods(false));
        }

        // GET: api/<controller>/includecustomers
        [HttpGet("includecustomers")]
        public IActionResult GetAllSportGoodsWithCustomers()
        {
            return Ok(_SportGoodRepository.GetAllSportGoods(true));
        }

        // GET: api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetSportGoodWithoutCustomers(int id)
        {
            var foundSportGood = _SportGoodRepository.GetSportGoodById(id, false);
            if (foundSportGood == null)
                return NotFound();
            else return Ok(foundSportGood);
        }

        // GET: api/<controller>/5/includecustomers
        [HttpGet("{id}/includecustomers")]
        public IActionResult GetSportGoodWithCustomers(int id)
        {
            var foundSportGood = _SportGoodRepository.GetSportGoodById(id, true);
            if (foundSportGood == null)
                return NotFound();
            else return Ok(foundSportGood);
        }

        // GET: api/<controller>/Footballball
        [HttpGet("name({name})")]
        public IActionResult GetSportGoodByNameWithoutCustomers(string name)
        {
            var foundSportGood = _SportGoodRepository.GetSportGoodByName(name, false);
            if (foundSportGood == null)
                return NotFound();
            else return Ok(foundSportGood);
        }

        // GET: api/<controller>/Football ball/includecustomers
        [HttpGet("name({name})/includecustomers")]
        public IActionResult GetSportGoodByNameWithCustomers(string name)
        {
            var foundSportGood = _SportGoodRepository.GetSportGoodByName(name, true);
            if (foundSportGood == null)
                return NotFound();
            else return Ok(foundSportGood);
        }

        // POST: api/<controller>
        [HttpPost]
        public IActionResult CreateSportGood([FromBody] SportGood SportGood)
        {
            if (SportGood == null)
                return BadRequest();
            if (SportGood.Name == string.Empty)
                ModelState.AddModelError("Name", "The name should contain a value.");
            /*if (_SportGoodRepository.ContainsSportGood(SportGood))
                ModelState.AddModelError("Sport Good", "This SportGood already exists in the database");*/
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSportGood = _SportGoodRepository.AddSportGood(SportGood);
            return Created("SportGood", createdSportGood);
        }

        // PUT: api/<controller>
        [HttpPut]
        public IActionResult UpdateSportGood([FromBody] SportGood sportGood)
        {
            if (sportGood == null)
                return BadRequest();

            if (sportGood.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The name should contain a value");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var SportGoodToUpdate = _SportGoodRepository.GetSportGoodById(sportGood.SportGoodId, true);

            if (SportGoodToUpdate == null)
                return NotFound();
            _SportGoodRepository.UpdateSportGood(sportGood);
            return NoContent();
        }
        
        [HttpPut("{id}_quantity={quantity}_{isRemove}")]
        public IActionResult UpdateSportGoodQuantityById(int id, uint quantity, bool isRemove)
        {
            if (id < 1)
                return BadRequest();
            var foundSportGood = _SportGoodRepository.GetSportGoodById(id, false);
            if (foundSportGood == null)
                return NotFound();

            if (isRemove)
            {
                var sportGoodQuantity = foundSportGood.Quantity;
                if (quantity > sportGoodQuantity)
                {
                    ModelState.AddModelError("Quantity", $"You can't remove quantity ({quantity}) that is bigger than total quantity({sportGoodQuantity})");
                    return BadRequest(ModelState);
                }

                _SportGoodRepository.UpdateQuantitySportGood(id, quantity, Operands.SUB);
                return NoContent();
            }
            _SportGoodRepository.UpdateQuantitySportGood(id, quantity, Operands.SUM);
            return NoContent();
        }

        // DELETE: api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSportGood(int id)
        {
            if (id == 0)
                return BadRequest();
            var SportGoodToDelete = _SportGoodRepository.GetSportGoodById(id, false);
            if (SportGoodToDelete == null)
                return NotFound();

            _SportGoodRepository.DeleteSportGood(id);
            return NoContent();
        }
    }
}
