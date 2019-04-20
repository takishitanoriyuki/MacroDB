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
    public class AdminController : ControllerBase
    {
        private readonly NutrientContext _context;

        public AdminController(NutrientContext context)
        {
            _context = context;
        }

        // GET: Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nutrient>>> GetAdmin()
        {
            List<Nutrient> result = new List<Nutrient>();
            IEnumerable<NutrientModel> ret = await _context.nutrients.ToListAsync();
            foreach(NutrientModel item in ret){
                if(item.approval == false){
                    Nutrient nutrient = new Nutrient();
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

        private const string ALL = "all";
        [HttpGet("{all}")]
        public async Task<ActionResult<IEnumerable<Nutrient>>> GetAdmin(string all)
        {
            List<Nutrient> result = new List<Nutrient>();
            if(all == ALL){
                IEnumerable<NutrientModel> ret = await _context.nutrients.ToListAsync();
                foreach(NutrientModel item in ret){
                    Nutrient nutrient = new Nutrient();
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
    }
}
