namespace MyApi.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Balance { get; set; }
        public double Income { get; set; }

        public int OwnerId { get; set; }
        public Person Owner { get; set; }

        public List<DebtIn> debtIns { get; set; }

        public void AddToBalance(double amount)
        {
            if (amount < 0) 
                throw new ArgumentOutOfRangeException("Montante inválido");

            this.Balance += amount;
        }
    }
}
