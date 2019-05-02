using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MacroDB.Models;
using MacroDB.Request;
using MacroDB.Response;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace MacroDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
        }

        // GET: Admin
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            ManagementModel model = null;
            using (var db = new ManagementContext())
            {
                model = await db.management
                    .Where(x => x.username.Contains(request.username))
                    .SingleAsync();

                if(model == null){
                    return NotFound();
                }

                // ハッシュ計算を行う
                string hash = calclateHashValue(request.password);
                if(!hash.Contains(model.hashnum)){
                    return BadRequest();
                }

                // トークンを生成する
                DateTime dt = DateTime.Now;
                model.token = createToken();
                model.token_update = dt;
                model.last_login = dt;
                db.management.Update(model);
                await db.SaveChangesAsync();

                return new LoginResponse(){ token = model.token };
            }
        }

        /**
         * ハッシュを計算する
         */
        private string calclateHashValue(string word){
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(word);
            byte[] bs = md5.ComputeHash(data);
            md5.Clear();
            StringBuilder result = new StringBuilder();
            foreach (byte b in bs)
            {
                result.Append(b.ToString("x2"));
            }
            return result.ToString();
        }

        /**
         * トークンを生成する
         */
        private string createToken()
        {
            byte[] token = new byte[16];
            RNGCryptoServiceProvider gen = new RNGCryptoServiceProvider();
            gen.GetBytes(token);

            StringBuilder buf = new StringBuilder();
            for (int i = 0; i < token.Length; i++)
            {
                buf.AppendFormat("{0:x2}", token[i]);
            }
            return buf.ToString();
        }
    }
}
