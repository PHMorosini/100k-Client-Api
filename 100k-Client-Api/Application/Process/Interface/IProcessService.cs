using _100k_Client_Api.Application.DTO;
using _100k_Client_Api.Domain.Team.Entity;
using _100k_Client_Api.Domain.User.Entity;

namespace _100k_Client_Api.Application.Process.Interface;

public interface IProcessService
{
    public void PersistirDadosLista(string jsonDados);
    public List<TopCountriesDTO> ObterTopCountries(List<User> Usuarios);
    public List<TeamInsightsDto> ObterTeamInsights(List<User> Usuarios);
    public List<ActiveUsersPerDayDTO> ObterActivesUserPerDay(List<User> Usuarios, int minimo);
    public List<User> ObterSuperUsers (List<User> Usuarios);

    public int ObterQuantidadeUsuarios();

}

