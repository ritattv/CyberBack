using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System;
using Ionic.Zip;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        public FileStreamResult Backup()
        {
            const string connectionString = "data source=localhost;initial catalog=cyberdb;integrated security=True;";
            const string dbName = "cyberdb";
            const string backupDir = @"C:\Users\Public\Downloads";

            var time = DateTime.Now;

            var backupFilenameWithoutExt = $"{backupDir}\\{dbName}_{time:yyyy-MM-dd_hh-mm-ss}";

            var backupFilenameWithExt = $"{backupFilenameWithoutExt}.bak";
            var zipFilename = $"{backupFilenameWithoutExt}.zip";

            var backupQuery = $"BACKUP DATABASE {dbName}\r\nTO DISK = '{backupFilenameWithExt}'";

            using SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();

            using (var sqlCommand = new SqlCommand(backupQuery, sqlConnection))
            {
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
            }

            using (var zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.AddFile(backupFilenameWithExt, @"\");
                zip.Save(zipFilename);
            }

            System.IO.File.Delete(backupFilenameWithExt);
            sqlConnection.Close();

            var fileStream = new FileStream(zipFilename, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/octet-stream", $"{dbName}_{time:yyyy-MM-dd_hh-mm-ss}.zip");
        }

        // GET: api/download
        [HttpGet]
        public FileStreamResult Get()
        {
            return Backup();
        }
    }
}