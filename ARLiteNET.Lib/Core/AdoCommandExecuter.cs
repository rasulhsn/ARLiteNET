using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ARLiteNET.Lib.Core
{
    public static class AdoCommandExecuter
    {
        public static AdoExecuterResult<object> Scalar(IDbCommand dbCommand)
        {
            AdoExecuterResult<object> result = new AdoExecuterResult<object>();
            try
            {
                dbCommand.Connection.Open();
                result.Object = dbCommand.ExecuteScalar();
                dbCommand.Connection.Close();
            }
            catch (Exception exp)
            {
                result.Errors = new List<Exception>
                {
                    exp
                };
            }
            finally
            {
                if (dbCommand.Connection != null && (dbCommand.Connection.State == ConnectionState.Open || dbCommand.Connection.State == ConnectionState.Fetching))
                    dbCommand.Connection.Close();
                if (dbCommand != null)
                    dbCommand.Dispose();
            }

            return result;
        }

        public static AdoExecuterResult<IEnumerable<T>> Reader<T>(IDbCommand dbCommand) where T : new()
        {
            AdoExecuterResult<IEnumerable<T>> result = new AdoExecuterResult<IEnumerable<T>>();
            try
            {
                dbCommand.Connection.Open();
                result.Object = Constructs<T>(dbCommand.ExecuteReader());
                dbCommand.Connection.Close();
            }
            catch (Exception exp)
            {
                result.Errors = new List<Exception>
                {
                    exp
                };
            }
            finally
            {
                if (dbCommand.Connection != null && (dbCommand.Connection.State == ConnectionState.Open || dbCommand.Connection.State == ConnectionState.Fetching))
                    dbCommand.Connection.Close();
                if (dbCommand != null)
                    dbCommand.Dispose();
            }

            return result;
        }

        public static AdoExecuterResult Query(IDbCommand dbCommand)
        {
            AdoExecuterResult result = new AdoExecuterResult();
            try
            {
                dbCommand.Connection.Open();
                dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
            }
            catch (Exception exp)
            {
                result.Errors = new List<Exception>
                {
                    exp
                };
            }
            finally
            {
                if (dbCommand.Connection != null && (dbCommand.Connection.State == ConnectionState.Open || dbCommand.Connection.State == ConnectionState.Fetching))
                    dbCommand.Connection.Close();
                if (dbCommand != null)
                    dbCommand.Dispose();
            }

            return result;
        }

        private static IDataReader GetDbReader(object data)
        {
            IDataReader reader = data as IDataReader;
            
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            
            return reader;
        }
        
        private static IEnumerable<T> Constructs<T>(object data) where T : new()
        {
            IDataReader reader = GetDbReader(data);
            Type resultType = typeof(T);

            if (!resultType.IsClass)
            {
                throw new Exception("Type could not be read! Because type isn't Class.");
            }

            List<T> dataResult = new List<T>();
            try
            {
                while (reader.Read())
                {
                    dataResult.Add((T)ConstructObject(typeof(T), reader));
                }
            }
            catch (Exception exp)
            {
                reader?.Dispose();
                throw exp;
            }

            return dataResult;
        }

        private static object ConstructObject(Type resultType, IDataReader reader)
        {
            bool _IsPrimitive(PropertyInfo property)
            {
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) != null ?
                                    Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType;

                if (propertyType.IsPrimitive
                            || propertyType.Equals(typeof(decimal))
                            || propertyType.Equals(typeof(string))
                            || propertyType.Equals(typeof(DateTime))
                            || propertyType.Equals(typeof(bool)))
                {
                    return true;
                }

                return false;
            }

            var properties = resultType.GetProperties();

            object newInstance = Activator.CreateInstance(resultType);

            foreach (var item in properties)
            {
                object value;
                if (_IsPrimitive(item))
                {
                    object tempValue = reader.GetValue(reader.GetOrdinal(item.Name));
                    if (tempValue is DBNull)
                    {
                        if (item.PropertyType.IsPrimitive)
                            value = Activator.CreateInstance(item.PropertyType);
                        else
                            value = null;
                    }
                    else
                    {
                        value = Convert.ChangeType(tempValue, item.PropertyType);
                    }
                }
                else
                {
                    value = ConstructObject(item.PropertyType, reader);
                }
                item.SetValue(newInstance, value);
            }
            return newInstance;
        }
    }
}
