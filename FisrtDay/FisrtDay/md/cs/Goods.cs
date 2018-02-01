using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FisrtDay.md
{
    partial class Goods
    {
        Entity db = new Entity();
        public dynamic TotalCost
        {
            get
            {
                return Count * Convert.ToDouble(CostUnit);
            }
        }

        public string Category
        {
            get
            {
                return db.Categories.First(w => w.ID == CategoryID).Name;
            }
        }

        public string Unit
        {
            get
            {
                return db.Units.First(w => w.ID == UnitID).ShortName;
            }
        }

        public double t = 0;
        public double Count2
        {
            get
            {
                return t;
            }
            set
            {
                t = value;
            }
        } 
    }
}
