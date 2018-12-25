using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace books
{
    [Table("payment_history")]
    public class payment_log
    {
        public payment_log()
        {

        }
       
        public DateTime _When { get; set; }
        public Guid Item { get; set; }
        [Key]
        public Guid Client { get; set; }
        public int Quantity { get; set; }
    }
}