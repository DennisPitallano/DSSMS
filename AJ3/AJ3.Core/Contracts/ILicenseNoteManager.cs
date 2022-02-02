using System.Collections.Generic;
using System.Threading.Tasks;
using AJ3.Core.Data.Entity;
using AJ3.Core.DTO;

namespace AJ3.Core.Contracts
{
    public interface ILicenseNoteManager : IRepository<LicenseNoteListItem,LicenseNote>
    {
        Task<IEnumerable<LicenseNote>> GetLicenseNoteByType(string type);
    }
}
