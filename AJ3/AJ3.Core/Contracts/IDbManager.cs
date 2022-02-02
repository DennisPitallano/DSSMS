using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AJ3.Core.Contracts
{
    public interface IDbManager
    {
        Task<bool> BackUpDb(string Location);
    }
}
