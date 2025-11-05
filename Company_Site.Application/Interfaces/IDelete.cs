using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Application.Interfaces
{
    public interface IDelete
    {
        Task<bool> SoftDelete();
    }
}
