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
    public class ProjectIndexViewModel
    {
        public IPagedList<Project> Projects { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
        public string Filter { get; set; }

        //public bool FilterByDTBoolean { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime? FilterFromDT { get; set; }
        //[DataType(DataType.DateTime)]
        //public DateTime? FilterToDT { get; set; }
    }

    public class ProjectCreateEditViewModel
    {
        public Project Project { get; set; }
//        public SelectList PublisherSelectList { get; set; }

        public int[] LeaderIds { get; set; }
        public MultiSelectList LeadersMultiSelectList { get; set; }

        public int[] PersonIds { get; set; }
        public MultiSelectList PersonsMultiSelectList { get; set; }

    }

}