using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_searchpics : BaseProcedure
	{
		public es_searchpics()
		{
            base.ConnKey = "connStr";
			spName = "es_searchpics";
			Parameters = new SqlParameter[5];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@page",SqlDbType.Int,4);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@size",SqlDbType.Int,4);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[3] = new SqlParameter("@key",SqlDbType.NVarChar,50);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[4] = new SqlParameter("@total",SqlDbType.Int,4);
			Parameters[4].Direction = ParameterDirection.InputOutput;
		}
		public es_searchpics(
			System.Int32 _page, 
			System.Int32 _size, 
			System.String _key
		)
		{
            base.ConnKey = "connStr";
			spName = "es_searchpics";
			Parameters = new SqlParameter[5];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@page",SqlDbType.Int,4);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _page; 
			Parameters[2] = new SqlParameter("@size",SqlDbType.Int,4);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[2].Value = _size; 
			Parameters[3] = new SqlParameter("@key",SqlDbType.NVarChar,50);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[3].Value = _key; 
			Parameters[4] = new SqlParameter("@total",SqlDbType.Int,4);
			Parameters[4].Direction = ParameterDirection.InputOutput;
			
		}
		public Int32 RETURN_VALUE
		{
			set
			{
				Parameters[0].Value = value;
			}
			get
			{
				return Parameters[0].Value.DBTypeToInt32();
			}
		}
		public Int32 page
		{
			set
			{
				Parameters[1].Value = value;
			}
			get
			{
				return Parameters[1].Value.DBTypeToInt32();
			}
		}
		public Int32 size
		{
			set
			{
				Parameters[2].Value = value;
			}
			get
			{
				return Parameters[2].Value.DBTypeToInt32();
			}
		}
		public String key
		{
			set
			{
				Parameters[3].Value = value;
			}
			get
			{
				return Parameters[3].Value.DBTypeToString();
			}
		}
		public Int32 total
		{
			set
			{
				Parameters[4].Value = value;
			}
			get
			{
				return Parameters[4].Value.DBTypeToInt32();
			}
		}
	}
	
}


