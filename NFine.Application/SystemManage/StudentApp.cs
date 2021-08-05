/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;

namespace NFine.Application.SystemManage
{
    public class StudentApp
    {
        private IStudentRepository service = new StudentRepository();

        public List<StudentEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<StudentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                //expression = expression.And(t => t.F_Account.Contains(keyword));
                //expression = expression.Or(t => t.F_RealName.Contains(keyword));
                //expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
            //expression = expression.And(t => t.F_Account != "admin");
            return service.FindList(expression, pagination);
        }
        public StudentEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(StudentEntity studentEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                studentEntity.Modify(keyValue);
                service.Update(studentEntity);
            }
            else
            {
                studentEntity.Create();
                service.Insert(studentEntity);
            }
        }
        public void UpdateForm(StudentEntity studentEntity)
        {
            service.Update(studentEntity);
        }
    }
}
