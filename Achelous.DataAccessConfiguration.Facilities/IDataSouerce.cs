namespace Achelous.Facilities.DataAccess
{
    public interface IDataSouerce
    {
        void CreateConnectionString(string server, string instance, string username, string password, string additionalDetails);
    }
}
