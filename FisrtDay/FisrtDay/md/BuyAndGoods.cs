//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FisrtDay.md
{
    using System;
    using System.Collections.Generic;
    
    public partial class BuyAndGoods
    {
        public int ID { get; set; }
        public int BuyID { get; set; }
        public int GoodsID { get; set; }
        public double Count { get; set; }
    
        public virtual Buy Buy { get; set; }
        public virtual Goods Goods { get; set; }
    }
}