using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTWebApp.Infrastructure.Database.Tables
{
    [Table("work_input")]
    public class WORKINPUT
    {
        [Key]
        [Column("work_input_id")]
        public string Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("work_item_id")]
        public string MajorWorkItemId { get; set; }

        [Column("middle_work_item_id")]
        public string MiddleWorkItemId { get; set; }

        [Column("minor_work_item_id")]
        public string MinorWorkItemId { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("time_stamp")]
        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }
    }
}
