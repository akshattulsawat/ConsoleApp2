// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;


HashSet<string> collection= new HashSet<string>();
collection.Add("akshat");
collection.Add("ravi");
collection.Add("aksht");
collection.Add("saksham");

//for(int i=0; i<collection.Count; i++)
//{
//    Console.WriteLine($"this value of string {collection.ToString()}");
//}

foreach (var item in collection)
{
    Console.WriteLine($"this value of string {item.ToString()}");
}


String[] arr = {"geeksforgeeks", "geeks",
                    "geek", "geezer","a"};
int n = arr.Length;

String ans = commonPrefix(arr, n);

if (ans.Length > 0)
{
    Console.Write("The longest common " +
                   "prefix is - " + ans);
}
else
{
    Console.Write("There is no common prefix");
}



static String commonPrefix(String[] arr, int n)
{
    String prefix = arr[0];

    for (int i = 1; i <= n - 1; i++)
    {
        prefix = commonPrefixUtil(prefix,
                     arr.GetValue(i).ToString());
    }

    return (prefix);
}

static String commonPrefixUtil(String str1,
                               String str2)
{
    String result = "";
    int n1 = str1.Length,
        n2 = str2.Length;

    // Compare str1 and str2 
    for (int i = 0, j = 0;
             i <= n1 - 1 && j <= n2 - 1;
             i++, j++)
    {
        if (str1[i] != str2[j])
        {
            break;
        }
        result += str1[i];
    }

    return (result);
}

//Item item = JsonFileReader.Read<Item>(@"C:\Users\Shaksham\source\repos\ConsoleApp2\ConsoleApp2\myFile.json");

//if (item != null)
//{
//    Console.WriteLine("Name :    " + item.Name.ToString());
//    Console.WriteLine("Roll No :    " + item.RollNo.ToString());
//    if (item.Subject != null)
//    {
//        foreach (var itm in item.Subject)
//        {
//            Console.WriteLine();
//            Console.WriteLine("Subject Started");
//            Console.WriteLine();
//            Console.WriteLine("Subject Name :    " + itm.Name.ToString());
//            Console.WriteLine("Subject Title :    " + itm.Title.ToString());
//            Console.WriteLine("Subject Publisher :    " + itm.Publisher.ToString());
//            Console.WriteLine("Subject Price :    " + itm.Price.ToString());
//            Console.WriteLine();
//            Console.WriteLine("Subject End");
//            Console.WriteLine();
//        }
//    }
//}
Console.ReadKey();



public class Item
{
    public string Name { get; set; }
    public string RollNo { get; set; }
    [JsonProperty("Subject")]
    [JsonConverter(typeof(SingleValueArrayConverter<Subject>))]
    public Subject[] Subject { get; set; }
}

public class Subject    
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string Price { get; set; }
}

public static class JsonFileReader
{
    public static T Read<T>(string filePath)
    {
        string text = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(text);
    }
}

public class SingleValueArrayConverter<T> : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartObject
            || reader.TokenType == JsonToken.String
            || reader.TokenType == JsonToken.Integer)
        {
            return new T[] { serializer.Deserialize<T>(reader) };
        }
        return serializer.Deserialize<T[]>(reader);
    }

    public override bool CanConvert(Type objectType)
    {
        return true;
    }
}