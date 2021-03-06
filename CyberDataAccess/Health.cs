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
    
    public partial class Health
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Health()
        {
            this.Stats = new HashSet<Stats>();
        }
    
        public int health_id { get; set; }
        public Nullable<double> health_temperature { get; set; }
        public Nullable<double> health_pulse { get; set; }
        public Nullable<int> health_systolicPressure { get; set; }
        public Nullable<int> health_diastolicPressure { get; set; }
        public Nullable<int> player_id { get; set; }
    
        public virtual Player Player { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stats> Stats { get; set; }
    }
}
