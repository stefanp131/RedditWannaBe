using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWannaBe.DAL.Models
{
    public class QueryForTopic
    {
        private int currentPage = 1;
        private int pageSize = 12;

        public int CurrentPage
        {
            get
            {
                if (currentPage < 1)
                {
                    return 1;
                }

                return currentPage;
            }

            set
            {
                currentPage = value;
            }
        }

        public int PageSize
        {
            get
            {
                if (pageSize < 4)
                {
                    return 4;
                }

                if (pageSize > 32)
                {
                    return 32;
                }

                return pageSize;
            }

            set
            {
                pageSize = value;
            }
        }

        public string SearchItemQuery { get; set; } = string.Empty;
    }
}
