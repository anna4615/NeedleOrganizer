using NeedleOrganizer.Models;


namespace NeedleOrganizer.Interfaces
{
    public interface INeedleService
    {
        Task<List<Needle>> GetNeedles();
        Task AddNeedle(Needle needle);
        void DeleteAppDataNeedlesFile();
    }
}
