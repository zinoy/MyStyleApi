using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_addtd : BaseProcedure
	{
		public es_addtd()
		{
            base.ConnKey = "connStr";
			spName = "es_addtd";
			Parameters = new SqlParameter[5];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@name",SqlDbType.NVarChar,50);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@email",SqlDbType.VarChar,200);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[3] = new SqlParameter("@mobile",SqlDbType.VarChar,20);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[4] = new SqlParameter("@ip",SqlDbType.VarChar,15);
			Parameters[4].Direction = ParameterDirection.Input;
		}
		public es_addtd(
			System.String _name, 
			System.String _email, 
			System.String _mobile, 
			System.String _ip
		)
		{
            base.ConnKey = "connStr";
			spName = "es_addtd";
			Parameters = new SqlParameter[5];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@name",SqlDbType.NVarChar,50);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _name; 
			Parameters[2] = new SqlParameter("@email",SqlDbType.VarChar,200);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[2].Value = _email; 
			Parameters[3] = new SqlParameter("@mobile",SqlDbType.VarChar,20);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[3].Value = _mobile; 
			Parameters[4] = new SqlParameter("@ip",SqlDbType.VarChar,15);
			Parameters[4].Direction = ParameterDirection.Input;
			Parameters[4].Value = _ip; 
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
		public String name
		{
			set
			{
				Parameters[1].Value = value;
			}
			get
			{
				return Parameters[1].Value.DBTypeToString();
			}
		}
		public String email
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
		public String mobile
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
		public String ip
		{
			set
			{
				Parameters[4].Value = value;
			}
			get
			{
				return Parameters[4].Value.DBTypeToString();
			}
		}
	}
	
}


