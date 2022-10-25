namespace RandomRecordServices
{
    /// <summary>
    /// Class which represents record in file.
    /// </summary>
    public class RandomRecord
    {
        private const string EnglishSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string RussianSymbols = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";

        /// <summary>
        /// Initializes a new instance of <see cref="RandomRecord"/>.
        /// </summary>
        public RandomRecord()
        {
            InitializeRandomProperties();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RandomRecord"/>.
        /// </summary>
        /// <param name="date">Date.</param>
        /// <param name="englishString">English string.</param>
        /// <param name="russianString">Russian string.</param>
        /// <param name="integerNumber">Integer number.</param>
        /// <param name="floatingNumber">Floating point number.</param>
        public RandomRecord(DateTime date, string englishString, string russianString, int integerNumber, double floatingNumber)
        {
            Date = date;
            EnglishString = englishString;
            RussianString = russianString;
            IntegerNumber = integerNumber;
            FloatingNumber = floatingNumber;
        }

        /// <summary>
        /// Gets Date.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets 10-letters string which consists of only english letters.
        /// </summary>
        public string EnglishString { get; private set; }

        /// <summary>
        /// Gets 10-letters string which consists of only russian letters.
        /// </summary>
        public string RussianString { get; private set; }

        /// <summary>
        /// Gets integer number from 1 to 100_000_000.
        /// </summary>
        public int IntegerNumber { get; private set; }

        /// <summary>
        /// Gets floating point number from 1 to 20 with 8 digits after point.
        /// </summary>
        public double FloatingNumber { get; private set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Date:dd.MM.yyyy}||{this.EnglishString}||{this.RussianString}||{this.IntegerNumber}||{this.FloatingNumber:0.00000000}||";
        }

        private void InitializeRandomProperties()
        {
            Random random = new();

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
            this.IntegerNumber = random.Next(50_000_000) * 2;
            this.FloatingNumber = random.NextDouble() * 19 + 1;
        }
    }
}