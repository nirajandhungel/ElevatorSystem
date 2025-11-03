using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ElevatorSystem.Database
{
    public static class DBConfig
    {
        public static string ConnectionString { get; } = "Server=localhost;Database=ElevatorSystemDB;Uid=root;Pwd=2005subash0910;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}