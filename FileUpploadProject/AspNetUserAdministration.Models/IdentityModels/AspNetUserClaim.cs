using System;
using System.ComponentModel.DataAnnotations;

namespace FileUpload.Models.IdentityModels
{
    public partial class AspNetUserClaim
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
