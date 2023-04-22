using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

public class TwoDimensionalArrayConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsArray && objectType.GetArrayRank() == 2 && objectType.GetElementType() == typeof(int);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var array = JArray.Load(reader);

        int rows = array.Count;
        int columns = array[0].Count();

        var result = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = array[i][j].Value<int>();
            }
        }

        return result;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        int[,] array = (int[,])value;

        writer.WriteStartArray();

        for (int i = 0; i < array.GetLength(0); i++)
        {
            writer.WriteStartArray();

            for (int j = 0; j < array.GetLength(1); j++)
            {
                writer.WriteValue(array[i, j]);
            }

            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }
}
