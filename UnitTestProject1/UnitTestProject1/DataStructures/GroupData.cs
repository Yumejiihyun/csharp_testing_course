namespace addressbooktests
{
    public class GroupData
    {
        private string name;
        private string header;
        private string footer;

        public GroupData(string name)
        {
            Name = name;
        }

        public string Name { get => name; set => name = value; }
        public string Header { get => header; set => header = value; }
        public string Footer { get => footer; set => footer = value; }
    }
}
