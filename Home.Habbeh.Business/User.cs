﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Home.Habbeh.Entity;

namespace Home.Habbeh.Business
{
    public class User
    {
        public void Register(string username, string email, string password)
        {
            SendEmail(email, "Verification");
            new TbUser() { Email = "RoomezOnline@yahoo.com", UserName = "roomezonline", StatusId = 1 };
        }

        public void SendForgiveInformation(string email)
        {
            SendEmail(email, "Forgive");
        }

        public TbUser Login(string username, string password)
        {
            return new TbUser() { Email = "RoomezOnline@yahoo.com", UserName = "roomezonline", StatusId = 1 };
        }

        public TbUser GetProfile(int userId)
        {
            return new TbUser() { Email = "RoomezOnline@yahoo.com", UserName = "roomezonline", StatusId = 1 };
        }

        public List<TbUser> Search(string searchText)
        {
            return new List<TbUser>() { 
                new TbUser() { Email = "RoomezOnline@yahoo.com", UserName = "roomezonline", StatusId = 1 },
                new TbUser() { Email = "karim_medusa@yahoo.com", UserName = "karim_medusa", StatusId = 1 }};
        }

        public void ChangeStatus(int userId, int statusCode,int changerUserId)
        {

        }

        public void Follow(int userId, int followerId)
        {

        }

        private void SendEmail(string email, string data)
        {

        }

    }
}