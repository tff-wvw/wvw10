using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class StackWithMin
    {
        private Stack<int> stack;
        private Stack<int> minStack;

        public StackWithMin()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }

        public void Push(int value)
        {
            stack.Push(value);
            if (minStack.Count == 0 || value <= minStack.Peek())
            {
                minStack.Push(value);
            }
        }

        public int Pop()
        {
            if (stack.Count == 0) throw new InvalidOperationException("Стек пуст.");
            int value = stack.Pop();
            if (value == minStack.Peek())
            {
                minStack.Pop();
            }
            return value;
        }

        public int Peek()
        {
            if (stack.Count == 0) throw new InvalidOperationException("Стек пуст.");
            return stack.Peek();
        }

        public int GetMin()
        {
            if (minStack.Count == 0) throw new InvalidOperationException("Стек пуст.");
            return minStack.Peek();
        }

        public List<int> ToList()
        {
            return new List<int>(stack);
        }

        public bool ContainsCycle()
        {
            return false;
        }

        public void RemoveDuplicates()
        {
            HashSet<int> seen = new HashSet<int>();
            Stack<int> tempStack = new Stack<int>();

            while (stack.Count > 0)
            {
                int value = stack.Pop();
                if (!seen.Contains(value))
                {
                    seen.Add(value);
                    tempStack.Push(value);
                }
            }

            while (tempStack.Count > 0)
            {
                stack.Push(tempStack.Pop());
            }
        }

        public int Sum()
        {
            int sum = 0;
            foreach (var item in stack)
            {
                sum += item;
            }
            return sum;
        }
    }

    class Program
    {
        public static bool AreParenthesesBalanced(string input)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in input)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else if (c == ')' || c == '}' || c == ']')
                {
                    if (stack.Count == 0) return false;
                    char top = stack.Pop();
                    if ((c == ')' && top != '(') ||
                        (c == '}' && top != '{') ||
                        (c == ']' && top != '['))
                    {
                        return false;
                    }
                }
            }
            return stack.Count == 0;
        }

        public static string ReverseString(string input)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in input)
            {
                stack.Push(c);
            }

            char[] reversed = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                reversed[i] = stack.Pop();
            }
            return new string(reversed);
        }

        public static bool IsPalindrome(string input)
        {
            string reversed = ReverseString(input);
            return input == reversed;
        }

        public static string InfixToPostfix(string expression)
        {
            Dictionary<char, int> precedence = new Dictionary<char, int>
        {
            { '+', 1 }, { '-', 1 }, { '*', 2 }, { '/', 2 }
        };
            Stack<char> operators = new Stack<char>();
            List<string> output = new List<string>();

            foreach (char c in expression)
            {
                if (char.IsDigit(c))
                {
                    output.Add(c.ToString());
                }
                else if (precedence.ContainsKey(c))
                {
                    while (operators.Count > 0 &&
                           precedence.ContainsKey(operators.Peek()) &&
                           precedence[operators.Peek()] >= precedence[c])
                    {
                        output.Add(operators.Pop().ToString());
                    }
                    operators.Push(c);
                }
            }

            while (operators.Count > 0)
            {
                output.Add(operators.Pop().ToString());
            }

            return string.Join(" ", output);
        }

        public static Stack<int> SortStack(Stack<int> stack)
        {
            Stack<int> sortedStack = new Stack<int>();
            while (stack.Count > 0)
            {
                int temp = stack.Pop();
                while (sortedStack.Count > 0 && sortedStack.Peek() > temp)
                {
                    stack.Push(sortedStack.Pop());
                }
                sortedStack.Push(temp);
            }
            return sortedStack;
        }

        static void Main()
        {
            StackWithMin stack = new StackWithMin();

            while (true)
            {
                Console.WriteLine("Выберите задание (1-10) или 0 для выхода:");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 10)
                {
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    continue;
                }

                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите значение для добавления в стек:");
                        if (int.TryParse(Console.ReadLine(), out int value))
                        {
                            stack.Push(value);
                            Console.WriteLine("Значение добавлено.");
                        }
                        else
                            Console.WriteLine("Неверный ввод.");
                        break;

                    case 2:
                        Console.WriteLine("Введите строку для проверки сбалансированности скобок:");
                        string bracketsInput = Console.ReadLine();
                        Console.WriteLine(AreParenthesesBalanced(bracketsInput)
                            ? "Скобки сбалансированы."
                            : "Скобки не сбалансированы.");
                        break;

                    case 3:
                        Console.WriteLine("Введите строку для переворота:");
                        string reverseInput = Console.ReadLine();
                        Console.WriteLine($"Перевернутая строка: {ReverseString(reverseInput)}");
                        break;

                    case 4:
                        Console.WriteLine("Минимальный элемент в стеке: " + stack.GetMin());
                        break;

                    case 5:
                        Console.WriteLine("Введите строку для проверки на палиндром:");
                        string palindromeInput = Console.ReadLine();
                        Console.WriteLine(IsPalindrome(palindromeInput)
                            ? "Строка является палиндромом."
                            : "Строка не является палиндромом.");
                        break;

                    case 6:
                        Console.WriteLine("Введите инфиксное выражение:");
                        string infix = Console.ReadLine();
                        Console.WriteLine("Постфиксное выражение: " + InfixToPostfix(infix));
                        break;

                    case 7:
                        Stack<int> sorted = SortStack(new Stack<int>(stack.ToList()));
                        Console.WriteLine("Отсортированный стек: " + string.Join(", ", sorted));
                        break;

                    case 8:
                        Console.WriteLine("Сумма всех элементов в стеке: " + stack.Sum());
                        break;

                    case 9:
                        stack.RemoveDuplicates();
                        Console.WriteLine("Дубликаты удалены.");
                        break;

                    case 10:
                        Console.WriteLine("Проверка на циклы: " + (stack.ContainsCycle() ? "Есть циклы." : "Циклов нет."));
                        break;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }

}

