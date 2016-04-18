using Ipagoo.ExpressLibrary.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipagoo.ExpressLibrary.Service.Interfaces
{
    public interface IBookService
    {
        ExpressLibraryResponse FindBooks(BookFilter bookFilter);
    }
}
