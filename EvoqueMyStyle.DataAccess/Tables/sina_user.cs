using System;
using System.Reflection;
using Bean.DataAccess;

namespace EvoqueMyStyle.DataAccess.Tables
{
	#region sina_user
	[Serializable]
	public class sina_user : EntityBase<sina_user>
	{
		public sina_user(){}
		
		public sina_user(
		 System.String sina_uid,
		 System.String sina_nm,
		 System.String sina_oauth_token,
		 System.String sina_oauth_token_secret,
		 System.String friends
		)
		{
			this.sina_uid = sina_uid; 
			this.sina_nm = sina_nm; 
			this.sina_oauth_token = sina_oauth_token; 
			this.sina_oauth_token_secret = sina_oauth_token_secret; 
			this.friends = friends; 
		}
	
		public static sina_user Instance { get { return new sina_user(); } }
		
		public static string TableName ="sina_user";
		public struct Columns
		{
			public static string sina_uid = "sina_uid";
			public static string sina_nm = "sina_nm";
			public static string sina_oauth_token = "sina_oauth_token";
			public static string sina_oauth_token_secret = "sina_oauth_token_secret";
			public static string friends = "friends";
			
		}
		private System.String  sina_uid; 
		public System.String  Sina_uid { get {return sina_uid; } set {sina_uid = value;}}
		private System.String  sina_nm; 
		public System.String  Sina_nm { get {return sina_nm; } set {sina_nm = value;}}
		private System.String  sina_oauth_token; 
		public System.String  Sina_oauth_token { get {return sina_oauth_token; } set {sina_oauth_token = value;}}
		private System.String  sina_oauth_token_secret; 
		public System.String  Sina_oauth_token_secret { get {return sina_oauth_token_secret; } set {sina_oauth_token_secret = value;}}
		private System.String  friends; 
		public System.String  Friends { get {return friends; } set {friends = value;}}
		
		protected override DataReaderDTOConverter<sina_user> GetDTOConverter(System.Data.IDataReader reader)
        {
            return new  sina_userDTOConverter(reader);
        }
	}
	
	public class sina_userDTOConverter : DataReaderDTOConverter<sina_user>
    {
        public sina_userDTOConverter(System.Data.IDataReader sdr) : base(sdr) { }

        protected override sina_user GetDataTransferObject(System.Data.IDataReader sdr)
        {
            return new sina_user
            (
				DBConvert.DBTypeToString(sdr[sina_user.Columns.sina_uid]),
				DBConvert.DBTypeToString(sdr[sina_user.Columns.sina_nm]),
				DBConvert.DBTypeToString(sdr[sina_user.Columns.sina_oauth_token]),
				DBConvert.DBTypeToString(sdr[sina_user.Columns.sina_oauth_token_secret]),
				DBConvert.DBTypeToString(sdr[sina_user.Columns.friends])
            );
        }
		
		protected override sina_user GetDataTransferObject(System.Data.IDataReader sdr, params string[] columns)
        {
            sina_user result = new sina_user();
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


