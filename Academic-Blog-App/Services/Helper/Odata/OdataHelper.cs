using System.Text;

namespace Academic_Blog_App.Services.Helper.Odata
{
    public class OdataHelper
    {
        public string Equal(string field, string value)
        {
            return "?$filter= " + field + "eq" + " '" + value + "'";
        }

        public string NotEqual(string field, string value)
        {
            return "?$filter= " + field + "ne" + " '" + value + "'";
        }

        public string Ascending(string field)
        {
            return "?$orderby= " + field + " asc";
        }

        public string Decending(string field)
        {
            return "?$orderby= " + field + " desc";
        }

        public string Select(string field)
        {
            return "?$select=" + field;
        }

        public string SkipAndTop(int skip, int top)
        {
            return "?skip=" + skip.ToString() + "&top=" + top.ToString();
        }

        public string Count()
        {
            return "$count";
        }

        public string Year(string field, int year)
        {
            return "?$filter=Year" + "(" + field + ") eq " + year.ToString();
        }

        public string Moth(string field, int month)
        {
            return "?$filter=Year" + "(" + field + ") eq " + month.ToString();
        }

        public string Day(string field, int day)
        {
            return "?$filter=Year" + "(" + field + ") eq " + day.ToString();
        }

        public string StartWith(string field, string value) 
        {
            return "?$filter=startswith(" + field + "," + value + "eq true";
        }

        public string EndWith(string field, string value)
        {
            return "?$filter=endswith(" + field + "," + value + "eq true";
        }

        public string Custom(string field, string value, string comparisonOperator)
        {
            field = Uri.EscapeDataString(field);
            value = Uri.EscapeDataString(value);

            return $"?$filter={field} {comparisonOperator} '{value}'";
        }

        public class Filters
        {
            public string? Field { get; set; }
            public string? Value { get; set; }
            public string? ComparisonOperator { get; set; }
        }

        public string CustomMutiple(List<Filters> filters, string type)
        {
            if (filters == null || filters.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder filterString = new StringBuilder("?$filter=");

            foreach (var filter in filters)
            {
                string field = Uri.EscapeDataString(filter.Field);
                string value = Uri.EscapeDataString(filter.Value);
                string comparisonOperator = filter.ComparisonOperator;

                filterString.Append($"{field} {comparisonOperator} '{value}'");

                if (filter != filters.Last())
                {
                    if(type == "and")
                    {
                        filterString.Append(" and ");
                    }
                    if(type == "or")
                    {
                        filterString.Append(" or ");
                    }
                }
            }

            return filterString.ToString();
        }
    }
}
