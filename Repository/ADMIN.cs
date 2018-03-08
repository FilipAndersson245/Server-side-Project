namespace Repository
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ADMINS")]
    public partial class ADMIN
    {
        [Key]
        [StringLength(64)]
        public string Username { get; set; }

        [Required]
        [StringLength(64)]
        public string Salt { get; set; }

        [Required]
        [StringLength(64)]
        public string PasswordHash { get; set; }

        public int PermissionLevel { get; set; }
    }
}