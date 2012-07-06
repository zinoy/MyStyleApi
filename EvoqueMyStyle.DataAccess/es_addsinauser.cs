using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_addsinauser : BaseProcedure
	{
		public es_addsinauser()
		{
            base.ConnKey = "connStr";
			spName = "es_addsinauser";
			Parameters = new SqlParameter[7];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@uid",SqlDbType.BigInt,8);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@name",SqlDbType.NVarChar,100);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[3] = new SqlParameter("@token",SqlDbType.VarChar,200);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[4] = new SqlParameter("@expire",SqlDbType.DateTime,8);
			Parameters[4].Direction = ParameterDirection.Input;
			Parameters[5] = new SqlParameter("@state",SqlDbType.VarChar,100);
			Parameters[5].Direction = ParameterDirection.Input;
			Parameters[6] = new SqlParameter("@follow",SqlDbType.NVarChar,-1);
			Parameters[6].Direction = ParameterDirection.Input;
		}
		public es_addsinauser(
			System.Int64 _uid, 
			System.String _name, 
			System.String _token, 
			System.DateTime _expire, 
			System.String _state, 
			System.String _follow
		)
		{
            base.ConnKey = "connStr";
			spName = "es_addsinauser";
			Parameters = new SqlParameter[7];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@uid",SqlDbType.BigInt,8);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _uid; 
			Parameters[2] = new SqlParameter("@name",SqlDbType.NVarChar,100);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[2].Value = _name; 
			Parameters[3] = new SqlParameter("@token",SqlDbType.VarChar,200);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[3].Value = _token; 
			Parameters[4] = new SqlParameter("@expire",SqlDbType.DateTime,8);
			Parameters[4].Direction = ParameterDirection.Input;
			Parameters[4].Value = _expire; 
			Parameters[5] = new SqlParameter("@state",SqlDbType.VarChar,100);
			Parameters[5].Direction = ParameterDirection.Input;
			Parameters[5].Value = _state; 
			Parameters[6] = new SqlParameter("@follow",SqlDbType.NVarChar,-1);
			Parameters[6].Direction = ParameterDirection.Input;
			Parameters[6].Value = _follow; 
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
		public String name
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
		public String token
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
		public DateTime expire
		{
			set
			{
				Parameters[4].Value = value;
			}
			get
			{
				return Parameters[4].Value.DBTypeToDateTime();
			}
		}
		public String state
		{
			set
			{
				Parameters[5].Value = value;
			}
			get
			{
				return Parameters[5].Value.DBTypeToString();
			}
		}
		public String follow
		{
			set
			{
				Parameters[6].Value = value;
			}
			get
			{
				return Parameters[6].Value.DBTypeToString();
			}
		}
	}
	
}


