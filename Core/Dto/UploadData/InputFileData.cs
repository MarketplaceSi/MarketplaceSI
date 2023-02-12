namespace Dto.UploadData;
public class InputFileData
{
    public string Name { get; set; } = string.Empty;
    public long? Length { get; set; }
    public Stream FileStream { get; set; }
}