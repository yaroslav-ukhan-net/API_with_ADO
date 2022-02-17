using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeTaskController : ControllerBase
    {
        private readonly HomeTaskService homeTaskService;

        public HomeTaskController(HomeTaskService homeTaskService)
        {
            this.homeTaskService = homeTaskService;
        }


        // GET: api/<HomeTaskController>
        [HttpGet]
        public ActionResult<IEnumerable<HomeTaskDto>> Get()
        {
            return Ok(homeTaskService.GetAllHomeTasks().Select(home=>HomeTaskDto.FromModel(home)));
        }

        // GET api/<HomeTaskController>/5
        [HttpGet("{id}")]
        public ActionResult<HomeTaskDto> Get(int id)
        {
            var hometask = homeTaskService.GetHomeTaskById(id);
            if(hometask == null)
            {
                return NotFound();
            }
            return Ok(HomeTaskDto.FromModel(hometask));
        }

        // POST api/<HomeTaskController>
        [HttpPost]
        public ActionResult Post([FromBody] HomeTaskDto value)
        {
            var result = homeTaskService.CreateHomeTask(value.ToModel());
            if (result.HasErrors)
            {
                return BadRequest(result.Errors);
            }
            return Accepted();

        }

        // PUT api/<HomeTaskController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] HomeTaskDto value)
        {
            var result = homeTaskService.UpdateHomeTask(value.ToModel());
            if (result.HasErrors)
            {
                return BadRequest(result.Errors);
            }
            return Accepted();
        }

        // DELETE api/<HomeTaskController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            homeTaskService.DeleteHomeTask(id);
            return Accepted();
        }
    }
}
