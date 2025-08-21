using FluentAssertions;

namespace WhyFunctional.Tests;

[TestClass]
public sealed class ListOfTests
{
	[TestMethod]
	public void VerifyNilProducesEmptyResult()
	{
		ListOf<int> sut = ListOf<int>.Nil();

		sut.Should().NotBeNull();
		sut.ToArray.Should().BeEmpty();
		sut.IsNil.Should().BeTrue();
	}

	[TestMethod]
	public void VerifySingleElement()
	{
		ListOf<int> sut = ListOf<int>.Cons(1, ListOf<int>.Nil());

		sut.Should().NotBeNull();
		int[] values = sut.ToArray;
		values.Should().NotBeEmpty();
		values.Should().HaveCount(1);
		values.Should().Contain(1);
		sut.IsNil.Should().BeFalse();
	}

	[TestMethod]
	public void VerifyThreeElements()
	{
		ListOf<int> sut = ListOf<int>.Cons(1, ListOf<int>.Cons(2, ListOf<int>.Cons(3, ListOf<int>.Nil())));

		sut.Should().NotBeNull();
		int[] values = sut.ToArray;
		values.Should().NotBeEmpty();
		values.Should().HaveCount(3);
		values.Should().Contain([1, 2, 3]);
		sut.IsNil.Should().BeFalse();
	}

	[TestMethod]
	[DataRow(new int[] { 1 }, 1)]
	[DataRow(new int[] { 13, 27, 3 }, 13)]
	public void VerifyHeadProperty(int[] values, int expected)
	{
		ListOf<int> sut = ListOf<int>.Nil();
		foreach (int value in values.Reverse())
		{
			sut = ListOf<int>.Cons(value, sut);
		}

		sut.Should().NotBeNull();
		int[] sutValues = sut.ToArray;
		sutValues.Should().HaveCount(values.Length);
		sutValues.Should().Contain(expected);
		sut.IsNil.Should().BeFalse();
		sut.Head.Should().Be(expected);
	}

	public static IEnumerable<object[]> VerifyTailTestData()
	{
		var tv_1 = ListOf<int>.Cons(1, ListOf<int>.Nil());
		var tv_3 = ListOf<int>.Cons(3, ListOf<int>.Nil());
		var tv_27 = ListOf<int>.Cons(27, tv_3);
		var tv_13 = ListOf<int>.Cons(13, tv_27);
		return
		[
			[tv_1.ToArray, tv_1.Head, Array.Empty<int>()],
			[tv_13.ToArray, tv_13.Head, tv_27.ToArray]
		];
	}

	[TestMethod]
	[DynamicData(nameof(VerifyTailTestData), DynamicDataSourceType.Method)]
	public void VerifyTailProperty(int[] values, int expectedHead, int[] expectedTail)
	{
		ListOf<int> sut = ListOf<int>.Nil();
		foreach (int value in values.Reverse())
		{
			sut = ListOf<int>.Cons(value, sut);
		}

		sut.Should().NotBeNull();
		int[] sutValues = sut.ToArray;
		sutValues.Should().HaveCount(values.Length);
		sutValues.Should().Contain(expectedHead);
		sut.IsNil.Should().BeFalse();
		sut.Head.Should().Be(expectedHead);
		sut.Tail.ToArray.Should().HaveCount(expectedTail.Length);
		sut.Tail.ToArray.Should().ContainInOrder(expectedTail);
	}
}