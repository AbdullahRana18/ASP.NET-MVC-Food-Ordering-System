//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace foodPandaDBMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrderFood
    {
        public int OrderID { get; set; }
        public int FoodID { get; set; }
        public int Quantity { get; set; }
    
        public virtual tblFood tblFood { get; set; }
        public virtual tblOrder tblOrder { get; set; }
    }
}
