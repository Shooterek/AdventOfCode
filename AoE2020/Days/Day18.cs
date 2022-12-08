using System;
public class Day18 : Day
{
    public Day18(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        long result = 0;
        var input = _inputLoader.LoadStringListInput(_inputPath);

        foreach(var expression in input){
            result += EvaluateExpression(expression.RemoveWhitespaceCharacters());
        }

        return result.ToString();
    }

    private long EvaluateExpression(string expression)
    {
        long result = 0;
        for(int i = 0; i < expression.Length;){
            if(expression[i] == '('){
                var level = 1;
                var parenthesisLength = 1;
                while(level > 0){
                    if(expression[i + parenthesisLength] == '('){
                        level++;
                    }
                    else if(expression[i + parenthesisLength] == ')'){
                        level--;
                    }
                    parenthesisLength++;
                }
                if(i == 0 || expression[i - 1] == '+'){   
                    result += EvaluateExpression(expression.Substring(i + 1, parenthesisLength - 2));
                }
                else{
                    result *= EvaluateExpression(expression.Substring(i + 1, parenthesisLength - 2));
                }
                i += parenthesisLength;
            }
            else if(char.IsDigit(expression[i])){
                var length = 0;
                while(i + length < expression.Length && char.IsDigit(expression[i + length])){
                    length++;
                }
                var value = Int32.Parse(expression.Substring(i, length));
                if(i == 0 || expression[i - 1] == '+'){
                    result += value;
                }
                else{
                    result *= value;
                }
                i += length;
            }
            else{
                i++;
            }
        }

        return result;
    }

    public override string SecondTask()
    {
        long result = 0;
        var input = _inputLoader.LoadStringListInput(_inputPath);

        foreach(var expression in input){
            result += EvaluateExpressionAdvanced(AddParenthesis(expression.RemoveWhitespaceCharacters()));
            Console.WriteLine(result);
        }

        return result.ToString();
    }

    private long EvaluateExpressionAdvanced(string expression)
    {
        StringToFormula stf = new StringToFormula();
        long result = stf.Eval(expression);
        return result;
    }

    private string AddParenthesis(string expression){
        for(int i = 0; i < expression.Length; i++){
            if(expression[i] == '+'){
                var firstIndex = FindFirstArgumentStartingIndex(expression, i);
                var lastIndex = FindSecondArgumentEndingIndex(expression, i);
                var currentlyEvaluatedExpression = expression.Substring(firstIndex, lastIndex - firstIndex);
                expression = expression.Insert(firstIndex, "(");
                expression = expression.Insert(lastIndex + 1, ")");
                i++;
            }
        }
        return expression;
    }
    private int FindFirstArgumentStartingIndex(string expression, int plusSignIndex)
    {
        for(int i = plusSignIndex - 1; i >= 0; i--){
            if(char.IsDigit(expression[i])){
                var length = 0;
                while(i - length >= 0 && char.IsDigit(expression[i - length])){
                    length++;
                }

                return i - length + 1;
            }
            else if(expression[i] == ')'){
                var length = 1;
                var level = -1;
                while(level < 0){
                    if(expression[i - length] == '('){
                        level++;
                    }
                    else if(expression[i - length] == ')'){
                        level--;
                    }
                    length++;
                }
                return i - length + 1;
            }
        }
        return -1;
    }

    private int FindSecondArgumentEndingIndex(string expression, int plusSignIndex)
    {
        for(int i = plusSignIndex + 1; i < expression.Length; i++){
            if(char.IsDigit(expression[i])){
                var length = 0;
                while(i + length < expression.Length && char.IsDigit(expression[i + length])){
                    length++;
                }

                return i + length;
            }            
            else if(expression[i] == '('){
                var length = 0;
                var level = 1;
                while(level > 0 && i + length < expression.Length){
                    if(expression[i + length] == '('){
                        level++;
                    }
                    else if(expression[i + length] == ')'){
                        level--;
                    }
                    length++;
                }
                return i + length;
            }
        }
        return -1;
    }
}