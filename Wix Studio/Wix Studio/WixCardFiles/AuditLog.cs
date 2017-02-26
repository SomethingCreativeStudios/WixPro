using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wix_Studio.WixCardFiles
{
    public static class AuditLog
    {
        public static string logPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Wix Cards\\logs\\";

        public static void log(String message , String logFileName = "wixStudioLog.txt" , bool clearLog = false)
        {
            try {
                String filePath = AuditLog.logPath + logFileName;
                StreamWriter writer = null;
                if ( !clearLog )
                    writer = File.AppendText(filePath);
                else
                    writer = File.CreateText(filePath);
                writer.WriteLine(message);
                writer.Close();
            }catch(Exception ex )
            {
                Debug.WriteLine(ex);
            }
        }

        public static void clear(String logFileName = "wixStudioLog.txt")
        {
            AuditLog.log("" , logFileName , true);
        }
    }
}
