using System.Text.Json.Serialization;

namespace DonkeyCarUI
{
    /// <summary>
    /// Donkeycar 데이터 레코드 모델.
    /// Form1.cs 전체에서 사용하는 FrameData 클래스입니다.
    /// (기존 DonkeyRecord.cs의 DonkeyRecord를 대체/통합합니다)
    /// </summary>
    public class FrameData
    {
        // ── JSON 필드 ──────────────────────────────────────────────
        [JsonPropertyName("cam/image_array")]
        public string ImagePath { get; set; } = string.Empty;

        [JsonPropertyName("user/angle")]
        public double Angle { get; set; }

        [JsonPropertyName("user/throttle")]
        public double Throttle { get; set; }

        // ── 내부 메타 필드 (멀티 JSON 포맷용) ────────────────────
        /// <summary>멀티 JSON 포맷에서 원본 파일명을 기억합니다.</summary>
        [JsonIgnore]
        public string SourceFileName { get; set; } = string.Empty;

        // ── 깊은 복사 ─────────────────────────────────────────────
        /// <summary>Undo/Redo 히스토리 스냅샷에 필요한 깊은 복사본을 반환합니다.</summary>
        public FrameData Clone() => new FrameData
        {
            ImagePath = this.ImagePath,
            Angle = this.Angle,
            Throttle = this.Throttle,
            SourceFileName = this.SourceFileName
        };
    }
}