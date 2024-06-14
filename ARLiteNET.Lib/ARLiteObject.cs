using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.SQLite;
using System.Collections.Generic;

namespace ARLiteNET.Lib
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ARLiteObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandBuilder"></param>
        /// <returns></returns>
        /// <exception cref="ARLiteObjectException"></exception>
        protected IEnumerable<T> RunEnumerable<T>(IDbCommandBuilder commandBuilder) where T : new()
        {
            var dbCommand = commandBuilder.Build();
            
            var result = AdoCommandExecuter.Reader<T>(dbCommand);
            
            if (!result.IsSuccess)
            {
                throw new ARLiteObjectException("Occur error!", result.Errors);
            }

            return result.Object;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandBuilder"></param>
        /// <returns></returns>
        /// <exception cref="ARLiteObjectException"></exception>
        protected T Run<T>(IDbCommandBuilder commandBuilder) where T : new()
        {      
            var dbCommand = commandBuilder.Build();

            var result = AdoCommandExecuter.Scalar(dbCommand);

            if (!result.IsSuccess)
            {
                throw new ARLiteObjectException("Occur error!", result.Errors);
            }

            return (T)result.Object;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandBuilder"></param>
        /// <exception cref="ARLiteObjectException"></exception>
        protected void Run(IDbCommandBuilder commandBuilder)
        {
            var dbCommand = commandBuilder.Build();

            var result = AdoCommandExecuter.Query(dbCommand);

            if (!result.IsSuccess)
            {
                throw new ARLiteObjectException("Occur error!", result.Errors);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected AdoCommandBuilder Query()
        {
            var factory = SQLiteConfigurationResolver.GetConfigurationFactory(this.GetType());
            var adoConnectionString = factory.CreateConnectionString();

            var commandBuilder = new AdoCommandBuilder(adoConnectionString);

            return commandBuilder;
        }
    }
}
