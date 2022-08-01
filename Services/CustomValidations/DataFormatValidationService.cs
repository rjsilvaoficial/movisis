using Newtonsoft.Json.Converters;

namespace MovisisCadastro.Services.CustomValidations
{
    public class DataFormatValidationService : IsoDateTimeConverter
    {
        public DataFormatValidationService(string format)
        {
            DateTimeFormat = format;
        }
    }
}
