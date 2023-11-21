using ChessLogic;
using ChessLogic.Moves;
using System.Text.Json;
using System.Text.Json.Serialization;
#pragma warning disable CS8603 // Possible null reference return.


namespace ChessServer.Services
{
    public class MoveConverter : JsonConverter<Move>
    {
        public override Move Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                if(root.TryGetProperty("Type", out var type))
                {
                MoveType moveType = Enum.Parse<MoveType>(type.ToString());
                return moveType switch
                {
                        MoveType.Normal => JsonSerializer.Deserialize<NormalMove>(root.GetRawText(), options),
                        MoveType.EnPassant => JsonSerializer.Deserialize<EnPassant>(root.GetRawText(), options),
                        MoveType.PawnPromotion => JsonSerializer.Deserialize<PawnPromotion>(root.GetRawText(), options),
                        MoveType.CastleKS => JsonSerializer.Deserialize<CastleKs>(root.GetRawText(), options),
                        MoveType.CastleQS => JsonSerializer.Deserialize<CastleQs>(root.GetRawText(), options),
                        _ => throw new JsonException($"Unsupported MoveType: {moveType}"),
                    };
                }
                throw new JsonException("MoveType property not found");
            }
        }

        public override void Write(Utf8JsonWriter writer, Move value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
