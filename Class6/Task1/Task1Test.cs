using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

public class Tests
{
    [Test]
    public void ApplyOperationTest()
    {
        That(ApplyOperation('*', 5, 2), Is.EqualTo(10));
        That(ApplyOperation('/', 10, 5), Is.EqualTo(2));
        Catch<DivByZero>(() => { ApplyOperation('/', 1, 0); });
        Catch<UnsupportedOperation>(() => { ApplyOperation('&', 1, 0); });
    }

    [Test]
    public void FormatLhsTest()
    {
        That(FormatLhs("*/", new[] {"1", "2", "3"}), Is.EqualTo("1*2/3"));
        That(FormatLhs("/*", new[] {"5", "2", "0"}), Is.EqualTo("5/2*0"));
    }
    
    [Test]
    public void ProcessStringTest()
    {
        That(ProcessString("*/", "1,2,3"), Is.EqualTo("1*2/3=0"));
        That(ProcessString("/**", "100,25,2,6"), Is.EqualTo("100/25*2*6=48"));
    }
}