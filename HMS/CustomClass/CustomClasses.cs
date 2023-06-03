using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MCtick_new.CustomClass
{
    public class CustomClasses
    {
        public static void UserLog(Boolean ErrorLog, string logtype, string Page_Event, string Comment, string username)
        {
            string sLogFormat = String.Empty;
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            string strUserType = "U_TLog"; string strLogPath = "~/Log/";
            if (logtype == "User") { strUserType = "U_TLog"; } else if (logtype == "Govt") { strUserType = "G_TLog"; } else if (logtype == "Admin") { strUserType = "A_TLog"; } else if (logtype == "SAdmin") { strUserType = "S_TLog"; }
            if (ErrorLog == true)
            {
                strLogPath = "~/Log/Err/";
            }

            string URL = HttpContext.Current.Server.MapPath(strLogPath) + strUserType + System.DateTime.Now.Year.ToString("0000") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00");
            string msg = System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00") + ":" + System.DateTime.Now.Millisecond.ToString("000");

            msg += "|" + HttpContext.Current.Request.UserHostAddress.ToString();
            msg += "|" + username.ToString();
            msg += "|" + Page_Event;
            msg += "|" + Comment.ToString();
            Task asyncTask = WriteFileAsync(URL, msg);
        }
        static async Task WriteFileAsync(string file, string content)
        {
            using (StreamWriter outputFile = new StreamWriter(file, true))
            {
                await outputFile.WriteLineAsync(content);
            }
        }
        public static void LogNEr(string Comment)
        {
            string sLogFormat = String.Empty;
            string cc = string.Empty;
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("action"))
                cc = (string)routeValues["action"];
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            string URL = HttpContext.Current.Server.MapPath("~/Log/Er/") + "U_TLog" + System.DateTime.Now.Year.ToString("0000") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00");
            StreamWriter sw = new StreamWriter(URL, true);
            string msg = System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00");
            string username = "-";
            try { username = HttpContext.Current.Session["LoggedIn"].ToString(); }
            catch { }
            msg += "|" + HttpContext.Current.Request.UserHostAddress.ToString();
            msg += "|" + username.ToString();
            msg += "|" + cc;
            msg += "|" + Comment.ToString();
            sw.WriteLine(sLogFormat + msg.ToString());
            sw.Flush();
            sw.Close();
        }
        public static void ALogN(string Comment)
        {
            string sLogFormat = String.Empty;
            string cc = string.Empty;
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("action"))
                cc = (string)routeValues["action"];
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            string URL = HttpContext.Current.Server.MapPath("~/Log/") + "A_TLog" + System.DateTime.Now.Year.ToString("0000") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00");
            StreamWriter sw = new StreamWriter(URL, true);
            string msg = System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00");
            string username = "-";
            try { username = HttpContext.Current.Session["LoggedIn"].ToString(); }
            catch { }
            msg += "|" + HttpContext.Current.Request.UserHostAddress.ToString();
            msg += "|" + username.ToString();
            msg += "|" + cc;
            msg += "|" + Comment.ToString();
            sw.WriteLine(sLogFormat + msg.ToString());
            sw.Flush();
            sw.Close();
        }
        public static void ALogNEr(string Comment)
        {
            string sLogFormat = String.Empty;
            string cc = string.Empty;
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            if (routeValues.ContainsKey("action"))
                cc = (string)routeValues["action"];
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            string URL = HttpContext.Current.Server.MapPath("~/Log/Er/") + "AEr_TLog" + System.DateTime.Now.Year.ToString("0000") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00");
            StreamWriter sw = new StreamWriter(URL, true);
            string msg = System.DateTime.Now.Hour.ToString("00") + ":" + System.DateTime.Now.Minute.ToString("00") + ":" + System.DateTime.Now.Second.ToString("00");
            string username = "-";
            try { username = HttpContext.Current.Session["LoggedIn"].ToString(); }
            catch { }
            msg += "|" + HttpContext.Current.Request.UserHostAddress.ToString();
            msg += "|" + username.ToString();
            msg += "|" + cc;
            msg += "|" + Comment.ToString();
            sw.WriteLine(sLogFormat + msg.ToString());
            sw.Flush();
            sw.Close();
        }

       

        public static string SendMail(string toemailid, string username, string password)
        {
            string param = string.Format("toemailid:{0}", toemailid);
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(toemailid.ToString()));
                message.IsBodyHtml = true;
                message.Body = string.Format("<html><head></head><body><h4>Dear Sir/Madam,</h4><p>R-Tick account of your company</p><p>Your login details are : </p><p>User Name : {0}</p><p>Password : {1}</p><p>It is recommended that you change your password after the first login</p></body></html>", username, password);
                message.Subject = string.Format("Forgot Password Report");
                message.From = new MailAddress("monika@msai.in", "MSAI");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("monika@msai.in", "monika@1984");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                try
                {
                    client.Send(message);
                    return "true";
                }
                catch (Exception ex)
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                //  CustomClasses.LogWrite(ex.Message, "CustomClasses SendMail", "DB Hit", param, Convert.ToInt32(HttpContext.Current.Session["LoginIDItick"]));
                return ex.Message;
            }

        }

        //public static OTPDetails OTPGeneration(string MobileNo)
        //{
        //    //CustomClass.CustomClasses.TraceWrite("OTP Generate", "Customclasses OTPGeneration", "OTP Generate", string.Format("mobile:{0}", mobile), null);
        //    var response = CustomClass.CustomClasses.sendsmsOtp(MobileNo);
        //    var details = new OTPDetails();
        //    details.phone = MobileNo;
        //    details.status = response.Keys.FirstOrDefault();
        //    details.OTP = Convert.ToString(response.Values.FirstOrDefault());
        //    return details;
        //}

        public static Dictionary<string, string> sendsmsOtp(string tomobile)
        {
            //HttpContext.Current.Session["ResponseCode"] = null;
            var dic = new Dictionary<string, string>();
            Random rng = new Random();
            string OTP = Convert.ToString(rng.Next(100000, 999999));
            HttpContext.Current.Session["OTP"] = OTP;
            string param = string.Format("mobile:{0},otp:{1}", tomobile, OTP);
            try
            {
                string strUrl = string.Format("http://enterprise.smsgupshup.com/GatewayAPI/rest?method=SendMessage&send_to=Number{0}&msg=Your unique verification code for login is : {1}&msg_type=TEXT&userid=2000172734&auth_scheme=plain&password=z5p1KIBLL&v=1.1&format=json", tomobile, OTP);

                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpContext.Current.Session["ResponseCode"] = response;
               
            }
            catch (Exception ex)
            {
                
                if (ex.Message.Equals("Unexpected character encountered while parsing value: <. Path '', line 0, position 0."))
                {
                    dic.Add("Check Network Connection", "0");
                }
                else
                {
                    dic.Add(ex.Message, "0");
                }

            }
            dic.Add("success", OTP);
            return dic;
        }
    }
    public class Crypto
    {
        public static string Encrypt(string data, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC; //remember this parameter
            rijndaelCipher.Padding = PaddingMode.PKCS7; //remember this parameter

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;

            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(data);

            return Convert.ToBase64String
            (transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        public static string Decrypt(string data, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] encryptedData = Convert.FromBase64String(data);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;

            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock
                        (encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);
        }
       
    }
}