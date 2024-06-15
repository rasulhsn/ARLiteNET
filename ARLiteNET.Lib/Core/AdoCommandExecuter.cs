using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ARLiteNET.Lib.Core
{
    public static class AdoCommandExecuter
    {
        public static AdoExecuterResult<object> PrimitiveScalar(IDbCommand dbCommand)
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

        public static AdoExecuterResult<T> Scalar<T>(IDbCommand dbCommand) where T : new()
        {
            AdoExecuterResult<T> result = new AdoExecuterResult<T>();
            try
            {
                dbCommand.Connection.Open();
                IDataReader adoReader = dbCommand.ExecuteReader();
                result.Object = DataReaderObjectConstructor.Construct<T>(adoReader);
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
                IDataReader adoReader = dbCommand.ExecuteReader();
                result.Object = DataReaderObjectConstructor.Constructs<T>(adoReader);
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
        
        class DataReaderObjectConstructor
        {
            public static T Construct<T>(IDataReader adoReader) where T : new()
            {
                Type resultType = typeof(T);

                if (!resultType.IsClass)
                {
                    throw new Exception("Type could not be read! Because type isn't Class.");
                }

                T instance = default(T);
                try
                {
                    if (adoReader.Read())
                    {
                        instance = (T)ConstructObject(typeof(T), adoReader);
                    }
                }
                catch (Exception exp)
                {
                    adoReader?.Dispose();
                    throw exp;
                }

                return instance;
            }
            public static IEnumerable<T> Constructs<T>(IDataReader adoReader) where T : new()
            {
                Type resultType = typeof(T);

                if (!resultType.IsClass)
                {
                    throw new Exception("Type could not be read! Because type isn't Class.");
                }

                List<T> dataResult = new List<T>();
                try
                {
                    while (adoReader.Read())
                    {
                        dataResult.Add((T)ConstructObject(typeof(T), adoReader));
                    }
                }
                catch (Exception exp)
                {
                    adoReader?.Dispose();
                    throw exp;
                }

                return dataResult;
            }

            private static object ConstructObject(Type resultType, IDataReader adoReader)
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
                        object tempValue = adoReader.GetValue(adoReader.GetOrdinal(item.Name));
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
                        value = ConstructObject(item.PropertyType, adoReader);
                    }
                    item.SetValue(newInstance, value);
                }
                return newInstance;
            }
        }
    }
}
