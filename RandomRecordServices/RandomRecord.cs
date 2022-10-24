namespace RandomRecordServices
{
    public class RandomRecord
    {
        private const string EnglishSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string RussianSymbols = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";
        public DateTime Date { get; private set; }
        public string EnglishString { get; private set; }
        public string RussianString { get; private set; }
        public int IntegerNumber { get; private set; }
        public double FloatingNumber { get; private set; }


        public RandomRecord()
        {
            InitializeRandomProperties();
        }

        public RandomRecord(DateTime date, string englishString, string russianString, int integerNumber, double floatingNumber)
        {
            Date = date;
            EnglishString = englishString;
            RussianString = russianString;
            IntegerNumber = integerNumber;
            FloatingNumber = floatingNumber;
        }

        public override string ToString()
        {
            return $"{this.Date:dd.MM.yyyy}||{this.EnglishString}||{this.RussianString}||{this.IntegerNumber}||{this.FloatingNumber:0.00000000}||";
        }

        private void InitializeRandomProperties()
        {
            Random random = new Random();

            DateTime startDate = DateTime.Today.AddYears(-5);
            int range = (DateTime.Today - startDate).Days;
            this.Date = startDate.AddDays(random.Next(range));

            char[] englishChars = new char[10];
            char[] russianChars = new char[10];
            for (int i = 0; i < 10; i++)
            {
                englishChars[i] = EnglishSymbols[random.Next(EnglishSymbols.Length)];
                russianChars[i] = RussianSymbols[random.Next(RussianSymbols.Length)];
            }

            this.EnglishString = new string(englishChars);
            this.RussianString = new string(russianChars);
            this.IntegerNumber = random.Next(500_000) * 2;
            this.FloatingNumber = random.NextDouble() * 19 + 1;
        }
    }
}