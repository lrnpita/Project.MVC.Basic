namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Purchase")]
    public partial class Purchase : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PurchaseId { get; set; }

        [StringLength(50)]
        public string PurchaseDescription { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
