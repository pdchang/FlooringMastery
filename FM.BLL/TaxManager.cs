using FM.Models;
using FM.Models.Interfaces;
using FM.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.BLL
{
    public class TaxManager
    {
        private ITaxRepository _taxRepository;

        public TaxManager(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public TaxResponse TaxRate(string state)
        {
            TaxResponse response = new TaxResponse();
            response.Tax = _taxRepository.GetTax(state);

            if(response.Tax == null)
            {
                response.Success = false;
                response.Message = $"{state} is an invalid State";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }
        public List<Tax> GetTaxes()
        {
            List<Tax> taxes = new List<Tax>();
            return _taxRepository.GetTaxes();
        }

    }
}
