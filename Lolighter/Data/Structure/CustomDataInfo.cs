namespace Lolighter.Data.Structure
{
    internal class CustomDataInfo
    {
        public Editors _editors { get; set; } = new();

        public class Editors
        {
            public string _lastEditedBy { get; set; } = "Lolighter 3.0.0";
        }
    }
}
