using MyProject.Model.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.CommonFunction
{
    public static class ConfigTableFunction
    {
        /// <summary>
        /// Hàm lấy tên bảng
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTableName(this Type type)
        {
            string tableName = string.Empty;
            var configTable = (ConfigTableAttribute)type.GetCustomAttributes(typeof(ConfigTableAttribute), true).FirstOrDefault();
            if (configTable != null)
            {
                tableName = configTable.TableName;
            }

            return tableName;
        }

        /// <summary>
        /// Hàm lấy tên FieldName bằng Attribute
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public static string GetFieldNameByAttribute(this Type type, Type attrType)
        {
            string fieldName = string.Empty;
            PropertyInfo[] props = type.GetProperties();
            if (props != null)
            {
                var propertyInfo = props.SingleOrDefault(x => x.GetCustomAttribute(attrType, true) != null);
                if (propertyInfo != null)
                {
                    fieldName = propertyInfo.Name;
                }
            }

            return fieldName;

        }

        /// <summary>
        /// Hàm lấy danh sách FieldName bằng Attribute
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public static List<string> GetListFieldNameByAttribute(this Type type, Type attrType)
        {
            List<string> fieldNames = new List<string>();
            PropertyInfo[] propertyInfos = type.GetProperties();
            if (propertyInfos != null)
            {
                var lstLpropertyInfo = propertyInfos.Where(x => x.GetCustomAttributes(attrType, true) != null);
                fieldNames = lstLpropertyInfo.Select(x => x.Name).ToList();
            }

            return fieldNames;
        }

        /// <summary>
        /// Hàm lấy Type của property theo PropertyName
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Type GetPropertyType(this Type type, string propertyName)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();
            if (propertyInfos != null)
            {
                var propertyInfo = propertyInfos.SingleOrDefault(x => x.Name.Equals(propertyName));
                if (propertyInfo != null)
                {
                    return propertyInfo.PropertyType;
                }
            }

            return typeof(object);
        }

        /// <summary>
        /// Hàm lấy tên fieldName của khóa chính
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPrimaryKeyName(this Type type)
        {
            var fieldNamePrimaryKey = string.Empty;
            var primaryKey = type.GetFieldNameByAttribute(typeof(KeyAttribute));
            if (!string.IsNullOrWhiteSpace(primaryKey))
            {
                fieldNamePrimaryKey = primaryKey.ToString();
            }

            return fieldNamePrimaryKey;
        }

        /// <summary>
        /// Hàm lấy tên fieldName của khóa ngoại
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetForeignName(this Type type)
        {
            var fieldName = string.Empty;
            var foreignKey = type.GetFieldNameByAttribute(typeof(ForeignKeyAttribute));
            if (!string.IsNullOrWhiteSpace(foreignKey))
            {
                fieldName = foreignKey.ToString();
            }

            return fieldName;
        }

        /// <summary>
        /// Hàm kiểm tra trong entity có property không
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool ContaintProperty(this Type type, string propertyName)
        {
            var fieldName = type.GetProperty(propertyName);
            return (fieldName != null);
        }
    }
}
