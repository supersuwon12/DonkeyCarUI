using System.Text.Json.Serialization;

namespace DonkeyCarUI
{
    // Donkeycar Data Record Model
    public class DonkeyRecord
    {
        [JsonPropertyName("cam/image_array")]
        public string ImagePath { get; set; } = string.Empty;

        [JsonPropertyName("user/angle")]
        public double Angle { get; set; }

        [JsonPropertyName("user/throttle")]
        public double Throttle { get; set; }
    }
}
