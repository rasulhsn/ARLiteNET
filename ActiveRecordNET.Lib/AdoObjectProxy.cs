using System;
using System.Collections.Generic;

namespace ActiveRecordNET
{
    public abstract class AdoObjectProxy
    {
        private readonly AdoCommandExecuter _commandExecuter;
        private readonly AdoConnectionStringBuilder _connectionStringBuilder;

        protected AdoObjectProxy()
        {
            _commandExecuter = new AdoCommandExecuter();
            _connectionStringBuilder = new AdoConnectionStringBuilder();

            Configure(_connectionStringBuilder);
        }

        protected IEnumerable<T> ReadRecords<T>(Action<AdoCommandBuilder> builderCallback) where T : new()
        {
            var dbCommandBuilder = _connectionStringBuilder.CreateCommand();
            builderCallback(dbCommandBuilder);
            var dbCommand = dbCommandBuilder.Build();
            
            var result = _commandExecuter.Array<T>(dbCommand);
            
            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }

            return result.Object;
        }

        protected T ReadRecord<T>(Action<AdoCommandBuilder> builderCallback) where T : new()
        {
            var dbCommandBuilder = _connectionStringBuilder.CreateCommand();
            builderCallback(dbCommandBuilder);
            var dbCommand = dbCommandBuilder.Build();

            var result = _commandExecuter.Single(dbCommand);

            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }

            return (T)result.Object;
        }

        protected abstract void Configure(AdoConnectionStringBuilder builder);
    }
}
