namespace SecurityServices.Common
{
    public class Secret
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public Secret() 
        { 
            Key = string.Empty; 
            Value = string.Empty;
        }
    }
}
