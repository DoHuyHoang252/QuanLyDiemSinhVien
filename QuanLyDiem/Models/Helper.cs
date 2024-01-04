using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyDiem.Models
{
    public class Helper
    {
        public double ChuyenDoiDiemHe4 (double diem){
            if (diem >= 9.0) return 4.0;
            else if (diem >= 8.5) return 3.7;
            else if (diem >= 8.0) return 3.5;
            else if (diem >= 7.0) return 3.0;
            else if (diem >= 6.5) return 2.5;
            else if (diem >= 5.5) return 2.0;
            else if (diem >= 5.0) return 1.5;
            else if (diem >= 4.0) return 1.0;
            else return 0.0;
        }
        public string ChuyenDoiDiemChu (double diem){
            if (diem >= 9.0) return "A+";
            else if (diem >= 8.5) return "A";
            else if (diem >= 8.0) return "B+";
            else if (diem >= 7.0) return "B";
            else if (diem >= 6.5) return "C+";
            else if (diem >= 5.5) return "C";
            else if (diem >= 5.0) return "D+";
            else if (diem >= 4.0) return "D";
            else return "F";
        }
    }
}