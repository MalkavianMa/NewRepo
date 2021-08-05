using System;
namespace NFine.Domain.Entity.ControllerManage.Model
{
	/// <summary>
	/// Module_Controller:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Controller_Entity
	{
		public Controller_Entity()
		{ }
		#region Model
		private int _tabid;
		private string _controllername;
		private string _controllerpath;
		private string _summary;
		private DateTime? _careatetime;
		private string _remark;
		private int? _moduleid;
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
		public string ControllerName
		{
			set { _controllername = value; }
			get { return _controllername; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ControllerPath
		{
			set { _controllerpath = value; }
			get { return _controllerpath; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Summary
		{
			set { _summary = value; }
			get { return _summary; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CareateTime
		{
			set { _careatetime = value; }
			get { return _careatetime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set { _remark = value; }
			get { return _remark; }
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ModuleID
		{
			set { _moduleid = value; }
			get { return _moduleid; }
		}
		#endregion Model

	}
}

