using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Repository.Support;
using Repository;
using AutoMapper;
using Service.Models;
using Service.Tools;

namespace Service.Managers
{
    public class BookAuthorClassificationManager
    {
        public BookAuthorClassification Setup()
        {
            ClassificationManager classificationManager = new ClassificationManager();
            AuthorManager authorManager = new AuthorManager();
            BookAuthorClassification bac = new BookAuthorClassification();
            bac.Authors = authorManager.GetAllAuthorsToList();
            bac.Classifications = classificationManager.GetAllClassifications();
            return bac;
        }
    }
}
