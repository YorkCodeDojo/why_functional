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
}