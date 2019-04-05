using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
	[RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private IMapper _mapper;
        private ICampRepository _repository;
        public CampsController(ICampRepository repository,
                                IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

		[Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();

                // Mapping
                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);
				if (result == null ) { return NotFound(); }
                var mappedResult = _mapper.Map<CampModel>(result);
                return Ok(mappedResult);
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }


        /*
                public async Task<IHttpActionResult> Action()
                {
                    try
                    {

                    }
                    catch (System.Exception e)
                    {
                        return InternalServerError(e);
                    }
                }

        */
    }
}

