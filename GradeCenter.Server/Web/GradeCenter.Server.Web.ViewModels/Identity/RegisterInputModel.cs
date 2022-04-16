namespace GradeCenter.Server.Web.ViewModels.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
