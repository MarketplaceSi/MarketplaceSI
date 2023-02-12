using Dto.UploadData;

namespace MarketplaceSI.Graphql.GraphqlExtensions;
public static class FileExtensions
{
    public static List<InputFileData> TransformToFilesData(this List<IFile>? files)
    {
        var listFiles = new List<InputFileData>();
        if (files != null)
        {
            foreach (var file in files)
            {
                listFiles.Add(file.TransformToFileData());
            }
        }
        return listFiles;
    }
    public static InputFileData TransformToFileData(this IFile file)
    {
        return new InputFileData()
        {
            Name = file.Name,
            Length = file.Length,
            FileStream = file.OpenReadStream()
        };
    }
}