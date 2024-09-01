namespace WinFormsLiteDbFromJson.Entities
{
    public class SubjectName
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }

        public SubjectName() { }
        public SubjectName(string title, string first, string last)
        {
            Title = title;
            First = first;
            Last = last;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Title, First, Last);
        }
    }
}
