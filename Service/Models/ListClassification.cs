using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    /// <summary>
    /// Model that allows easy conversion of the user's selected classifications in search querys.
    /// </summary>
    public class ListClassification
    {
        public List<Classification> Classifications { get; set; }

        public List<int> SelectedClassification { get; set; }
    }
}
