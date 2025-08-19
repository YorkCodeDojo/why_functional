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

		var result = sut.Sum<int,int>();
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

		var result = sut.Product<int, int>();
		result.Should().Be(expected);
	}

	[TestMethod]
	[DataRow(new int[] { })]
	[DataRow(new int[] { 1 })]
	[DataRow(new int[] { 1, 2, 3, 5, 7 })]
	public void VerifyBuildListExtension(int[] values)
	{
		ListOf<int> sut = values.BuildList();

		sut.Should().NotBeNull();
		sut.ToArray.Length.Should().Be(values.Length);
		sut.ToArray.Should().ContainInOrder(values);
	}
}