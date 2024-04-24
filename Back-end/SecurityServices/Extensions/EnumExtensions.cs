using SecurityServices.Attributes;

namespace SecurityServices.Extensions
{
    public static class EnumExtensions
    {

        public static string GetGuidFromValue(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return null;

            var attributes = (GuidAttribute[])field.GetCustomAttributes(typeof(GuidAttribute), false);

            if (attributes == null)
                return null;

            return (attributes.Length > 0) ? attributes[0].GuidValue.ToString() : null;
        }
    }
}
