namespace IcoBet.Services
{
    using System.Threading.Tasks;

    public interface IPullingService
    {
        Task<string> Pull();
    }
}
