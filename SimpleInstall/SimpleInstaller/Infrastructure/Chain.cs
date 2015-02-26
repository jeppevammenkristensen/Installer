namespace SimpleInstaller.Infrastructure
{
    public abstract class Chain<T> 
    {
        public abstract bool IsMatch { get; }

        public abstract T GetValue();
    }
}