using System;
using System.Data.SqlClient;
using System.Data;
using Bean.DataAccess;


namespace EvoqueMyStyle.DataAccess
{
	public class es_getcount : BaseProcedure
	{
		public es_getcount()
		{
            base.ConnKey = "connStr";
			spName = "es_getcount";
			Parameters = new SqlParameter[1];
			Parameters[0] = new SqlParameter("@RETURN_VALUE",SqlDbType.Int,4);
			Parameters[0].Direction = ParameterDirection.ReturnValue;
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
	}
	
}


