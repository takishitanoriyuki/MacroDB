using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacroDB.Models;
using MacroDB.Response;
using Microsoft.AspNetCore.Http;

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
        public async Task<ActionResult<IEnumerable<Nutrient>>> GetNutrient()
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

        [HttpPost]
        public async Task<ActionResult> PostNutrient(Nutrient item){
            NutrientModel model = new NutrientModel();
            DateTime dt = DateTime.Now;
            model.name = item.name;
            model.protein = item.protein;
            model.lipid = item.lipid;
            model.carbohydrate = item.carbohydrate;
            model.calorie = item.calorie;
            model.created_at = dt;
            model.updated_at = dt;

            _context.nutrients.Add(model);
            await _context.SaveChangesAsync();
            return Created(nameof(PostNutrient), new object());
        }
    }
}
