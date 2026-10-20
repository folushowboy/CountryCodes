namespace CountryCodes.Dtos
{
    public class CountryLookupResponseDto
    {
        public string Number { get; set; } = string.Empty;
        public CountryResponseDto Country { get; set; } = new();
    }
}
