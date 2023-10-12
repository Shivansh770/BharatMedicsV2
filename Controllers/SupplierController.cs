using AutoMapper;
using Azure;
using BharatMedicsV2.Models;
using BharatMedicsV2.Models.DTOs;
using BharatMedicsV2.Repository;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BharatMedicsV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly ISupplier supplierRepository;
        protected APIs reply;
        private readonly IMapper mapper;

        public SupplierController(ISupplier _supplierRepository,  IMapper mapper)
        {
            supplierRepository = _supplierRepository;
            reply = new();
            this.mapper = mapper;
        }

        #region Add New Supplier
        [HttpPost("Add Supplier")] // This method inserts new supplier in database
        public async Task<ActionResult> AddSupplier([FromBody] Supplier supplier)
        {

            try
            {
                if (supplier == null)
                {
                    return BadRequest();
                }

                if(supplier.SupplierId > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

               

                await supplierRepository.CreateAsync(supplier);
                await supplierRepository.SaveAsync();

                reply.Response = supplier;
                reply.StatusCode = HttpStatusCode.Created;
                return Ok();
                // return CreatedAtRoute("GetDrug", new { id = drug.Id }, responses);
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Ok();


        }
        #endregion

        #region Get All Suppliers


        [HttpGet("Get All Suppliers")] 
        public async Task<ActionResult<List<Supplier>>> GetAllSupplier()
        {

            try
            {

                
                var supp = supplierRepository.GetAllSuppliers();


                reply.StatusCode = HttpStatusCode.OK;
                return Ok(supp.Result);

            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Ok(reply.Response);

            #endregion

        }


        #region Get Particular Supplier

        [HttpGet("{Id}", Name = "GetSuppliers")]
        public async Task<ActionResult<APIs>> GetSupplier(int Id)
        {


            try
            {

                if (Id == 0)
                {

                    return BadRequest();
                }
                var temp = await supplierRepository.GetAsync(u => u.SupplierId == Id);
                if (temp == null)
                {

                    return NotFound();
                }



                reply.Response = await supplierRepository.GetAsync(u => u.SupplierId == Id);
                reply.StatusCode = HttpStatusCode.OK;
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

        #region Update Suppliers
        [HttpPut("{Id}", Name = "UpdateSupplier")] // This method updates the drug
        public async Task<ActionResult> UpdateSupplier(int Id, SupplierDTO supplier)
        {

            try
            {

                if (Id == null || Id == 0 || Id != supplier.SupplierId) return BadRequest();
                var druggs = await supplierRepository.GetAsync(u => u.SupplierId == Id, tracked: false);


                druggs = mapper.Map<Supplier>(supplier);


                
                await supplierRepository.UpdateSupplierAsync(druggs);
                await supplierRepository.SaveAsync();

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

        #region Delete Supplier
        [HttpDelete("{Id}", Name = "DeleteSupplier")] // This method deletes existince drug from database
        public async Task<ActionResult<APIs>> DeleteSupplier(int Id)
        {

            try
            {
                if (Id == null)
                {
                    return BadRequest();
                }

                var drz = await supplierRepository.GetAsync(u => u.SupplierId== Id);

                if (drz == null)
                {
                    return BadRequest();
                }

                
                await supplierRepository.DeleteAsync(drz);
                await supplierRepository.SaveAsync();

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


    }
}
