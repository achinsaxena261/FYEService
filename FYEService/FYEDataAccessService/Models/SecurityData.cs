using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYEDataAccessService.Models
{
	public class SecurityData
	{
		public string SchoolId { get; set; }
		public string AccessLevelFactor { get; set; }
		public string AccessCheckFactor { get; set; }
		public string CrimeFactor { get; set; }
		public string BVFactor { get; set; }
		public string OverAllFactor { get; set; }
	}
}
