﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Resources;
using Common.Utilities;
using System.Threading.Tasks;

namespace Entity.Common
{
    [Serializable]
    public class Pager
    {
        [Display(Name = "Lbl_PageNo", ResourceType = typeof(Resources))]
        [LocalizedRegex("([0-9]+)", "ValErr_NumericOnly")]
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
        public int TotalRecords { get; set; }

        public string TotalRecordsDisplay
        {
            get { return this.TotalRecords.FormatNumber(); }
        }

        public int PageCount
        {
            get
            {
                int pageCount = (int)Math.Ceiling(this.TotalRecords / (double)this.PageSize);
                return pageCount;
            }
        }

        public int FirstPageIndex
        {
            get
            {
                int pageIndex = (this.PageNo * this.PageSize) - this.PageSize + 1;
                return pageIndex;
            }
        }

        public int LastPageIndex
        {
            get
            {
                int pageIndex = 0;
                if (this.PageNo == this.PageCount)
                {
                    pageIndex = this.TotalRecords;
                }
                else
                {
                    pageIndex = this.PageNo * this.PageSize;
                }

                return pageIndex;
            }
        }
    }
}
