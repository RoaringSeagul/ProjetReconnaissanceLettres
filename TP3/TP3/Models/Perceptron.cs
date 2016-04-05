namespace TPARCHIPERCEPTRON
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Perceptron")]
    public partial class SavedPerceptron
    {
        [Key]
        public int PerceptronID { get; set; }

        [Required]
        [StringLength(1)]
        public string LettresPerceptron { get; set; }

        [Required]
        [StringLength(256)]
        public string BitArray { get; set; }
    }
}
