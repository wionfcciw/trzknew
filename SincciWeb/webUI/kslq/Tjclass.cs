using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SincciKC.webUI.kslq
{
    public class Tjclass
    {
        private string status;
        private string msg;
        private string pzt;
        private int cj = 0;

        public int Cj
        {
            get { return cj; }
            set { cj = value; }
        }
        public string Pzt
        {
            get { return pzt; }
            set { pzt = value; }
        }
        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private DataTable dt = new DataTable();

        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; }
        }
    }
}