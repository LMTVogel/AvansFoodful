using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DomainServices.Repos.Intf
{
    public interface ICanteenRepo
    {
        Canteen GetCanteen(int id);
    }
}
