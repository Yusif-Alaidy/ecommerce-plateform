using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.core.Service_Interfaces
{
    public interface IImageManagementService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection file, string src);
        void DeleteImageAsync(string src);
    }
}
