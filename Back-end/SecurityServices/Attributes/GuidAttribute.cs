namespace SecurityServices.Attributes
{
    public class GuidAttribute : Attribute
    {
        protected Guid guidValue { get; set; }

        public Guid GuidValue
        {
            get
            {
                return guidValue;
            }

            set
            {
                guidValue = value;
            }
        }
        public GuidAttribute(string guid)
        {
            guidValue = new Guid(guid);
        }

    }
}
