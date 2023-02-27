

namespace DishoutOLO.ViewModel.Helper
{
    public class DataTableFilterModel
    {
        public class Search
        {
            public string value { get; set; }
            public bool regex { get; set; }
        }
        public class OrderInfo
        {
            public int column { get; set; }
            public string dir { get; set; }
        }
        public class ColumnInfo
        {
            public string data { get; set; }
            public string name { get; set; }
            public bool searchable { get; set; }
            public bool orderable { get; set; }
            public Search search { get; set; }
        }
        public string draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Search search { get; set; }
        public OrderInfo[] order { get; set; }
        public ColumnInfo[] columns { get; set; }
        public Nullable<int> BaseId { get; set; }
        public IEnumerable<dynamic> data { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public int GuideID { get; set; }
        public Guid guId { get; set; }
    }
}

