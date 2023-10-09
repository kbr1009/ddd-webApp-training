
namespace TESTWebApp.Domain.Models.Shared
{
    public abstract class Identity
    {
        public string Value { get; }

        public Identity()
        {
            Value = Guid.NewGuid().ToString("N");
        }

        public Identity(string id)
        {
            if(id is null)
                throw new ArgumentNullException(id, nameof(id));
            Value = id;
        }

        public override bool Equals(object obj)
        {
            var identity = obj as Identity;
            return identity != null &&
                   Value == identity.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

        public static bool operator ==(Identity identity1, Identity identity2)
        {
            return EqualityComparer<Identity>.Default.Equals(identity1, identity2);
        }

        public static bool operator !=(Identity identity1, Identity identity2)
        {
            return !(identity1 == identity2);
        }
    }
}
