namespace CRMManagement.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class EmailHelper
    {
        /// <summary>
        /// Sending An Email with master mail template
        /// </summary>
        /// <param name="mailFrom">Mail From</param>
        /// <param name="mailTo">Mail To</param>
        /// <param name="mailCC">Mail CC</param>
        /// <param name="mailBCC">Mail BCC</param>
        /// <param name="subject">Subject of mail</param>
        /// <param name="body">Body of mail</param>
        /// <param name="attachment">Attachment for the mail</param>
        /// <param name="emailType">Email Type</param>
        /// <returns>return send status</returns>
        public static bool Send(string mailTo, string mailCC, string mailBCC, string subject, string body, List<byte[]> attachmentFile = null, List<string> attachmentName = null)
        {
            Boolean issent = true;
            string mailFrom;
            mailFrom = Configurations.FromEmailAddress;
            mailBCC = Configurations.BccEmailAddress;
            try
            {
                if (!string.IsNullOrWhiteSpace(mailFrom) && !string.IsNullOrWhiteSpace(mailTo))
                {
                    MailMessage mailMesg = new MailMessage();
                    SmtpClient objSMTP = new SmtpClient();

                    objSMTP.Host = Configurations.EmailHost;

                    objSMTP.EnableSsl = Convert.ToBoolean(Configurations.EnableSsl);

                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                    NetworkCred.UserName = Configurations.EmailUserName; //reading from web.config  

                    NetworkCred.Password = Configurations.EmailPassword; //reading from web.config  

                    objSMTP.UseDefaultCredentials = true;                   

                    objSMTP.Credentials = new System.Net.NetworkCredential(NetworkCred.UserName, NetworkCred.Password);

                    objSMTP.Port = int.Parse(Configurations.Port);

                    mailMesg.From = new System.Net.Mail.MailAddress(mailFrom);
                    mailMesg.To.Add(mailTo);

                    if (!string.IsNullOrEmpty(mailCC))
                    {
                        string[] mailCCArray = mailCC.Split(';');
                        foreach (string email in mailCCArray)
                        {
                            mailMesg.CC.Add(email);
                        }
                    }

                    if (!string.IsNullOrEmpty(mailBCC))
                    {
                        mailBCC = mailBCC.Replace(";", ",");
                        mailMesg.Bcc.Add(mailBCC);
                    }

                    if (attachmentFile != null && attachmentName != null)
                    {
                        byte[][] Files = attachmentFile.ToArray();
                        string[] Names = attachmentName.ToArray();
                        if (Files != null)
                        {
                            for (int i = 0; i < Files.Length; i++)
                            {
                                mailMesg.Attachments.Add(new Attachment(new MemoryStream(Files[i]), Names[i]));
                            }
                        }
                    }

                    mailMesg.Subject = subject;
                    mailMesg.Body = body;
                    mailMesg.IsBodyHtml = true;

                    try
                    {
                        objSMTP.Send(mailMesg);
                        issent = true;
                        return issent;
                    }
                    catch (Exception ex)
                    {
                        mailMesg.Dispose();
                        mailMesg = null;
                        //CommonService.Log(e);
                        issent = false;
                        return issent;
                    }
                    finally
                    {
                        mailMesg.Dispose();
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Method is used to Validate Email
        /// </summary>
        /// <param name="fromEmail">From email List</param>
        /// <param name="toEmail">To Email list</param>
        /// <returns>Returns validation result</returns>
        private static bool ValidateEmail(string fromEmail, string toEmail)
        {
            bool isValid = true;
            if (!IsEmail(fromEmail))
            {
                isValid = false;
            }

            if (!string.IsNullOrEmpty(toEmail))
            {
                toEmail = toEmail.Replace(" ", string.Empty);
                string[] emailList = null;
                try
                {
                    emailList = toEmail.Split(',');
                }
                catch
                {
                    isValid = false;
                }

                if (emailList != null && emailList.Count() > 0)
                {
                    foreach (string email in emailList)
                    {
                        if (!IsEmail(email))
                        {
                            isValid = false;
                        }
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Check email string is Email or not
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>return email validation result</returns>
        private static bool IsEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Send1(string mailTo, string mailCC, string mailBCC, string subject, string body, System.Net.Mail.Attachment Attach1 )
        {
            Boolean issent = true;
            string mailFrom;
            mailFrom = Configurations.FromEmailAddress;
            mailBCC = Configurations.BccEmailAddress;
            try
            {
                if (!string.IsNullOrWhiteSpace(mailFrom) && !string.IsNullOrWhiteSpace(mailTo))
                {
                    MailMessage mailMesg = new MailMessage();
                    SmtpClient objSMTP = new SmtpClient();

                    objSMTP.Host = Configurations.EmailHost;

                    objSMTP.EnableSsl = Convert.ToBoolean(Configurations.EnableSsl);

                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                    NetworkCred.UserName = Configurations.EmailUserName; //reading from web.config  

                    NetworkCred.Password = Configurations.EmailPassword; //reading from web.config  

                    objSMTP.UseDefaultCredentials = true;

                    objSMTP.Credentials = new System.Net.NetworkCredential(NetworkCred.UserName, NetworkCred.Password);

                    objSMTP.Port = int.Parse(Configurations.Port);

                    mailMesg.From = new System.Net.Mail.MailAddress(mailFrom);
                    mailMesg.To.Add(mailTo);

                    if (!string.IsNullOrEmpty(mailCC))
                    {
                        string[] mailCCArray = mailCC.Split(';');
                        foreach (string email in mailCCArray)
                        {
                            mailMesg.CC.Add(email);
                        }
                    }

                    if (!string.IsNullOrEmpty(mailBCC))
                    {
                        mailBCC = mailBCC.Replace(";", ",");
                        mailMesg.Bcc.Add(mailBCC);
                    }

                    if (Attach1 != null)
                    {
                       mailMesg.Attachments.Add(Attach1);
                    }

                    mailMesg.Subject = subject;
                    mailMesg.Body = body;
                    mailMesg.IsBodyHtml = true;

                    try
                    {
                        objSMTP.Send(mailMesg);
                        issent = true;
                        return issent;
                    }
                    catch (Exception ex)
                    {
                        mailMesg.Dispose();
                        mailMesg = null;
                        //CommonService.Log(e);
                        issent = false;
                        return issent;
                    }
                    finally
                    {
                        mailMesg.Dispose();
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        public static  System.Net.Mail.Attachment AddAttachment(string DetailsSubject, string DetailsHTML, string BeginDate, string EndDate)
        {
            StringBuilder sb = new StringBuilder();
            string DateFormat = "yyyyMMddTHHmmssZ";
            string DateFormat2 = "MM/dd/yyyy HH:mm:ss";
            string now = DateTime.Now.ToUniversalTime().ToString(DateFormat);

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Compnay Inc//Product Application//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:PUBLISH");
                        
            DateTime dtStart = DateTime.ParseExact(BeginDate, DateFormat2, CultureInfo.InvariantCulture).AddHours(9) ; //Convert.ToDateTime(BeginDate);
            DateTime dtEnd = DateTime.ParseExact(EndDate, DateFormat2, CultureInfo.InvariantCulture).AddHours(9).AddMinutes(30) ; // Convert.ToDateTime(EndDate);

            sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine("DTSTART:" + dtStart.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTEND:" + dtEnd.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTSTAMP:" + now);
                sb.AppendLine("UID:" + Guid.NewGuid());
                sb.AppendLine("CREATED:" + now);
                sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + DetailsHTML);
                //sb.AppendLine("DESCRIPTION:" + res.Details);
                sb.AppendLine("LAST-MODIFIED:" + now);
                sb.AppendLine("LOCATION: N/A" );
                sb.AppendLine("SEQUENCE:0");
                sb.AppendLine("STATUS:CONFIRMED");
                sb.AppendLine("SUMMARY: " + DetailsSubject);
                sb.AppendLine("TRANSP:OPAQUE");
                sb.AppendLine("END:VEVENT");
            
            sb.AppendLine("END:VCALENDAR");

            var calendarBytes = Encoding.UTF8.GetBytes(sb.ToString());
            MemoryStream ms = new MemoryStream(calendarBytes);
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(ms, "event.ics", "text/calendar");
            return attachment; 
        } 
    }
}
