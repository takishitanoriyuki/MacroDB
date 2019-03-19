using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacroDB.Models;

namespace MacroDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutrientController : ControllerBase
    {
        private readonly NutrientContext _context;

        public NutrientController(NutrientContext context)
        {
            _context = context;

            if(context.nutrients.Count() == 0){

            }
        }

        // GET: Nutrient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutrientModel>>> GetNutrientModels()
        {
            return await _context.nutrients.ToListAsync();
        }

    }
}
