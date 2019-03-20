using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacroDB.Models;
using MacroDB.Response;

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
        public async Task<ActionResult<IEnumerable<Nutrient>>> GetNutrientModels()
        {
            List<Nutrient> result = new List<Nutrient>();
            IEnumerable<NutrientModel> ret = await _context.nutrients.ToListAsync();
            foreach(NutrientModel item in ret){
                Nutrient nutrient = new Nutrient();
                nutrient.name = item.name;
                nutrient.protein = item.protein;
                nutrient.lipid = item.lipid;
                nutrient.carbohydrate = item.carbohydrate;
                nutrient.calorie = item.calorie;
                result.Add(nutrient);
            }
            return result;
        }

    }
}
