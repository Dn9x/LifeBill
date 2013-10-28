using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LifeBill.Models.Entity;

namespace LifeBill.Models.Interface
{
    public interface IBill
    {

        /// <summary>
        /// 根据年、月查询主档信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        DataTable SelectMasterByDate(int year, int month);

        /// <summary>
        /// 根据主档ID查询主档信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable SelectMasterById(int id);

        /// <summary>
        /// 根据年查询主档信息
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        DataTable SelectMasterByYear(int year);

        /// <summary>
        /// 查询所有分类列表
        /// </summary>
        /// <returns></returns>
        DataTable SelectTagList();

        /// <summary>
        /// 根据是否显示查询所有分类列表
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        DataTable SelectTagListByIsShow(string isShow);

        /// <summary>
        /// 根据类型查询分类列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        DataTable SelectTagByType(string type);

        /// <summary>
        /// 根据ID查询分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable SelectTagById(int id);

        /// <summary>
        /// 根据父类ID查询分类
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        DataTable SelectTagByPid(int pid);

        /// <summary>
        /// 根据主档ID查询明细档信息
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        DataTable SelectDetailByMasterId(int mid);

        /// <summary>
        /// 根据分类ID查询明细档信息
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        DataTable SelectDetailByTagId(int tid);

        /// <summary>
        /// 添加账单信息
        /// </summary>
        /// <param name="master"></param>
        /// <returns></returns>
        string InsertBill(List<string> sqls);


    }
}