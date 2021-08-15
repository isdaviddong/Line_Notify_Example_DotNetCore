using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace notify.Pages
{
    public class callbackModel : PageModel
    {
        public void OnGet()
        {
            //取得返回的code
            var code = Request.Query["code"].ToString();
            if (code == null)
            {
                Write("沒有正確回應code");
                return;
            }
            //顯示，測試用
            //Response.Write("<br/> code : " + code);
        
            //從Code取回toke
            var token = isRock.LineNotify.Utility.GetTokenFromCode(code,
                "_________________",  //TODO:請更正為你自己的 client_id
                "__________________________________", //TODO:請更正為你自己的 client_secret
                "https://localhost:5001/Callback");  //TODO:要將此 redirect url 填回你的 LineNotify 後台設定
            //顯示，測試用
            //Write("<br/> token : " + token.access_token);

            //利用token發個測試訊息
            isRock.LineNotify.Utility.SendNotify(token.access_token, "msg test - " + System.DateTime.Now.ToString());
            //導入首頁，帶入token
            //(注意這是範例，token不該用明碼傳遞，也不該出現在用戶端，你應該自行記錄在資料庫或ServerSite session中)
            Response.Redirect("/index?token=" + token.access_token);
        }

        private void Write(string msg)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(msg);
            HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
