# ARLiteNET

ARLiteNet is a .NET Standard library designed to simplify access to any SQLite database. Through its declarative approach, it provides maximum abstraction that simplifies access to any SQLite database. The main purpose of the library is to provide a simple CRUD operation through the Active Record Pattern.

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

### License & Copyright

[ARLiteNET](https://github.com/rasulhsn/ARLiteNET) is Copyright © 2022-2024 Rasul Huseynov and lincensed under the [MIT license](https://github.com/rasulhsn/ARLiteNET/blob/main/LICENSE).
