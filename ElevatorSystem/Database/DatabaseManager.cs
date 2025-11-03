using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ElevatorSystem.Database
{
    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager()
        {
            connectionString = DBConfig.ConnectionString;
            InitializeDatabase();
        }

        public void InitializeDatabase()
        {
            try
            {
                CreateDatabaseIfNotExists();
                CreateTablesIfNotExist();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization error: {ex.Message}",
                              "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDatabaseIfNotExists()
        {
            string masterConnectionString = "Server=localhost;Uid=root;Pwd=2005subash0910;";

            using (var conn = new MySqlConnection(masterConnectionString))
            {
                conn.Open();
                string checkDbQuery = "CREATE DATABASE IF NOT EXISTS ElevatorSystemDB";
                using (var cmd = new MySqlCommand(checkDbQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CreateTablesIfNotExist()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // Create Logs table
                string createLogsTable = @"
                    CREATE TABLE IF NOT EXISTS ElevatorLogs (
                        LogID INT PRIMARY KEY AUTO_INCREMENT,
                        Timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        Message VARCHAR(500) NOT NULL,
                        Type VARCHAR(50) NOT NULL DEFAULT 'INFO',
                        CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                    )";

                // Create ElevatorStatus table
                string createStatusTable = @"
                    CREATE TABLE IF NOT EXISTS ElevatorStatus (
                        StatusID INT PRIMARY KEY AUTO_INCREMENT,
                        CurrentFloor INT NOT NULL,
                        State VARCHAR(50) NOT NULL,
                        Direction VARCHAR(20),
                        DoorsOpen BOOLEAN NOT NULL,
                        Timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                    )";

                // Create MaintenanceLog table
                string createMaintenanceTable = @"
                    CREATE TABLE IF NOT EXISTS MaintenanceLog (
                        MaintenanceID INT PRIMARY KEY AUTO_INCREMENT,
                        Description VARCHAR(500) NOT NULL,
                        PerformedBy VARCHAR(100),
                        MaintenanceDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        NextScheduledDate DATETIME
                    )";

                using (var cmd = new MySqlCommand(createLogsTable, conn))
                    cmd.ExecuteNonQuery();

                using (var cmd = new MySqlCommand(createStatusTable, conn))
                    cmd.ExecuteNonQuery();

                using (var cmd = new MySqlCommand(createMaintenanceTable, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        // Log Methods
        public int InsertLog(string timestamp, string message, string type)
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO ElevatorLogs (Timestamp, Message, Type) 
                               VALUES (@Timestamp, @Message, @Type);
                               SELECT LAST_INSERT_ID();";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Parse(timestamp));
                    cmd.Parameters.AddWithValue("@Message", message);
                    cmd.Parameters.AddWithValue("@Type", type);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public DataTable GetRecentLogs(int count)
        {
            var dt = new DataTable();
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = $@"SELECT LogID, Timestamp, Message, Type 
                                FROM ElevatorLogs 
                                ORDER BY LogID DESC 
                                LIMIT {count}";

                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            // Reverse to show oldest first
            DataTable reversed = dt.Clone();
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
                reversed.ImportRow(dt.Rows[i]);

            return reversed;
        }

        public DataTable GetAllLogs()
        {
            var dt = new DataTable();
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = "SELECT LogID, Timestamp, Message, Type FROM ElevatorLogs ORDER BY LogID";
                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public void ClearLogs()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM ElevatorLogs";
                using (var cmd = new MySqlCommand(query, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        public int GetLogCount()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM ElevatorLogs";
                using (var cmd = new MySqlCommand(query, conn))
                    return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public DataTable GetLogStatistics()
        {
            var dt = new DataTable();
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = @"SELECT Type, COUNT(*) as Count 
                               FROM ElevatorLogs 
                               GROUP BY Type 
                               ORDER BY Count DESC";
                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // Status Methods
        public void SaveElevatorStatus(int currentFloor, string state, string direction, bool doorsOpen)
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO ElevatorStatus (CurrentFloor, State, Direction, DoorsOpen) 
                               VALUES (@CurrentFloor, @State, @Direction, @DoorsOpen)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrentFloor", currentFloor);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@Direction", direction ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DoorsOpen", doorsOpen);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetStatusHistory(int records = 50)
        {
            var dt = new DataTable();
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = $@"SELECT * FROM ElevatorStatus 
                                ORDER BY StatusID DESC 
                                LIMIT {records}";
                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // Maintenance Methods
        public void InsertMaintenanceRecord(string description, string performedBy, DateTime? nextScheduledDate = null)
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO MaintenanceLog (Description, PerformedBy, NextScheduledDate) 
                               VALUES (@Description, @PerformedBy, @NextScheduledDate)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@PerformedBy", performedBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NextScheduledDate",
                        nextScheduledDate.HasValue ? (object)nextScheduledDate.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetMaintenanceHistory()
        {
            var dt = new DataTable();
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM MaintenanceLog ORDER BY MaintenanceDate DESC";
                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}