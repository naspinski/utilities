﻿using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace Naspinski.Utilities
{
    public static class LinqToSql
    {
        /// <summary>
        /// Gets the primary key of a Linq-to-SQL table; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type that you wish to find the primary key of</typeparam>
        /// <returns>PropertyInfo of the Primary Key of the supplied Type</returns>
        public static PropertyInfo GetPrimaryKey<T>() where T : class, INotifyPropertyChanged
        {
            PropertyInfo[] infos = typeof(T).GetProperties();
            PropertyInfo PKProperty = null;
            foreach (PropertyInfo info in infos)
            {
                var column = info.GetCustomAttributes(false)
                    .Where(x => x.GetType() == typeof(ColumnAttribute))
                    .SingleOrDefault(x => ((ColumnAttribute)x).IsPrimaryKey && ((ColumnAttribute)x).DbType.Contains("NOT NULL"));
                if (column != null)
                {
                    PKProperty = info;
                    break;
                }
            }
            if (PKProperty == null) 
                throw new NotSupportedException(typeof(T).ToString() + " has no Primary Key");
            return PKProperty;
        }

        /// <summary>
        /// Universal Get accessor for any Linq-to-SQL DataContext; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">Type of the object you wish to return, must be a table in the DataContext</typeparam>
        /// <param name="dataContext">DataContext that holds the table you wish to query</param>
        /// <param name="primaryKey">Primary Key of the object</param>
        /// <returns>T</returns>
        public static T Get<T>(this DataContext dataContext, object primaryKey) where T : class, INotifyPropertyChanged
        {
            if (GetPrimaryKey<T>().PropertyType != primaryKey.GetType())
                throw new ArgumentException("Primary Key of Table and primaryKey argument are not of the same Type; Primary Key of Table is of Type: " + GetPrimaryKey<T>().PropertyType.ToString() + ", primaryKey argument supplied is of Type: " + primaryKey.GetType().ToString());

            return dataContext.GetTable(typeof(T)).Cast<T>().Get<T>(primaryKey);
        }

        /// <summary>
        /// Universal Get accessor for any Linq-to-SQL Table; requires that the table has a PRIMARY KEY NOT NULL
        /// </summary>
        /// <typeparam name="T">The type of the table</typeparam>
        /// <param name="table">The table you wish to query for the object</param>
        /// <param name="primaryKey">Primary Key of the object</param>
        /// <returns>T</returns>
        private static T Get<T>(this IQueryable<T> table, object primaryKey) where T : class, INotifyPropertyChanged
        {
            Type[] typesThatUseEquals = new Type[] { typeof(Guid), typeof(string) };
            return table.Where(GetPrimaryKey<T>().Name + ".Equals(@0)", primaryKey).FirstOrDefault();
        }
    }
}