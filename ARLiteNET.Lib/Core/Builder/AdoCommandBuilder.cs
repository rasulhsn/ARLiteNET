using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ARLiteNET.Lib.Core
{
    public partial class AdoCommandBuilder : IDbCommandBuilder
    {
        private readonly AdoConnectionString _connectionString;

        private List<IDbDataParameter> _parameters;
        private string _commandText;
        private CommandType _commandType;

        internal AdoCommandBuilder(AdoConnectionString connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _parameters = new List<IDbDataParameter>();
        }

        public string ProviderName => _connectionString.ProviderName;

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

            var dbParam = AdoDbObjectFactory.CreateParameter(_connectionString);
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

        IDbCommand IDbCommandBuilder.Build()
        {
            var connection = AdoDbObjectFactory.CreateConnection(_connectionString);
            var command = AdoDbObjectFactory.CreateCommand(_connectionString);

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
