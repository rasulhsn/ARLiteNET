# ARLiteNET

ARLiteNet is a .NET Standard library designed to access to any relational database with Micro-ORM features. Through its declarative approach, it provides maximum abstraction that simplifies access to any relational database. The main purpose of the library is to provide a simple CRUD operation through the Active Record Pattern.



### Support
* Nested types
* Dynamic Configuration
* Type mapping
* Query Generation
* Flexiable Data Retrieve

### Configuration for SQLite3
🔴 Currently, the library only provides SQLite!

```csharp
public sealed class SQLiteConfigurationFactory : ARLiteConfigurationFactory
{
    protected override void Configure(ARLiteConnectionStringBuilder connectionStringBuilder)
    {
        connectionStringBuilder.SetSQLite3(@"D:\ARDatabase.db"); // basic sample!                              
    }
}
```

## Declarative Approach
```csharp
 [ARLiteConfiguration(typeof(SQLiteConfigurationFactory))]
 public class UserObject : ARLiteObject
 {
     public long Id { get; set; }
     public string Name { get; set; }
     public bool IsActive { get; set; }
     public DateTime BirthDate { get; set; }
     
    public IEnumerable<UserObject> GetAll()
    {
        // Declarative approach
        var selectQuery = base.Query()
                               .Select<UserObject>("Users");
    
        selectQuery.Column(x => x.Id).Only();
        selectQuery.Column(x => x.Name).Only();
        selectQuery.Column(x => x.IsActive).Only();
    
        return base.RunEnumerable<UserObject>(selectQuery);
    }
    
    public IEnumerable<UserObject> GetByName(string name)
    {
        // Declarative approach
        var selectQuery = this.Query()
                               .Select<UserObject>("Users");
    
        selectQuery.Column(x => x.Id).Only();
        selectQuery.Column(x => x.Name).Only();
        selectQuery.Column(x => x.IsActive).Only();
        selectQuery.Column(x => x.Name).EqualTo(name);
    
        return this.RunEnumerable<UserObject>(selectQuery);
    }
    
    public bool Add(UserObject newObject)
    {
        // Declarative approach
        var insertQuery = this.Query()
                                .Insert("Users", newObject);
    
        insertQuery.Column(x => x.Id).Ignore();
        insertQuery.Column(x => x.BirthDate).Ignore();
    
        int affectedRows = this.Run(insertQuery);
    
        return affectedRows == 1;
    }
    
    public bool Update(UserObject updateObject)
    {
        // Declarative approach
        var updateQuery = this.Query()
                                .Update("Users", updateObject);
    
        updateQuery.Column(x => x.BirthDate).Ignore();
        updateQuery.Column(x => x.Id).Ignore();
        updateQuery.Column(x => x.Id).EqualTo(updateObject.Id);
    
        int affectedRows = this.Run(updateQuery);
    
        return affectedRows >= 1;
    }
    
    public bool DeleteAll()
    {
        // Declarative approach
        var deleteQuery = this.Query()
                                .Delete<UserObject>("Users");
    
        int affectedRows = this.Run(deleteQuery);
    
        return affectedRows >= 1;
    }
    
    public bool DeleteByName(string name)
    {
        // Declarative approach
        var deleteQuery = this.Query()
                                .Delete<UserObject>("Users");
    
        deleteQuery.Column(x => x.Name).EqualTo(name);
    
        int affectedRows = this.Run(deleteQuery);
    
        return affectedRows >= 1;
    }
 }
```
## Raw SQL Approach
```csharp
[ARLiteConfiguration(typeof(SQLiteConfigurationFactory))]
public class UserObject : ARLiteObject
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime BirthDate { get; set; }

    public IEnumerable<UserObject> GetAll()
    {
        var queryBuilder = this.Query()
                          .SetCommand("SELECT * FROM Users");
    
        return this.RunEnumerable<UserObject>(queryBuilder);
    }
    
    public void Add(UserObject newObject)
    {
        var queryBuilder = this.Query()
            .SetCommand("INSERT INTO Users (Name, IsActive) VALUES (@name, @isActive)")
                .AddParam((param) =>
                {
                    param.ParameterName = "@name";
                    param.DbType = System.Data.DbType.String;
                    param.Value = newObject.Name;
                }).AddParam((param) => {
                    param.ParameterName = "@isActive";
                    //param.DbType = System.Data.DbType.Boolean;
                    param.Value = newObject.IsActive;
                });
    
        this.Run(queryBuilder);
    }
}
```

### License & Copyright

[ARLiteNET](https://github.com/rasulhsn/ARLiteNET) is Copyright © 2024 Rasul Huseynov and lincensed under the [MIT license](https://github.com/rasulhsn/ARLiteNET/blob/main/LICENSE).
