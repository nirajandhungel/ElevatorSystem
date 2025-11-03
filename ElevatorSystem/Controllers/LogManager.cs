using ElevatorSystem.Database;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ElevatorSystem.Controllers
{
    public class LogManager
    {
        private readonly DatabaseManager dbManager;
        private readonly DataGridView logGrid;
        private readonly BackgroundWorker logWorker;

        public LogManager(DatabaseManager db, DataGridView grid)
        {
            dbManager = db;
            logGrid = grid;

            logWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            logWorker.DoWork += LogWorker_DoWork;
            logWorker.ProgressChanged += LogWorker_ProgressChanged;
            logWorker.RunWorkerCompleted += LogWorker_RunWorkerCompleted;

            ConfigureDataGridView();
            RefreshLogs();
        }

        private void ConfigureDataGridView()
        {
            logGrid.AutoGenerateColumns = false;
            logGrid.AllowUserToAddRows = false;
            logGrid.AllowUserToDeleteRows = false;
            logGrid.ReadOnly = true;
            logGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            logGrid.MultiSelect = false;
            logGrid.RowHeadersVisible = false;

            // Configure columns
            logGrid.Columns.Clear();
            logGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Timestamp",
                HeaderText = "Time",
                DataPropertyName = "Timestamp",
                Width = 150
            });
            logGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Type",
                HeaderText = "Type",
                DataPropertyName = "Type",
                Width = 100
            });
            logGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Message",
                HeaderText = "Activity",
                DataPropertyName = "Message",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            logGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            logGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            logGrid.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        public void LogActivity(string message, string logType)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (!logWorker.IsBusy)
                {
                    logWorker.RunWorkerAsync(new LogEntry
                    {
                        Timestamp = timestamp,
                        Message = message,
                        LogType = logType
                    });
                }
                else
                {
                    // If worker is busy, log synchronously
                    dbManager.InsertLog(timestamp, message, logType);
                    RefreshLogs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Logging error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LogEntry entry = e.Argument as LogEntry;
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                worker.ReportProgress(30, "Logging activity...");

                // Insert log
                dbManager.InsertLog(entry.Timestamp, entry.Message, entry.LogType);
                worker.ReportProgress(70, "Activity logged...");

                // Retrieve updated logs
                DataTable logs = dbManager.GetRecentLogs(100);
                worker.ReportProgress(100, "Logs updated...");

                e.Result = logs;
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void LogWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Progress updates can be handled here if needed
        }

        private void LogWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show($"Logging error: {e.Error.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Result is DataTable dt)
            {
                UpdateLogGrid(dt);
            }
            else if (e.Result is Exception ex)
            {
                MessageBox.Show($"Logging error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshLogs()
        {
            try
            {
                DataTable logs = dbManager.GetRecentLogs(100);
                UpdateLogGrid(logs);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLogGrid(DataTable logs)
        {
            if (logGrid.InvokeRequired)
            {
                logGrid.Invoke(new Action(() => UpdateLogGrid(logs)));
                return;
            }

            logGrid.DataSource = logs;

            // Apply color coding based on log type
            foreach (DataGridViewRow row in logGrid.Rows)
            {
                if (row.Cells["Type"].Value != null)
                {
                    string logType = row.Cells["Type"].Value.ToString();
                    row.Cells["Type"].Style.ForeColor = GetLogTypeColor(logType);
                    row.Cells["Type"].Style.Font = new Font(
                        logGrid.Font, FontStyle.Bold);
                }
            }

            // Scroll to bottom to show latest logs
            if (logGrid.Rows.Count > 0)
            {
                logGrid.FirstDisplayedScrollingRowIndex = logGrid.Rows.Count - 1;
            }
        }

        private Color GetLogTypeColor(string logType)
        {
            return logType switch
            {
                "INFO" => Color.Green,
                "REQUEST" => Color.Blue,
                "MOVEMENT" => Color.DarkBlue,
                "DOOR" => Color.Orange,
                "ARRIVAL" => Color.DarkGreen,
                "STATE" => Color.Purple,
                "QUEUE" => Color.Teal,
                "WARNING" => Color.DarkOrange,
                "ERROR" => Color.Red,
                "EMERGENCY" => Color.DarkRed,
                _ => Color.Black
            };
        }

        public void ClearLogs()
        {
            try
            {
                dbManager.ClearLogs();
                RefreshLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing logs: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportLogs(string filePath)
        {
            try
            {
                DataTable logs = dbManager.GetAllLogs();

                string extension = Path.GetExtension(filePath).ToLower();

                if (extension == ".csv")
                {
                    ExportToCsv(logs, filePath);
                }
                else
                {
                    ExportToText(logs, filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Export failed: {ex.Message}", ex);
            }
        }

        private void ExportToCsv(DataTable logs, string filePath)
        {
            StringBuilder csv = new StringBuilder();

            // Add headers
            csv.AppendLine("Timestamp,Message,Type");

            // Add data rows
            foreach (DataRow row in logs.Rows)
            {
                csv.AppendLine($"\"{row["Timestamp"]}\",\"{row["Message"]}\",\"{row["Type"]}\"");
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        private void ExportToText(DataTable logs, string filePath)
        {
            StringBuilder txt = new StringBuilder();

            txt.AppendLine("=====================================");
            txt.AppendLine("  ELEVATOR SYSTEM ACTIVITY LOG");
            txt.AppendLine($"  Generated: {DateTime.Now}");
            txt.AppendLine($"  Total Entries: {logs.Rows.Count}");
            txt.AppendLine("=====================================");
            txt.AppendLine();

            foreach (DataRow row in logs.Rows)
            {
                txt.AppendLine($"[{row["Timestamp"]}] [{row["Type"]}]");
                txt.AppendLine($"  {row["Message"]}");
                txt.AppendLine();
            }

            File.WriteAllText(filePath, txt.ToString());
        }

        private class LogEntry
        {
            public string Timestamp { get; set; }
            public string Message { get; set; }
            public string LogType { get; set; }
        }
    }
}