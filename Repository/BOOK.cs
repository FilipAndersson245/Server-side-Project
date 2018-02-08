namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("BOOK")]
    public partial class BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOK()
        {
            AUTHORs = new HashSet<AUTHOR>();
        }

        [Key]
        [StringLength(15)]
        public string ISBN { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public int? SignId { get; set; }

        [StringLength(10)]
        public string PublicationYear { get; set; }

        [StringLength(255)]
        public string publicationinfo { get; set; }

        public short? pages { get; set; }

        public virtual CLASSIFICATION CLASSIFICATION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTHOR> AUTHORs { get; set; }
    }
}
