using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacroDB.Models;
using MacroDB.Request;
using MacroDB.Response;
using MacroDB.Util;
using System.Security.Cryptography;
using System.Text;

namespace MacroDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public AdminController()
        {
        }

        // POST: Admin
        [HttpPost]
        public async Task<ActionResult<AdminPostResponse>> PostAdmin(AdminPostRequest request)
        {
            if(!(await checkToken(request.username, request.token))){
                return BadRequest();
            }

            AdminPostResponse result = new AdminPostResponse();
            using(var db = new NutrientContext())
            {
                result.nutrients = new List<NutrientGetResponse>();
                IEnumerable<NutrientModel> ret;
                if(request.all == true){
                    ret = await db.nutrients
                                    .ToListAsync();
                }else{
                    ret = await db.nutrients
                                    .Where(x => x.approval == true)
                                    .ToListAsync();
                }
                foreach(NutrientModel item in ret){
                    NutrientGetResponse nutrient = new NutrientGetResponse();
                    nutrient.id = item.id;
                    nutrient.name = item.name;
                    nutrient.protein = item.protein;
                    nutrient.lipid = item.lipid;
                    nutrient.carbohydrate = item.carbohydrate;
                    nutrient.calorie = item.calorie;
                    result.nutrients.Add(nutrient);
                }
                result.token = await updateToken(request.username);
            }
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<AdminPutResponse>> PutAdmin(AdminPutRequest request){
            if(!(await checkToken(request.username, request.token))){
                return BadRequest();
            }

            AdminPutResponse result = new AdminPutResponse();
            using(var db = new NutrientContext())
            {
                NutrientModel model = await db.nutrients.FindAsync(request.id);
                if(model == null){
                    return NotFound();
                }
                DateTime dt = DateTime.Now;
                model.approval = true;
                model.approvaled_at = dt;

                db.nutrients.Update(model);
                await db.SaveChangesAsync();

                result.token = await updateToken(request.username);
            }
            return result;
        }

        public async Task<bool> checkToken(string username, string token){
            using(var db = new ManagementContext())
            {
                ManagementModel model = await db.management
                                        .Where(x => x.username == username)
                                        .SingleAsync();
                if(model.token == token){
                    return true;
                }else{
                    return false;
                }
            }
        }

        private async Task<string> updateToken(string username){
            using(var db = new ManagementContext())
            {
                ManagementModel model = await db.management
                                                .Where(x => x.username == username)
                                                .SingleAsync();
                DateTime dt = DateTime.Now;
                if(dt.Subtract((DateTime)(model.token_update)).Minutes > 3){
                    model.token = Token.createToken();
                    model.token_update = DateTime.Now;
                    db.management.Update(model);
                    await db.SaveChangesAsync();
                }
                return model.token;
            }
        }
    }
}
