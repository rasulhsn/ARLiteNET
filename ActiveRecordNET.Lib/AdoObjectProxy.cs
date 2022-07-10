using System;
using System.Collections.Generic;
using ActiveRecordNET.Lib.Core;

namespace ActiveRecordNET
{
    public abstract class AdoObjectProxy
    {
        private readonly AdoCommandExecuter _commandExecuter;

        protected AdoObjectProxy()
        {
            _commandExecuter = new AdoCommandExecuter();
        }

        protected IEnumerable<T> ReadRecords<T>(Action<AdoCommandBuilder> builderCallback) where T : new()
        {
            var dbCommandBuilder = CreateAdoCommandBuilder();
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
            var dbCommandBuilder = CreateAdoCommandBuilder();
            builderCallback(dbCommandBuilder);
            var dbCommand = dbCommandBuilder.Build();

            var result = _commandExecuter.Single(dbCommand);

            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }

            return (T)result.Object;
        }

        private AdoCommandBuilder CreateAdoCommandBuilder()
        {
            var factory = AdoConfigurationResolver.GetConfigurationFactory(this.GetType());
            var adoConnectionString = factory.CreateConnectionString();

            return new AdoCommandBuilder(adoConnectionString);
        }
    }
}
