using Microsoft.AspNetCore.Mvc;
using Model.Infra.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities = Model.Domain.Entities;

namespace Model.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public IRepositoryTask Repository { get; }

        public TasksController(IRepositoryTask repository)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }
        [HttpGet]
        public IAsyncEnumerable<Entities.Task> Get()
        {
            return Repository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await Repository.GetAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                return Ok(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entities.Task task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Repository.AddAsync(task);
                    return CreatedAtAction("Get", new { id = task.Id }, task);
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Entities.Task task)
        {
            try
            {
                if (ModelState.IsValid && id == task.Id)
                {
                    if (await Repository.EditAsync(task))
                    {
                        return Ok(task);
                    }
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var task = await Repository.GetAsync(id);
                if (task != null)
                {
                    if (await Repository.RemoveAsync(task))
                    {
                        return Ok(task);
                    }
                }
                return NotFound();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
