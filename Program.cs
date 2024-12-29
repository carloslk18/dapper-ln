using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using Dapper;
using Data_Access.Models;
using Microsoft.Data.SqlClient;

namespace Data_Access{

class Program{

    public static void Main(string[] args){

        const string connectionString = "Server=localhost,1433;Database=Balta;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True";

        using (var connection = new SqlConnection(connectionString)){

            //CreateCategory(connection);
            //ListCategories(connection);
            //CreateStudent(connection);
            //CreateManyCategories(connection);
            //UpdateCategory(connection);
            //DeleteCategory(connection);
            //ExecuteProcedure(connection);
            //ExecuteReadProcedure(connection);
            //ExecuteScalar(connection);
            //ReadView(connection);
            //OneToOne(connection);
            //QueryMultiple(connection);
            //SelectIn(connection);
            //Like(connection);
            Transactions(connection);
            
        }
    }
       
    public static void ListCategories(SqlConnection connection){

        var categories = connection.Query <Category> ( "SELECT * FROM [Category]");
        foreach (var item in categories ){
            Console.WriteLine($"{item.Id} - {item.Title} - {item.Url} - {item.Summary} - {item.Order} - {item.Description} - {item.Featured}");
        }
    }

    public static void CreateCategory(SqlConnection connection){

        var category = new Category();

        category.Id = Guid.NewGuid();
        category.Title = "MS Azure";
        category.Url = "Microsoft";
        category.Summary = "Azure Cloud";
        category.Order = 1;
        category.Description = "Categoria destinada a serviços Azure";
        category.Featured = true;

        var insertSql = "INSERT INTO [Category] VALUES (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

        var rows = connection.Execute(insertSql, new{
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });

            Console.WriteLine($"{rows} rows inserted");
    }

    public static void CreateStudent(SqlConnection connection){
        var student = new Student();
        student.Id = Guid.NewGuid();
        student.Name = "Carlos Barbosa";
        student.Email = "carloslk18@ms.com";
        student.Document = "Pararatibum";
        student.Phone =  6184836659;
        student.Birthdate = new DateTime(1993,1,18);
        student.Createdate = DateTime.Now;
        
        var sql = "INSERT INTO [Student] VALUES (@Id, @Name, @Email, @Document, @Phone, @Birthdate, @Createdate)";
    
        var rows = connection.Execute(sql, new{
            student.Id,
            student.Name,
            student.Email,
            student.Document,
            student.Phone,
            student.Birthdate,
            student.Createdate

        });

        Console.WriteLine("{0} rows affected",rows);
    }
    public static void UpdateCategory(SqlConnection connection){

        var updateQuerry = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";
        var rows = connection.Execute(updateQuerry, new{
            id = "af3407aa-11ae-4621-a2ef-2028b85507c4",
            title = "Frontend"
        });

        Console.WriteLine("{0}rows updated",rows);

    }

   public static void DeleteCategory(SqlConnection connection){

        var sql = "DELETE [Student] WHERE [Id] in (@Id, @Id2)";
        var rows = connection.Execute(sql, new{
            Id = "00000000-0000-0000-0000-000000000000",
            Id2 = "00000000-0000-0000-0000-000000000000"
        });

        Console.WriteLine("{0}rows updated",rows);
    
    }

    public static void CreateManyCategories(SqlConnection connection){
        var category = new Category();

        category.Id = Guid.NewGuid();
        category.Title = "MS Azure";
        category.Url = "Microsoft";
        category.Summary = "Azure Cloud";
        category.Order = 1;
        category.Description = "Categoria destinada a serviços Azure";
        category.Featured = true;

        var category2 = new Category();

        category2.Id = Guid.NewGuid();
        category2.Title = "Cat2";
        category2.Url = "Cat2";
        category2.Summary = "Cat2";
        category2.Order = 2;
        category2.Description = "Cat2";
        category2.Featured = false;

        var insertSql = "INSERT INTO [Category] VALUES (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

        var rows = connection.Execute(insertSql, new[]{
            new{
            category.Id,
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        },
            new{
            category2.Id,
            category2.Title,
            category2.Url,
            category2.Summary,
            category2.Order,
            category2.Description,
            category2.Featured
        }});

            Console.WriteLine($"{rows} rows inserted");
    }

    public static void ExecuteProcedure(SqlConnection connection){

        var sql = "[spDeleteStudent]";
        var pars = new {StudentId = "afacbe44-66ed-4ba0-bb9a-db3bb45c7824"};

        var rows = connection.Execute(sql, pars, commandType: CommandType.StoredProcedure);

        Console.WriteLine($"{rows} rows affected");
    }

    public static void ExecuteReadProcedure(SqlConnection connection){

        var sql = "[spGetCoursesByCategory]";
        var pars = new {CategoryId = "af3407aa-11ae-4621-a2ef-2028b85507c4"};
        var courses = connection.Query(sql,pars,commandType: CommandType.StoredProcedure);

        foreach (var item in courses){
            Console.WriteLine(item.Title);
        }
    }

    public static void ExecuteScalar(SqlConnection connection){

        var category = new Category();

        category.Title = "MS Azure4";
        category.Url = "Microsoft";
        category.Summary = "Azure Cloud3";
        category.Order = 1;
        category.Description = "Categoria destinada a serviços Azure";
        category.Featured = true;

        var insertSql = "INSERT INTO [Category] OUTPUT INSERTED.[Id] VALUES (NEWID(), @Title, @Url, @Summary, @Order, @Description, @Featured) SELECT SCOPE_IDENTITY()";

        var id = connection.ExecuteScalar<Guid>(insertSql, new{
            category.Title,
            category.Url,
            category.Summary,
            category.Order,
            category.Description,
            category.Featured
        });

            Console.WriteLine($"{id} rows inserted");
    }

    public static void ReadView(SqlConnection connection){
        var sql = "Select * FROM [vwCourses]";
        var courses = connection.Query(sql);

        foreach(var item in courses){
            Console.WriteLine($"{item.Id} - {item.Title}");
        }
    }

    public static void OneToOne(SqlConnection connection){
        var sql = "SELECT * FROM [CareerItem] JOIN [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

        var items = connection.Query(sql);

        foreach(var item in items){
            
            Console.WriteLine(item);
        }
    }

    public static void QueryMultiple(SqlConnection connection){

        var sql = "SELECT * FROM [Course]; SELECT * FROM [Category]";

            //forma otimizada de using
            using var multi = connection.QueryMultiple(sql);

            var courses = multi.Read<Course>();
            var categories = multi.Read<Category>();

            foreach (var item in courses)
            {
                Console.WriteLine(item.Title);
            }

            foreach (var item in categories)
            {
                Console.WriteLine(item.Title);
            }
    }

    public static void SelectIn (SqlConnection connection){
        var sql = "SELECT * FROM [Career] WHERE [Id] IN @Id";

        var items = connection.Query<Career>(sql, new{
            Id = new[]{
                "01ae8a85-b4e8-4194-a0f1-1c6190af54cb",
                "e6730d1c-6870-4df3-ae68-438624e04c72"
            }
        });

        foreach (var i in items){
            Console.WriteLine(i.Title);
        }
    }    

    public static void Like (SqlConnection connection){
        var sql = "SELECT * FROM [Course] WHERE [Title] LIKE @exp";

        var items = connection.Query<Course>(sql, new{
            exp = "%backend%"
                    
        });

        foreach (var i in items){
            Console.WriteLine(i.Title);
        }
    }

    public static void Transactions(SqlConnection connection){
        var student = new Student();
        student.Id = Guid.NewGuid();
        student.Name = "NaoEParaSalvar";
        student.Email = "NaoEParaSalvar";
        student.Document = "NaoEParaSalvar";
        student.Phone =  6184836659;
        student.Birthdate = new DateTime(1993,1,18);
        student.Createdate = DateTime.Now;
        
        var sql = "INSERT INTO [Student] VALUES (@Id, @Name, @Email, @Document, @Phone, @Birthdate, @Createdate)";
    
            connection.Open();
            using (var transaction = connection.BeginTransaction()){

                var rows = connection.Execute(sql, new{
                student.Id,
                student.Name,
                student.Email,
                student.Document,
                student.Phone,
                student.Birthdate,
                student.Createdate,
            },transaction);

            transaction.Commit();
            //transaction.Rollback();

            Console.WriteLine($"{rows} rows inserted");
        }        
    }
}
}