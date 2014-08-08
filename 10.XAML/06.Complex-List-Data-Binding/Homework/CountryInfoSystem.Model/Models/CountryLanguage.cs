using System;
using System.Collections.Generic;

namespace CountryInfoSystem.Model.Models
{
    public partial class CountryLanguage
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Language { get; set; }
        public bool IsOfficial { get; set; }
        public float Percentage { get; set; }
        public virtual Country Country { get; set; }
    }
}
