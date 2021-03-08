using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityGuideWebAPI.DataAccess;
using CityGuideWebAPI.Dtos;
using CityGuideWebAPI.Entities;
using Microsoft.AspNetCore.Routing;

namespace CityGuideWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        public ActionResult GetCities()
        {
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }
        [HttpGet]
        [Route("detail")]
        public ActionResult GetCityById(int cityId)
        {
            var city = _appRepository.GetCityById(cityId);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }

        [HttpGet]
        [Route("photos")]
        public ActionResult GetPhotosByCityId(int cityId)
        {
            var photos = _appRepository.GetPhotosByCityId(cityId);
            return Ok(photos);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city);
        }
    }
}
