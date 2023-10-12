using Azure;
using BharatMedicsV2.Models;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BharatMedicsV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DrugController : Controller
    {
        private readonly IDrug _drugRepository;
        protected APIs reply;

        public DrugController(IDrug drugRepository)
        {
            _drugRepository = drugRepository;
            reply = new();
            
        }

        #region Add Drugs
        [HttpPost("Add Drug")]
        public async Task<ActionResult> AddDrugs([FromBody] Drug drug)
        {
            try
            {
                if(drug == null)
                {
                    return BadRequest();
                }

                if (drug.DrugId > 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                await _drugRepository.CreateAsync(drug);
                await _drugRepository.SaveAsync();

                reply.Response = drug;
                reply.StatusCode = HttpStatusCode.Created;

                return Ok();
                return CreatedAtRoute("GetDrug", new { id = drug.DrugId });
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>();
            }

            return Ok();
        }
        #endregion

        #region Get All Drugs
        [HttpGet("Get All Drugs")]
        public async Task<ActionResult<List<Drug>>> GetAllDrugs()
        {
            try
            {
                reply.Response = await _drugRepository.GetAllAsync();
                reply.StatusCode = HttpStatusCode.OK;
                return Ok(reply.Response);
            }
            catch(Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>();
                return BadRequest();
            }

            return Ok(reply.Response);
        }
        #endregion

        #region Get Particular Drug
        [HttpGet("{DrugId}", Name = "GetDrugs")]
        public async Task<ActionResult<APIs>> GetDrugs(int DrugId)
        {
            try
            {
                if (DrugId == 0)
                {
                    
                    return BadRequest();
                }
                var temp = await _drugRepository.GetAsync(u => u.DrugId == DrugId);
                if (temp == null)
                {
                    
                    return NotFound();
                }
                reply.Response = await _drugRepository.GetAsync(u => u.DrugId == DrugId);
                reply.StatusCode = HttpStatusCode.OK;
                return Ok(reply.Response);
            }
            catch (Exception ex)
            {
                reply.IsSuccess=false;
                reply.ErrorMessages = new List<string>();
            }

            return reply;
        }
        #endregion

        #region Delete Specific Drug
        [HttpDelete("{DrugId}", Name = "DeleteDrug")] // This method deletes existince drug from database
        public async Task<ActionResult<APIs>> DeleteDrug(int DrugId)
        {

            try
            {
                if (DrugId == null)
                {
                    return BadRequest();
                }

                var drugggs = await _drugRepository.GetAsync(u => u.DrugId == DrugId);

                if (drugggs == null)
                {
                    return BadRequest();
                }

                
                await _drugRepository.DeleteAsync(drugggs);
                await _drugRepository.SaveAsync();

                reply.StatusCode = HttpStatusCode.NoContent;
                reply.IsSuccess = true;
                return Ok(reply);
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return reply;
        }
        #endregion

        #region Update Drugs
        [HttpPut("{DrugId}", Name = "UpdateDrugs")]
        public async Task<ActionResult> Update(int DrugId, Drug drug)
        {

            try
            {

                if (DrugId == null || DrugId == 0 || DrugId != drug.DrugId) return BadRequest();
                var druggggs = await _drugRepository.GetAsync(u => u.DrugId == DrugId);

                
                druggggs.Price = drug.Price;
                druggggs.DrugName = drug.DrugName;
                druggggs.Description = drug.Description;
                druggggs.ExpiryDate = drug.ExpiryDate;


                await _drugRepository.UpdateDrugsAsync(druggggs);
                await _drugRepository.SaveAsync();

                reply.StatusCode = HttpStatusCode.NoContent;
                reply.IsSuccess = true;
                return Ok(reply.Response);
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return BadRequest(reply.Response);
        }

        #endregion

    }
}
