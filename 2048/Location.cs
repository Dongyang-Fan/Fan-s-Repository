using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    /// <summary>
    /// 记录行列索引
    /// </summary>
    struct Location
    {
        public int RIndex { get; set; }
        public int CIndex { get; set; }

        public Location(int RIndex, int CIndex) : this()
        {
            this.RIndex = RIndex;
            this.CIndex = CIndex;
        }
    }
}
