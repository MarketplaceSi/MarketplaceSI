namespace Infrastructure.Extensions;
public static class FileExtensions
{
    public static string GetFileName(this string fileName)
    {
        return fileName.Split('/')[fileName.Split('/').Count() - 1];
    }
}