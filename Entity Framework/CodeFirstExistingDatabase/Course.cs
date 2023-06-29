namespace CodeFirstExistingDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Tags = new HashSet<Tags>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

      
        public int Level { get; set; }

        public float FullPrice { get; set; }

        public int? Auther_Id { get; set; }

        public virtual Authers Authers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tags> Tags { get; set; }
    }
}
