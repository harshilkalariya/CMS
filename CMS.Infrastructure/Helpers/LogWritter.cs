namespace CMS.Infrastructure
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;

    /// <summary>
    /// This class is used to write log files.
    /// </summary>
    /// <CreatedBy>Harshil Kalariya</CreatedBy>
    /// <CreatedDate>8 Nov 2020</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewedBy></ReviewedBy>
    /// <ReviewedDate></ReviewedDate>
    public class LogWritter
    {
        #region Methods

        /// <summary>
        /// write log files for exception
        /// </summary>
        /// <param name="ex">ex value</param>
        /// <param name="userName">userName value</param>
        /// <param name="platform">platform value</param>
        /// <param name="isForMobile">isForMobile value</param>
        /// <param name="isForAdmin">isForAdmin value</param>
        public static void WriteErrorFile(Exception ex, string userName = "", string platform = "", bool isForMobile = false, bool isForAdmin = false)
        {
            if (ex != null && !string.IsNullOrEmpty(ex.Message) && !ex.Message.Contains("Thread was being aborted."))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("DateTime = " + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYHHMMSS.GetDisplayName()) + System.Environment.NewLine);
                sb.Append("UserName = " + userName + ", Platform = " + platform + ", IsFromMobile = " + isForMobile + ", IsForAdmin = " + isForAdmin + System.Environment.NewLine);
                sb.Append("ExceptionMesage = " + ex.Message + System.Environment.NewLine);

                if (ex.InnerException != null)
                {
                    sb.Append("Inner Exception = " + ex.InnerException + System.Environment.NewLine);
                }

                sb.Append("Exception Source = " + ex.Source + System.Environment.NewLine);
                sb.Append("ExceptionStack = " + ex.StackTrace + System.Environment.NewLine);

                if (ex.Message.Contains("The HTTP request is unauthorized with client authentication scheme 'Anonymous'"))
                {
                    WriteErrorFileForAnonymousAuthError(sb.ToString(), true);
                }
                else
                {
                    WriteErrorFile(sb.ToString(), true);
                }
            }
        }

        /// <summary>
        /// write log files for error text
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        public static void WriteErrorFile(string textTowrite, bool isNewLine)
        {
            try
            {
                string fileName = "error_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                IOManager.CreateDirectory(HttpContext.Current.Server.MapPath("~/Error"));

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Error");
                string txtPath = txtFolderPath + "/" + fileName;

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + textTowrite);

                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// writes error file for Anonymous Authorization Error, just to prevent that error from flooding the crucial error log file.
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        public static void WriteErrorFileForAnonymousAuthError(string textTowrite, bool isNewLine)
        {
            try
            {
                string fileName = "auth_error_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                IOManager.CreateDirectory(HttpContext.Current.Server.MapPath("~/Error"));

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Error");
                string txtPath = txtFolderPath + "/" + fileName;

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + textTowrite);

                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// write log files for error text
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        public static void WriteErrorFileForService(string textTowrite, bool isNewLine)
        {
            try
            {
                string fileName = "error_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Error");
                string txtPath = txtFolderPath + "/" + fileName;

                IOManager.CreateDirectory(txtFolderPath);

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + textTowrite);

                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// write log files for error text
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        public static void WriteLogFileForServices(string textTowrite, bool isNewLine)
        {
            try
            {
                string fileName = "log_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Log");
                IOManager.CreateDirectory(txtFolderPath);
                string txtPath = txtFolderPath + "/" + fileName;

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYY.GetDisplayName() + " HH:mm:ss") + "   " + textTowrite);

                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, System.Environment.NewLine + "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// WriteQuotes
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        public static void WriteQuotes(string textTowrite, bool isNewLine)
        {
            try
            {
                string fileName = "QuoteLog_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                IOManager.CreateDirectory(HttpContext.Current.Server.MapPath("~/QuoteLogs"));

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "QuoteLogs");
                string txtPath = txtFolderPath + "/" + fileName;

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + textTowrite);

                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// WriteWPResponseLog
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        /// <param name="referenceNumber">referenceNumber</param>
        public static void WriteWPResponseLog(string textTowrite, bool isNewLine, string referenceNumber)
        {
            try
            {
                string fileName = "WPResponseLog_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                IOManager.CreateDirectory(HttpContext.Current.Server.MapPath("~/WPResponseLog"));

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "WPResponseLog");
                string txtPath = txtFolderPath + "/" + fileName;

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + "Date : " + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYY.GetDisplayName() + " HH:mm:ss") + "   " + System.Environment.NewLine + "Reference Number :" + referenceNumber + "   " + System.Environment.NewLine + textTowrite);
                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// WriteQuotes
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        public static void WriteIBANAPILog(string textTowrite, bool isNewLine)
        {
            try
            {
                string fileName = "IBANLog_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                IOManager.CreateDirectory(HttpContext.Current.Server.MapPath("~/IBANLogs"));

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "IBANLogs");
                string txtPath = txtFolderPath + "/" + fileName;

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + textTowrite);

                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// WriteQuotes
        /// </summary>
        /// <param name="textTowrite">Text To write value</param>
        /// <param name="isNewLine">is NewLine value</param>
        public static void WriteBankTransactionLog(string textTowrite, bool isNewLine)
        {
            try
            {
                string fileName = "BankTransaction_" + DateTime.Now.ToString(EnumHelpers.DATEFORMAT.DDMMYYYYFile.GetDisplayName()) + ".txt";

                IOManager.CreateDirectory(HttpContext.Current.Server.MapPath("~/BankTransactionLogs"));

                string txtFolderPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BankTransactionLogs");
                string txtPath = txtFolderPath + "/" + fileName;

                if (IOManager.CreateFile(txtPath))
                {
                    File.AppendAllText(txtPath, System.Environment.NewLine + textTowrite);

                    if (isNewLine)
                    {
                        File.AppendAllText(txtPath, "---------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
