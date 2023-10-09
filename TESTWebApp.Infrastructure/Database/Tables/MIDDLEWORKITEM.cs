using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTWebApp.Infrastructure.Database.Tables
{
    [Table("middle_work_item")]
    public class MIDDLEWORKITEM
    {
        [Key]
        [Column("middle_work_item_id")]
        public string Id { get; set; }

        [Column("middle_work_item_name")]
        public string MiddleWorkItemName { get; set; }

        [Column("major_work_item_id")]
        public string MajorWorkItemId { get; set; }

        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("modified_by")]
        public string ModifiedBy { get; set; }

        [Column("created")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [Column("modified")]
        [DataType(DataType.Date)]
        public DateTime Modified { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
