using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYEDataAccessService.Models
{
	public class FactorData
	{
		public string FactorName { get; set; }
		public string year { get; set; }
		public double[] FactorValues { get; set; }

		public FactorData()
		{
			FactorValues = new double[4];
		}
	}
}
