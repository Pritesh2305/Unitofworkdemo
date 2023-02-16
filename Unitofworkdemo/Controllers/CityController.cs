using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitofworkdemo.Dtos;
using Unitofworkdemo.Interfaces;
using Unitofworkdemo.Model;

namespace Unitofworkdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        //private readonly ICityRepository repo;
        //public CityController(ICityRepository _repo)
        //{
        //    this.repo = _repo;
        //}

        public CityController(IUnitOfWork _uow,IMapper mapper)
        {
            uow = _uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await uow.CityRepository.GetCitiesAsync();
            var citiesDto = mapper.Map <IEnumerable<CityDto>>(cities);

            //var cdto = from c in cities
            //           select new CityDto()
            //           {
            //               cityname = c.cityname,
            //               id = c.id
            //           };

            return Ok(citiesDto);
        }
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto citydto)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var city = mapper.Map<City>(citydto);            

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(Int64 id,CityDto citydto)
        {
            try
            {
                if (id != citydto.id)
                    return BadRequest("Update not Allowed.");

                var cityfromdb = await uow.CityRepository.FindCity(id);

                if (cityfromdb == null)
                    return BadRequest("Update not Allowed.");

                mapper.Map(citydto, cityfromdb);
                await uow.SaveAsync();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message.ToString());
            }
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(Int64 id,JsonPatchDocument<City> citytopatch)
        {
            // JSON PASS THIS WAY   
            //[ 
            //{
            //"operationType": 0,
            //"path": "cityname",
            //"op": "replace",    
            //"value" :"pritesh"
            //}
            //]

            // MULTIPAL JSON PASS THIS WAY   
            //[ 
            //{
            //"operationType": 0,
            //"path": "cityname",
            //"op": "replace",    
            //"value" :"pritesh"
            //},
            //{
            //"operationType": 0,
            //"path": "country",
            //"op": "replace",    
            //"value" :"USA"
            //}
            //]



            var cityfromdb = await uow.CityRepository.FindCity(id);
            citytopatch.ApplyTo(cityfromdb, ModelState);
            //mapper.Map(citydto, cityfromdb);
            await uow.SaveAsync();
            return StatusCode(200);
        }

    }
}
