using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Repository.Support;
using AutoMapper;
using Repository;
using Service.Managers;
using Service.Tools;

namespace Service.Models
{
    public class Classification
    {
        public string Signum { get; set; }

        public int SignId { get; set; }

        public string Description { get; set; }

    }
}