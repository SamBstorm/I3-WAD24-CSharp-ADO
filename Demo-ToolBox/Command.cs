namespace Demo_ToolBox
{
    public class Command
    {
        public string Query { get; private set; }
        public Dictionary<string,object?> Parameters { get; private set; }
        public bool IsStoredProcedure { get; private set; }

        public Command(string query, bool isStoredProcedure = false)
        {
            Parameters = new Dictionary<string,object?>();
            Query = query;
            IsStoredProcedure = isStoredProcedure;
        }

        public Command(string query, Dictionary<string, object?> parameters, bool isStoredProcedure = false) : this(query,isStoredProcedure)
        {
            foreach (KeyValuePair<string, object?> parameter in parameters)
            {
                AddParameter(parameter.Key, parameter.Value);
            }
        }

        public void AddParameter(string name, object? value) {
            if (Parameters.ContainsKey(name)) throw new ArgumentException(nameof(name));
            value ??= DBNull.Value;
            Parameters.Add(name, value);
        }
    }
}
