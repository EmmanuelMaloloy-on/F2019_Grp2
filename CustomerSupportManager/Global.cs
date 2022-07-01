using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerSupportManager
{
    public static class Global
    {
        enum loginTypes
        {
            None,
            Admin,
            Customer
        }
        static int loggedInType = 0;
    }
}