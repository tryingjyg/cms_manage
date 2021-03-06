﻿using System.Collections.Generic;
using LZManager.DAL;
using System.Data;
using System.Data.Common;

namespace LZManager.BLL
{
    public class ModulePermissionsManage
    {
        ModulepermissionsMO mdpMO = new ModulepermissionsMO("LZBgAuthorityConn");

        /// <summary>
        /// 获取模块功能关系信息
        /// </summary>
        /// <param name="ptId"></param>
        /// <returns></returns>
        public ModulepermissionsEO GetModulePermissionsByPK(string ptId)
        {
            return mdpMO.GetByPK(ptId);
        }

        /// <summary>
        /// 创建模块功能关系信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddModulePermissions(ModulepermissionsEO item)
        {
            return mdpMO.Add(item);
        }

        /// <summary>
        /// 按主键删除
        /// </summary>
        /// <param name = "ptId">模块功能id</param>
        /// <param name = "pmId">功能id</param>
        /// <param name="tm">事务管理对象</param>
        public virtual int RemoveByPK(string ptId)
        {
            return mdpMO.RemoveByPK(ptId);
        }

        /// <summary>
        /// 按自定义条件查询
        /// </summary>
        /// <param name = "where">自定义条件,where子句</param>
        /// <param name = "top">获取行数</param>
        /// <param name = "sort">排序表达式</param>
        /// <return>Table</return>
        public virtual DataTable GetTable(string where, IEnumerable<DbParameter> paras)
        {
            return mdpMO.GetTable(where, -1, string.Empty,paras);
        }
    }
}
