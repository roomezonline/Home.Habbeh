﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Home.Habbeh.Entity;
using Home.Habbeh.Entity.Common;
using System.Net.Mail;
using System.Net;

namespace Home.Habbeh.Business
{
    public static class User
    {
        public static void Register(TbUser user)
        {
            using (DataAccess.User userData = new DataAccess.User())
            {
                Entity.TbUser newUser = new TbUser();
                newUser.UserName = user.UserName;
                newUser.Email = user.Email;
                newUser.Password = user.Password;
                newUser.Status = "I am Online In Habbeh";
                newUser.RegisterDate = DateTime.Now;

                /*Check Required Fields UserName,Email,Password*/
                if (string.IsNullOrEmpty(user.UserName)) { throw new HabbeException("نام کاربری اجباری است"); }
                if (string.IsNullOrEmpty(user.Email)) { throw new HabbeException("ایمیل اجباری است"); }
                if (string.IsNullOrEmpty(user.Password)) { throw new HabbeException("رمز عبور اجباری است"); }

                /*Check Duplicate UserName*/
                if (userData.Retrieve(user.UserName) != null)
                {
                    throw new HabbeException("نام کاربری تکراری است");
                }

                /*Check Duplicate Email */
                if (userData.RetrieveByEmail(user.Email) != null)
                {
                    throw new HabbeException("ایمیل تکراری است");
                }

                /*Create User*/
                userData.Create(newUser);
            }

            /*Send Verification Email*/
            SendEmail(user.Email, EmailType.Verification);
        }

        public static void SendForgiveInformation(string email)
        {
            SendEmail(email, EmailType.Forgive);
        }

        public static TbUser Login(string userName, string password)
        {
            using (DataAccess.User userData = new DataAccess.User())
            {
                TbUser user = userData.Retrieve(userName, password);

                /*clear password before send to client*/
                if (user != null)
                    user.Password = null;

                return user;
            }
        }

        public static TbUser GetProfile(int userId)
        {
            if (userId > 0)
            {
                using (DataAccess.User db = new DataAccess.User())
                {
                    TbUser user =  db.Retrieve(userId);
                    if (user == null)
                    {
                        throw new HabbeException("پروفایل کاربر یافت نشد");
                    }
                    return user;
                }
            }
            else
            {
                throw new HabbeException("شناسه کاربر نامعتبر است");
            }
        }

        public static void SaveProfile(TbUser user)
        {
            using (DataAccess.User db = new DataAccess.User())
            {
                db.Update(user);
            }
        }

        public static List<TbUser> Search(string searchText)
        {
            using (DataAccess.User db = new DataAccess.User())
            {
                return db.RetrieveList(searchText);
            }
        }

        public static void ChangeStatus(string userName, int statusId)
        {
            using (DataAccess.User db = new DataAccess.User())
            {
                db.Update(userName, statusId);
            }
        }

        public static void Follow(string userName, string followerUserName)
        {
            using (DataAccess.User db = new DataAccess.User())
            {
                TbUser user = db.Retrieve(userName);

                TbUser follower = db.Retrieve(followerUserName);

                if (user != null && follower != null)
                {
                    using (DataAccess.UserFollow ufDb = new DataAccess.UserFollow())
                    {
                        ufDb.Create(user.Id, follower.Id);
                    }
                }
            }
        }

        private static void SendEmail(string email, EmailType emailType)
        {
            //TODO: Email Service, background Thread for send email 

            /*TODO : Create some recored in database*/

            switch (emailType)
            {
                case EmailType.Forgive:
                    using (DataAccess.User userData = new DataAccess.User())
                    {
                        TbUser user = userData.RetrieveByEmail(email);


                        SendEmail("<html><body><b>UserName :   <font color=red>" + user.UserName + "</font> <br> <font color=black> Password :</font> <font color=red>" + user.Password + "</font><br><font color=blue> Habbeh Android Application</font></body></html> ", email, "ایمیل یاداوری");
                    }
                    break;
                case EmailType.Verification:
                    SendEmail("ارسال ایمیل تایید ثبت نام", email, "ایمیل تایید ثبت نام");
                    break;
            }
        }

        private static void SendEmail(string body, string email, string subject)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("habbeh.info@gmail.com");
            msg.To.Add(email);

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;
            //msg.Priority = MailPriority.High;


            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("habbeh.info@gmail.com", "habbeh_android");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(msg);
        }

        public static void ChangePassword(string userName, string oldPass, string newPass)
        {
            using (DataAccess.User db = new DataAccess.User())
            {
                TbUser user = db.Retrieve(userName, oldPass);
                if (user != null)
                {
                    user.Password = newPass;
                    db.UpdatePassword(user);
                }
                else
                {
                    throw new HabbeException("رمز عبور فعلی اشتباه است");
                }
            }
        }
    }
}
