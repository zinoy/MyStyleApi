using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_getfriends : BaseProcedure
	{
		public es_getfriends()
		{
            base.ConnKey = "connStr";
			spName = "es_getfriends";
			Parameters = new SqlParameter[3];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@uid",SqlDbType.BigInt,8);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@friends",SqlDbType.NVarChar,-1);
			Parameters[2].Direction = ParameterDirection.InputOutput;
		}
		public es_getfriends(
			System.Int64 _uid
		)
		{
            base.ConnKey = "connStr";
			spName = "es_getfriends";
			Parameters = new SqlParameter[3];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@uid",SqlDbType.BigInt,8);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _uid; 
			Parameters[2] = new SqlParameter("@friends",SqlDbType.NVarChar,-1);
			Parameters[2].Direction = ParameterDirection.InputOutput;
			
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
		public Int64 uid
		{
			set
			{
				Parameters[1].Value = value;
			}
			get
			{
				return Parameters[1].Value.DBTypeToInt64();
			}
		}
		public String friends
		{
			set
			{
				Parameters[2].Value = value;
			}
			get
			{
				return Parameters[2].Value.DBTypeToString();
			}
		}
	}
	
}


