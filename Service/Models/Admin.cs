namespace Service.Models
{
    public enum Rank
    {
        Editor = 0, //Can edit, create and delete books and authors.
        Admin,      //Editor + Can reset password.
        SuperAdmin  //Admin + Can create and delete admins.
    }

    public class Admin
    {
        public string Username { get; set; } //Primary Key

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public Rank PermissionLevel { get; set; }

        public bool CanEditClassifications { get; set; }
    }
}