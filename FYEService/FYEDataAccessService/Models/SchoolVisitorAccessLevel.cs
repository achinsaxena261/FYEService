using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYEDataAccessService
{
	public class SchoolVisitorAccessLevel
	{
		public decimal school_id { get; set; }
		public string year { get; set; }
		public decimal playground { get; set; }
		public decimal classroom { get; set; }
		public decimal washroom { get; set; }
		public decimal corridor { get; set; }
		public int visitor_access_level_id { get; set; }
		public decimal factor { get; set; }
		public string Rating { get; set; }
	}

	public class SchoolVisitorCheckLevel
	{
		public decimal school_id { get; set; }
		public string year { get; set; }
		public decimal id_xerox { get; set; }
		public decimal id_submission { get; set; }
		public decimal request_permission { get; set; }
		public decimal registry_entry { get; set; }
		public string rating { get; set; }

		public int visitor_check_level_id { get; set; }
	}
}
