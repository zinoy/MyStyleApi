using System;
using System.Reflection;
using Bean.DataAccess;

namespace EvoqueMyStyle.DataAccess.Tables
{
	#region ES_TestDrive
	[Serializable]
	public class ES_TestDrive : EntityBase<ES_TestDrive>
	{
		public ES_TestDrive(){}
		
		public ES_TestDrive(
		 System.Int32 iD,
		 System.String name,
		 System.String email,
		 System.String mobile,
		 System.DateTime addTime,
		 System.String hostAddress
		)
		{
			this.iD = iD; 
			this.name = name; 
			this.email = email; 
			this.mobile = mobile; 
			this.addTime = addTime; 
			this.hostAddress = hostAddress; 
		}
	
		public static ES_TestDrive Instance { get { return new ES_TestDrive(); } }
		
		public static string TableName ="ES_TestDrive";
		public struct Columns
		{
			public static string ID = "ID";
			public static string Name = "Name";
			public static string Email = "Email";
			public static string Mobile = "Mobile";
			public static string AddTime = "AddTime";
			public static string HostAddress = "HostAddress";
			
		}
		private System.Int32  iD; 
		public System.Int32  ID { get {return iD; } set {iD = value;}}
		private System.String  name; 
		public System.String  Name { get {return name; } set {name = value;}}
		private System.String  email; 
		public System.String  Email { get {return email; } set {email = value;}}
		private System.String  mobile; 
		public System.String  Mobile { get {return mobile; } set {mobile = value;}}
		private System.DateTime  addTime; 
		public System.DateTime  AddTime { get {return addTime; } set {addTime = value;}}
		private System.String  hostAddress; 
		public System.String  HostAddress { get {return hostAddress; } set {hostAddress = value;}}
		
		protected override DataReaderDTOConverter<ES_TestDrive> GetDTOConverter(System.Data.IDataReader reader)
        {
            return new  ES_TestDriveDTOConverter(reader);
        }
	}
	
	public class ES_TestDriveDTOConverter : DataReaderDTOConverter<ES_TestDrive>
    {
        public ES_TestDriveDTOConverter(System.Data.IDataReader sdr) : base(sdr) { }

        protected override ES_TestDrive GetDataTransferObject(System.Data.IDataReader sdr)
        {
            return new ES_TestDrive
            (
				DBConvert.DBTypeToInt32(sdr[ES_TestDrive.Columns.ID]),
				DBConvert.DBTypeToString(sdr[ES_TestDrive.Columns.Name]),
				DBConvert.DBTypeToString(sdr[ES_TestDrive.Columns.Email]),
				DBConvert.DBTypeToString(sdr[ES_TestDrive.Columns.Mobile]),
				DBConvert.DBTypeToDateTime(sdr[ES_TestDrive.Columns.AddTime]),
				DBConvert.DBTypeToString(sdr[ES_TestDrive.Columns.HostAddress])
            );
        }
		
		protected override ES_TestDrive GetDataTransferObject(System.Data.IDataReader sdr, params string[] columns)
        {
            ES_TestDrive result = new ES_TestDrive();
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


