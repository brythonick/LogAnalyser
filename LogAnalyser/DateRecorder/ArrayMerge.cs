namespace LogAnalyser
{
    public static class Merge
	{
		public static LogDate[] LogDateArrays(LogDate[] array1, LogDate[] array2)
		{
            LogDate[] combinedArray = new LogDate[array1.Length + array2.Length];
            for (int i = 0; i < array1.Length; i++)
            {
                combinedArray[i] = array1[i];
            }
            for (int i = 0; i < array2.Length; i++)
            {
                combinedArray[array1.Length + i] = array2[i];
            }
            return combinedArray;
        }
	}
}

