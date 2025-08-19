using FluentAssertions;

namespace WhyFunctional.Tests;

[TestClass]
public class ListOfExtensionsTests
{
	[TestMethod]
	[DataRow(new int[] { 1 })]
	[DataRow(new int[] { 10 })]
	[DataRow(new int[] { 1, 2, 3 })]
	public void VerifySumExtension(int[] values)
	{
		int expected = 0;
		ListOf<int> sut = ListOf<int>.Nil();
		foreach (int value in values.Reverse())
		{
			sut = ListOf<int>.Cons(value, sut);
			expected += value;
		}

		var result = sut.Sum();
		result.Should().Be(expected);
	}
	
	[TestMethod]
	[DataRow(new int[] { 1 })]
	[DataRow(new int[] { 10 })]
	[DataRow(new int[] { 1, 2, 3 })]
	public void VerifyProductExtension(int[] values)
	{
		int expected = 1;
		ListOf<int> sut = ListOf<int>.Nil();
		foreach (int value in values.Reverse())
		{
			sut = ListOf<int>.Cons(value, sut);
			expected *= value;
		}

		var result = sut.Product();
		result.Should().Be(expected);
	}
}