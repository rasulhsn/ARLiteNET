using System;
using System.Collections.Generic;

namespace ActiveRecordNET
{
    public abstract class AdoObjectProxy
    {
        private AdoCommandExecuter _commandExecuter;

        protected AdoObjectProxy()
        {
            _commandExecuter = new AdoCommandExecuter();
        }

        protected IEnumerable<T> ReadRecords<T>(Func<AdoCommandBuilder> builder) where T : new()
        {
            var dbCommandBuilder = builder();
            var dbCommand = dbCommandBuilder.Build();
            
            var result = _commandExecuter.Array<T>(dbCommand);
            
            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }

            return result.Object;
        }

        protected T ReadRecord<T>(Func<AdoCommandBuilder> builder) where T : new()
        {
            var dbCommandBuilder = builder();
            var dbCommand = dbCommandBuilder.Build();

            var result = _commandExecuter.Single(dbCommand);

            if (!result.IsSuccess)
            {
                throw new AdoObjectProxyException("Occur error!", result.Errors);
            }

            return (T)result.Object;
        }
    }
}
