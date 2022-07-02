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
        private readonly string _connectionString;
        private readonly string _providerName;

        public User(string connectionString, string providerName)
        {
           _connectionString = connectionString;
           _providerName = providerName;
        }

        public IEnumerable<User> GetAll()
        {
            return this.ReadRecords<User>((builder) =>
            {
                builder.SetCommand("SELECT * FROM Users");
            });
        }
        
        protected override void Configure(AdoConnectionStringBuilder builder)
        {
            builder.ConnectionString(_connectionString)
                   .ProviderName(_providerName);
        }
}

```
