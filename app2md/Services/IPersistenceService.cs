using app2md.Enums;
using app2md.Models;

namespace app2md.Services
{
    public interface IPersistenceService
    {
        int InsertAndFetchId(ContactFormViewModel model);
        bool IsNewName(string name, NameType nameType);
    }
}
