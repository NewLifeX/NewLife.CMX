using System;
using System.ComponentModel;
using NewLife.CommonEntity;
using XCode;
using XCode.DataAccessLayer;

namespace NewLife.CMX
{
    [BindTable("Admin", Description = "系统管理员", ConnName = "CMX", DbType = DatabaseType.SqlServer)]
    public class Admin : Administrator<Admin, Role, Menu, RoleMenu, NewLife.CommonEntity.Log>
    {
        #region 扩展属性
        private String _Telephone;
        /// <summary>电话</summary>
        [DisplayName("电话")]
        [Description("电话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "Telephone", "电话", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Telephone
        {
            get { return _Telephone; }
            set { if (OnPropertyChanging("Telephone", value)) { _Telephone = value; OnPropertyChanged("Telephone"); } }
        }

        private String _Mail;
        /// <summary>邮箱</summary>
        [DisplayName("邮箱")]
        [Description("邮箱")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "Mail", "邮箱", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Mail
        {
            get { return _Mail; }
            set { if (OnPropertyChanging("Mail", value)) { _Mail = value; OnPropertyChanged("Mail"); } }
        }
        #endregion
    }

    public class XManagerProvider : CommonManageProvider<Admin>, ICommonManageProvider { }
}