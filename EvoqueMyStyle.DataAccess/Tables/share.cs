using System;
using System.Reflection;
using Bean.DataAccess;

namespace EvoqueMyStyle.DataAccess.Tables
{
	#region share
	[Serializable]
	public class share : EntityBase<share>
	{
		public share(){}
		
		public share(
		 System.Decimal id,
		 System.String sina_uid,
		 System.String sina_name,
		 System.String type,
		 System.String comment,
		 System.String img,
		 System.DateTime sys_dt,
		 System.String status,
		 System.String mobile,
		 System.Boolean ling
		)
		{
			this.id = id; 
			this.sina_uid = sina_uid; 
			this.sina_name = sina_name; 
			this.type = type; 
			this.comment = comment; 
			this.img = img; 
			this.sys_dt = sys_dt; 
			this.status = status; 
			this.mobile = mobile; 
			this.ling = ling; 
		}
	
		public static share Instance { get { return new share(); } }
		
		public static string TableName ="share";
		public struct Columns
		{
			public static string id = "id";
			public static string sina_uid = "sina_uid";
			public static string sina_name = "sina_name";
			public static string type = "type";
			public static string comment = "comment";
			public static string img = "img";
			public static string sys_dt = "sys_dt";
			public static string status = "status";
			public static string mobile = "mobile";
			public static string ling = "ling";
			
		}
		private System.Decimal  id; 
		public System.Decimal  Id { get {return id; } set {id = value;}}
		private System.String  sina_uid; 
		public System.String  Sina_uid { get {return sina_uid; } set {sina_uid = value;}}
		private System.String  sina_name; 
		public System.String  Sina_name { get {return sina_name; } set {sina_name = value;}}
		private System.String  type; 
		public System.String  Type { get {return type; } set {type = value;}}
		private System.String  comment; 
		public System.String  Comment { get {return comment; } set {comment = value;}}
		private System.String  img; 
		public System.String  Img { get {return img; } set {img = value;}}
		private System.DateTime  sys_dt; 
		public System.DateTime  Sys_dt { get {return sys_dt; } set {sys_dt = value;}}
		private System.String  status; 
		public System.String  Status { get {return status; } set {status = value;}}
		private System.String  mobile; 
		public System.String  Mobile { get {return mobile; } set {mobile = value;}}
		private System.Boolean  ling; 
		public System.Boolean  Ling { get {return ling; } set {ling = value;}}
		
		protected override DataReaderDTOConverter<share> GetDTOConverter(System.Data.IDataReader reader)
        {
            return new  shareDTOConverter(reader);
        }
	}
	
	public class shareDTOConverter : DataReaderDTOConverter<share>
    {
        public shareDTOConverter(System.Data.IDataReader sdr) : base(sdr) { }

        protected override share GetDataTransferObject(System.Data.IDataReader sdr)
        {
            return new share
            (
				DBConvert.DBTypeToDecimal(sdr[share.Columns.id]),
				DBConvert.DBTypeToString(sdr[share.Columns.sina_uid]),
				DBConvert.DBTypeToString(sdr[share.Columns.sina_name]),
				DBConvert.DBTypeToString(sdr[share.Columns.type]),
				DBConvert.DBTypeToString(sdr[share.Columns.comment]),
				DBConvert.DBTypeToString(sdr[share.Columns.img]),
				DBConvert.DBTypeToDateTime(sdr[share.Columns.sys_dt]),
				DBConvert.DBTypeToString(sdr[share.Columns.status]),
				DBConvert.DBTypeToString(sdr[share.Columns.mobile]),
				DBConvert.DBTypeTobool(sdr[share.Columns.ling])
            );
        }
		
		protected override share GetDataTransferObject(System.Data.IDataReader sdr, params string[] columns)
        {
            share result = new share();
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


