using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public interface IProductService
    {
        string GetValue(int id);
        string[] GetValues();
    }
}
