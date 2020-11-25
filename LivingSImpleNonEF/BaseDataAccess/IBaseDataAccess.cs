using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LivingSimpleNonEF.BaseDataAccess
{
    public interface IBaseDataAccess
    {
        DataTable Get(IDbCommand command);
   
        int Add(IDbCommand command);
    }
}
