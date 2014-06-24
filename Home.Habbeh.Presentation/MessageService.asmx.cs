﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Home.Habbeh.Entity;

namespace Home.Habbeh.Presentation
{
    /// <summary>
    /// Summary description for MessageService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MessageService : System.Web.Services.WebService
    {

        [WebMethod]
        public string Ping()
        {
            return "Hello Message Service!";
        }

        [WebMethod]
        public void InsertMessage(int categoryId, int userId, string description)
        {
            TbMessage msg = new TbMessage();
            msg.CategoryId = categoryId;
            msg.UserId = userId;
            msg.Description = description;

            Business.Message.Create(msg);
        }

        [WebMethod]
        public bool LikeMessage(int userId, int messageId)
        {
            TbLike like = new TbLike();
            like.UserId = userId;
            like.MessageId = messageId;
            like.Rank = 1;

            return Business.Like.Create(like);
        }

        [WebMethod]
        public List<TbMessage> ReadMessage(String lastUpdateMessage)
        {
            return Business.Message.ReadMessage(lastUpdateMessage);
        }

        [WebMethod]
        public int CountNewMessage(String LastReadMessage)
        {
            return Business.Message.CountNewMessage(LastReadMessage);
        }

        [WebMethod]
        public int CountLike(int messageid)
        {
            return Business.Like.CountLike(messageid);
        }

        [WebMethod]
        public List<TbCategory> RetrieveCategoryList()
        {
            return Business.Category.RetrieveList();
        }

        [WebMethod]
        public void SendMessage(int messageId)
        {
            Business.Message.SendMessage(messageId);
        }
    }
}
