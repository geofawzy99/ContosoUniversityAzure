using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.DAL.SalesModel.Mvc
{
	public class SalesContextController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
