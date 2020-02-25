using System.IO;
using System.Xml.Serialization;

public static class Helper
{
    public static string Serialize<T>(this T toSerialize)
    {
        XmlSerializer xmlS = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xmlS.Serialize(writer, toSerialize);
        return writer.ToString();
    }

    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDeserialize);
        return (T) xml.Deserialize(reader);
    }
}
