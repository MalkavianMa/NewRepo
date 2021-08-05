using System;
namespace NFine.ControllerManage.Model
{
	/// <summary>
	/// TB_Module_Entity:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TB_Module_Entity
	{
		public TB_Module_Entity()
		{ }
		#region Model
		private int _tabid;
		private string _modulename;
		private string _modulepath;
		private DateTime? _createtime;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int TABID
		{
			set { _tabid = value; }
			get { return _tabid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleName
		{
			set { _modulename = value; }
			get { return _modulename; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModulePath
		{
			set { _modulepath = value; }
			get { return _modulepath; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set { _createtime = value; }
			get { return _createtime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set { _remark = value; }
			get { return _remark; }
		}
		#endregion Model

	}
}

