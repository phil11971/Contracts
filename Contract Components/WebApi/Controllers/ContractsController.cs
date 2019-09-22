using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contract.Models;
using Contract.Models.DTO;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Contract.Controllers
{
    [Route("api/contracts")]
    [ApiController]
    public class ContractsController : Controller
    {
        private readonly ApplicationContext _context;

        public ContractsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/ContractsWeb
        [HttpGet]
        [ResponseType(typeof(ContractDTO))]
        public IEnumerable<ContractDTO> GetContract()
        {

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Models.Contract, ContractDTO>()
                .ForMember(dto => dto.Stages, opt => opt.MapFrom(x => x.StageContracts.Select(y => y.Stage)));

                cfg.CreateMap<Stage, StageDTO>();

                cfg.CreateMap<ContractDTO, Models.Contract>()
                    .ForMember(
                        opt => opt.StageContracts,
                        dto => dto.MapFrom(x => x.Stages))
                    .AfterMap((model, entity) =>
                    {
                        foreach (var stageContract in entity.StageContracts)
                        {
                            stageContract.Contract = entity;
                        }
                    });

            });

            //var contracts = _context.Contract.Include(c => c.StageContracts).
            //    ThenInclude(sc => sc.Stage).
            //    Select<Models.Contract, ContractDTO>(c =>
            //    {
            //        var stages = c.StageContracts.Select(sc => new StageDTO()
            //        {
            //            StageName = sc.Stage.StageName,
            //            PlanCompletionDate = sc.PlanCompletionDate,
            //            ProjCompletionDate = sc.ProjCompletionDate,
            //            FactCompletionDate = sc.FactCompletionDate
            //        });
            //        new ContractDTO()
            //        {
            //            ContractName = c.ContractName,
            //            PlanCost = c.PlanCost,
            //            FactCost = c.FactCost,
            //            Stages = stages,
            //        };
            //    });

            var map = _context.Contract.ProjectTo<ContractDTO>(config).ToList();

            return map;// contracts.ToList();
        }

        // GET: api/ContractsWeb/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContract([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contract = await _context.Contract.FindAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        // PUT: api/ContractsWeb/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract([FromRoute] int id, [FromBody] Models.Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contract.ContractId)
            {
                return BadRequest();
            }

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContractsWeb
        [HttpPost]
        public async Task<IActionResult> PostContract([FromBody] Models.Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contract.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContract", new { id = contract.ContractId }, contract);
        }

        // DELETE: api/ContractsWeb/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contract = await _context.Contract.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contract.Remove(contract);
            await _context.SaveChangesAsync();

            return Ok(contract);
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.ContractId == id);
        }
    }
}