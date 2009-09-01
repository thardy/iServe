using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace System.Web.Mvc {
	public static class HtmlHelpers {

		public static string Pager<T>(this HtmlHelper source, IPagedList<T> pagedList) {

			StringBuilder pagerHTML = new StringBuilder();

			pagerHTML.Append("<div class=\"pager_container\">");
			pagerHTML.Append("<ul class=\"pagination\">");

			if (pagedList.HasPreviousPage) {
				pagerHTML.Append("<li class=\"previous\">");
				pagerHTML.Append("<a href=\"");
				pagerHTML.Append(ViewDataURL(source.ViewContext.RouteData));
				pagerHTML.Append("?page=").Append(pagedList.PageNumber - 1);
				pagerHTML.Append("\">").Append("« Previous").Append("</a>");
				pagerHTML.Append("</li>");
			}
			else {
				pagerHTML.Append("<li class=\"previous-off\">« Previous</li>");
			}


			for (int page = 1; page <= pagedList.PageCount; page++) {

				if (page == pagedList.PageNumber) {
					pagerHTML.Append("<li class=\"active\">").Append(page).Append("</li>");
				}
				else {
					pagerHTML.Append("<li>");
					pagerHTML.Append("<a href=\"");
					pagerHTML.Append(ViewDataURL(source.ViewContext.RouteData));
					pagerHTML.Append("?page=").Append(page);
					pagerHTML.Append("\">").Append(page).Append("</a>");
					pagerHTML.Append("</li>");
				}
			}

			if (pagedList.HasNextPage) {
				pagerHTML.Append("<li class=\"next\">");
				pagerHTML.Append("<a href=\"");
				pagerHTML.Append(ViewDataURL(source.ViewContext.RouteData));
				pagerHTML.Append("?page=").Append(pagedList.PageNumber + 1);
				pagerHTML.Append("\">").Append("Next »").Append("</a>");
				pagerHTML.Append("</<li>");
			}
			else {
				pagerHTML.Append("<li class=\"next-off\">Next »</li>");
			}

			pagerHTML.Append("</ul>");
			pagerHTML.Append(GetDetailInformation(pagedList.PageSize, pagedList.TotalItemCount, pagedList.PageNumber));
			pagerHTML.Append("</div>");


			return pagerHTML.ToString();
		}

		private static string ViewDataURL(System.Web.Routing.RouteData route) {

			StringBuilder rVal = new StringBuilder();

			rVal.Append("/").Append(route.Values["controller"].ToString()).Append("/").Append(route.Values["action"].ToString());

			// If there is an ID on the route then add it to the URL
			if (route.Values["id"] != null && !string.IsNullOrEmpty(route.Values["id"].ToString())) {
				rVal.Append("/").Append(route.Values["id"].ToString());
			}

			return rVal.ToString();
		}

		private static string GetDetailInformation(int pageSize, int recordCount, int pageNumber) {

			StringBuilder pagerHTML = new StringBuilder();

			// Show the details of the paged list
			pagerHTML.Append("<div class=\"pager_details\">");
			pagerHTML.Append("Showing <strong>");

			if (recordCount <= pageSize) {
				pagerHTML.Append("1 - ");
				pagerHTML.Append(recordCount.ToString());
				pagerHTML.Append("</strong> out of <strong>");
				pagerHTML.Append(recordCount.ToString());
				pagerHTML.Append("</strong>");
			}
			else {
				int beginNum = 0;
				if (pageNumber == 1) {
					beginNum = 1;
				}
				else {
					int i = pageNumber - 1;
					int j = i * pageSize;
					beginNum = j + 1;
				}
				pagerHTML.Append(beginNum.ToString());
				pagerHTML.Append(" - ");
				int endNum = pageNumber * pageSize;
				if (endNum > recordCount) {
					endNum = recordCount;
				}
				pagerHTML.Append(endNum.ToString());
				pagerHTML.Append("</strong> out of <strong>");
				pagerHTML.Append(recordCount.ToString());
				pagerHTML.Append("</strong>");
			}

			pagerHTML.Append("</div>");

			return pagerHTML.ToString();
		}
	}
}

