using System;

class StackFullException : Exception
{
    public StackFullException(string message) : base(message) { }
}

class StackEmptyException : Exception
{
    public StackEmptyException(string message) : base(message) { }
}

class Stack
{
    private int[] stackArray;
    private int top;

    public Stack(int maxSize)
    {
        if (maxSize <= 0)
        {
            throw new ArgumentException("Illegal argument");
        }

        stackArray = new int[maxSize];
        top = -1;
    }

    public void Push(int value)
    {
        if (top == stackArray.Length - 1)
        {
            throw new StackFullException("Stack is full");
        }

        stackArray[++top] = value;
    }

    public int Pop()
    {
        if (top == -1)
        {
            throw new StackEmptyException("Stack is empty");
        }

        return stackArray[top--];
    }

    public void Clear()
    {
        top = -1;
    }

    public int Top()
    {
        if (top == -1)
        {
            throw new StackEmptyException("Stack is empty");
        }

        return stackArray[top];
    }

    public int Size()
    {
        return top + 1;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Stack stack = new Stack(5);
        try
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            Console.WriteLine("Stack size: " + stack.Size());
            Console.WriteLine("Top: " + stack.Top());
            Console.WriteLine("Popped: " + stack.Pop());
            Console.WriteLine("Popped: " + stack.Pop());
            Console.WriteLine("Stack size: " + stack.Size());
        }
        catch (StackFullException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (StackEmptyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.ReadKey();
    }
}