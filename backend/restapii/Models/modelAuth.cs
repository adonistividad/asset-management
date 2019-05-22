using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace restapii.Models
{
    public enum MODE_SIGN { MODE_REGISTER = 1, MODE_LOGIN = 2 }
    public class modelAuth
    {
        public MODE_SIGN mode { get; set; }
        public string bruid { get; set; }
        public string name { get; set; }
        public string user { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string token { get; set; }
        public string message { get; set; }
    }

    public class modelTest
    {

        public String test;

        String getTest() { return test; }
        void setTest(String test) { this.test = test; }

    }
    public class Test
    {
        public string field1 { get; set; }
        public string field2 { get; set; }
    } 
}