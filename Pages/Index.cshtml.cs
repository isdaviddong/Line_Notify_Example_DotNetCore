using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace notify.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string txb_token { get; set; }

        [BindProperty]
        public string txb_msg { get; set; }

        public string show_msg { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
        }

        public void OnGet()
        {
            //如果從callback導回此頁，應該可以取得token
            var token = Request.Query["token"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                //如果有，則保留於text
                //(注意這不安全，應該要保留在後端，此為範例)
                txb_token = token;
            }
        }

        public void OnPostButtonSend()
        {
            //透過LineNotSDK中的API，傳送
            var ret = isRock.LineNotify.Utility.SendNotify(this.txb_token, this.txb_msg);
            this.show_msg = $"send '{this.txb_msg}'..." + ret.message;
        }
    }
}
