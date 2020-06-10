using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verstoppertje
{
    class Switch
    {
        private int idx;
        private string status;
        private string name;
        private string switchType;

        public int Idx { get => this.idx; set => this.idx = value; }
        public string Status { get => this.status; set => this.status = value; }
        public string Name { get => this.name; set => this.name = value; }
        public string SwitchType { get => this.switchType; set => this.switchType = value; }
        public Switch(Dictionary<string, object> switche)
        {
            this.idx = Int32.Parse(switche["idx"].ToString());
            this.status = switche["Status"].ToString();
            this.name = switche["Name"].ToString();
            this.switchType = switche["SwitchType"].ToString();
        }
        public override string ToString()
        {
            return name +" : "+ status+"  \tid:"+idx;
        }
    }
}
