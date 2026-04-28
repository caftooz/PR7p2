using System;

public static class MatrixCipher
{
    /// <summary>
    /// Шифрует текст матричным методом: запись по строкам, чтение по столбцам.
    /// </summary>
    /// <param name="message">Исходный текст.</param>
    /// <param name="rows">Количество строк матрицы.</param>
    /// <param name="cols">Количество столбцов матрицы.</param>
    /// <returns>Зашифрованный текст.</returns>
    public static string Encrypt(string message, int rows, int cols)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message), "Текст не может быть null.");
        if (message.Length == 0)
            throw new ArgumentException("Текст не может быть пустым.", nameof(message));
        if (rows <= 0)
            throw new ArgumentOutOfRangeException(nameof(rows), "Количество строк должно быть больше нуля.");
        if (cols <= 0)
            throw new ArgumentOutOfRangeException(nameof(cols), "Количество столбцов должно быть больше нуля.");
        if (rows * cols < message.Length)
            throw new ArgumentException($"Размер матрицы ({rows * cols}) меньше длины текста ({message.Length}).");

        string padded = message.PadRight(rows * cols);

        char[] result = new char[rows * cols];
        int index = 0;
        for (int col = 0; col < cols; col++)
            for (int row = 0; row < rows; row++)
                result[index++] = padded[row * cols + col];

        return new string(result);
    }

    /// <summary>
    /// Дешифрует текст матричным методом: запись по столбцам, чтение по строкам.
    /// </summary>
    /// <param name="cipher">Зашифрованный текст.</param>
    /// <param name="rows">Количество строк матрицы.</param>
    /// <param name="cols">Количество столбцов матрицы.</param>
    /// <returns>Расшифрованный текст.</returns>
    public static string Decrypt(string cipher, int rows, int cols)
    {
        if (cipher == null)
            throw new ArgumentNullException(nameof(cipher), "Текст не может быть null.");
        if (cipher.Length == 0)
            throw new ArgumentException("Текст не может быть пустым.", nameof(cipher));
        if (rows <= 0)
            throw new ArgumentOutOfRangeException(nameof(rows), "Количество строк должно быть больше нуля.");
        if (cols <= 0)
            throw new ArgumentOutOfRangeException(nameof(cols), "Количество столбцов должно быть больше нуля.");
        if (rows * cols != cipher.Length)
            throw new ArgumentException($"Длина зашифрованного текста ({cipher.Length}) не совпадает с размером матрицы ({rows * cols}).");

        char[] matrix = new char[rows * cols];
        int index = 0;
        for (int col = 0; col < cols; col++)
            for (int row = 0; row < rows; row++)
                matrix[row * cols + col] = cipher[index++];

        return new string(matrix);
    }
}