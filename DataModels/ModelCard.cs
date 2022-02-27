namespace Rover
{
    public class ModelCard : IEquatable<ModelCard>
    {
        public string id;
        public string name;
        public string description;
        public short series;
        public int value;
        public short rarity;

        public ModelCard()
        {
            id = "";
            name = "";
            description = "";
            series = 0;
            value = 0;
            rarity = 0;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ModelCard);
        }

        public bool Equals(ModelCard? other)
        {
            return other != null && id == other.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}