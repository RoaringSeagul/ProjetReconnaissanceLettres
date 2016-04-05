using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TPARCHIPERCEPTRON
{
    public partial class BDPerceptron : DbContext
    {
        public BDPerceptron()
            : base("name=BDPerceptron")
        {
        }

        public virtual DbSet<SavedPerceptron> Perceptrons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
