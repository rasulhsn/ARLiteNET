# ActiveRecordNET
<strong>This project violates some design principles.</strong>

The main purpose of this library represents the RD table as an object via the Active-Record pattern.

- Relation between objects is not supported.
- Complex type completely not supported.
- Nested array is not supported.

Note: if you want to overcome the SRP problem, you can apply some design principles.

```csharp
public class User : AdoObjectProxy
{	    
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        // Internals
        private readonly AdoConnectionStringBuilder _connectionStringBuilder;

        public TestProxy(string connectionString, string providerName)
        {
            _connectionStringBuilder = new AdoConnectionStringBuilder()
                        .ConnectionString(connectionString)
                        .ProviderName(providerName);
        }

        public IEnumerable<User> GetAll()
        {
            return this.ReadRecords<User>(() =>
            {
                return _connectionStringBuilder
                        .CreateCommand()
                        .SetCommand("SELECT * FROM Users");
            });
        }
}

```
