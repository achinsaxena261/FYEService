using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FYEDataAccessService
{
	public class BaseDataAccess
	{
		SqlConnection con = new SqlConnection(@"Data Source = V-SHYASH-DT2;" +
			  "Initial Catalog=FYEDB;" + 
              "User id=sa;" + 
              "Password=admin@123");
		SqlDataAdapter adp = new SqlDataAdapter();
		DataSet ds = new DataSet();

		public DataSet ExecuteDS(string query)
		{
			ds = new DataSet();
			SqlDataAdapter adp = new SqlDataAdapter(query, con);
			adp.Fill(ds);
			return ds;
		}
	}
	public class SchoolDataAccess:BaseDataAccess
    {
		public List<SchoolVisitorAccessLevel> GetHistoricVistorAccess()
		{
			DataSet ds = ExecuteDS("select * from SchoolVisitorAccessLevel");
			List<SchoolVisitorAccessLevel> dt = (from dr in ds.Tables[0].AsEnumerable()
					  select new SchoolVisitorAccessLevel
					  {
						  visitor_access_level_id = dr.Field<int>("visitor_access_level_id"),
						  year = dr.Field<string>("year"),
						  playground = dr.Field<decimal>("playground"),
						  factor = dr.Field<decimal>("factor"),
						  classroom = dr.Field<decimal>("classroom"),
						  washroom = dr.Field<decimal>("washroom"),
						  Rating = dr.Field<string>("Rating")
					  }).ToList();

			return dt;
		}

		public List<SchoolVisitorCheckLevel> GetHistoricVistorCheckLevel()
		{
			DataSet ds = ExecuteDS("select * from VisitorCheckLevel");
			List<SchoolVisitorCheckLevel> dt = (from dr in ds.Tables[0].AsEnumerable()
												 select new SchoolVisitorCheckLevel
												 {
													 visitor_check_level_id = dr.Field<int>("visitor_check_level_id"),
													 year = dr.Field<string>("year"),
													 id_xerox = dr.Field<decimal>("id_xerox"),
													 registry_entry = dr.Field<decimal>("registry_entry"),
													 id_submission = dr.Field<decimal>("id_submission"),
													 request_permission = dr.Field<decimal>("request_permission"),
													 rating = dr.Field<string>("rating")
												 }).ToList();

			return dt;
		}

		public List<Models.FactorData> GetSchoolAccessLevel(string schoolId, string[] years = null)
		{
			string query = "";
			if (years != null)
			{
				query = " and (year = '" + years[0] + "' ";
				if (years.Length > 1)
				{
					for (int i = 1; i < years.Length; i++)
					{
						query += " OR year = '" + years[i] + "' ";
					}
				}
				query += ")";
			}

			DataSet ds = new DataSet();

			ds = ExecuteDS("select * from SchoolVisitorAccessLevelPredict where school_id='" +schoolId+ "'" + query);
			List<Models.FactorData> dt = (from dr in ds.Tables[0].AsEnumerable()
												 select new  Models.FactorData
												 {
													 FactorName = "VisitorAccessLevel",
													 FactorValues = new double[]{dr.Field<double>("playground"), dr.Field<double>("washroom"), dr.Field<double>("classroom"), dr.Field<double>("factor") }
												 }).ToList();
			return dt;
		}

		public List<Models.FactorData> GetSchoolVisitorCheckLevel(string schoolId, string[] years = null)
		{
			string query = "";
			if (years != null)
			{
				query = " and (year = '" + years[0] + "' ";
				if (years.Length > 1)
				{
					for (int i = 1; i < years.Length; i++)
					{
						query += " OR year = '" + years[i] + "' ";
					}
				}
				query += ")";
			}

			DataSet ds = new DataSet();

			ds = ExecuteDS("select * from VisitorCheckLevelPredict where school_id='" + schoolId + "'" + query);
			List<Models.FactorData> dt = (from dr in ds.Tables[0].AsEnumerable()
										  select new Models.FactorData
										  {
											  FactorName = "VisitorCheckLevel",
											  FactorValues = new double[] { dr.Field<double>("id_xerox"), dr.Field<double>("registry_entry"), dr.Field<double>("id_submission"), dr.Field<double>("request_permission") }
										  }).ToList();
			return dt;
		}
	}
}
