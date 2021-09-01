using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyJetWallet.Domain;
using MyJetWallet.Sdk.Authorization;
using MyJetWallet.Sdk.Authorization.Http;
using MyJetWallet.Sdk.WalletApi.Common;
using Newtonsoft.Json;
using Service.WalletApi.PingApi.Controllers.Contracts;
using SimpleTrading.ClientApi.Utils;

namespace Service.WalletApi.PingApi.Controllers
{
    [ApiController]
    [Route("/api/v1/PingApi/Debug")]
    public class DebugController: ControllerBase
    {
        /// <summary>
        /// Parse token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public IActionResult ParseToken([FromBody] TokenDto request)
        {
            var (res, data) = MyControllerBaseHelper.ParseToken(request.Token);

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            return Ok($"Result: {res}.\nData:\n{json}");
        }

        [HttpGet("hello")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello world!");
        }


        [HttpPost("make-signature")]
        public IActionResult MakeSignatureAsync([FromBody] TokenDto data, [FromHeader(Name = "private-key")] string key)
        {
            return Ok();
        }

        [HttpPost("generate-keys")]
        public IActionResult GenerateKeysAsync()
        {
            var rsa = RSA.Create();

            var publicKey = rsa.ExportRSAPublicKey();
            var privateKey = rsa.ExportRSAPrivateKey();

            var response = new
            {
                PrivateKeyBase64 = Convert.ToBase64String(privateKey),
                PublicKeyBase64 = Convert.ToBase64String(publicKey)
            };

            return Ok(response);
        }

        [HttpGet("my-ip")]
        public IActionResult GetMyApiAsync()
        {
            var ip = this.HttpContext.GetIp();
            
            var xff = HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var xffheader) ? xffheader.ToString() : "none";
            var cf = HttpContext.Request.Headers.TryGetValue("CF-Connecting-IP", out var cfheader) ? cfheader.ToString() : "none";
            
            return Ok(new {IP = ip, XFF = xff, CF = cf});
        }
    }
}
