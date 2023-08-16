namespace TESTWebApp.Models
{
    public class SignUpViewModel
    {
        public string NewUserName { get; set; } = string.Empty;
        public bool HasErr { get; set; } = false;
        public string ErrMsg { get; set; } = string.Empty;
        public string InfoMsg { get; set; } = string.Empty;
    }
}
