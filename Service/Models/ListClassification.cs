using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class ListClassification
    {
        public List<Classification> Classifications { get; set; }
        public List<int> SelectedClassification { get; set; }
    }
}
