
using System.Globalization;

namespace Assignment1
{
    public class Program
    {
        // 1. load from file as a list of records (2p)
        private static Random rand = new Random();
        private static string[] allFirstNames = File.ReadAllLines(
                    "sources/first_names.txt");
        private static string[] allLastNames = File.ReadAllLines(
                    "sources/last_names.txt");
        private const float MAX_READING_SPEED_IN_MINUTES = 10f;
        private const float MIN_READING_SPEED_IN_MINUTES = 4f;
        private static string GetRandomName()
        {
            return String.Join(" ", allFirstNames[rand.Next(allFirstNames.Length)], allLastNames[rand.Next(allLastNames.Length)]);
        }
        private static float GetRandomReadingSpeed()
        {
            return rand.NextSingle() * (MAX_READING_SPEED_IN_MINUTES - MIN_READING_SPEED_IN_MINUTES) + MIN_READING_SPEED_IN_MINUTES;
        }
        static List<Record> LoadRecords(string path)
        {
            var records = new List<Record>();
            var readers = new List<Reader>();

            foreach (var csvRow in File.ReadAllLines(path))
            {
                var columns = csvRow.Split(',');

                var book = new Book(
                    columns[1], int.Parse(columns[2]), columns[0]);

                var readerId = int.Parse(columns[3]);
                var reader = readers.SingleOrDefault(
                    r => r.ReaderId == readerId, new Reader(
                        GetRandomName(), readerId, GetRandomReadingSpeed()));
                var borrowedTime = DateTime.ParseExact(
                    columns[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                Record record;
                if (columns[5] == "")
                {
                    var returned = false;
                    record = new Record(book, reader, borrowedTime, returned);
                }
                else
                {
                    var returnedTime = DateTime.ParseExact(
                        columns[5], "yyyy-MM-dd",
                        CultureInfo.InvariantCulture);
                    record = new Record(
                        book, reader, borrowedTime, returnedTime);
                }

                records.Add(record);
                reader.AddReading(record);
            }
            return records;
        }

        // 2. find title of most commonly borrowed book from records (0.5p)
        static string FindMostReadBook(string path)
        {
            var records = LoadRecords(path);

            return records.GroupBy(
                x => (x.Book.Title, x.Book.Author.GetAuthorName()))
                .OrderByDescending(g => g.Count()).First().Key.Title;
        }

        // 3. find most read author (1p)
        static string FindMostReadAuthor(string path)
        {
            var records = LoadRecords(path);

            return records.GroupBy(x => x.Book.Author.GetAuthorName())
                .OrderByDescending(g => g.Count()).First().Key;
        }

        // 3. find most avid reader (0.5p)
        static int FindMostAvidReader(string path)
        {
            var records = LoadRecords(path);

            return records.OrderByDescending(x => x.Reader.Reads.Count())
                .First().Reader.ReaderId;
        }

        // 4. calculate income to a given day (1p)
        static float CalculateIncome(string path, DateTime date)
        {
            var records = LoadRecords(path);
            float income = records.Aggregate(0f, (income, record) => income += record.Reader.CalculateAllTimeFee(date));

            return income;
        }

        static void Main(string[] args)
        {

        }
    }
}