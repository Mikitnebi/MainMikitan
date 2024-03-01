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
            if (ids.Count() > allActiveIds.Count())
            {
                response.ErrorType = ErrorResponseType.BadCategoryIdRequest;
            }

            response.Result = ids.SequenceEqual(allActiveIds);
            if (!response.Result)
            {
                response.ErrorType = ErrorResponseType.BadCategoryIdRequest;
            }
            return response;
        }
    }
}
