using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_getrandpic : BaseProcedure
	{
		public es_getrandpic()
		{
            base.ConnKey = "connStr";
			spName = "es_getrandpic";
			Parameters = new SqlParameter[2];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@count",SqlDbType.Int,4);
			Parameters[1].Direction = ParameterDirection.Input;
		}
		public es_getrandpic(
			System.Int32 _count
		)
		{
            base.ConnKey = "connStr";
			spName = "es_getrandpic";
			Parameters = new SqlParameter[2];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@count",SqlDbType.Int,4);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _count; 
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
		public Int32 count
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
	}
	
}


