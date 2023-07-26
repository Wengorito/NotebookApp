using System;

namespace EvernoteClone.Model
{
    public class Note : HasId
    {
        //[PrimaryKey, AutoIncrement]
        public string Id { get; set; }

        //[Indexed]
        public string NotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileLocation { get; set; }
    }
}
