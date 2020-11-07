using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    public class Films
    {
        public string city = null;
        public string cinema = null;
        public string movie = null;
        public string date = null;
        public string time = null;
        public string price = null;
        public Films() { }
        public Films(string[] data)
        {
            city = data[0];
            cinema = data[1];
            movie = data[2];
            date = data[3];
            time = data[4];
            price = data[5];
        }
        public bool Compare(Films obj)
        {
            if((this.city == obj.city) && (this.cinema == obj.cinema)
                && (this.movie == obj.movie) && (this.date == obj.date) 
                && (this.time == obj.time) && (this.price == obj.price))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
