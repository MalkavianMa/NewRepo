/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;

namespace NFine.Repository.SystemManage
{
    public class StudentRepository : RepositoryBase<StudentEntity>, IStudentRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<StudentEntity>(t => t.F_Id == keyValue);
                db.Commit();
            }
        }

        public void SubmitForm(StudentEntity studentEntity, string keyValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
