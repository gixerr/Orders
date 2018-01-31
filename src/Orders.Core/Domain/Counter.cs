namespace Orders.Core.Domain
{
    public class Counter
    {
        public int Value {get; private set;}

        public Counter()
        {
            this.Value = 1;
        }

        public void Increase()
        {
            this.Value++;
        }

        public void Decrease()
        {
            if(this.Value is 1)
            {
                return;
            }
            this.Value--;
        }
    }
}