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

		var result = sut.Sum<int, int>();
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

	[TestMethod]
	[DataRow(new bool[] { }, false)]
	[DataRow(new bool[] { false }, false)]
	[DataRow(new bool[] { false, false, true }, true)]
	[DataRow(new bool[] { false, true, false }, true)]
	public void VerifyAnyTrueExtension(bool[] values, bool expected)
	{
		ListOf<bool> sut = values.BuildList();

		sut.Should().NotBeNull();
		sut.ToArray.Length.Should().Be(values.Length);
		sut.ToArray.Should().ContainInOrder(values);
		ListOfExtensions.AnyTrue(sut).Should().Be(expected);
	}

	[TestMethod]
	[DataRow(new bool[] { }, true)]
	[DataRow(new bool[] { false }, false)]
	[DataRow(new bool[] { false, false, true }, false)]
	[DataRow(new bool[] { false, true, false }, false)]
	[DataRow(new bool[] { true }, true)]
	[DataRow(new bool[] { true, true }, true)]
	[DataRow(new bool[] { true, true, false }, false)]
	public void VerifyAllTrueExtension(bool[] values, bool expected)
	{
		ListOf<bool> sut = values.BuildList();

		sut.Should().NotBeNull();
		sut.ToArray.Length.Should().Be(values.Length);
		sut.ToArray.Should().ContainInOrder(values);
		ListOfExtensions.AllTrue(sut).Should().Be(expected);
	}

	[TestMethod]
	[DataRow(new int[] { })]
	[DataRow(new int[] { 1 })]
	[DataRow(new int[] { 10, 20, 30 })]
	public void VerifyCopyExtension(int[] values)
	{
		ListOf<int> sut = values.BuildList();

		ListOf<int> copy = sut.Copy();

		sut.Should().NotBeNull();
		copy.Should().NotBeNull();
		copy.Should().NotBeSameAs(sut);
		copy.ToArray.Length.Should().Be(sut.ToArray.Length);
		copy.ToArray.Should().ContainInOrder(sut.ToArray);
	}

	[TestMethod]
	[DataRow(new int[] { }, new int[] { })]
	[DataRow(new int[] { 1 }, new int[] { 2 })]
	[DataRow(new int[] { 1 }, new int[] { })]
	[DataRow(new int[] { }, new int[] { 2 })]
	[DataRow(new int[] { 1, 11 }, new int[] { 2, 22 })]
	public void VerifyAppendExtension(int[] lhs, int[] rhs)
	{
		ListOf<int> first = lhs.BuildList();
		ListOf<int> second = rhs.BuildList();

		first.Should().NotBeNull();
		second.Should().NotBeNull();

		int[] expectedAppend = lhs.Concat(rhs).ToArray();

		ListOf<int> sut = first.Append(second);

		sut.Should().NotBeNull();
		sut.ToArray.Length.Should().Be(expectedAppend.Length);
		sut.ToArray.Should().ContainInOrder(expectedAppend);
	}
}