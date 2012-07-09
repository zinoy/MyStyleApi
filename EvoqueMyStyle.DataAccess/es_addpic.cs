using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_addpic : BaseProcedure
	{
		public es_addpic()
		{
            base.ConnKey = "connStr";
			spName = "es_addpic";
			Parameters = new SqlParameter[6];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			Parameters[1] = new SqlParameter("@uid",SqlDbType.NVarChar,50);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[2] = new SqlParameter("@type",SqlDbType.NVarChar,50);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[3] = new SqlParameter("@comment",SqlDbType.NVarChar,200);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[4] = new SqlParameter("@img",SqlDbType.NVarChar,200);
			Parameters[4].Direction = ParameterDirection.Input;
			Parameters[5] = new SqlParameter("@mobile",SqlDbType.NVarChar,50);
			Parameters[5].Direction = ParameterDirection.Input;
		}
		public es_addpic(
			System.String _uid, 
			System.String _type, 
			System.String _comment, 
			System.String _img, 
			System.String _mobile
		)
		{
            base.ConnKey = "connStr";
			spName = "es_addpic";
			Parameters = new SqlParameter[6];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
			
			Parameters[1] = new SqlParameter("@uid",SqlDbType.NVarChar,50);
			Parameters[1].Direction = ParameterDirection.Input;
			Parameters[1].Value = _uid; 
			Parameters[2] = new SqlParameter("@type",SqlDbType.NVarChar,50);
			Parameters[2].Direction = ParameterDirection.Input;
			Parameters[2].Value = _type; 
			Parameters[3] = new SqlParameter("@comment",SqlDbType.NVarChar,200);
			Parameters[3].Direction = ParameterDirection.Input;
			Parameters[3].Value = _comment; 
			Parameters[4] = new SqlParameter("@img",SqlDbType.NVarChar,200);
			Parameters[4].Direction = ParameterDirection.Input;
			Parameters[4].Value = _img; 
			Parameters[5] = new SqlParameter("@mobile",SqlDbType.NVarChar,50);
			Parameters[5].Direction = ParameterDirection.Input;
			Parameters[5].Value = _mobile; 
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
		public String uid
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
		public String type
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
		public String comment
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
		public String img
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
		public String mobile
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
	}
	
}


