using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(1)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [MinLength(1)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [MinLength(6)]
        [Compare(nameof(Password), ErrorMessage = "Parolanız eşleşmiyor.")]
        public string ConfirmPassword { get; set; } = null!;


    }
}
