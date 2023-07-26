namespace EvernoteClone.Model
{
    public class Notebook : HasId
    {
        //[PrimaryKey, AutoIncrement]
        public string Id { get; set; }

        //[Indexed]
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
