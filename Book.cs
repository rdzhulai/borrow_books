
namespace Assignment1
{

    public class Book
    {
        private string title;
        private int length;
        private Author author;
        private const int MINUTES_IN_HOUR = 60;


        public Book(string title, int length, string authorName)
        {
            this.title = title;
            this.length = length;
            string[] authorFirstLastNames = authorName.Split(' ');
            author = new Author(
                authorFirstLastNames[0], authorFirstLastNames[1]);
        }

        public int GetReadingTime(float minutesPerPage, bool inHours)
        {
            int readingTimeInMinutes = (int)Math.Ceiling(
                minutesPerPage * length);
            if (!inHours)
            {
                return readingTimeInMinutes;
            }
            int readingTimeInHours = (int)Math.Ceiling(
                (double)readingTimeInMinutes / MINUTES_IN_HOUR);
            return readingTimeInHours;
        }

        public string Title
        {
            get { return title; }
        }

        public Author Author
        {
            get { return author; }
        }
    }
}
