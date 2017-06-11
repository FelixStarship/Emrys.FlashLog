using System.Web;
using System.Web.Mvc;
using log4netTest.Filter;

namespace log4netTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyErrorAttribute());
        }
    }
}
