﻿
using System.Data;
using System.Globalization;

namespace Assignment1
{
    public class Program
    {
        // 1. load from file as a list of records (2p)
        private static Random rand = new Random();
        private const float MAX_READING_SPEED_IN_MINUTES = 10f;
        private const float MIN_READING_SPEED_IN_MINUTES = 4f;
        private static float GetRandomReadingSpeed()
        {
            return rand.NextSingle() * (MAX_READING_SPEED_IN_MINUTES - MIN_READING_SPEED_IN_MINUTES) + MIN_READING_SPEED_IN_MINUTES;
        }
        private enum csvColumns
        {
            authorName = 0, bookName = 1, length = 2, readerId = 3, borrowedTime = 4, returnedTime = 5
        }
        static List<Record> LoadRecords(string path)
        {
            var records = new List<Record>();
            var readers = new List<Reader>();
            var authors = new List<Author>();

            foreach (var csvRow in File.ReadAllLines(path))
            {
                var columns = csvRow.Split(',');

                var book = new Book(
                    columns[(int)csvColumns.bookName], int.Parse(columns[(int)csvColumns.length]), columns[(int)csvColumns.authorName]);

                var readerId = int.Parse(columns[(int)csvColumns.readerId]);
                var reader = readers.Find(r => r.ReaderId == readerId);
                if (reader is null)
                {
                    reader = new Reader("XXXXX", readerId, GetRandomReadingSpeed());
                    readers.Add(reader);
                }

                var author = authors.Find(a => a.GetAuthorName()
                    .Equals(columns[(int)csvColumns.authorName]));
                if (author is null)
                {
                    author = new Author(columns[(int)csvColumns.authorName]);
                    authors.Add(author);
                }

                var borrowedTime = DateTime.ParseExact(
                    columns[(int)csvColumns.borrowedTime], "yyyy-MM-dd", CultureInfo.InvariantCulture);

                Record record;
                if (columns.Length >= (int)csvColumns.returnedTime)
                {
                    var returned = false;
                    record = new Record(book, reader, borrowedTime, returned);
                }
                else
                {
                    var returnedTime = DateTime.ParseExact(
                        columns[(int)csvColumns.returnedTime], "yyyy-MM-dd",
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

            return records.OrderByDescending(x => x.Reader.Reads.Count)
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
            Console.WriteLine(FindMostReadBook("library_records.csv"));
            Console.WriteLine(FindMostReadAuthor("library_records.csv"));
            Console.WriteLine(FindMostAvidReader("library_records.csv"));
            Console.WriteLine(CalculateIncome(
                "library_records.csv", new DateTime(2023, 11, 1)));
        }
    }
}