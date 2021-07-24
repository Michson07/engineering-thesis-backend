namespace Core.Domain.Test
{
    public abstract class Builder<T>
    {
        public T Build()
        {
            return BuildInstance();
        }

        protected abstract T BuildInstance();
    }
}
