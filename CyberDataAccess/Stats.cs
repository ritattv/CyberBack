//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CyberDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stats
    {
        public int stats_id { get; set; }
        public Nullable<double> stats_median { get; set; }
        public Nullable<int> stats_mode { get; set; }
        public Nullable<double> stats_mean { get; set; }
        public Nullable<double> stats_stdev { get; set; }
        public Nullable<int> health_id { get; set; }
    
        public virtual Health Health { get; set; }
    }
}
