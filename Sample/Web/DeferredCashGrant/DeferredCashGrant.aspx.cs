using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class DeferredCashGrant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var mstPage = Master as CSSMaster;
            mstPage.displayDocMode = true;
            mstPage.displayMasterJSLibrary = false;
        }
    }
}