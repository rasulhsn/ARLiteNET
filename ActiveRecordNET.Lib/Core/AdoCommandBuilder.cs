using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ActiveRecordNET.Lib
{
    public class AdoCommandBuilder
    {
        private readonly AdoConnectionString _connectionString;

        private List<IDbDataParameter> _parameters;
        private string _commandText;
        private CommandType _commandType;

        public AdoCommandBuilder(AdoConnectionString connectionString)
        {
            _connectionString = connectionString;
            _parameters = new List<IDbDataParameter>();
        }

        public AdoCommandBuilder AddParam(params IDbDataParameter[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(IDbDataParameter));

            foreach (IDbDataParameter param in parameters)
            {
                this._parameters.Add(param);
            }
            return this;
        }

        public AdoCommandBuilder AddParam(Action<IDbDataParameter> newParam)
        {
            if (newParam == null)
                throw new ArgumentNullException(nameof(newParam));

            var dbParam = AdoObjectFactory.CreateParameter(_connectionString);
            newParam(dbParam);

            return this.AddParam(dbParam);
        }

        public AdoCommandBuilder SetCommand(string commandText,
            System.Data.CommandType commandType = System.Data.CommandType.Text)
        {
            this._commandText = commandText;
            this._commandType = commandType;
            return this;
        }

        internal IDbCommand Build()
        {
            var connection = AdoObjectFactory.CreateConnection(_connectionString);
            var command = AdoObjectFactory.CreateCommand(_connectionString);
            
            command.Connection = connection;
            command.CommandText = this._commandText;
            command.CommandType = this._commandType;
            
            if (_parameters.Any())
            {
                _parameters.ForEach(param => command.Parameters.Add(param));
            }

            return command;
        }
    }
}
