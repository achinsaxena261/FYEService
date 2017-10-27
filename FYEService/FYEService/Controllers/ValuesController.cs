using FYEService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FYEDataAccessService;
using FYEDataAccessService.Models;

namespace FYEService.Controllers
{
	public class ValuesController : ApiController
	{
		BALServices bal = new FYEDataAccessService.BALServices();

		public ValuesController()
		{

		}
		//// GET api/values
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		//// GET api/values/5
		//public string Get(int id)
		//{
		//	return "value";
		//}

		//// POST api/values
		//public void Post([FromBody]string value)
		//{
		//}

		//// PUT api/values/5
		//public void Put(int id, [FromBody]string value)
		//{
		//}

		// DELETE api/values/5
		[HttpGet]
		public SecurityData  GetSecurity(string id)
		{
			return bal.SecurityLevel(id);
		}

		[HttpGet]
		public IEnumerable<School> GetSchools()
		{
			List<School> schools = new List<School>();
			schools.Add(new School() { Id = 1, Name = "TEST1" });
			schools.Add(new School() { Id = 2, Name = "TEST2" });
			return schools;
		}
	}
}
