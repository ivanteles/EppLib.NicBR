namespace EppLib.Entities.Domain
{
    public class DomainPeriod
    {
        public DomainPeriod(int value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        ///     Quantidade de anos 1, 2, ou 5
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        ///     y - anos
        /// </summary>
        public string Unit { get; set; }
    }
}