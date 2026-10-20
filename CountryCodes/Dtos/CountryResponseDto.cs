namespace CountryCodes.Dtos
{
    public class CountryResponseDto
    {
        public string CountryCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string CountryIso { get; set; } = string.Empty;

        public List<CountryDetailResponseDto> CountryDetails { get; set; }
            = new();
    }
}
