using ARLiteNET.Core;
using ARLiteNET.Exceptions;
using System.Collections.Generic;

namespace ARLiteNET
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ARLiteObject
    {
        /// <summary>
        /// 
        /// </summary>
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
        protected T RunScalar<T>(IDbCommandBuilder commandBuilder)
        {
            var dbCommand = commandBuilder.Build();

            var result = AdoCommandExecuter.PrimitiveScalar(dbCommand);

            if (!result.IsSuccess)
            {
                throw new ARLiteObjectException("Occur error!", result.Errors);
            }

            return (T)result.Object;
        }

        /// <summary>
        /// 
        /// </summary>
        protected T Run<T>(IDbCommandBuilder commandBuilder) where T : new()
        {      
            var dbCommand = commandBuilder.Build();

            var result = AdoCommandExecuter.Scalar<T>(dbCommand);

            if (!result.IsSuccess)
            {
                throw new ARLiteObjectException("Occur error!", result.Errors);
            }

            return result.Object;
        }

        /// <summary>
        /// 
        /// </summary>
        protected int Run(IDbCommandBuilder commandBuilder)
        {
            var dbCommand = commandBuilder.Build();

            var result = AdoCommandExecuter.Query(dbCommand);

            if (!result.IsSuccess)
            {
                throw new ARLiteObjectException("Occur error!", result.Errors);
            }

            return result.AffectedRows;
        }

        /// <summary>
        /// 
        /// </summary>
        protected AdoCommandBuilder Query()
        {
            var factory = ARLiteConfigurationResolver.GetConfigurationFactory(this.GetType());
            var adoConnectionString = factory.CreateConnectionString();

            var commandBuilder = new AdoCommandBuilder(adoConnectionString);

            return commandBuilder;
        }
    }
}
