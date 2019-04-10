using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HR.Reports
{
    public partial class ReportsHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var mstPage = Master as CSSMaster;
            mstPage.displayDocMode = true;
            mstPage.displayMasterJSLibrary = false;

            
        }
    }
}