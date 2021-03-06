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
    public class TestApp
    {
        private ITestRepository service = new TestRepository();

        public List<TestEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<TestEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_PartNo.Contains(keyword));
            }
            //expression = expression.And(t => t.F_Account != "admin");

            return service.FindList(expression, pagination);
        }

        public TestEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(TestEntity areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
                service.Update(areaEntity);
            }
            else
            {
                areaEntity.Create();
                service.Insert(areaEntity);
            }
        }
    }
}
