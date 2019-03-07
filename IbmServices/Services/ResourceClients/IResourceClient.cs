using IbmServices.Models;
using System.Collections.Generic;

namespace IbmServices.Services.ResourceClients
{
    public interface IResourceClient<T> where T : BaseEntity
    {
        List<T> Fetch();
    }
}
