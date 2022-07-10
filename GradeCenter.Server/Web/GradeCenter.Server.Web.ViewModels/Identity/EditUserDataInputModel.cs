namespace GradeCenter.Server.Web.ViewModels.Identity
{
    using System;

    public class EditUserDataInputModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Password { get; set; }
    }
}
