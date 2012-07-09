using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_checkuser : BaseProcedure
	{
		public es_checkuser()
		{
            base.ConnKey = "connStr";
			spName = "es_checkuser";
			Parameters = new SqlParameter[4];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@mail",SqlDbType.VarChar,200);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@pass",SqlDbType.Char,40);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[3] = new SqlParameter("@uid",SqlDbType.UniqueIdentifier,16);
			Parameters[3].Direction = ParameterDirection.InputOutput;
		}
		public es_checkuser(
			System.String _mail, 
			System.String _pass
		)
		{
            base.ConnKey = "connStr";
			spName = "es_checkuser";
			Parameters = new SqlParameter[4];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@mail",SqlDbType.VarChar,200);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _mail; 
			Parameters[2] = new SqlParameter("@pass",SqlDbType.Char,40);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[2].Value = _pass; 
			Parameters[3] = new SqlParameter("@uid",SqlDbType.UniqueIdentifier,16);
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
		public String mail
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
		public String pass
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
		public Guid uid
		{
			set
			{
				Parameters[3].Value = value;
			}
			get
			{
				return Parameters[3].Value.DBTypeToGuid();
			}
		}
	}
	
}


