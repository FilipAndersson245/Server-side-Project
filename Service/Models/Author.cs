﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository.Support;
using Repository;
using AutoMapper;
using PagedList;
using Service.Managers;
using Service.Tools;

namespace Service.Models
{
    public class Author
    {
        public Author(){}

        public string Aid { get; set; } //Primary Key

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? BirthYear { get; set; } = null;

        public IPagedList<Book> BookList { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

    }
}