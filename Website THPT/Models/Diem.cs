using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website_THPT.Models
{
    public class Diem
    {
        public KETQUA ketqua { get; set; }
        public MONHOCCHITIET monhocct { get; set; }
        public MONHOC monhoc { get; set; }
        public HOCSINH hocsinh { get; set; }
        public HOCKY hocky { get; set; }
        public double DiemTB { get; set; }
    }
}