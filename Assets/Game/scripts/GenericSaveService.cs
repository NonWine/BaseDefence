using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class GenericSaveService<T>
{
    private readonly string _filePath;
    private JsonSerializerSettings _serializerSettings;
    
    public GenericSaveService(string fileName)
    {
        _filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");
        _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ContractResolver = new IncludeReadonlyFieldsContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

    }

    public void SaveData(T data)
    {

        string json = JsonConvert.SerializeObject(data, Formatting.Indented,
            _serializerSettings);
        File.WriteAllText(_filePath, json);
        Debug.Log($"Data saved to {_filePath}");
    }

    public T LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<T>(json, _serializerSettings);
        }
        else
        {
            
            Debug.LogWarning($"No save file found at {_filePath}");
            return default; 
        }
    }
}

public class IncludeReadonlyFieldsContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var properties = base.CreateProperties(type, memberSerialization);

        // Добавляем только уникальные поля
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .Where(field => field.IsInitOnly || field.IsPublic);

        foreach (var field in fields)
        {
            if (properties.All(p => p.PropertyName != field.Name))
            {
                properties.Add(new JsonProperty
                {
                    PropertyName = field.Name,
                    PropertyType = field.FieldType,
                    ValueProvider = new ReflectionValueProvider(field),
                    Readable = true,
                    Writable = true
                });
            }
        }

        return properties;
    }
}

