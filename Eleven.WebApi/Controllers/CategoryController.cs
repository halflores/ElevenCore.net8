using Eleven.Service.Modelo.SAP;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eleven.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/<TestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("GetAllCategoriesAsync")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                List<MdoCategory> items = await _categoryService.GetAllCategoriesAsync(); // hanaQuery object
                return Ok(items);
            }
            catch (Exception ex)
            {
                //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        [HttpGet]
        [Route("GetCategoryByIdAsync")]
        public async Task<IActionResult> GetCategoryByIdAsync(string itmsGrpCod)
        {
            try
            {
                List<MdoCategory> items = await _categoryService.GetAllCategoriesAsync(); //
                return Ok(items);
            }
            catch (Exception ex)
            {
                    //LogException.Write(ex);
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }


        }
    }
}
