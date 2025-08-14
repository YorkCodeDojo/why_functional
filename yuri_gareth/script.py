# A token type used for just being Nil
class Nil: # None
    def IsNil(self):
        return True

class listOf:
    def __init__(self, elem, rest):
        self.head = elem
        self.tail = rest

    def IsNil(self):
        return False

    def Head(self):
        return self.head

    def Tail(self):
        return self.tail

def Cons(elem, rest):
    return listOf(elem, rest)

def Sum(a_list):
    return foldr(int.__add__, 0, a_list)

def product(a_list):
    return foldr(int.__mul__, 1, a_list)

def foldr(opr, initValue, a_list):
    # print("foldr")
    # if a_list.IsNil():
    #     print(f"return {initValue}")
    #     return initValue
    # val = foldr(opr, initValue, a_list.Tail())
    # head = a_list.Head()
    # res = opr(head, val)
    # print(f"{head} {opr} {val} == {res}")
    # return res
   return initValue if a_list.IsNil() else opr(a_list.Head(), foldr(opr, initValue, a_list.Tail()))


def buildList(l):
    return Nil() if not l else Cons(l[0], buildList(l[1:]))

def AnyTrue(a_list):
    return foldr(bool.__or__, False, a_list)

def AllTrue(a_list):
    return foldr(bool.__and__, True, a_list)

def copy(a_list):
    return foldr(Cons, Nil(), a_list)

def append(a_list, b_list):
    return foldr(Cons, copy(b_list), a_list)

def length(a_list):
    return foldr(lambda _,count_so_far: count_so_far + 1, 0, a_list)

def Map(func, a_list):
    return foldr(lambda a, b: Cons(func(a), b), Nil(), a_list)

def DoubleAll(a_list):
    double = lambda x: x*2
    return Map(double, a_list)

myEmptyList = Nil()
print(type(myEmptyList))

l1 = Cons(1, Cons(2, Cons(3, Nil())))
print(type(l1))


# myList = Cons(13, Cons(27, Cons(3, Nil())))
myList = buildList([13,27,3])
print(myList.IsNil())  # False
print(myList.Head()) # 13
print(myList.Tail().Head()) # 27

print(Sum(myList))
print(foldr(int.__add__, 0, myList))
print(product(myList))
print(foldr(int.__mul__, 1, myList))




l = buildList([1,2,3])

print(AnyTrue(buildList([False, False, True])))
print(AnyTrue(buildList([False, False, False])))
print(AnyTrue(buildList([])))


print(AllTrue(buildList([True, True, True])))
print(AllTrue(buildList([False, False, True])))
print(AllTrue(buildList([])))


print("------")
print(product(myList))
list_copy = copy(myList)
print(myList is not list_copy)
print(Sum(append(buildList([1,2]), buildList([3,4]))))



print(length(buildList([1,2,3,4,5,6])))

print("------")
print(Sum(buildList([1,2,3,4,5,6])))
print(Sum(DoubleAll(buildList([1,2,3,4,5,6]))))
