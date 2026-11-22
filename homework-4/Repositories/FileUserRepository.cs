using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using homework_4.Models;

namespace homework_4.Repositories;

public class FileUserRepository : IUserRepository
{
    private List<User> _cache = null;
    private readonly string _filePath = "D:\\TOP\\ASP_NET\\Homeworks\\homework-4\\data\\users.json";
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
    };
    
    public List<User> GetUsers()
    {
        EnsureCacheLoaded();
        
        return _cache;
    }

    public User? GetUser(int id)
    {
        EnsureCacheLoaded();
        
        return _cache.FirstOrDefault(u => u.Id == id);
    }

    public User Save(User user)
    {
        // List<User> users = GetUsers();
        EnsureCacheLoaded();
        
        if (user.Id == -1)
        {
            user.Id = _cache.Count > 0 ? _cache.Count + 1 : 1;
            _cache.Add(user);
            
        }
        else
        {
            int index = _cache.FindIndex(u => u.Id == user.Id);
            _cache[index] = user;
        }
        
        StorageUsers();
        return user;
    }

    public void DeleteUser(int id)
    {
        EnsureCacheLoaded();
        int removed = _cache.RemoveAll(u => u.Id == id);

        if (removed > 0)
            StorageUsers();
    }

    private void EnsureCacheLoaded()
    {
        if (_cache == null)
        {
            if (!File.Exists(_filePath))
                _cache = [];

            string json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                _cache = [];

            _cache = JsonSerializer.Deserialize<List<User>>(json, _jsonOptions) ?? [];
        }
    }
    
    private void StorageUsers()
    {
        string? directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        string json = JsonSerializer.Serialize(_cache, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}