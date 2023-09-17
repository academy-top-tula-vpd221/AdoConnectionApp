using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=work_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
//string connectionString2 = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

/*
SqlConnection connection = new SqlConnection(connectionString);

try
{
    await connection.OpenAsync();
    Console.WriteLine("Connection is open");
}
catch(SqlException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    if(connection.State == System.Data.ConnectionState.Open)
    {
        await connection.CloseAsync();
        Console.WriteLine("Connection is close");
    }
}
*/

//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    await connection.OpenAsync();
//    Console.WriteLine("Connection is open\n");
//    Console.WriteLine($"connection string:\t{connection.ConnectionString}");
//    Console.WriteLine($"server:\t{connection.DataSource}");
//    Console.WriteLine($"database:\t{connection.Database}");
//    Console.WriteLine($"server version:\t{connection.ServerVersion}");
//    Console.WriteLine($"connection string:\t{connection.State}");
//    Console.WriteLine($"client id:\t{connection.ClientConnectionId}");
//}
//SqlConnection.ClearAllPools();

using (SqlConnection connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();
    Console.WriteLine("Connection is open\n");

    SqlCommand command = connection.CreateCommand(); // new SqlCommand("", connection);

    //command.CommandText = "CREATE DATABASE work_db COLLATE Cyrillic_General_CI_AS";
    //command.CommandText = @"CREATE TABLE authors
    //(
    //    id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    //    last_name NVARCHAR(50) NOT NULL,
    //    first_name NVARCHAR(50) NULL,
    //    birth_date DATE NULL
    //)";

    //command.CommandText = @"INSERT INTO authors 
    //                (last_name, first_name)
    //                VALUES('Достоевский', 'Федор')";

    //int count = await command.ExecuteNonQueryAsync();
    //Console.WriteLine($"count of modified lines: {count}");

    command.CommandText = "SELECT * FROM authors";

    using (SqlDataReader reader = await command.ExecuteReaderAsync())
    {
        if (reader.HasRows)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                Console.Write($"\t{reader.GetName(i)}");
            Console.WriteLine();

            while (await reader.ReadAsync())
            {
                //for (int i = 0; i < reader.FieldCount; i++)
                //    Console.Write($"\t{reader.GetValue(i)}");
                //Console.Write($"\t{reader[i]}");
                //Console.Write($"\t{reader["first_name"]}\t{reader["last_name"]}");
                Console.Write($"\t{reader.GetInt32(0)}\t{reader.GetString(1)}{((reader.GetString(1).Length < 8) ? "\t" : "")}\t{reader.GetString(2)}{((reader.GetString(2).Length < 8) ? "\t" : "")}\t{reader.GetDateTime(3).ToLongDateString()}");
                Console.WriteLine();
            }

        }
    }
}

Console.WriteLine("Connection is close");