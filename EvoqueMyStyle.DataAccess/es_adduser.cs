using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_adduser : BaseProcedure
	{
		public es_adduser()
		{
            base.ConnKey = "connStr";
			spName = "es_adduser";
			Parameters = new SqlParameter[5];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@uid",SqlDbType.UniqueIdentifier,16);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@mail",SqlDbType.VarChar,200);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[3] = new SqlParameter("@pass",SqlDbType.Char,40);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[4] = new SqlParameter("@ip",SqlDbType.VarChar,15);
			Parameters[4].Direction = ParameterDirection.Input;
		}
		public es_adduser(
			System.Guid _uid, 
			System.String _mail, 
			System.String _pass, 
			System.String _ip
		)
		{
            base.ConnKey = "connStr";
			spName = "es_adduser";
			Parameters = new SqlParameter[5];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@uid",SqlDbType.UniqueIdentifier,16);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _uid; 
			Parameters[2] = new SqlParameter("@mail",SqlDbType.VarChar,200);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[2].Value = _mail; 
			Parameters[3] = new SqlParameter("@pass",SqlDbType.Char,40);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[3].Value = _pass; 
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
		public Guid uid
		{
			set
			{
				Parameters[1].Value = value;
			}
			get
			{
				return Parameters[1].Value.DBTypeToGuid();
			}
		}
		public String mail
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
		public String pass
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


