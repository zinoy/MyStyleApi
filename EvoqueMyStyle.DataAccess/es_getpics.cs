using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_getpics : BaseProcedure
	{
		public es_getpics()
		{
            base.ConnKey = "connStr";
			spName = "es_getpics";
			Parameters = new SqlParameter[4];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@page",SqlDbType.Int,4);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@size",SqlDbType.Int,4);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[3] = new SqlParameter("@total",SqlDbType.Int,4);
			Parameters[3].Direction = ParameterDirection.InputOutput;
		}
		public es_getpics(
			System.Int32 _page, 
			System.Int32 _size
		)
		{
            base.ConnKey = "connStr";
			spName = "es_getpics";
			Parameters = new SqlParameter[4];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@page",SqlDbType.Int,4);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _page; 
			Parameters[2] = new SqlParameter("@size",SqlDbType.Int,4);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[2].Value = _size; 
			Parameters[3] = new SqlParameter("@total",SqlDbType.Int,4);
			Parameters[3].Direction = ParameterDirection.InputOutput;
			
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
		public Int32 total
		{
			set
			{
				Parameters[3].Value = value;
			}
			get
			{
				return Parameters[3].Value.DBTypeToInt32();
			}
		}
	}
	
}


