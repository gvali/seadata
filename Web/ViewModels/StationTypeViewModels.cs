using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using PagedList;

namespace Web.ViewModels
{
    public class StationTypeIndexViewModel
    {
        public List<StationType> StationTypes { get; set; }


    }

    public class StationTypeCreateEditViewModel
    {
        public StationType StationType { get; set; }

        public MultiLangString StationTypeName { get; set; }

    }

}