using System;
using System.Reflection;
using Bean.DataAccess;

namespace EvoqueMyStyle.DataAccess.Tables
{
	#region ES_SinaUser
	[Serializable]
	public class ES_SinaUser : EntityBase<ES_SinaUser>
	{
		public ES_SinaUser(){}
		
		public ES_SinaUser(
		 System.Int64 uID,
		 System.String name,
		 System.String accessToken,
		 System.DateTime expires,
		 System.String state,
		 System.DateTime addTime,
		 System.String following,
		 System.Byte status
		)
		{
			this.uID = uID; 
			this.name = name; 
			this.accessToken = accessToken; 
			this.expires = expires; 
			this.state = state; 
			this.addTime = addTime; 
			this.following = following; 
			this.status = status; 
		}
	
		public static ES_SinaUser Instance { get { return new ES_SinaUser(); } }
		
		public static string TableName ="ES_SinaUser";
		public struct Columns
		{
			public static string UID = "UID";
			public static string Name = "Name";
			public static string AccessToken = "AccessToken";
			public static string Expires = "Expires";
			public static string State = "State";
			public static string AddTime = "AddTime";
			public static string Following = "Following";
			public static string Status = "Status";
			
		}
		private System.Int64  uID; 
		public System.Int64  UID { get {return uID; } set {uID = value;}}
		private System.String  name; 
		public System.String  Name { get {return name; } set {name = value;}}
		private System.String  accessToken; 
		public System.String  AccessToken { get {return accessToken; } set {accessToken = value;}}
		private System.DateTime  expires; 
		public System.DateTime  Expires { get {return expires; } set {expires = value;}}
		private System.String  state; 
		public System.String  State { get {return state; } set {state = value;}}
		private System.DateTime  addTime; 
		public System.DateTime  AddTime { get {return addTime; } set {addTime = value;}}
		private System.String  following; 
		public System.String  Following { get {return following; } set {following = value;}}
		private System.Byte  status; 
		public System.Byte  Status { get {return status; } set {status = value;}}
		
		protected override DataReaderDTOConverter<ES_SinaUser> GetDTOConverter(System.Data.IDataReader reader)
        {
            return new  ES_SinaUserDTOConverter(reader);
        }
	}
	
	public class ES_SinaUserDTOConverter : DataReaderDTOConverter<ES_SinaUser>
    {
        public ES_SinaUserDTOConverter(System.Data.IDataReader sdr) : base(sdr) { }

        protected override ES_SinaUser GetDataTransferObject(System.Data.IDataReader sdr)
        {
            return new ES_SinaUser
            (
				DBConvert.DBTypeToInt64(sdr[ES_SinaUser.Columns.UID]),
				DBConvert.DBTypeToString(sdr[ES_SinaUser.Columns.Name]),
				DBConvert.DBTypeToString(sdr[ES_SinaUser.Columns.AccessToken]),
				DBConvert.DBTypeToDateTime(sdr[ES_SinaUser.Columns.Expires]),
				DBConvert.DBTypeToString(sdr[ES_SinaUser.Columns.State]),
				DBConvert.DBTypeToDateTime(sdr[ES_SinaUser.Columns.AddTime]),
				DBConvert.DBTypeToString(sdr[ES_SinaUser.Columns.Following]),
				DBConvert.DBTypeToByte(sdr[ES_SinaUser.Columns.Status])
            );
        }
		
		protected override ES_SinaUser GetDataTransferObject(System.Data.IDataReader sdr, params string[] columns)
        {
            ES_SinaUser result = new ES_SinaUser();
            foreach (string s in columns)
            {
                PropertyInfo info = result.GetType().GetProperty(s);
                object value = sdr[s];
                info.SetValue(result, value is DBNull ? null : value, null);
            }

            return result;
        }
    }
	#endregion
}


