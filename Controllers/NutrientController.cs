using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MacroDB.Models;
using MacroDB.Request;
using MacroDB.Response;

namespace MacroDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutrientController : ControllerBase
    {
        public NutrientController()
        {
        }

        // GET: Nutrient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutrientGetResponse>>> GetNutrient()
        {
            List<NutrientGetResponse> result = new List<NutrientGetResponse>();
            using(var db = new NutrientContext())
            {
                IEnumerable<NutrientModel> ret = await db.nutrients
                                                        .Where(x => x.approval == true)
                                                        .ToListAsync();
                foreach(NutrientModel item in ret){
                    NutrientGetResponse nutrient = new NutrientGetResponse();
                    nutrient.id = item.id;
                    nutrient.name = item.name;
                    nutrient.protein = item.protein;
                    nutrient.lipid = item.lipid;
                    nutrient.carbohydrate = item.carbohydrate;
                    nutrient.calorie = item.calorie;
                    result.Add(nutrient);
                }
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> PostNutrient(NutrientPostRequest item){
            using(var db = new NutrientContext())
            {
                NutrientModel model = new NutrientModel();
                DateTime dt = DateTime.Now;
                model.name = item.name;
                model.protein = item.protein;
                model.lipid = item.lipid;
                model.carbohydrate = item.carbohydrate;
                model.calorie = item.calorie;
                model.created_at = dt;
                model.updated_at = dt;

                db.nutrients.Add(model);
                await db.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
