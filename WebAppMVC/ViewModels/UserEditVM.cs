namespace WebAppMVC.ViewModels
{
    public class UserEditVM
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public string PageTitle
        {
            get
            {
                return $"Edit Account";
            }
            set { }
        }
    }
}
