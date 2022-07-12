# ActiveRecordNET
The main purpose of this library represents the RD table as an object via the Active-Record pattern.

- Relation between objects is not supported.
- Complex type completely not supported.
- Property array is not supported.

Note: if you want to overcome the SRP problem, you can apply some design principles.

```csharp
public class DefaultConfigurationFactory : AdoConfigurationFactory
{
	public override AdoConnectionString CreateConnectionString()
        {
		// Sample connection string.
		string connStr = "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";			
            	return AdoConnectionStringBuilder.ConnectionString(connStr)
                   	.MSSQL()
			.Build();
        }
}

[AdoConfiguration(typeof(DefaultConfigurationFactory))]
public class User : AdoObjectProxy
{	    
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<User> GetAll()
        {
            return this.RunEnumerable<User>((builder) =>
            {
                builder.SetCommand("SELECT * FROM Users");
            });
        }
		
	public void Add(User userObject)
        {
            this.Run((builder) =>
            {
                builder.SetCommand("INSERT INTO Users (Name, IsActive) VALUES (@name, @isActive)")
                    .AddParam((param) =>
                    {
                        param.ParameterName = "@name";
                        param.DbType = System.Data.DbType.String;
                        param.Value = userObject.Name;
                    }).AddParam((param) => {
                        param.ParameterName = "@isActive";
                        param.Value = userObject.IsActive;
                    });
            });
        }
}

```
