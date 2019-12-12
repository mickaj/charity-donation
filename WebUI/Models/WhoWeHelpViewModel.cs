using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data.DataModel;

namespace WebUI.Models
{
    public class WhoWeHelpViewModel
    {
        private List<Institution> _institutions = new List<Institution>();
        public IEnumerable<Tuple<Institution, Institution>> Institutions 
        { 
            get
            {
                var result = new List<Tuple<Institution, Institution>>();
                for(int i = 0; i < _institutions.Count; i+=2)
                {
                    var pair = new Tuple<Institution, Institution>(_institutions[i], _institutions.Count > i+1 ? _institutions[i+1] : null);
                    result.Add(pair);
                }
                return result;
            }
        }

        public void AddInstitutions(IEnumerable<Institution> institutions)
        {
            _institutions.AddRange(institutions);
        }
    }
}
