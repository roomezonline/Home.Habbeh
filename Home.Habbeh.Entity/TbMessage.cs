﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Home.Habbeh.Entity
{
    public class TbMessage
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public String CategoryTitle { get; set; }
        public int UserId { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime SendDate { get; set; }
        public string Description { get; set; }
        public int Share { get; set; }

        public static TbMessage ToEntity(System.Data.IDataReader reader)
        {
            TbMessage msg = null;
            msg = new TbMessage();
            msg.Id = Convert.ToInt32(reader["Id"]);
            msg.CategoryId = Convert.ToInt32(reader["CategoryId"]);
            msg.UserId = Convert.ToInt32(reader["UserId"]);
            msg.Description = Convert.ToString(reader["Description"]);
            msg.RegisterDate = Convert.ToDateTime(reader["RegisterDate"]);
            if (reader["SendDate"] != DBNull.Value)
                msg.SendDate = Convert.ToDateTime(reader["SendDate"]);
            msg.CategoryTitle = Convert.ToString(reader["CategoryTitle"]);
            if (reader["Share"] != DBNull.Value)
                msg.Share = Convert.ToInt32(reader["Share"]);
            return msg;
        }
    }
}
