using System;
using System.Collections.Generic;

namespace ActiveRecordNET.Lib
{
    public abstract class AdoObjectProxy
    {
        private readonly AdoCommandExecuter _commandExecuter;

        protected AdoObjectProxy()
        {
            _commandExecuter = new AdoCommandExecuter();
        }

        protected IEnumerable<T> RunEnumerable<T>(Action<AdoCommandBuilder> builderCallback) where T : new()
        {
            var dbCommandBuilder = CreateAdoCommandBuilder();
            builderCallback(dbCommandBuilder);
            var dbCommand = dbCommandBuilder.Build();
            
            var result = _commandExecuter.Reader<T>(dbCommand);
            
            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }

            return result.Object;
        }

        protected T Run<T>(Action<AdoCommandBuilder> builderCallback) where T : new()
        {
            var dbCommandBuilder = CreateAdoCommandBuilder();
            builderCallback(dbCommandBuilder);
            var dbCommand = dbCommandBuilder.Build();

            var result = _commandExecuter.Scalar(dbCommand);

            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }

            return (T)result.Object;
        }

        protected void Run(Action<AdoCommandBuilder> builderCallback)
        {
            var dbCommandBuilder = CreateAdoCommandBuilder();
            builderCallback(dbCommandBuilder);
            var dbCommand = dbCommandBuilder.Build();

            var result = _commandExecuter.Query(dbCommand);

            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }
        }

        private AdoCommandBuilder CreateAdoCommandBuilder()
        {
            var factory = AdoConfigurationResolver.GetConfigurationFactory(this.GetType());
            var adoConnectionString = factory.CreateConnectionString();

            return new AdoCommandBuilder(adoConnectionString);
        }
    }
}
