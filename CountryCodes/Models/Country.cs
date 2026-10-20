using System.ComponentModel.DataAnnotations;

namespace CountryCodes.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Country code is required")]
        public int CountryCode { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryIso { get; set; } = string.Empty;
        public List<CountryDetail> CountryDetails { get; set; } = new();
    }
}



