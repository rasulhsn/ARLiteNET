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
         var selectQuery = this.Query()
                                .Select<UserObject>("Users");

         return this.RunEnumerable<UserObject>(selectQuery);
     }

     public IEnumerable<UserObject> GetByName(string name)
     {
         // Declarative approach
         var selectQuery = this.Query()
                                .Select<UserObject>("Users");

         selectQuery.Column(x => x.Name).EqualTo(name);

         return this.RunEnumerable<UserObject>(selectQuery);
     }

     public void Add(UserObject newObject)
     {
         // Declarative approach
         var insertQuery = this.Query()
                                 .Insert("Users", newObject);

         insertQuery.Column(x => x.IsActive).Ignore();

         this.Run(insertQuery);
     }

     public void Update(UserObject newObject)
     {
         // Declarative approach
         var updateQuery = this.Query()
                                 .Update("Users", newObject);

         updateQuery.Column(nameof(UserObject.Id)).EqualTo(newObject.Id);
         updateQuery.Column(nameof(UserObject.IsActive)).Ignore(); // updateQuery.Column(x => x.IsActive).Ignore();
         
         this.Run(updateQuery);
     }

     public void Delete(int id)
     {
         // Declarative approach
         var deleteQuery = this.Query()
                                 .Delete<UserObject>("Users");

         deleteQuery.Column(nameof(UserObject.Id)).EqualTo(id);

         this.Run(deleteQuery);
     }
 }
```

## Strict Object Query Approach
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
        var selectQuery = base.Query()
                               .Object<UserObject>((queryBuilder) =>
                               {
                                   return queryBuilder.Select()
                                               .From("Users")
                                               .Where(nameof(UserObject.Name))
                                               .EqualTo("Rasul")
                                               .Or(nameof(UserObject.Id))
                                               .GreaterThan(2);
                               });

        return base.RunEnumerable<UserObject>(selectQuery);
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
        var selectQuery = base.Query()
                               .ObjectSelect<UserObject>((queryBuilder) =>
                               {
                                   return queryBuilder.Select()
                                               .From("Users")
                                               .Where(nameof(UserObject.Name))
                                               .EqualTo("Rasul")
                                               .Or(nameof(UserObject.Id))
                                               .GreaterThan(2);
                               });

        return base.RunEnumerable<UserObject>(selectQuery);
    }

    public void Add(UserDtoStub newObject)
    {
        var queryBuilder = this.Query()
                                .ObjectInsert<UserObject>("Users", (queryBuilder) =>
                                 {
                                     InsertValueObject[] insertValue =
                                     [
                                         new(nameof(newObject.Name), newObject.Name, InsertDataType.TEXT),
                                         new(nameof(newObject.IsActive), newObject.IsActive, InsertDataType.BOOLEAN)
                                     ];
                                     return queryBuilder.Value(insertValue);     
                                 });

        this.Run(queryBuilder);
    }
}
```

### License & Copyright

[ARLiteNET](https://github.com/rasulhsn/ARLiteNET) is Copyright © 2024 Rasul Huseynov and lincensed under the [MIT license](https://github.com/rasulhsn/ARLiteNET/blob/main/LICENSE).
