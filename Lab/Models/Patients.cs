//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patients()
        {
            this.Bio = new HashSet<Bio>();
        }
    
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patron { get; set; }
        public Nullable<System.DateTime> Birth { get; set; }
        public Nullable<int> Series { get; set; }
        public Nullable<int> Number { get; set; }
        public string TelNum { get; set; }
        public string EMail { get; set; }
        public string InsurancePolicy { get; set; }
        public Nullable<int> PolicyType { get; set; }
        public Nullable<int> InsuranceCompanyID { get; set; }
        public Nullable<int> PatientUserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bio> Bio { get; set; }
        public virtual PlicyTypes PlicyTypes { get; set; }
        public virtual Users Users { get; set; }
    }
}