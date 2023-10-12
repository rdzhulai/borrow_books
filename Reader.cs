namespace Assignment1
{
    public class Reader
    {
        private string name;
        private int readerId;
        private float readingSpeed;
        private List<Record> reads;

        public Reader(string name, int readerId, float readingSpeed)
        {
            this.name = name;
            this.readerId = readerId;
            this.readingSpeed = readingSpeed;
            reads = new List<Record>();
        }

        public int GetTotalReadingTime()
        {
            int totalReadingTime = 0;
            foreach (Record record in reads)
            {
                totalReadingTime += record.Book.GetReadingTime(
                    readingSpeed, false);
            }
            return totalReadingTime;
        }

        public void AddReading(Record record)
        {
            if (record.Reader.ReaderId == readerId)
            {
                reads.Add(record);
            }
        }

        public float ReturnBooks(DateTime date)
        {
            float totalFee = .0f;
            foreach (Record record in reads)
            {
                if (!record.Returned)
                    continue;
                totalFee += record.ReturnBook(date);
            }
            return totalFee;
        }

        public float CalculateAllTimeFee(DateTime date)
        {
            float allTimeFee = reads.Aggregate(0f, (allTimeFee, record) =>
                allTimeFee += record.GetFee(
                record.Returned ? record.ReturnedTime : date));

            return allTimeFee;
        }
        public int ReaderId
        {
            get { return readerId; }
        }
        public List<Record> Reads
        {
            get { return reads; }
        }
    }


}
