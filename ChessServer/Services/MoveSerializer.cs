using System.Formats.Asn1;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ChessLogic;
#pragma warning disable CS8603 // Possible null reference return.


namespace ChessServer.Services
{
    public class MoveSerializer : JsonConverter<Move>
    {
        public override Move Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                if (root.TryGetProperty("type", out var type))
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
            System.Text.Json.JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }

    public class PieceArrayConverter : JsonConverter<Piece[,]>
    {
        public override Piece[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var array = doc.RootElement.EnumerateArray();
                int rows = array.Count();
                int cols = array.First().GetArrayLength();

                Piece[,] result = new Piece[rows, cols];

                int i = 0;
                foreach (var row in array)
                {
                    int j = 0;
                    foreach (var piece in row.EnumerateArray())
                    {
                        result[i, j++] = JsonSerializer.Deserialize<Piece>(piece.GetRawText(), options);
                    }
                    i++;
                }

                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, Piece[,] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            for (int i = 0; i < value.GetLength(0); i++)
            {
                writer.WriteStartArray();

                for (int j = 0; j < value.GetLength(1); j++)
                {
                    JsonSerializer.Serialize(writer, value[i, j], options);
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
}
