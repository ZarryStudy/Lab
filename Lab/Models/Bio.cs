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
    
    public partial class Bio
    {
        public int BioID { get; set; }
        public string BioTitle { get; set; }
        public Nullable<int> BioPatientID { get; set; }
        public Nullable<int> BioCount { get; set; }
    
        public virtual Patients Patients { get; set; }
    }
}