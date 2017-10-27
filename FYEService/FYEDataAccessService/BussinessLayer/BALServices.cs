using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MachineLearning;
using FYEDataAccessService.Models;

namespace FYEDataAccessService
{
	public static class Criteria
	{

		public static string AccessLevel
		{
			get
			{
				return "securityAccessLevelStr";
			}
		}
		public static string CheckLevel
		{
			get
			{
				return "securityAccessLevelStr";
			}
		}
	}

	public class BALServices
	{
		SchoolDataAccess dal = new SchoolDataAccess();
		MachineLearning.BackPropagation bp = new MachineLearning.BackPropagation();
		public BALServices()
		{
			string[] securityAccessLevel = dal.GetHistoricVistorAccess().Select(o => o.classroom + "," + o.playground + "," + o.washroom + "," + o.factor + "," + o.Rating).ToList().ToArray();
			string securityAccessLevelStr = string.Join("\r\n", securityAccessLevel);
			bp.UpdateNeuralNet(securityAccessLevelStr, Criteria.AccessLevel);

			string[] securityCheckLevel = dal.GetHistoricVistorCheckLevel().Select(o => o.id_xerox + "," + o.registry_entry + "," + o.id_submission + "," + o.request_permission).ToList().ToArray();
			string securityCheckLevelStr = string.Join("\r\n", securityCheckLevel);
			bp.UpdateNeuralNet(securityCheckLevelStr, Criteria.CheckLevel);
		}

		public SecurityData SecurityLevel(string schoolId)
		{
			SecurityData sq = new SecurityData();
			
			List<FactorData> accessLevels= dal.GetSchoolAccessLevel(schoolId, new string[] {"2017"});
			List<FactorData> checkLevels = dal.GetSchoolVisitorCheckLevel(schoolId, new string[] {"2017"});
			sq.SchoolId = schoolId;
			if (accessLevels.Count > 0)
			{
				string[] distinct = bp.GetDistinctLevel(Criteria.AccessLevel);
				sq.AccessLevelFactor = bp.EvaluateData(Criteria.AccessLevel, distinct, accessLevels[0].FactorValues);
			}
			if (checkLevels.Count > 0)
			{
				string[] distinct = bp.GetDistinctLevel(Criteria.CheckLevel);
				sq.AccessCheckFactor = bp.EvaluateData(Criteria.CheckLevel, distinct, checkLevels[0].FactorValues);
			}
			return sq;
		}
	}
}
