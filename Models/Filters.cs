namespace ToDoDemo.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {

            FilterString = filterstring ?? "all-all-all";
            string[] filters = FilterString.Split('-');
            CategoryId = filters[0];
            Due = filters[1];
            StatusId = filters[2];
        }

        public string FilterString { get; }
        public string CategoryId { get; }
        public string Due { get; }
        public string StatusId { get; }

        public bool HasCategory => !CategoryId.Equals("all", StringComparison.CurrentCultureIgnoreCase);
        public bool HasDue => !Due.Equals("all", StringComparison.CurrentCultureIgnoreCase);
        public bool HasStatus => !StatusId.Equals("all", StringComparison.CurrentCultureIgnoreCase);
        public static Dictionary<string, string> DueFilterValues =>
            new()
            {
                { "future" , "Future" },
                { "past" , "Past" },
                { "today" , "Today" }
            };

        public bool IsPast => Due.ToLower() == "past";
        public bool IsFuture => Due.ToLower() == "future";
        public bool IsToday => Due.ToLower() == "today";
    }
}
