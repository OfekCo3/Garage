using System;

namespace Ex03.ConsoleUI
{
    public class InputValidator
    {
        public static int ParseAndValidateInteger(string userInput, string fieldName)
        {
            if (!int.TryParse(userInput, out int parsedValue))
            {
                throw new FormatException($"ERROR: Invalid input for {fieldName}. Please enter an integer value.");
            }

            return parsedValue;
        }

        public static float ParseAndValidateFloat(string userInput, string fieldName)
        {
            if (!float.TryParse(userInput, out float parsedValue))
            {
                throw new FormatException($"ERROR: Invalid input for {fieldName}. Please enter an float value.");
            }

            return parsedValue;
        }

        public static int ParseAndValidateEnumChoice(string userInput, string fieldName, int i_EnumSize)
        {
            if (!int.TryParse(userInput, out int parsedValue) || parsedValue < 1 || parsedValue > i_EnumSize)
            {
                throw new FormatException(string.Format($"ERROR: Invalid input for {fieldName}. Please enter an integer value 1-{0}.", i_EnumSize));
            }

            return parsedValue;
        }
    }
}
