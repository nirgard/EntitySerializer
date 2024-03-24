using System.Reflection;

namespace nivwer.EntityMapSerializer;

public class EntityMapSerializer<T>
{
    public Dictionary<string, object?> SerializeToMap(T entity)
    {   
        Dictionary<string, object?> map = new Dictionary<string, object?>();

        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            string propertyName = property.Name;
            object? propertyValue = property.GetValue(entity);

            map.Add(propertyName, propertyValue);
        }

        return map;
    }
}


/*
class Program
{
    static void Main(string[] args)
    {
        EntitySerializer<Persona> serializer = new EntitySerializer<Persona>();
        Persona persona = new Persona { Nombre = "Juan", Edad = 30 };

        // Serializar objeto Persona a un mapa (diccionario)
        Dictionary<string, object> mapa = serializer.SerializeToMap(persona);

        // Imprimir el mapa serializado
        foreach (var kvp in mapa)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
} 
*/
