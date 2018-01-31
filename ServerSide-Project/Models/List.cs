using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide_Project.Models
{
    public class List
    {
        Repository repo { get; set; }

        int currentPageIndex { get; set; }

        int totalBooksInList { get; set; }
    }
}