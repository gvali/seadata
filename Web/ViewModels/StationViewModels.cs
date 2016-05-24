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
    public class StationIndexViewModel
    {
        public IPagedList<Station> Stations { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
        public string Filter { get; set; }

    }

    public class StationCreateEditViewModel
    {
        public Station Station { get; set; }

        public SelectList StationTypeSelectList { get; set; }

    }

}