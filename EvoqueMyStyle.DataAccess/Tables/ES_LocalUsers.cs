using System;
using System.Reflection;
using Bean.DataAccess;

namespace EvoqueMyStyle.DataAccess.Tables
{
	#region ES_LocalUsers
	[Serializable]
	public class ES_LocalUsers : EntityBase<ES_LocalUsers>
	{
		public ES_LocalUsers(){}
		
		public ES_LocalUsers(
		 System.Guid iD,
		 System.String email,
		 System.String password,
		 System.DateTime addTime,
		 System.String hostAddress,
		 System.Byte status
		)
		{
			this.iD = iD; 
			this.email = email; 
			this.password = password; 
			this.addTime = addTime; 
			this.hostAddress = hostAddress; 
			this.status = status; 
		}
	
		public static ES_LocalUsers Instance { get { return new ES_LocalUsers(); } }
		
		public static string TableName ="ES_LocalUsers";
		public struct Columns
		{
			public static string ID = "ID";
			public static string Email = "Email";
			public static string password = "password";
			public static string AddTime = "AddTime";
			public static string HostAddress = "HostAddress";
			public static string Status = "Status";
			
		}
		private System.Guid  iD; 
		public System.Guid  ID { get {return iD; } set {iD = value;}}
		private System.String  email; 
		public System.String  Email { get {return email; } set {email = value;}}
		private System.String  password; 
		public System.String  Password { get {return password; } set {password = value;}}
		private System.DateTime  addTime; 
		public System.DateTime  AddTime { get {return addTime; } set {addTime = value;}}
		private System.String  hostAddress; 
		public System.String  HostAddress { get {return hostAddress; } set {hostAddress = value;}}
		private System.Byte  status; 
		public System.Byte  Status { get {return status; } set {status = value;}}
		
		protected override DataReaderDTOConverter<ES_LocalUsers> GetDTOConverter(System.Data.IDataReader reader)
        {
            return new  ES_LocalUsersDTOConverter(reader);
        }
	}
	
	public class ES_LocalUsersDTOConverter : DataReaderDTOConverter<ES_LocalUsers>
    {
        public ES_LocalUsersDTOConverter(System.Data.IDataReader sdr) : base(sdr) { }

        protected override ES_LocalUsers GetDataTransferObject(System.Data.IDataReader sdr)
        {
            return new ES_LocalUsers
            (
				DBConvert.DBTypeToGuid(sdr[ES_LocalUsers.Columns.ID]),
				DBConvert.DBTypeToString(sdr[ES_LocalUsers.Columns.Email]),
				DBConvert.DBTypeToString(sdr[ES_LocalUsers.Columns.password]),
				DBConvert.DBTypeToDateTime(sdr[ES_LocalUsers.Columns.AddTime]),
				DBConvert.DBTypeToString(sdr[ES_LocalUsers.Columns.HostAddress]),
				DBConvert.DBTypeToByte(sdr[ES_LocalUsers.Columns.Status])
            );
        }
		
		protected override ES_LocalUsers GetDataTransferObject(System.Data.IDataReader sdr, params string[] columns)
        {
            ES_LocalUsers result = new ES_LocalUsers();
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


