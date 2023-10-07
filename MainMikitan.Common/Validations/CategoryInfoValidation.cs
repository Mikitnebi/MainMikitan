using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.InternalServicesAdapter.Validations
{
    public class CategoryInfoValidation
    {
        public static ResponseModel<bool> Validate(List<int> ids, List<int> allActiveIds)
        {
            var response = new ResponseModel<bool>();
            response.Result = ids.All(x => allActiveIds.Contains(x));
            if (!response.Result)
            {
                response.ErrorType = ErrorType.BadCategoryIdRequest;
            }
            return response;
        }
    }
}
