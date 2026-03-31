namespace _100k_Client_Api.Domain.User.Entity;
using TeamEntity = _100k_Client_Api.Domain.Team.Entity.Team;
using LogEntity = _100k_Client_Api.Domain.Logs.Entity.Log;
using System.Text.Json.Serialization;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Score { get; set; }
    public bool Active { get; set; }
    public string Country { get; set; }
    public TeamEntity Team { get; set; }

    public List<LogEntity> Logs { get; set; }

    [JsonConstructor]
    public User() { }

}