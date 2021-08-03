using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Net.Mime;
using System.Collections.Generic;
using GameStore_Entidades;
using System.Security.Cryptography;

namespace GameStore_Presentacion.Models
{
    public class Tools
    {
        static readonly string password = "G4MeSt0R3";

        #region elementos_para_el_carrito
        public static List<tb_Factura_Det> cart = new List<tb_Factura_Det>();
        public static int number_items = 0;
        public static decimal igv = 0;
        public static decimal subtotal_sale = 0;
        public static decimal total_sale = 0;
        #endregion

        public static object sendMail(string nombres, string idFac, string ttFac, string correo)
        {
            try
            {
                StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/html/TemplateEmail.html"));
                string readFile = reader.ReadToEnd();
                string StrContent = "";
                StrContent = readFile;

                StrContent = StrContent.Replace("{NOMBRES}", nombres);
                StrContent = StrContent.Replace("{NUMFACTURA}", idFac);
                StrContent = StrContent.Replace("{TOTALF}", ttFac);

                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(StrContent, null, MediaTypeNames.Text.Html);

                LinkedResource pic1 = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/Logotipo_Tienda_GameStore.jpg"), MediaTypeNames.Image.Jpeg);
                pic1.ContentId = "Pic1";
                avHtml.LinkedResources.Add(pic1);

                LinkedResource pic2 = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/QR.jpg"), MediaTypeNames.Image.Jpeg);
                pic2.ContentId = "Pic2";
                avHtml.LinkedResources.Add(pic2);

                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(new MailAddress(correo, "GameStore", Encoding.UTF8));
                    mail.From = new MailAddress("mogolloncarlosalberto23@gmail.com", "GAME STORE", Encoding.UTF8);
                    mail.Subject = "Se realizo tu Pedido en Game Store";
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.Body = StrContent;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(avHtml);
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.UseDefaultCredentials = true;
                    // crear credencial de autenticacion
                    smtp.Credentials = new NetworkCredential("mogolloncarlosalberto23@gmail.com", "Alejandrina2002");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return "0";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static object sendMailRegister(string nombres, string apellidos, string user, string pass, string correo)
        {
            try
            {
                StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/html/TemplateEmailRegistroEmple.html"));
                string readFile = reader.ReadToEnd();
                string StrContent = "";
                StrContent = readFile;

                StrContent = StrContent.Replace("{NOMBRES}", $"{nombres} {apellidos}");
                StrContent = StrContent.Replace("{USER}", user);
                StrContent = StrContent.Replace("{PASSWORD}", pass);

                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(StrContent, null, MediaTypeNames.Text.Html);

                LinkedResource pic1 = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/Logotipo_Tienda_GameStore.jpg"), MediaTypeNames.Image.Jpeg);
                pic1.ContentId = "Pic1";
                avHtml.LinkedResources.Add(pic1);

                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(new MailAddress(correo, "GameStore", Encoding.UTF8));
                    mail.From = new MailAddress("mogolloncarlosalberto23@gmail.com", "GAME STORE", Encoding.UTF8);
                    mail.Subject = $"Bienvenido a Game Sotre {nombres}";
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.Body = StrContent;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(avHtml);
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.UseDefaultCredentials = true;
                    // crear credencial de autenticacion
                    smtp.Credentials = new NetworkCredential("mogolloncarlosalberto23@gmail.com", "Alejandrina2002");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return "0";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static object sendMailRegisterClient(string nombres, string apellidos, string pass, string correo)
        {
            try
            {
                StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Content/html/TemplateEmailRegistroClient.html"));
                string readFile = reader.ReadToEnd();
                string StrContent = "";
                StrContent = readFile;

                StrContent = StrContent.Replace("{NOMBRES}", $"{nombres} {apellidos}");
                StrContent = StrContent.Replace("{USER}", correo);
                StrContent = StrContent.Replace("{PASSWORD}", pass);

                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(StrContent, null, MediaTypeNames.Text.Html);

                LinkedResource pic1 = new LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/Logotipo_Tienda_GameStore.jpg"), MediaTypeNames.Image.Jpeg);
                pic1.ContentId = "Pic1";
                avHtml.LinkedResources.Add(pic1);

                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(new MailAddress(correo, "GameStore", Encoding.UTF8));
                    mail.From = new MailAddress("mogolloncarlosalberto23@gmail.com", "GAME STORE", Encoding.UTF8);
                    mail.Subject = $"Bienvenido a Game Sotre {nombres}";
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.Body = StrContent;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(avHtml);
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.UseDefaultCredentials = true;
                    // crear credencial de autenticacion
                    smtp.Credentials = new NetworkCredential("mogolloncarlosalberto23@gmail.com", "Alejandrina2002");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return "0";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }



        public static string Encrypt(string plainText)
        {
            if (plainText == null)
            {
                return null;
            }
            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="encryptedText">String to be decrypted</param>
        /// <exception cref="FormatException"></exception>
        public static string Decrypt(string encryptedText)
        {
            if (encryptedText == null)
            {
                return null;
            }
            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}