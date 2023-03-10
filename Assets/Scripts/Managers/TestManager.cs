public interface ITestManager
{
    void SetValue(int value);
    int GetValue();
    void IncrementValue();
    void IncreaseValue(int amount);
}

public class TestManager : ITestManager
{
    private int _value;

    public void SetValue(int value)
    {
        _value = value;
    }

    public int GetValue()
    {
        return _value;
    }

    public void IncrementValue()
    {
        _value++;
    }

    public void IncreaseValue(int amount)
    {
        _value += amount;
    }
}
