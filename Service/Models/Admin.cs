namespace Service.Models
{
    public enum Rank
    {
        Editor = 0, //do all books / author
        Admin,      //also reset password
        SuperAdmin  //create / delete admins
    }

    public class Admin
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public Rank PermissionLevel { get; set; }

        public bool? CanValidateClassification { get; set; }
    }
}